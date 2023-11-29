using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EPE3_PUNTONET.Models;

namespace EPE3_PUNTONET.Repositorio
{
    public class ReservaRepository
    {
        private readonly string connectionString; // Tu cadena de conexión a la base de datos

        public ReservaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<Reserva>> GetAllReservas()
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Reserva";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Reserva reserva = new Reserva
                        {
                            IdReserva = Convert.ToInt32(reader["idReserva"]),
                            Especialidad = reader["Especialidad"].ToString(),
                            DiaReserva = Convert.ToDateTime(reader["DiaReserva"]),
                            Paciente_idPaciente = Convert.ToInt32(reader["Paciente_idPaciente"])
                        };
                        reservas.Add(reserva);
                    }
                }
            }

            return reservas;
        }

        public async Task<Reserva> GetReservaById(int idReserva)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Reserva WHERE idReserva = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idReserva);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Reserva reserva = new Reserva
                            {
                                IdReserva = Convert.ToInt32(reader["idReserva"]),
                                Especialidad = reader["Especialidad"].ToString(),
                                DiaReserva = Convert.ToDateTime(reader["DiaReserva"]),
                                Paciente_idPaciente = Convert.ToInt32(reader["Paciente_idPaciente"])
                            };
                            return reserva;
                        }
                    }
                }
            }

            return null; // Retorna null si la reserva no se encuentra
        }

        public async Task<int> InsertReserva(Reserva reserva)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Reserva (Especialidad, DiaReserva, Paciente_idPaciente) " +
                               "VALUES (@especialidad, @diaReserva, @idPaciente); SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@especialidad", reserva.Especialidad);
                    command.Parameters.AddWithValue("@diaReserva", reserva.DiaReserva);
                    command.Parameters.AddWithValue("@idPaciente", reserva.Paciente_idPaciente);

                    int nuevaReservaId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return nuevaReservaId;
                }
            }
        }

        public async Task UpdateReserva(Reserva reserva)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Reserva SET Especialidad = @especialidad, DiaReserva = @diaReserva, Paciente_idPaciente = @idPaciente " +
                               "WHERE idReserva = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@especialidad", reserva.Especialidad);
                    command.Parameters.AddWithValue("@diaReserva", reserva.DiaReserva);
                    command.Parameters.AddWithValue("@idPaciente", reserva.Paciente_idPaciente);
                    command.Parameters.AddWithValue("@id", reserva.IdReserva);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteReserva(int idReserva)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Reserva WHERE idReserva = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idReserva);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

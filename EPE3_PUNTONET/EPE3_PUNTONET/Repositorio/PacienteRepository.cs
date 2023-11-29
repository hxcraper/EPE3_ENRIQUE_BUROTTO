using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EPE3_PUNTONET.Models;

namespace EPE3_PUNTONET.Repositorio
{
    public class PacienteRepository
    {
        private readonly string connectionString; // Tu cadena de conexión a la base de datos

        public PacienteRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<List<Paciente>> GetAllPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Paciente";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Paciente paciente = new Paciente
                        {
                            IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                            NombrePac = reader["NombrePac"].ToString(),
                            ApellidoPac = reader["ApellidoPac"].ToString(),
                            RunPac = reader["RunPac"].ToString(),
                            Nacionalidad = reader["Nacionalidad"].ToString(),
                            Visa = reader["Visa"].ToString(),
                            Genero = reader["Genero"].ToString(),
                            SintomasPac = reader["SintomasPac"].ToString(),
                            Medico_idMedico = Convert.ToInt32(reader["Medico_idMedico"])
                        };
                        pacientes.Add(paciente);
                    }
                }
            }

            return pacientes;
        }

        public async Task<Paciente> GetPacienteById(int idPaciente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Paciente WHERE idPaciente = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idPaciente);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Paciente paciente = new Paciente
                            {
                                IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                                NombrePac = reader["NombrePac"].ToString(),
                                ApellidoPac = reader["ApellidoPac"].ToString(),
                                RunPac = reader["RunPac"].ToString(),
                                Nacionalidad = reader["Nacionalidad"].ToString(),
                                Visa = reader["Visa"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                SintomasPac = reader["SintomasPac"].ToString(),
                                Medico_idMedico = Convert.ToInt32(reader["Medico_idMedico"])
                            };
                            return paciente;
                        }
                    }
                }
            }

            return null; // Retorna null si el paciente no se encuentra
        }

        public async Task<int> InsertPaciente(Paciente paciente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Paciente (NombrePac, ApellidoPac, RunPac, Nacionalidad, Visa, Genero, SintomasPac, Medico_idMedico) " +
                               "VALUES (@nombre, @apellido, @run, @nacionalidad, @visa, @genero, @sintomas, @idMedico); SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", paciente.NombrePac);
                    command.Parameters.AddWithValue("@apellido", paciente.ApellidoPac);
                    // ... (otras propiedades)

                    int nuevoId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return nuevoId;
                }
            }
        }

        public async Task UpdatePaciente(Paciente paciente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE Paciente SET NombrePac = @nombre, ApellidoPac = @apellido, RunPac = @run, Nacionalidad = @nacionalidad, " +
                               "Visa = @visa, Genero = @genero, SintomasPac = @sintomas, Medico_idMedico = @idMedico WHERE idPaciente = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", paciente.NombrePac);
                    command.Parameters.AddWithValue("@apellido", paciente.ApellidoPac);
                    // ... (otras propiedades)
                    command.Parameters.AddWithValue("@id", paciente.IdPaciente);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeletePaciente(int idPaciente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM Paciente WHERE idPaciente = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idPaciente);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

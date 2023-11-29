using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EPE3_PUNTONET.Models;

public class MedicoRepository
{
    private readonly string connectionString; // Tu cadena de conexión a la base de datos

    public MedicoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<List<Medico>> GetAllMedicos()
    {
        List<Medico> medicos = new List<Medico>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT * FROM Medico";
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Medico medico = new Medico
                    {
                        IdMedico = Convert.ToInt32(reader["idMedico"]),
                        NombreMed = reader["NombreMed"].ToString(),
                        ApellidoMed = reader["ApellidoMed"].ToString(),
                        // ... (otras propiedades)
                    };
                    medicos.Add(medico);
                }
            }
        }

        return medicos;
    }

    public async Task<Medico> GetMedicoById(int idMedico)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT * FROM Medico WHERE idMedico = @id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idMedico);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        Medico medico = new Medico
                        {
                            IdMedico = Convert.ToInt32(reader["idMedico"]),
                            NombreMed = reader["NombreMed"].ToString(),
                            ApellidoMed = reader["ApellidoMed"].ToString(),
                            // ... (otras propiedades)
                        };
                        return medico;
                    }
                }
            }
        }

        return null; // Retorna null si el médico no se encuentra
    }

    public async Task<int> InsertMedico(Medico medico)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Medico (NombreMed, ApellidoMed, RunMed, Eunacom, NacionalidadMed, Especialidad, Horarios, TarifaHr) " +
                           "VALUES (@nombre, @apellido, @run, @eunacom, @nacionalidad, @especialidad, @horarios, @tarifaHr); SELECT SCOPE_IDENTITY()";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", medico.NombreMed);
                command.Parameters.AddWithValue("@apellido", medico.ApellidoMed);

                int nuevoId = Convert.ToInt32(await command.ExecuteScalarAsync());
                return nuevoId;
            }
        }
    }

    public async Task UpdateMedico(Medico medico)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Medico SET NombreMed = @nombre, ApellidoMed = @apellido, RunMed = @run, Eunacom = @eunacom, " +
                           "NacionalidadMed = @nacionalidad, Especialidad = @especialidad, Horarios = @horarios, TarifaHr = @tarifaHr " +
                           "WHERE idMedico = @id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", medico.NombreMed);
                command.Parameters.AddWithValue("@apellido", medico.ApellidoMed);
                // ... (otras propiedades)
                command.Parameters.AddWithValue("@id", medico.IdMedico);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteMedico(int idMedico)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Medico WHERE idMedico = @id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idMedico);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

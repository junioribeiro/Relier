using Azure;
using Microsoft.Data.SqlClient;
using Relier.Domain.Account;
using Relier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Infra.Data.Account
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly string _connectionString;
        public AuthenticateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<User?> Authenticate(string email, string password)
        {
            User? response = null;
            var queryString = "Select Id, Email, Role, Password From Users where email = @email and Password = @password";
            using (SqlConnection connection = new(_connectionString))
            {
                
                // Create the Command and Parameter objects.
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        if (await reader.ReadAsync())
                        {
                            response = Load(reader);
                        }
                    }
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return response;
            }
        }

        private User Load(SqlDataReader reader)
        {
            var response = new User();

            if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                response.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            if (!reader.IsDBNull(reader.GetOrdinal("Email")))
                response.Email = reader.GetString(reader.GetOrdinal("Email"));
            if (!reader.IsDBNull(reader.GetOrdinal("Password")))
                response.Password = reader.GetString(reader.GetOrdinal("Password"));
            if (!reader.IsDBNull(reader.GetOrdinal("Role")))
                response.Role = reader.GetString(reader.GetOrdinal("Role"));

            return response;
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(User user)
        {
            var queryString = "Insert Into Users(Email,Password,Role) OUTPUT INSERTED.Id Values(@email,@password,@role)";
            using (SqlConnection connection = new(_connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new(queryString, connection);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@role", user.Role);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return true;
        }
    }
}

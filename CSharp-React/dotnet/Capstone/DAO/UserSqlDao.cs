using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace Capstone.DAO
{
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;

        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<User> GetUsers()
        {
            IList<User> users = new List<User>();

            string sql = "SELECT user_id, username, password_hash, salt, user_role, current_league_id FROM users";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = MapRowToUser(reader);
                        users.Add(user);
                    }
                }
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            User user = null;

            string sql = "SELECT user_id, username, password_hash, salt, user_role, current_league_id FROM users WHERE user_id = @user_id";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) 
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;

            string sql = "SELECT user_id, username, password_hash, salt, user_role, current_league_id FROM users WHERE username = @username";

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    // cmd.Parameters.Add(new NpgsqlParameter("@username", NpgsqlDbType.Text) { Value = username ?? (object)DBNull.Value });
                    cmd.Parameters.AddWithValue("@username", username);
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        public User CreateUser(string username, string password, string role)
        {
            User newUser = null;

            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            string sql = "INSERT INTO users (username, password_hash, salt, user_role) " +
                        //  "OUTPUT INSERTED.user_id " +
                         "VALUES (@username, @password_hash, @salt, @user_role)";

            int newUserId = 0;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);

                    // newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.ExecuteNonQuery();
                    
                }
                // newUser = GetUserById(newUserId);
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return GetUserByUsername(username);
        }

        public async Task SetCurrentLeagueAsync(int userId, int fantasyLeagueId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string sql = "UPDATE users SET current_league_id = @leagueId WHERE user_id = @userId;";

                NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("@leagueId", fantasyLeagueId);
                command.Parameters.AddWithValue("@userId", userId);

                await command.ExecuteNonQueryAsync();
            }
        }

        private User MapRowToUser(NpgsqlDataReader reader)
        {
            User user = new User();
            user.UserId = Convert.ToInt32(reader["user_id"]);
            user.Username = Convert.ToString(reader["username"]);
            user.PasswordHash = Convert.ToString(reader["password_hash"]);
            user.Salt = Convert.ToString(reader["salt"]);
            user.Role = Convert.ToString(reader["user_role"]);
            user.FantasyLeagueId = reader["current_league_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["current_league_id"]);
            return user;
        }
    }
}

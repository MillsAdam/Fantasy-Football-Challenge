using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO
{
    public class FantasyLeagueSqlDao : IFantasyLeagueDao
    {
        private readonly string _connectionString;
        public FantasyLeagueSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }


        public async Task<List<FantasyLeagueModel>> GetFantasyLeagues()
        {
            List<FantasyLeagueModel> fantasyLeagues = new List<FantasyLeagueModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT
                        league_id, 
                        user_id, 
                        league_name, 
                        league_password_hash, 
                        league_salt 
                    FROM fantasy_leagues;", connection);
                {
                    using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        FantasyLeagueModel fantasyLeague = MapRowToFantasyLeague(reader);
                        fantasyLeagues.Add(fantasyLeague);
                    }
                }
            }
            return fantasyLeagues;
        }
        

        public FantasyLeagueModel GetFantasyLeagueByLeagueId(int fantasyLeagueId)
        {
            FantasyLeagueModel fantasyLeague = new FantasyLeagueModel();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT
                        league_id, 
                        user_id, 
                        league_name, 
                        league_password_hash, 
                        league_salt 
                    FROM fantasy_leagues
                    WHERE league_id = @league_id;", connection);
                {
                    command.Parameters.AddWithValue("@league_id", fantasyLeagueId);
                    using NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        fantasyLeague = MapRowToFantasyLeague(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return fantasyLeague;
        }

        public FantasyLeagueModel GetFantasyLeagueByLeagueName(string leagueName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT
                        league_id, 
                        user_id, 
                        league_name, 
                        league_password_hash, 
                        league_salt 
                    FROM fantasy_leagues
                    WHERE league_name = @league_name;", connection);
                {
                    command.Parameters.AddWithValue("@league_name", leagueName);
                    using NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return MapRowToFantasyLeague(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<FantasyLeagueModel> CreateFantasyLeague(User user, string leagueName, string leaguePassword)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(leaguePassword);
            FantasyLeagueModel createdLeague = new FantasyLeagueModel();

            string createLeagueSql = @"
                INSERT INTO fantasy_leagues (user_id, league_name, league_password_hash, league_salt)
                VALUES (@user_id, @league_name, @league_password_hash, @league_salt) 
                RETURNING league_id, user_id, league_name, league_password_hash, league_salt;";

            string addMemberSql = @"
                INSERT INTO fantasy_members (user_id, league_id)
                VALUES (@user_id, @league_id);";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlTransaction transaction = await connection.BeginTransactionAsync())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(createLeagueSql, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", user.UserId);
                            command.Parameters.AddWithValue("@league_name", leagueName);
                            command.Parameters.AddWithValue("@league_password_hash", hash.Password);
                            command.Parameters.AddWithValue("@league_salt", hash.Salt);

                            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                            {
                                if (await reader.ReadAsync())
                                {
                                    createdLeague =  MapRowToFantasyLeague(reader);
                                }
                            }
                        }

                        if (createdLeague != null)
                        {
                            using (NpgsqlCommand command = new NpgsqlCommand(addMemberSql, connection))
                            {
                                command.Parameters.AddWithValue("@user_id", user.UserId);
                                command.Parameters.AddWithValue("@league_id", createdLeague.FantasyLeagueId);
                                await command.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                    }
                    
                }
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return createdLeague;
        }

        public async Task<List<FantasyLeagueModel>> GetListOfFantasyLeaguesByLeagueName(string leagueName)
        {
            List<FantasyLeagueModel> fantasyLeagues = new List<FantasyLeagueModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT
                        league_id, 
                        user_id, 
                        league_name, 
                        league_password_hash, 
                        league_salt 
                    FROM fantasy_leagues
                    WHERE lower(league_name) ILIKE @league_name;", connection);
                {
                    command.Parameters.AddWithValue("@league_name", $"%{leagueName}%");
                    using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        FantasyLeagueModel fantasyLeague = MapRowToFantasyLeague(reader);
                        fantasyLeagues.Add(fantasyLeague);
                    }
                }
            }
            return fantasyLeagues;
        }

        public async Task SetCurrentLeagueAsync(User user, int fantasyLeagueId)
        {
            string sql = @"UPDATE users SET current_league_id = @fantasy_league_id WHERE user_id = @user_id;";
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fantasy_league_id", fantasyLeagueId);
                        command.Parameters.AddWithValue("@user_id", user.UserId);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }

        public async Task<int> GetCurrentFantasyLeagueIdByUser(User user)
        {
            int fantasyLeagueId = 0;
            string sql = @"SELECT current_league_id FROM users WHERE user_id = @user_id;";
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", user.UserId);
                        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            fantasyLeagueId = reader["current_league_id"] == DBNull.Value? 0 : Convert.ToInt32(reader["current_league_id"]);
                        }
                    }
                }
            }
            catch (PostgresException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return fantasyLeagueId;
        }

        private FantasyLeagueModel MapRowToFantasyLeague(NpgsqlDataReader reader)
        {
            FantasyLeagueModel fantasyLeague = new FantasyLeagueModel();
            fantasyLeague.FantasyLeagueId = Convert.ToInt32(reader["league_id"]);
            fantasyLeague.UserId = Convert.ToInt32(reader["user_id"]);
            fantasyLeague.LeagueName = Convert.ToString(reader["league_name"]);
            fantasyLeague.LeaguePasswordHash = Convert.ToString(reader["league_password_hash"]);
            fantasyLeague.LeagueSalt = Convert.ToString(reader["league_salt"]);
            return fantasyLeague;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace Capstone.DAO
{
    public class FantasyRosterSqlDao : IFantasyRosterDao
    {
        private readonly string _connectionString;

        public FantasyRosterSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task CreateFantasyRoster(User user, string teamName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO fantasy_rosters (user_id, team_name) VALUES (@user_id, @team_name);", connection);
                command.Parameters.AddWithValue("@user_id", user.UserId);
                command.Parameters.AddWithValue("@team_name", teamName);
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<FantasyRoster>> GetFantasyRosters()
        {
            List<FantasyRoster> fantasyRosters = new List<FantasyRoster>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, user_id, team_name FROM fantasy_rosters;", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FantasyRoster fantasyRoster = new FantasyRoster();
                    {
                        fantasyRoster.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        fantasyRoster.UserId = Convert.ToInt32(reader["user_id"]);
                        fantasyRoster.TeamName = Convert.ToString(reader["team_name"]);
                    
                    };
                    fantasyRosters.Add(fantasyRoster);
                }
            }
            return fantasyRosters;
        }

        public async Task<FantasyRoster> GetFantasyRosterByUser(User user)
        {
            FantasyRoster fantasyRoster = new FantasyRoster();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, user_id, team_name FROM fantasy_rosters WHERE user_id = @user_id;", connection);
                command.Parameters.AddWithValue("@user_id", user.UserId);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fantasyRoster.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                    fantasyRoster.UserId = Convert.ToInt32(reader["user_id"]);
                    fantasyRoster.TeamName = Convert.ToString(reader["team_name"]);
                }
            }
            return fantasyRoster;
        }
    }
}
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
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO fantasy_rosters (user_id, team_name, total_score) VALUES (@user_id, @team_name, @total_score);", connection);
                command.Parameters.AddWithValue("@user_id", user.UserId);
                command.Parameters.AddWithValue("@team_name", teamName);
                command.Parameters.AddWithValue("@total_score", 0);
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<FantasyRosterDto>> GetFantasyRosters()
        {
            List<FantasyRosterDto> fantasyRosterDtos = new List<FantasyRosterDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT fr.roster_id, fr.user_id, fr.team_name, u.username, fr.total_score FROM fantasy_rosters fr JOIN users u ON fr.user_id = u.user_id;", connection);
                using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                while (reader.Read())
                {
                    FantasyRosterDto fantasyRosterDto = new FantasyRosterDto();
                    {
                        fantasyRosterDto.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        fantasyRosterDto.UserId = Convert.ToInt32(reader["user_id"]);
                        fantasyRosterDto.TeamName = Convert.ToString(reader["team_name"]);
                        fantasyRosterDto.Username = Convert.ToString(reader["username"]);
                        fantasyRosterDto.TotalScore = Convert.ToDouble(reader["total_score"]);
                    };
                    fantasyRosterDtos.Add(fantasyRosterDto);
                }
            }
            return fantasyRosterDtos;
        }

        public async Task<FantasyRoster> GetFantasyRosterByUser(User user)
        {
            FantasyRoster fantasyRoster = new FantasyRoster();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, user_id, team_name, total_score FROM fantasy_rosters WHERE user_id = @user_id;", connection);
                command.Parameters.AddWithValue("@user_id", user.UserId);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fantasyRoster.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                    fantasyRoster.UserId = Convert.ToInt32(reader["user_id"]);
                    fantasyRoster.TeamName = Convert.ToString(reader["team_name"]);
                    fantasyRoster.TotalScore = Convert.ToDouble(reader["total_score"]);
                }
            }
            return fantasyRoster;
        }
    }
}
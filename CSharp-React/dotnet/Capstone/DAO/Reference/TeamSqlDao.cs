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
    public class TeamSqlDao : ITeamDao
    {
        private readonly string _connectionString;

        public TeamSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task AddTeamAsync(TeamDto teamDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO teams (team_id, team, city, name, conference, division, status) " + 
                    "VALUES (@team_id, @team, @city, @name, @conference, @division, @status);", connection);
                {
                    command.Parameters.AddWithValue("@team_id", teamDto.TeamId);
                    command.Parameters.AddWithValue("@team", teamDto.Team);
                    command.Parameters.AddWithValue("@city", teamDto.City);
                    command.Parameters.AddWithValue("@name", teamDto.Name);
                    command.Parameters.AddWithValue("@conference", teamDto.Conference);
                    command.Parameters.AddWithValue("@division", teamDto.Division);
                    command.Parameters.AddWithValue("@status", "Inactive");
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ToggleTeamStatusAsync(string teamName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(
                    @"UPDATE teams 
                    SET status = CASE 
                        WHEN status = 'Active' 
                        THEN 'Inactive' ELSE 'Active' 
                    END 
                    WHERE team = @team;", connection))
                {
                    command.Parameters.AddWithValue("@team", teamName);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<TeamDto>> GetTeamsAsync()
        {
            List<TeamDto> teams = new List<TeamDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM teams;", connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            TeamDto team = new TeamDto
                            {
                                TeamId = Convert.ToInt32(reader["team_id"]),
                                Team = Convert.ToString(reader["team"]),
                                City = Convert.ToString(reader["city"]),
                                Name = Convert.ToString(reader["name"]),
                                Conference = Convert.ToString(reader["conference"]),
                                Division = Convert.ToString(reader["division"]),
                                Status = Convert.ToString(reader["status"])
                            };
                            teams.Add(team);
                        }
                    }
                }
            }
            return teams;
        }

        public async Task<List<TeamDto>> GetActiveTeamsAsync()
        {
            List<TeamDto> teams = new List<TeamDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM teams WHERE status = 'Active';", connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            TeamDto team = new TeamDto
                            {
                                TeamId = Convert.ToInt32(reader["team_id"]),
                                Team = Convert.ToString(reader["team"]),
                                City = Convert.ToString(reader["city"]),
                                Name = Convert.ToString(reader["name"]),
                                Conference = Convert.ToString(reader["conference"]),
                                Division = Convert.ToString(reader["division"]),
                                Status = Convert.ToString(reader["status"])
                            };
                            teams.Add(team);
                        }
                    }
                }
            }
            return teams;
        }
    }
}
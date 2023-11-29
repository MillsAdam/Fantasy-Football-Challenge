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
                    command.Parameters.AddWithValue("@status", "Active");
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateTeamStatusAsync(int teamId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "UPDATE teams SET status = 'Inactive' WHERE team_id = @team_id;", connection);
                {
                    command.Parameters.AddWithValue("@team_id", teamId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
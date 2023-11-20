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
    public class PlayerSqlDao : IPlayerDao
    {
        private readonly string _connectionString;

        public PlayerSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task AddPlayerAsync(PlayerDto playerDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO players (player_id, team_id, first_name, last_name, position, position_category, status, injury_status) VALUES (@player_id, @team_id, @first_name, @last_name, @position, @position_category, @status, @injury_status);", connection);
                command.Parameters.AddWithValue("@player_id", playerDto.PlayerId);
                if (playerDto.TeamId != null)
                {
                    command.Parameters.AddWithValue("@team_id", playerDto.TeamId);
                }
                else
                {
                    command.Parameters.AddWithValue("@team_id", DBNull.Value);
                }
                command.Parameters.AddWithValue("@first_name", playerDto.FirstName);
                command.Parameters.AddWithValue("@last_name", playerDto.LastName);
                command.Parameters.AddWithValue("@position", playerDto.Position);
                command.Parameters.AddWithValue("@position_category", playerDto.PositionCategory);
                command.Parameters.AddWithValue("@status", playerDto.Status);
                if (playerDto.InjuryStatus != null)
                {
                    command.Parameters.AddWithValue("@injury_status", playerDto.InjuryStatus);
                }
                else
                {
                    command.Parameters.AddWithValue("@injury_status", DBNull.Value);
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
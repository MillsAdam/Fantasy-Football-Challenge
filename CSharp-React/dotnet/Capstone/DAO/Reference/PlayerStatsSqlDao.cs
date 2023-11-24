using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Reference
{
    public class PlayerStatsSqlDao : IPlayerStatsDao
    {
        private readonly string _connectionString;

        public PlayerStatsSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task AddPlayerStatsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO player_stats (player_id, team_id, week, name, position, status, injury_status, fantasy_points) " +
                    "VALUES (@player_id, @team_id, @week, @name, @position, @status, @injury_status, @fantasy_points);", connection);
                command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                command.Parameters.AddWithValue("@week", playerStatsDto.Week);
                command.Parameters.AddWithValue("@name", playerStatsDto.Name);
                command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                if (playerStatsDto.InjuryStatus == null)
                {
                    command.Parameters.AddWithValue("@injury_status", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus);
                }
                command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);
                command.ExecuteNonQuery();
            }
        }

        public async Task AddDefenseStatsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO player_stats (player_id, team_id, week, name, position, status, injury_status, fantasy_points) " +
                    "VALUES (@player_id, @team_id, @week, (SELECT name FROM players WHERE player_id = @player_id), @position, @status, @injury_status, @fantasy_points);", connection);
                command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                command.Parameters.AddWithValue("@week", playerStatsDto.Week);
                // command.Parameters.AddWithValue("@name", playerStatsDto.Name);
                command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                if (playerStatsDto.InjuryStatus == null)
                {
                    command.Parameters.AddWithValue("@injury_status", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus);
                }
                command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);
                command.ExecuteNonQuery();
            }
        }
    }
}
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
        private readonly IConfigurationDao _configurationDao;

        public PlayerStatsSqlDao(IConfiguration configuration, IConfigurationDao configurationDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _configurationDao = configurationDao;
        }

        public async Task AddPlayerStatsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO player_stats (player_id, team_id, season_type, week, name, position, status, injury_status, fantasy_points) " +
                    "VALUES (@player_id, @team_id, @season_type, @week, @name, @position, @status, @injury_status, @fantasy_points);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsDto.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsDto.Week);
                    command.Parameters.AddWithValue("@name", playerStatsDto.Name);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddDefenseStatsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO player_stats (player_id, team_id, season_type, week, name, position, status, injury_status, fantasy_points) " +
                    "VALUES (@player_id, @team_id, @season_type, @week, " +
                        "(SELECT name FROM players WHERE player_id = @player_id), " +
                        "@position, @status, @injury_status, @fantasy_points);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsDto.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsDto.Week);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddPlayerProjectionsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO player_projections (player_id, team_id, season_type, week, name, position, status, injury_status, fantasy_points) " +
                    "VALUES (@player_id, @team_id, @season_type, @week, @name, @position, @status, @injury_status, @fantasy_points);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsDto.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsDto.Week);
                    command.Parameters.AddWithValue("@name", playerStatsDto.Name);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddDefenseProjectionsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO player_projections (
                        player_id, 
                        team_id, 
                        season_type, 
                        week, name, 
                        position, 
                        status, 
                        injury_status, 
                        fantasy_points
                    ) 
                    VALUES (
                        @player_id, 
                        @team_id, 
                        @season_type, 
                        @week, 
                        (
                            SELECT name 
                            FROM players 
                            WHERE player_id = @player_id), 
                        @position, 
                        @status, 
                        @injury_status, 
                        @fantasy_points
                    );", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsDto.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsDto.Week);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePlayerStatsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_stats
                    SET team_id = @team_id,
                        name = @name,
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points
                    WHERE player_id = @player_id 
                        AND season_type = @season_type 
                        AND week = @week;";

                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", playerStatsDto.Name);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDefenseStatsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_stats
                    SET team_id = @team_id,
                        season_type = @season_type,
                        week = @week,
                        name = (SELECT name FROM players WHERE player_id = @player_id),
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points
                    WHERE player_id = @player_id 
                        AND season_type = @season_type 
                        AND week = @week;";

                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePlayerProjectionsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_projections
                    SET team_id = @team_id,
                        name = @name,
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points
                    WHERE player_id = @player_id 
                        AND season_type = @season_type 
                        AND week = @week;";

                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", playerStatsDto.Name);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDefenseProjectionsDtoAsync(PlayerStatsDto playerStatsDto)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_projections
                    SET team_id = @team_id,
                        name = (SELECT name FROM players WHERE player_id = @player_id),
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points
                    WHERE player_id = @player_id;";

                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsDto.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@position", playerStatsDto.Position);
                    command.Parameters.AddWithValue("@status", playerStatsDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsDto.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsDto.FantasyPoints);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
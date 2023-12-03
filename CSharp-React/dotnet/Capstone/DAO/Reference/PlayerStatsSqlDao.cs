using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.Models.Data;
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
                    @"INSERT INTO player_projections (player_id, team_id, season_type, week, name, position, status, injury_status, fantasy_points) 
                    VALUES (@player_id, @team_id, @season_type, @week, 
                        (SELECT name FROM players WHERE player_id = @player_id), @position, @status, @injury_status, @fantasy_points);", connection);
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


        // Add PlayerDBStatsDto to database
        public async Task AddPlayerStatsExtAsync(PlayerStatsExt playerStatsExt)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO player_stats_ext (
                        player_id, team_id, season_type, week, name, 
                        position, status, injury_status, fantasy_points, passing_completions, 
                        passing_attempts, passing_yards, passing_touchdowns, passing_interceptions, passing_rating, 
                        rushing_attempts, rushing_yards, rushing_touchdowns, receiving_targets, receptions, 
                        receiving_yards, receiving_touchdowns, return_touchdowns, two_point_conversions, fumbles_lost, 
                        field_goals_made, field_goals_attempted, field_goals_made_0_to_19, field_goals_made_20_to_29, field_goals_made_30_to_39, 
                        field_goals_made_40_to_49, field_goals_made_50_plus, extra_points_made, extra_points_attempted, defensive_touchdowns, 
                        special_teams_touchdowns, touchdowns_scored, fumbles_forced, fumbles_recovered, interceptions, 
                        tackles_for_loss, quarterback_hits, sacks, safeties, blocked_kicks, points_allowed) 
                    VALUES (
                        @player_id, @team_id, @season_type, @week, @name, 
                        @position, @status, @injury_status, @fantasy_points, @passing_completions, 
                        @passing_attempts, @passing_yards, @passing_touchdowns, @passing_interceptions, @passing_rating, 
                        @rushing_attempts, @rushing_yards, @rushing_touchdowns, @receiving_targets, @receptions, 
                        @receiving_yards, @receiving_touchdowns, @return_touchdowns, @two_point_conversions, @fumbles_lost, 
                        @field_goals_made, @field_goals_attempted, @field_goals_made_0_to_19, @field_goals_made_20_to_29, @field_goals_made_30_to_39, 
                        @field_goals_made_40_to_49, @field_goals_made_50_plus, @extra_points_made, @extra_points_attempted, @defensive_touchdowns, 
                        @special_teams_touchdowns, @touchdowns_scored, @fumbles_forced, @fumbles_recovered, @interceptions, 
                        @tackles_for_loss, @quarterback_hits, @sacks, @safeties, @blocked_kicks, @points_allowed);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsExt.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsExt.Week);
                    command.Parameters.AddWithValue("@name", playerStatsExt.Name);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddDefenseStatsExtAsync(PlayerStatsExt playerStatsExt)
        {
             using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO player_stats_ext (
                        player_id, team_id, season_type, week, name, 
                        position, status, injury_status, fantasy_points, passing_completions, 
                        passing_attempts, passing_yards, passing_touchdowns, passing_interceptions, passing_rating, 
                        rushing_attempts, rushing_yards, rushing_touchdowns, receiving_targets, receptions, 
                        receiving_yards, receiving_touchdowns, return_touchdowns, two_point_conversions, fumbles_lost, 
                        field_goals_made, field_goals_attempted, field_goals_made_0_to_19, field_goals_made_20_to_29, field_goals_made_30_to_39, 
                        field_goals_made_40_to_49, field_goals_made_50_plus, extra_points_made, extra_points_attempted, defensive_touchdowns, 
                        special_teams_touchdowns, touchdowns_scored, fumbles_forced, fumbles_recovered, interceptions, 
                        tackles_for_loss, quarterback_hits, sacks, safeties, blocked_kicks, points_allowed) 
                    VALUES (
                        @player_id, @team_id, @season_type, @week, (SELECT name FROM players WHERE player_id = @player_id), 
                        @position, @status, @injury_status, @fantasy_points, @passing_completions, 
                        @passing_attempts, @passing_yards, @passing_touchdowns, @passing_interceptions, @passing_rating, 
                        @rushing_attempts, @rushing_yards, @rushing_touchdowns, @receiving_targets, @receptions, 
                        @receiving_yards, @receiving_touchdowns, @return_touchdowns, @two_point_conversions, @fumbles_lost, 
                        @field_goals_made, @field_goals_attempted, @field_goals_made_0_to_19, @field_goals_made_20_to_29, @field_goals_made_30_to_39, 
                        @field_goals_made_40_to_49, @field_goals_made_50_plus, @extra_points_made, @extra_points_attempted, @defensive_touchdowns, 
                        @special_teams_touchdowns, @touchdowns_scored, @fumbles_forced, @fumbles_recovered, @interceptions, 
                        @tackles_for_loss, @quarterback_hits, @sacks, @safeties, @blocked_kicks, @points_allowed);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsExt.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsExt.Week);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddPlayerProjectionsExtAsync(PlayerStatsExt playerStatsExt)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO player_projections_ext (
                        player_id, team_id, season_type, week, name, 
                        position, status, injury_status, fantasy_points, passing_completions, 
                        passing_attempts, passing_yards, passing_touchdowns, passing_interceptions, passing_rating, 
                        rushing_attempts, rushing_yards, rushing_touchdowns, receiving_targets, receptions, 
                        receiving_yards, receiving_touchdowns, return_touchdowns, two_point_conversions, fumbles_lost, 
                        field_goals_made, field_goals_attempted, field_goals_made_0_to_19, field_goals_made_20_to_29, field_goals_made_30_to_39, 
                        field_goals_made_40_to_49, field_goals_made_50_plus, extra_points_made, extra_points_attempted, defensive_touchdowns, 
                        special_teams_touchdowns, touchdowns_scored, fumbles_forced, fumbles_recovered, interceptions, 
                        tackles_for_loss, quarterback_hits, sacks, safeties, blocked_kicks, points_allowed) 
                    VALUES (
                        @player_id, @team_id, @season_type, @week, @name, 
                        @position, @status, @injury_status, @fantasy_points, @passing_completions, 
                        @passing_attempts, @passing_yards, @passing_touchdowns, @passing_interceptions, @passing_rating, 
                        @rushing_attempts, @rushing_yards, @rushing_touchdowns, @receiving_targets, @receptions, 
                        @receiving_yards, @receiving_touchdowns, @return_touchdowns, @two_point_conversions, @fumbles_lost, 
                        @field_goals_made, @field_goals_attempted, @field_goals_made_0_to_19, @field_goals_made_20_to_29, @field_goals_made_30_to_39, 
                        @field_goals_made_40_to_49, @field_goals_made_50_plus, @extra_points_made, @extra_points_attempted, @defensive_touchdowns, 
                        @special_teams_touchdowns, @touchdowns_scored, @fumbles_forced, @fumbles_recovered, @interceptions, 
                        @tackles_for_loss, @quarterback_hits, @sacks, @safeties, @blocked_kicks, @points_allowed);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsExt.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsExt.Week);
                    command.Parameters.AddWithValue("@name", playerStatsExt.Name);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AddDefenseProjectionsExtAsync(PlayerStatsExt playerStatsExt)
        {
             using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO player_projections_ext (
                        player_id, team_id, season_type, week, name, 
                        position, status, injury_status, fantasy_points, passing_completions, 
                        passing_attempts, passing_yards, passing_touchdowns, passing_interceptions, passing_rating, 
                        rushing_attempts, rushing_yards, rushing_touchdowns, receiving_targets, receptions, 
                        receiving_yards, receiving_touchdowns, return_touchdowns, two_point_conversions, fumbles_lost, 
                        field_goals_made, field_goals_attempted, field_goals_made_0_to_19, field_goals_made_20_to_29, field_goals_made_30_to_39, 
                        field_goals_made_40_to_49, field_goals_made_50_plus, extra_points_made, extra_points_attempted, defensive_touchdowns, 
                        special_teams_touchdowns, touchdowns_scored, fumbles_forced, fumbles_recovered, interceptions, 
                        tackles_for_loss, quarterback_hits, sacks, safeties, blocked_kicks, points_allowed) 
                    VALUES (
                        @player_id, @team_id, @season_type, @week, (SELECT name FROM players WHERE player_id = @player_id), 
                        @position, @status, @injury_status, @fantasy_points, @passing_completions, 
                        @passing_attempts, @passing_yards, @passing_touchdowns, @passing_interceptions, @passing_rating, 
                        @rushing_attempts, @rushing_yards, @rushing_touchdowns, @receiving_targets, @receptions, 
                        @receiving_yards, @receiving_touchdowns, @return_touchdowns, @two_point_conversions, @fumbles_lost, 
                        @field_goals_made, @field_goals_attempted, @field_goals_made_0_to_19, @field_goals_made_20_to_29, @field_goals_made_30_to_39, 
                        @field_goals_made_40_to_49, @field_goals_made_50_plus, @extra_points_made, @extra_points_attempted, @defensive_touchdowns, 
                        @special_teams_touchdowns, @touchdowns_scored, @fumbles_forced, @fumbles_recovered, @interceptions, 
                        @tackles_for_loss, @quarterback_hits, @sacks, @safeties, @blocked_kicks, @points_allowed);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", playerStatsExt.SeasonType);
                    command.Parameters.AddWithValue("@week", playerStatsExt.Week);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePlayerStatsExtAsync(PlayerStatsExt playerStatsExt)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_stats_ext
                    SET team_id = @team_id,
                        name = @name,
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points,
                        passing_completions = @passing_completions,
                        passing_attempts = @passing_attempts,
                        passing_yards = @passing_yards,
                        passing_touchdowns = @passing_touchdowns,
                        passing_interceptions = @passing_interceptions,
                        passing_rating = @passing_rating,
                        rushing_attempts = @rushing_attempts,
                        rushing_yards = @rushing_yards,
                        rushing_touchdowns = @rushing_touchdowns,
                        receiving_targets = @receiving_targets,
                        receptions = @receptions,
                        receiving_yards = @receiving_yards,
                        receiving_touchdowns = @receiving_touchdowns,
                        return_touchdowns = @return_touchdowns,
                        two_point_conversions = @two_point_conversions,
                        fumbles_lost = @fumbles_lost,
                        field_goals_made = @field_goals_made,
                        field_goals_attempted = @field_goals_attempted,
                        field_goals_made_0_to_19 = @field_goals_made_0_to_19,
                        field_goals_made_20_to_29 = @field_goals_made_20_to_29,
                        field_goals_made_30_to_39 = @field_goals_made_30_to_39,
                        field_goals_made_40_to_49 = @field_goals_made_40_to_49,
                        field_goals_made_50_plus = @field_goals_made_50_plus,
                        extra_points_made = @extra_points_made,
                        extra_points_attempted = @extra_points_attempted,
                        defensive_touchdowns = @defensive_touchdowns,
                        special_teams_touchdowns = @special_teams_touchdowns,
                        touchdowns_scored = @touchdowns_scored,
                        fumbles_forced = @fumbles_forced,
                        fumbles_recovered = @fumbles_recovered,
                        interceptions = @interceptions,
                        tackles_for_loss = @tackles_for_loss,
                        quarterback_hits = @quarterback_hits,
                        sacks = @sacks,
                        safeties = @safeties,
                        blocked_kicks = @blocked_kicks,
                        points_allowed = @points_allowed
                    WHERE player_id = @player_id
                        AND season_type = @season_type
                        AND week = @week;";
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", playerStatsExt.Name);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDefenseStatsExtAsync(PlayerStatsExt playerStatsExt)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_stats_ext
                    SET team_id = @team_id,
                        name = (SELECT name FROM players WHERE player_id = @player_id),
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points,
                        passing_completions = @passing_completions,
                        passing_attempts = @passing_attempts,
                        passing_yards = @passing_yards,
                        passing_touchdowns = @passing_touchdowns,
                        passing_interceptions = @passing_interceptions,
                        passing_rating = @passing_rating,
                        rushing_attempts = @rushing_attempts,
                        rushing_yards = @rushing_yards,
                        rushing_touchdowns = @rushing_touchdowns,
                        receiving_targets = @receiving_targets,
                        receptions = @receptions,
                        receiving_yards = @receiving_yards,
                        receiving_touchdowns = @receiving_touchdowns,
                        return_touchdowns = @return_touchdowns,
                        two_point_conversions = @two_point_conversions,
                        fumbles_lost = @fumbles_lost,
                        field_goals_made = @field_goals_made,
                        field_goals_attempted = @field_goals_attempted,
                        field_goals_made_0_to_19 = @field_goals_made_0_to_19,
                        field_goals_made_20_to_29 = @field_goals_made_20_to_29,
                        field_goals_made_30_to_39 = @field_goals_made_30_to_39,
                        field_goals_made_40_to_49 = @field_goals_made_40_to_49,
                        field_goals_made_50_plus = @field_goals_made_50_plus,
                        extra_points_made = @extra_points_made,
                        extra_points_attempted = @extra_points_attempted,
                        defensive_touchdowns = @defensive_touchdowns,
                        special_teams_touchdowns = @special_teams_touchdowns,
                        touchdowns_scored = @touchdowns_scored,
                        fumbles_forced = @fumbles_forced,
                        fumbles_recovered = @fumbles_recovered,
                        interceptions = @interceptions,
                        tackles_for_loss = @tackles_for_loss,
                        quarterback_hits = @quarterback_hits,
                        sacks = @sacks,
                        safeties = @safeties,
                        blocked_kicks = @blocked_kicks,
                        points_allowed = @points_allowed
                    WHERE player_id = @player_id
                        AND season_type = @season_type
                        AND week = @week;";
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePlayerProjectionsExtAsync(PlayerStatsExt playerStatsExt)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_projections_ext
                    SET team_id = @team_id,
                        name = @name,
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points,
                        passing_completions = @passing_completions,
                        passing_attempts = @passing_attempts,
                        passing_yards = @passing_yards,
                        passing_touchdowns = @passing_touchdowns,
                        passing_interceptions = @passing_interceptions,
                        passing_rating = @passing_rating,
                        rushing_attempts = @rushing_attempts,
                        rushing_yards = @rushing_yards,
                        rushing_touchdowns = @rushing_touchdowns,
                        receiving_targets = @receiving_targets,
                        receptions = @receptions,
                        receiving_yards = @receiving_yards,
                        receiving_touchdowns = @receiving_touchdowns,
                        return_touchdowns = @return_touchdowns,
                        two_point_conversions = @two_point_conversions,
                        fumbles_lost = @fumbles_lost,
                        field_goals_made = @field_goals_made,
                        field_goals_attempted = @field_goals_attempted,
                        field_goals_made_0_to_19 = @field_goals_made_0_to_19,
                        field_goals_made_20_to_29 = @field_goals_made_20_to_29,
                        field_goals_made_30_to_39 = @field_goals_made_30_to_39,
                        field_goals_made_40_to_49 = @field_goals_made_40_to_49,
                        field_goals_made_50_plus = @field_goals_made_50_plus,
                        extra_points_made = @extra_points_made,
                        extra_points_attempted = @extra_points_attempted,
                        defensive_touchdowns = @defensive_touchdowns,
                        special_teams_touchdowns = @special_teams_touchdowns,
                        touchdowns_scored = @touchdowns_scored,
                        fumbles_forced = @fumbles_forced,
                        fumbles_recovered = @fumbles_recovered,
                        interceptions = @interceptions,
                        tackles_for_loss = @tackles_for_loss,
                        quarterback_hits = @quarterback_hits,
                        sacks = @sacks,
                        safeties = @safeties,
                        blocked_kicks = @blocked_kicks,
                        points_allowed = @points_allowed
                    WHERE player_id = @player_id
                        AND season_type = @season_type
                        AND week = @week;";
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", playerStatsExt.Name);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateDefenseProjectionsExtAsync(PlayerStatsExt playerStatsExt)
        {
            int week = await _configurationDao.GetConfigurationValue("current_week");
            int seasonType = await _configurationDao.GetConfigurationValue("current_season_type");

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE player_projections_ext
                    SET team_id = @team_id,
                        name = (SELECT name FROM players WHERE player_id = @player_id),
                        position = @position,
                        status = @status,
                        injury_status = @injury_status,
                        fantasy_points = @fantasy_points,
                        passing_completions = @passing_completions,
                        passing_attempts = @passing_attempts,
                        passing_yards = @passing_yards,
                        passing_touchdowns = @passing_touchdowns,
                        passing_interceptions = @passing_interceptions,
                        passing_rating = @passing_rating,
                        rushing_attempts = @rushing_attempts,
                        rushing_yards = @rushing_yards,
                        rushing_touchdowns = @rushing_touchdowns,
                        receiving_targets = @receiving_targets,
                        receptions = @receptions,
                        receiving_yards = @receiving_yards,
                        receiving_touchdowns = @receiving_touchdowns,
                        return_touchdowns = @return_touchdowns,
                        two_point_conversions = @two_point_conversions,
                        fumbles_lost = @fumbles_lost,
                        field_goals_made = @field_goals_made,
                        field_goals_attempted = @field_goals_attempted,
                        field_goals_made_0_to_19 = @field_goals_made_0_to_19,
                        field_goals_made_20_to_29 = @field_goals_made_20_to_29,
                        field_goals_made_30_to_39 = @field_goals_made_30_to_39,
                        field_goals_made_40_to_49 = @field_goals_made_40_to_49,
                        field_goals_made_50_plus = @field_goals_made_50_plus,
                        extra_points_made = @extra_points_made,
                        extra_points_attempted = @extra_points_attempted,
                        defensive_touchdowns = @defensive_touchdowns,
                        special_teams_touchdowns = @special_teams_touchdowns,
                        touchdowns_scored = @touchdowns_scored,
                        fumbles_forced = @fumbles_forced,
                        fumbles_recovered = @fumbles_recovered,
                        interceptions = @interceptions,
                        tackles_for_loss = @tackles_for_loss,
                        quarterback_hits = @quarterback_hits,
                        sacks = @sacks,
                        safeties = @safeties,
                        blocked_kicks = @blocked_kicks,
                        points_allowed = @points_allowed
                    WHERE player_id = @player_id
                        AND season_type = @season_type
                        AND week = @week;";
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerStatsExt.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerStatsExt.TeamId);
                    command.Parameters.AddWithValue("@season_type", seasonType);
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@position", playerStatsExt.Position);
                    command.Parameters.AddWithValue("@status", playerStatsExt.Status);
                    command.Parameters.AddWithValue("@injury_status", playerStatsExt.InjuryStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fantasy_points", playerStatsExt.FantasyPoints);
                    command.Parameters.AddWithValue("@passing_completions", playerStatsExt.PassingCompletions);
                    command.Parameters.AddWithValue("@passing_attempts", playerStatsExt.PassingAttempts);
                    command.Parameters.AddWithValue("@passing_yards", playerStatsExt.PassingYards);
                    command.Parameters.AddWithValue("@passing_touchdowns", playerStatsExt.PassingTouchdowns);
                    command.Parameters.AddWithValue("@passing_interceptions", playerStatsExt.PassingInterceptions);
                    command.Parameters.AddWithValue("@passing_rating", playerStatsExt.PassingRating);
                    command.Parameters.AddWithValue("@rushing_attempts", playerStatsExt.RushingAttempts);
                    command.Parameters.AddWithValue("@rushing_yards", playerStatsExt.RushingYards);
                    command.Parameters.AddWithValue("@rushing_touchdowns", playerStatsExt.RushingTouchdowns);
                    command.Parameters.AddWithValue("@receiving_targets", playerStatsExt.ReceivingTargets);
                    command.Parameters.AddWithValue("@receptions", playerStatsExt.Receptions);
                    command.Parameters.AddWithValue("@receiving_yards", playerStatsExt.ReceivingYards);
                    command.Parameters.AddWithValue("@receiving_touchdowns", playerStatsExt.ReceivingTouchdowns);
                    command.Parameters.AddWithValue("@return_touchdowns", playerStatsExt.ReturnTouchdowns);
                    command.Parameters.AddWithValue("@two_point_conversions", playerStatsExt.TwoPointConversions);
                    command.Parameters.AddWithValue("@fumbles_lost", playerStatsExt.FumblesLost);
                    command.Parameters.AddWithValue("@field_goals_made", playerStatsExt.FieldGoalsMade);
                    command.Parameters.AddWithValue("@field_goals_attempted", playerStatsExt.FieldGoalsAttempted);
                    command.Parameters.AddWithValue("@field_goals_made_0_to_19", playerStatsExt.FieldGoalsMade0to19 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_20_to_29", playerStatsExt.FieldGoalsMade20to29 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_30_to_39", playerStatsExt.FieldGoalsMade30to39 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_40_to_49", playerStatsExt.FieldGoalsMade40to49 ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@field_goals_made_50_plus", playerStatsExt.FieldGoalsMade50Plus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@extra_points_made", playerStatsExt.ExtraPointsMade);
                    command.Parameters.AddWithValue("@extra_points_attempted", playerStatsExt.ExtraPointsAttempted);
                    command.Parameters.AddWithValue("@defensive_touchdowns", playerStatsExt.DefensiveTouchdowns);
                    command.Parameters.AddWithValue("@special_teams_touchdowns", playerStatsExt.SpecialTeamsTouchdowns);
                    command.Parameters.AddWithValue("@touchdowns_scored", playerStatsExt.TouchdownsScored);
                    command.Parameters.AddWithValue("@fumbles_forced", playerStatsExt.FumblesForced);
                    command.Parameters.AddWithValue("@fumbles_recovered", playerStatsExt.FumblesRecovered);
                    command.Parameters.AddWithValue("@interceptions", playerStatsExt.Interceptions);
                    command.Parameters.AddWithValue("@tackles_for_loss", playerStatsExt.TacklesForLoss);
                    command.Parameters.AddWithValue("@quarterback_hits", playerStatsExt.QuarterbackHits);
                    command.Parameters.AddWithValue("@sacks", playerStatsExt.Sacks);
                    command.Parameters.AddWithValue("@safeties", playerStatsExt.Safeties);
                    command.Parameters.AddWithValue("@blocked_kicks", playerStatsExt.BlockedKicks);
                    command.Parameters.AddWithValue("@points_allowed", playerStatsExt.PointsAllowed);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Quarterback
{
    public class QBSeasonTotalSqlDao : IQBSeasonTotalDao
    {
        private readonly string _connectionString;
        public QBSeasonTotalSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private static string SELECT_SQL = 
            @"SELECT 
                p.player_id, 
                COUNT(DISTINCT pse.week) AS week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                SUM(pse.passing_completions) AS passing_completions, 
                SUM(pse.passing_attempts) AS passing_attempts, 
                CASE 
                    WHEN SUM(pse.passing_attempts) = 0 THEN 0 
                    ELSE ROUND(SUM(pse.passing_completions) / SUM(pse.passing_attempts), 2) 
                END AS passing_completion_percentage, 
                SUM(pse.passing_yards) AS passing_yards, 
                SUM(pse.passing_touchdowns) AS passing_touchdowns, 
                SUM(pse.passing_interceptions) AS passing_interceptions, 
                ROUND(AVG(pse.passing_rating), 2) AS passing_rating, 
                SUM(pse.rushing_attempts) AS rushing_attempts, 
                SUM(pse.rushing_yards) AS rushing_yards, 
                SUM(pse.rushing_touchdowns) AS rushing_touchdowns, 
                SUM(pse.two_point_conversions) AS two_point_conversions, 
                SUM(pse.fumbles_lost) AS fumbles_lost, 
                SUM(pse.fantasy_points) AS fantasy_points_total, 
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_average 
            FROM player_stats_ext pse 
            JOIN players p ON p.player_id = pse.player_id 
            JOIN teams t ON t.team_id = pse.team_id 
            WHERE p.position = 'QB' ";

        private static string GROUP_BY_SQL =
            @"GROUP BY 
                p.player_id, 
                p.position, 
                t.team, 
                pse.position, 
                p.name, 
                p.status, 
                pse.injury_status 
            ORDER BY fantasy_points_total DESC;";

        private static string CONF_SQL = 
            @"AND lower(t.conference) ILIKE @conf ";

        private static string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private static string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";

        
        public async Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsAsync()
        {
            List<PlayerStatsExtDto> playerStatsExtDtos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection);
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        PlayerStatsExtDto playerStatsExtDto = new PlayerStatsExtDto();
                        {
                            playerStatsExtDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            playerStatsExtDto.Week = Convert.ToInt32(reader["week"]);
                            playerStatsExtDto.Position = Convert.ToString(reader["position"]);
                            playerStatsExtDto.Team = Convert.ToString(reader["team"]);
                            playerStatsExtDto.Name = Convert.ToString(reader["name"]);
                            playerStatsExtDto.Status = Convert.ToString(reader["status"]);
                            playerStatsExtDto.InjuryStatus = Convert.ToString(reader["injury_status"]);
                            playerStatsExtDto.PassingCompletions = Convert.ToDouble(reader["passing_completions"]);
                            playerStatsExtDto.PassingAttempts = Convert.ToDouble(reader["passing_attempts"]);
                            playerStatsExtDto.PassingCompletionPercentage = Convert.ToDouble(reader["passing_completion_percentage"]);
                            playerStatsExtDto.PassingYards = Convert.ToDouble(reader["passing_yards"]);
                            playerStatsExtDto.PassingTouchdowns = Convert.ToDouble(reader["passing_touchdowns"]);
                            playerStatsExtDto.PassingInterceptions = Convert.ToDouble(reader["passing_interceptions"]);
                            playerStatsExtDto.PassingRating = Convert.ToDouble(reader["passing_rating"]);
                            playerStatsExtDto.RushingAttempts = Convert.ToDouble(reader["rushing_attempts"]);
                            playerStatsExtDto.RushingYards = Convert.ToDouble(reader["rushing_yards"]);
                            playerStatsExtDto.RushingTouchdowns = Convert.ToDouble(reader["rushing_touchdowns"]);
                            playerStatsExtDto.TwoPointConversions = Convert.ToDouble(reader["two_point_conversions"]);
                            playerStatsExtDto.FumblesLost = Convert.ToDouble(reader["fumbles_lost"]);
                            playerStatsExtDto.FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]);
                            playerStatsExtDto.FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]);

                            // C# should handle null values for unused fields
                        };
                        playerStatsExtDtos.Add(playerStatsExtDto);
                    }
                }
            }
            return playerStatsExtDtos;
        }

        public async Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsByConfAsync(string conf)
        {
            List<PlayerStatsExtDto> playerStatsExtDtos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection);
                {
                    command.Parameters.AddWithValue("conf", $"%{conf}%");
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        PlayerStatsExtDto playerStatsExtDto = new PlayerStatsExtDto();
                        {
                            playerStatsExtDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            playerStatsExtDto.Week = Convert.ToInt32(reader["week"]);
                            playerStatsExtDto.Position = Convert.ToString(reader["position"]);
                            playerStatsExtDto.Team = Convert.ToString(reader["team"]);
                            playerStatsExtDto.Name = Convert.ToString(reader["name"]);
                            playerStatsExtDto.Status = Convert.ToString(reader["status"]);
                            playerStatsExtDto.InjuryStatus = Convert.ToString(reader["injury_status"]);
                            playerStatsExtDto.PassingCompletions = Convert.ToDouble(reader["passing_completions"]);
                            playerStatsExtDto.PassingAttempts = Convert.ToDouble(reader["passing_attempts"]);
                            playerStatsExtDto.PassingCompletionPercentage = Convert.ToDouble(reader["passing_completion_percentage"]);
                            playerStatsExtDto.PassingYards = Convert.ToDouble(reader["passing_yards"]);
                            playerStatsExtDto.PassingTouchdowns = Convert.ToDouble(reader["passing_touchdowns"]);
                            playerStatsExtDto.PassingInterceptions = Convert.ToDouble(reader["passing_interceptions"]);
                            playerStatsExtDto.PassingRating = Convert.ToDouble(reader["passing_rating"]);
                            playerStatsExtDto.RushingAttempts = Convert.ToDouble(reader["rushing_attempts"]);
                            playerStatsExtDto.RushingYards = Convert.ToDouble(reader["rushing_yards"]);
                            playerStatsExtDto.RushingTouchdowns = Convert.ToDouble(reader["rushing_touchdowns"]);
                            playerStatsExtDto.TwoPointConversions = Convert.ToDouble(reader["two_point_conversions"]);
                            playerStatsExtDto.FumblesLost = Convert.ToDouble(reader["fumbles_lost"]);
                            playerStatsExtDto.FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]);
                            playerStatsExtDto.FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]);

                            // C# should handle null values for unused fields
                        };
                        playerStatsExtDtos.Add(playerStatsExtDto);
                    }
                }
            }
            return playerStatsExtDtos;
        }

        public async Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsByTeamAsync(string team)
        {
            List<PlayerStatsExtDto> playerStatsExtDtos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection);
                {
                    command.Parameters.AddWithValue("team", $"%{team}%");
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        PlayerStatsExtDto playerStatsExtDto = new PlayerStatsExtDto();
                        {
                            playerStatsExtDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            playerStatsExtDto.Week = Convert.ToInt32(reader["week"]);
                            playerStatsExtDto.Position = Convert.ToString(reader["position"]);
                            playerStatsExtDto.Team = Convert.ToString(reader["team"]);
                            playerStatsExtDto.Name = Convert.ToString(reader["name"]);
                            playerStatsExtDto.Status = Convert.ToString(reader["status"]);
                            playerStatsExtDto.InjuryStatus = Convert.ToString(reader["injury_status"]);
                            playerStatsExtDto.PassingCompletions = Convert.ToDouble(reader["passing_completions"]);
                            playerStatsExtDto.PassingAttempts = Convert.ToDouble(reader["passing_attempts"]);
                            playerStatsExtDto.PassingCompletionPercentage = Convert.ToDouble(reader["passing_completion_percentage"]);
                            playerStatsExtDto.PassingYards = Convert.ToDouble(reader["passing_yards"]);
                            playerStatsExtDto.PassingTouchdowns = Convert.ToDouble(reader["passing_touchdowns"]);
                            playerStatsExtDto.PassingInterceptions = Convert.ToDouble(reader["passing_interceptions"]);
                            playerStatsExtDto.PassingRating = Convert.ToDouble(reader["passing_rating"]);
                            playerStatsExtDto.RushingAttempts = Convert.ToDouble(reader["rushing_attempts"]);
                            playerStatsExtDto.RushingYards = Convert.ToDouble(reader["rushing_yards"]);
                            playerStatsExtDto.RushingTouchdowns = Convert.ToDouble(reader["rushing_touchdowns"]);
                            playerStatsExtDto.TwoPointConversions = Convert.ToDouble(reader["two_point_conversions"]);
                            playerStatsExtDto.FumblesLost = Convert.ToDouble(reader["fumbles_lost"]);
                            playerStatsExtDto.FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]);
                            playerStatsExtDto.FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]);

                            // C# should handle null values for unused fields
                        };
                        playerStatsExtDtos.Add(playerStatsExtDto);
                    }
                }
            }
            return playerStatsExtDtos;
        }

        public async Task<List<PlayerStatsExtDto>> getQBSeasonTotalStatsByNameAsync(string name)
        {
            List<PlayerStatsExtDto> playerStatsExtDtos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection);
                {
                    command.Parameters.AddWithValue("name", $"%{name}%");
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        PlayerStatsExtDto playerStatsExtDto = new PlayerStatsExtDto();
                        {
                            playerStatsExtDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            playerStatsExtDto.Week = Convert.ToInt32(reader["week"]);
                            playerStatsExtDto.Position = Convert.ToString(reader["position"]);
                            playerStatsExtDto.Team = Convert.ToString(reader["team"]);
                            playerStatsExtDto.Name = Convert.ToString(reader["name"]);
                            playerStatsExtDto.Status = Convert.ToString(reader["status"]);
                            playerStatsExtDto.InjuryStatus = Convert.ToString(reader["injury_status"]);
                            playerStatsExtDto.PassingCompletions = Convert.ToDouble(reader["passing_completions"]);
                            playerStatsExtDto.PassingAttempts = Convert.ToDouble(reader["passing_attempts"]);
                            playerStatsExtDto.PassingCompletionPercentage = Convert.ToDouble(reader["passing_completion_percentage"]);
                            playerStatsExtDto.PassingYards = Convert.ToDouble(reader["passing_yards"]);
                            playerStatsExtDto.PassingTouchdowns = Convert.ToDouble(reader["passing_touchdowns"]);
                            playerStatsExtDto.PassingInterceptions = Convert.ToDouble(reader["passing_interceptions"]);
                            playerStatsExtDto.PassingRating = Convert.ToDouble(reader["passing_rating"]);
                            playerStatsExtDto.RushingAttempts = Convert.ToDouble(reader["rushing_attempts"]);
                            playerStatsExtDto.RushingYards = Convert.ToDouble(reader["rushing_yards"]);
                            playerStatsExtDto.RushingTouchdowns = Convert.ToDouble(reader["rushing_touchdowns"]);
                            playerStatsExtDto.TwoPointConversions = Convert.ToDouble(reader["two_point_conversions"]);
                            playerStatsExtDto.FumblesLost = Convert.ToDouble(reader["fumbles_lost"]);
                            playerStatsExtDto.FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]);
                            playerStatsExtDto.FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]);

                            // C# should handle null values for unused fields
                        };
                        playerStatsExtDtos.Add(playerStatsExtDto);
                    }
                }
            }
            return playerStatsExtDtos;
        }
    }
}
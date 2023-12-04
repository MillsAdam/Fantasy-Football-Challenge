using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Quarterback
{
    public class QBWeeklyProjectedSqlDao : IQBWeeklyProjectedDao
    {
        private readonly string _connectionString;
        public QBWeeklyProjectedSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"SELECT 
                p.player_id,
                ppe.week,
                p.position,
                t.team,
                p.name,
                p.status,
                p.injury_status,
                ppe.passing_completions,
                ppe.passing_attempts,
                CASE
                    WHEN ppe.passing_attempts = 0 THEN 0
                    ELSE ROUND((ppe.passing_completions / ppe.passing_attempts) * 100, 2)
                END AS passing_completion_percentage,
                ppe.passing_yards,
                ppe.passing_touchdowns,
                ppe.passing_interceptions,
                ppe.passing_rating,
                ppe.rushing_attempts,
                ppe.rushing_yards,
                ppe.rushing_touchdowns,
                ppe.two_point_conversions,
                ppe.fumbles_lost,
                ppe.fantasy_points AS fantasy_points_total,
                ppe.fantasy_points AS fantasy_points_average,
                t.conference,
                t.status AS team_status
            FROM player_projections_ext ppe
            JOIN players p ON p.player_id = ppe.player_id
            JOIN teams t ON t.team_id = ppe.team_id
            WHERE p.position = 'QB'
                AND ppe.week = @week ";
        
        private const string GROUP_BY_SQL =
            @"GROUP BY
                p.player_id,
                ppe.week,
                p.position,
                t.team,
                p.name,
                p.status,
                p.injury_status,
                ppe.passing_completions,
                ppe.passing_attempts,
                ppe.passing_yards,
                ppe.passing_touchdowns,
                ppe.passing_interceptions,
                ppe.passing_rating,
                ppe.rushing_attempts,
                ppe.rushing_yards,
                ppe.rushing_touchdowns,
                ppe.two_point_conversions,
                ppe.fumbles_lost,
                ppe.fantasy_points,
                t.conference,
                t.status 
            ORDER BY fantasy_points_total DESC;";

        private const string CONF_SQL = 
            @"AND lower(t.conference) ILIKE @conf ";
        
        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsAsync(int week)
        {
            List<PlayerStatsExtDto> qbWeeklyProjectedStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbWeeklyProjectedStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyProjectedStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsByConfAsync(string conf, int week)
        {
            List<PlayerStatsExtDto> qbWeeklyProjectedStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbWeeklyProjectedStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyProjectedStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsByTeamAsync(string team, int week)
        {
            List<PlayerStatsExtDto> qbWeeklyProjectedStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbWeeklyProjectedStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyProjectedStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyProjectedStatsByNameAsync(string name, int week)
        {
            List<PlayerStatsExtDto> qbWeeklyProjectedStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbWeeklyProjectedStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyProjectedStats;
        }

        private PlayerStatsExtDto MapRowToQBStat(NpgsqlDataReader reader)
        {
            return new PlayerStatsExtDto()
            {
                PlayerId = Convert.ToInt32(reader["player_id"]),
                Week = Convert.ToInt32(reader["week"]),
                Position = Convert.ToString(reader["position"]),
                Team = Convert.ToString(reader["team"]),
                Name = Convert.ToString(reader["name"]),
                Status = Convert.ToString(reader["status"]),
                InjuryStatus = Convert.ToString(reader["injury_status"]),
                PassingCompletions = Convert.ToDouble(reader["passing_completions"]),
                PassingAttempts = Convert.ToDouble(reader["passing_attempts"]),
                PassingCompletionPercentage = Convert.ToDouble(reader["passing_completion_percentage"]),
                PassingYards = Convert.ToDouble(reader["passing_yards"]),
                PassingTouchdowns = Convert.ToDouble(reader["passing_touchdowns"]),
                PassingInterceptions = Convert.ToDouble(reader["passing_interceptions"]),
                PassingRating = Convert.ToDouble(reader["passing_rating"]),
                RushingAttempts = Convert.ToDouble(reader["rushing_attempts"]),
                RushingYards = Convert.ToDouble(reader["rushing_yards"]),
                RushingTouchdowns = Convert.ToDouble(reader["rushing_touchdowns"]),
                TwoPointConversions = Convert.ToDouble(reader["two_point_conversions"]),
                FumblesLost = Convert.ToDouble(reader["fumbles_lost"]),
                FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]),
                FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]),
                Conference = Convert.ToString(reader["conference"]),
                TeamStatus = Convert.ToString(reader["team_status"])
            };
        }
    }
}
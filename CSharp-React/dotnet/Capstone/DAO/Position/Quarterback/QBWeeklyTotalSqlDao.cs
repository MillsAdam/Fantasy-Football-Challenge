using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Quarterback
{
    public class QBWeeklyTotalSqlDao : IQBWeeklyTotalDao
    {
        private readonly string _connectionString;
        public QBWeeklyTotalSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"SELECT 
                p.player_id, 
                pse.week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                pse.passing_completions, 
                pse.passing_attempts, 
                CASE 
                    WHEN pse.passing_attempts = 0 THEN 0 
                    ELSE ROUND((pse.passing_completions / pse.passing_attempts) * 100, 2) 
                END AS passing_completion_percentage, 
                pse.passing_yards, 
                pse.passing_touchdowns, 
                pse.passing_interceptions, 
                pse.passing_rating, 
                pse.rushing_attempts, 
                pse.rushing_yards, 
                pse.rushing_touchdowns, 
                pse.two_point_conversions, 
                pse.fumbles_lost, 
                pse.fantasy_points AS fantasy_points_total, 
                pse.fantasy_points AS fantasy_points_average, 
                t.conference, 
                t.status AS team_status 
            FROM player_stats_ext pse 
            JOIN players p ON p.player_id = pse.player_id 
            JOIN teams t ON t.team_id = pse.team_id 
            WHERE p.position = 'QB' 
                AND pse.week = @week ";
        
        private const string GROUP_BY_SQL = 
            @"GROUP BY 
                p.player_id, 
                pse.week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                pse.passing_completions, 
                pse.passing_attempts, 
                pse.passing_yards, 
                pse.passing_touchdowns, 
                pse.passing_interceptions, 
                pse.passing_rating, 
                pse.rushing_attempts, 
                pse.rushing_yards, 
                pse.rushing_touchdowns, 
                pse.two_point_conversions, 
                pse.fumbles_lost, 
                pse.fantasy_points, 
                t.conference, 
                t.status 
            ORDER BY fantasy_points_total DESC;";

        private const string CONF_SQL =
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";
        
        public async Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsAsync(int week)
        {
            List<PlayerStatsExtDto> qbWeeklyTotalStats = new List<PlayerStatsExtDto>();
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
                            qbWeeklyTotalStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsByConfAsync(string conf, int week)
        {
            List<PlayerStatsExtDto> qbWeeklyTotalStats = new List<PlayerStatsExtDto>();
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
                            qbWeeklyTotalStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsByTeamAsync(string team, int week)
        {
            List<PlayerStatsExtDto> qbWeeklyTotalStats = new List<PlayerStatsExtDto>();
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
                            qbWeeklyTotalStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBWeeklyTotalStatsByNameAsync(string name, int week)
        {
            List<PlayerStatsExtDto> qbWeeklyTotalStats = new List<PlayerStatsExtDto>();
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
                            qbWeeklyTotalStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbWeeklyTotalStats;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Quarterback
{
    public class QBLast4AverageSqlDao : IQBLast4AverageDao
    {
        private readonly string _connectionString;
        private readonly IConfigurationDao _configurationDao;
        public QBLast4AverageSqlDao(IConfiguration configuration, IConfigurationDao configurationDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _configurationDao = configurationDao;
        }

        private const string SELECT_SQL = 
            @"WITH StartingWeek AS (
                SELECT week 
                FROM player_stats_ext 
                WHERE week = @week 
                ORDER BY week 
                LIMIT 1
            ) 
            SELECT 
                p.player_id, 
                COUNT(DISTINCT pse.week) AS week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                ROUND(AVG(pse.passing_completions), 2) AS passing_completions, 
                ROUND(AVG(pse.passing_attempts), 2) AS passing_attempts, 
                CASE 
                    WHEN ROUND(AVG(pse.passing_attempts), 2) = 0 THEN 0 
                    ELSE ROUND((ROUND(AVG(pse.passing_completions), 2) / ROUND(AVG(pse.passing_attempts), 2)) * 100, 2) 
                END AS passing_completion_percentage, 
                ROUND(AVG(pse.passing_yards), 2) AS passing_yards, 
                ROUND(AVG(pse.passing_touchdowns), 2) AS passing_touchdowns, 
                ROUND(AVG(pse.passing_interceptions), 2) AS passing_interceptions, 
                ROUND(AVG(pse.passing_rating), 2) AS passing_rating, 
                ROUND(AVG(pse.rushing_attempts), 2) AS rushing_attempts, 
                ROUND(AVG(pse.rushing_yards), 2) AS rushing_yards, 
                ROUND(AVG(pse.rushing_touchdowns), 2) AS rushing_touchdowns, 
                ROUND(AVG(pse.two_point_conversions), 2) AS two_point_conversions, 
                ROUND(AVG(pse.fumbles_lost), 2) AS fumbles_lost, 
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_total, 
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_average, 
                t.conference, 
                t.status AS team_status 
            FROM player_stats_ext pse 
            JOIN players p ON p.player_id = pse.player_id 
            JOIN teams t ON t.team_id = pse.team_id 
            CROSS JOIN StartingWeek AS sw 
            WHERE p.position = 'QB' 
                AND pse.week <= sw.week  
                AND pse.week >= sw.week - 3";

        private const string GROUP_BY_SQL =
            @"GROUP BY 
                p.player_id, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                t.conference, 
                t.status 
            ORDER BY fantasy_points_total DESC;";

        private const string CONF_SQL = 
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL = 
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";

        public async Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsAsync()
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> qbLast4AverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbLast4AverageStats.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbLast4AverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsByConfAsync(string conf)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> qbLast4AverageStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbLast4AverageStatsByConf.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbLast4AverageStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsByTeamAsync(string team)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> qbLast4AverageStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week -1);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbLast4AverageStatsByTeam.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbLast4AverageStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getQBLast4AverageStatsByNameAsync(string name)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> qbLast4AverageStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week -1);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            qbLast4AverageStatsByName.Add(MapRowToQBStat(reader));
                        }
                    }
                }
            }
            return qbLast4AverageStatsByName;
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
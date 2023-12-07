using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Flex
{
    public class FlexLast4AverageSqlDao : IFlexLast4AverageDao
    {
        private readonly string _connectionString;
        private readonly IConfigurationDao _configurationDao;
        public FlexLast4AverageSqlDao(IConfiguration configuration, IConfigurationDao configurationDao)
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
            ), PlayerTotals AS (
                SELECT 
                    team_id, 
                    position, 
                    SUM(average_total_touches) AS total_touches 
                FROM (
                    SELECT 
                        player_id, 
                        team_id, 
                        position, 
                        ROUND(AVG(rushing_attempts + receiving_targets), 2) AS average_total_touches 
                    FROM player_stats_ext pse
                    CROSS JOIN StartingWeek AS sw 
                    WHERE position IN ('RB', 'WR', 'TE') 
                        AND pse.week <= sw.week 
                        AND pse.week >= sw.week - 3 
                    GROUP BY player_id, team_id, position
                ) AS Subquery 
                GROUP BY team_id, position
            ) 
            SELECT 
                p.player_id, 
                COUNT(DISTINCT pse.week) AS week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                ROUND(AVG(pse.rushing_attempts), 2) AS rushing_attempts, 
                ROUND(AVG(pse.rushing_yards), 2) AS rushing_yards, 
                CASE 
                    WHEN AVG(pse.rushing_attempts) = 0 THEN 0 
                    ELSE ROUND((AVG(pse.rushing_yards) / AVG(pse.rushing_attempts)), 2) 
                END AS rushing_yards_per_attempt, 
                ROUND(AVG(pse.rushing_touchdowns), 2) AS rushing_touchdowns, 
                ROUND(AVG(pse.receiving_targets), 2) AS receiving_targets, 
                ROUND(AVG(pse.receptions), 2) AS receptions, 
                ROUND(AVG(pse.receiving_yards), 2) AS receiving_yards, 
                CASE 
                    WHEN AVG(pse.receptions) = 0 THEN 0 
                    ELSE ROUND((AVG(pse.receiving_yards) / AVG(pse.receptions)), 2) 
                END AS receiving_yards_per_reception, 
                ROUND(AVG(pse.receiving_touchdowns), 2) AS receiving_touchdowns, 
                ROUND(AVG(pse.return_touchdowns), 2) AS return_touchdowns, 
                ROUND(AVG(pse.two_point_conversions), 2) AS two_point_conversions, 
                ROUND(AVG(pse.rushing_attempts + pse.receiving_targets) * 100 / AVG(pt.total_touches), 2) AS usage, 
                ROUND(AVG(pse.fumbles_lost), 2) AS fumbles_lost, 
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_total, 
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_average, 
                t.conference, 
                t.status AS team_status 
            FROM player_stats_ext pse 
            JOIN players p ON p.player_id = pse.player_id 
            JOIN teams t ON t.team_id = pse.team_id 
            JOIN PlayerTotals pt ON pt.team_id = pse.team_id AND pt.position = pse.position 
            CROSS JOIN StartingWeek sw 
            WHERE pse.week <= sw.week 
                AND pse.week >= sw.week - 3 ";

        private const string FLEX_SQL = 
            @"AND pse.position IN ('RB', 'WR', 'TE') ";

        private const string POSITION_SQL = 
            @"AND lower(pse.position) ILIKE @pos ";

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

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsAsync()
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByConfAsync(string conf)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByTeamAsync(string team)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByNameAsync(string name)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByName;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAsync(string pos)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByPos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByPos.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByPos;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndConfAsync(string pos, string conf)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByPosAndConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByPosAndConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByPosAndConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndTeamAsync(string pos, string team)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByPosAndTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByPosAndTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByPosAndTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4AverageStatsByPosAndNameAsync(string pos, string name)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4AverageStatsByPosAndName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4AverageStatsByPosAndName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4AverageStatsByPosAndName;
        }


        private PlayerStatsExtDto MapRowToFlexStat(NpgsqlDataReader reader)
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
                RushingAttempts = Convert.ToDouble(reader["rushing_attempts"]),
                RushingYards = Convert.ToDouble(reader["rushing_yards"]),
                RushingYardsPerAttempt = Convert.ToDouble(reader["rushing_yards_per_attempt"]),
                RushingTouchdowns = Convert.ToDouble(reader["rushing_touchdowns"]),
                ReceivingTargets = Convert.ToDouble(reader["receiving_targets"]),
                Receptions = Convert.ToDouble(reader["receptions"]),
                ReceivingYards = Convert.ToDouble(reader["receiving_yards"]),
                ReceivingYardsPerReception = Convert.ToDouble(reader["receiving_yards_per_reception"]),
                ReceivingTouchdowns = Convert.ToDouble(reader["receiving_touchdowns"]),
                ReturnTouchdowns = Convert.ToDouble(reader["return_touchdowns"]),
                TwoPointConversions = Convert.ToDouble(reader["two_point_conversions"]),
                Usage = Convert.ToDouble(reader["usage"]),
                FumblesLost = Convert.ToDouble(reader["fumbles_lost"]),
                FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]),
                FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]),
                Conference = Convert.ToString(reader["conference"]),
                TeamStatus = Convert.ToString(reader["team_status"])
            };
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Flex
{
    public class FlexLast4TotalSqlDao : IFlexLast4TotalDao
    {

        private readonly string _connectionString;
        private readonly IConfigurationDao _configurationDao;
        public FlexLast4TotalSqlDao(IConfiguration configuration, IConfigurationDao configurationDao)
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
                    SUM(rushing_attempts + receiving_targets) AS total_touches 
                FROM player_stats_ext pse 
                CROSS JOIN StartingWeek sw 
                WHERE position IN ('RB', 'WR', 'TE') 
                    AND pse.week <= sw.week 
                    AND pse.week >= sw.week - 3 
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
                SUM(pse.rushing_attempts) AS rushing_attempts, 
                SUM(pse.rushing_yards) AS rushing_yards, 
                CASE 
                    WHEN SUM(pse.rushing_attempts) = 0 THEN 0 
                    ELSE ROUND(SUM(pse.rushing_yards) / SUM(pse.rushing_attempts), 2) 
                END AS rushing_yards_per_attempt, 
                SUM(pse.rushing_touchdowns) AS rushing_touchdowns, 
                SUM(pse.receiving_targets) AS receiving_targets, 
                SUM(pse.receptions) AS receptions, 
                SUM(pse.receiving_yards) AS receiving_yards, 
                CASE 
                    WHEN SUM(pse.receptions) = 0 THEN 0 
                    ELSE ROUND(SUM(pse.receiving_yards) / SUM(pse.receptions), 2) 
                END AS receiving_yards_per_reception, 
                SUM(pse.receiving_touchdowns) AS receiving_touchdowns, 
                SUM(pse.return_touchdowns) AS return_touchdowns, 
                SUM(pse.two_point_conversions) AS two_point_conversions, 
                ROUND(SUM(pse.rushing_attempts + pse.receiving_targets) * 100 / pt.total_touches, 2) AS usage, 
                SUM(pse.fumbles_lost) AS fumbles_lost, 
                SUM(pse.fantasy_points) AS fantasy_points_total, 
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

        private const string GROUP_BY = 
            @"GROUP BY 
                p.player_id, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                t.conference, 
                t.status, 
                pt.total_touches 
            ORDER BY fantasy_points_total DESC";

        private const string CONF_SQL = 
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";

                
        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsAsync()
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByConfAsync(string conf)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + CONF_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@conf", conf);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByTeamAsync(string team)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@team", team);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByNameAsync(string name)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@name", name);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByName;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAsync(string pos)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByPos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", pos);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByPos.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByPos;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndConfAsync(string pos, string conf)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByPosAndConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + CONF_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@conf", conf);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByPosAndConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByPosAndConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndTeamAsync(string pos, string team)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByPosAndTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@team", team);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByPosAndTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByPosAndTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexLast4TotalStatsByPosAndNameAsync(string pos, string name)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> flexLast4TotalStatsByPosAndName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_BY, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@name", name);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexLast4TotalStatsByPosAndName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexLast4TotalStatsByPosAndName;
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
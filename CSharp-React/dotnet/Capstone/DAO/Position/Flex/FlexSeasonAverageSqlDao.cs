using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Flex
{
    public class FlexSeasonAverageSqlDao : IFlexSeasonAverageDao
    {
        private readonly string _connectionString;
        public FlexSeasonAverageSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"WITH PlayerTotals AS (
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
                    FROM player_stats_ext  
                    WHERE position IN ('RB', 'WR', 'TE') 
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
                    WHEN SUM(pse.rushing_attempts) = 0 THEN 0 
                    ELSE ROUND(SUM(pse.rushing_yards) / SUM(pse.rushing_attempts), 2) 
                END AS rushing_yards_per_attempt, 
                ROUND(AVG(pse.rushing_touchdowns), 2) AS rushing_touchdowns, 
                ROUND(AVG(pse.receiving_targets), 2) AS receiving_targets, 
                ROUND(AVG(pse.receptions), 2) AS receptions, 
                ROUND(AVG(pse.receiving_yards), 2) AS receiving_yards, 
                CASE 
                    WHEN SUM(pse.receptions) = 0 THEN 0 
                    ELSE ROUND(SUM(pse.receiving_yards) / SUM(pse.receptions), 2) 
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
            JOIN PlayerTotals pt ON pt.team_id = pse.team_id AND pt.position = pse.position ";

        private const string FLEX_SQL = 
            @"WHERE pse.position IN ('RB', 'WR', 'TE') ";

        private const string POSITION_SQL = 
            @"WHERE lower(pse.position) ILIKE @pos ";

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
            ORDER BY fantasy_points_total DESC";

        private const string CONF_SQL =
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsAsync()
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + GROUP_BY_SQL, connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByConfAsync(string conf)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@conf", $"%{conf.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByTeamAsync(string team)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@team", $"%{team.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByNameAsync(string name)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@name", $"%{name.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAsync(string pos)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@pos", $"%{pos.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndConfAsync(string pos, string conf)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@pos", $"%{pos.ToLower()}%");
                    command.Parameters.AddWithValue("@conf", $"%{conf.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndTeamAsync(string pos, string team)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@pos", $"%{pos.ToLower()}%");
                    command.Parameters.AddWithValue("@team", $"%{team.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonAverageStatsByPosAndNameAsync(string pos, string name)
        {
            List<PlayerStatsExtDto> flexSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@pos", $"%{pos.ToLower()}%");
                    command.Parameters.AddWithValue("@name", $"%{name.ToLower()}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexSeasonAverageStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonAverageStats;
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
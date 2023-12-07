using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Flex
{
    public class FlexSeasonTotalSqlDao : IFlexSeasonTotalDao
    {
        private readonly string _connectionString;
        public FlexSeasonTotalSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"WITH PlayerTotals AS (
                SELECT 
                    pse.team_id, 
                    pse.position, 
                    SUM(pse.rushing_attempts + pse.receiving_targets) AS total_touches 
                FROM player_stats_ext pse 
                WHERE pse.position IN ('RB', 'WR', 'TE') 
                GROUP BY pse.team_id, pse.position
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
                pse.position, 
                p.name, 
                p.status, 
                p.injury_status, 
                t.conference, 
                t.status, 
                pt.total_touches 
            ORDER BY fantasy_points_total DESC;";

        private const string CONF_SQL =
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsAsync()
        {
            List<PlayerStatsExtDto> flexSeasonTotalStats = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + GROUP_BY_SQL, connection))
                {
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByConfAsync(string conf)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByConf = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("conf", $"%{conf}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByTeamAsync(string team)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByTeam = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("team", $"%{team}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByNameAsync(string name)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByName = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("name", $"%{name}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByName;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAsync(string pos)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByPos = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("pos", $"%{pos}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByPos.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByPos;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAndConfAsync(string pos, string conf)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByPosAndConf = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("pos", $"%{pos}%");
                    command.Parameters.AddWithValue("conf", $"%{conf}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByPosAndConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByPosAndConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAndTeamAsync(string pos, string team)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByPosAndTeam = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("pos", $"%{pos}%");
                    command.Parameters.AddWithValue("team", $"%{team}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByPosAndTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByPosAndTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexSeasonTotalStatsByPosAndNameAsync(string pos, string name)
        {
            List<PlayerStatsExtDto> flexSeasonTotalStatsByPosAndName = new List<PlayerStatsExtDto>();
            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("pos", $"%{pos}%");
                    command.Parameters.AddWithValue("name", $"%{name}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            flexSeasonTotalStatsByPosAndName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexSeasonTotalStatsByPosAndName;
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
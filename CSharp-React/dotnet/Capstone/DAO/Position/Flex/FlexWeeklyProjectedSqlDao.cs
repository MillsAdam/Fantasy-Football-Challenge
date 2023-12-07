using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Flex
{
    public class FlexWeeklyProjectedSqlDao : IFlexWeeklyProjectedDao
    {
        private readonly string _connectionString;
        public FlexWeeklyProjectedSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"WITH PlayerTotals AS (
                SELECT 
                    week,
                    team_id,
                    position,
                    SUM(rushing_attempts + receiving_targets) AS total_touches 
                FROM player_projections_ext
                WHERE position IN ('RB', 'WR', 'TE')
                    AND week = @week
                GROUP BY week, team_id, position
            ) 
            SELECT 
                p.player_id,
                ppe.week,
                p.position,
                t.team,
                p.name,
                p.status,
                p.injury_status,
                ppe.rushing_attempts,
                ppe.rushing_yards,
                CASE
                    WHEN ppe.rushing_attempts = 0 THEN 0
                    ELSE ROUND((ppe.rushing_yards / ppe.rushing_attempts), 2)
                END AS rushing_yards_per_attempt,
                ppe.rushing_touchdowns,
                ppe.receiving_targets,
                ppe.receptions,
                ppe.receiving_yards,
                CASE
                    WHEN ppe.receptions = 0 THEN 0
                    ELSE ROUND((ppe.receiving_yards / ppe.receptions), 2)
                END AS receiving_yards_per_reception,
                ppe.receiving_touchdowns,
                ppe.return_touchdowns,
                ppe.two_point_conversions,
                CASE
                    WHEN pt.total_touches = 0 THEN 0
                    ELSE ROUND(((ppe.rushing_attempts + ppe.receiving_targets) / pt.total_touches) * 100, 2)
                END AS usage,
                ppe.fumbles_lost,
                ppe.fantasy_points AS fantasy_points_total,
                ppe.fantasy_points AS fantasy_points_average,
                t.conference,
                t.status AS team_status
            FROM player_projections_ext ppe
            JOIN players p ON p.player_id = ppe.player_id
            JOIN teams t ON t.team_id = ppe.team_id
            JOIN PlayerTotals pt ON pt.week = ppe.week AND pt.team_id = ppe.team_id AND pt.position = ppe.position ";
            
        private const string FLEX_SQL = 
            @"WHERE p.position IN ('RB', 'WR', 'TE') ";

        private const string POSITION_SQL = 
            @"WHERE lower(p.position) ILIKE @pos ";

        private const string GROUP_BY_SQL = 
            @"GROUP BY
                p.player_id,
                ppe.week,
                p.position,
                t.team,
                p.name,
                p.status,
                p.injury_status,
                ppe.rushing_attempts,
                ppe.rushing_yards,
                ppe.rushing_touchdowns,
                ppe.receiving_targets,
                ppe.receptions,
                ppe.receiving_yards,
                ppe.receiving_touchdowns,
                ppe.return_touchdowns,
                ppe.two_point_conversions,
                pt.total_touches,
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

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsAsync(int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByConfAsync(string conf, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@conf", conf);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByTeamAsync(string team, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@team", team);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByNameAsync(string name, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", name);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByName;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAsync(string pos, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByPos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", pos);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByPos.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByPos;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndConfAsync(string pos, string conf, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByPosAndConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@conf", conf);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByPosAndConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByPosAndConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndTeamAsync(string pos, string team, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByPosAndTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(); 
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@team", team);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByPosAndTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByPosAndTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyProjectedStatsByPosAndNameAsync(string pos, string name, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyProjectedStatsByPosAndName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(); 
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", pos);
                    command.Parameters.AddWithValue("@name", name);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyProjectedStatsByPosAndName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyProjectedStatsByPosAndName;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Flex
{
    public class FlexWeeklyTotalSqlDao : IFlexWeeklyTotalDao
    {
        private readonly string _connectionString;
        public FlexWeeklyTotalSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"WITH PlayerTotals AS (
                SELECT 
                    week, 
                    team_id, 
                    position, 
                    SUM(rushing_attempts + receiving_targets) as total_touches 
                FROM player_stats_ext 
                WHERE position IN ('RB', 'WR', 'TE') 
                    AND week = @week 
                GROUP BY week, team_id, position
            ) 
            SELECT 
                p.player_id, 
                pse.week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                pse.rushing_attempts, 
                pse.rushing_yards, 
                CASE 
                    WHEN pse.rushing_attempts = 0 THEN 0 
                    ELSE ROUND((pse.rushing_yards / pse.rushing_attempts), 2) 
                END AS rushing_yards_per_attempt, 
                pse.rushing_touchdowns, 
                pse.receiving_targets, 
                pse.receptions, 
                pse.receiving_yards, 
                CASE 
                    WHEN pse.receptions = 0 THEN 0 
                    ELSE ROUND((pse.receiving_yards / pse.receptions), 2) 
                END AS receiving_yards_per_reception, 
                pse.receiving_touchdowns, 
                pse.return_touchdowns, 
                pse.two_point_conversions, 
                CASE 
                    WHEN pt.total_touches = 0 THEN 0 
                    ELSE ROUND(((pse.rushing_attempts + pse.receiving_targets) * 100 / pt.total_touches), 2) 
                END AS usage, 
                pse.fumbles_lost, 
                pse.fantasy_points AS fantasy_points_total, 
                pse.fantasy_points AS fantasy_points_average, 
                t.conference, 
                t.status AS team_status 
            FROM player_stats_ext pse 
            JOIN players p ON p.player_id = pse.player_id 
            JOIN teams t ON t.team_id = pse.team_id 
            JOIN PlayerTotals pt ON pt.week = pse.week AND pt.team_id = pse.team_id AND pt.position = pse.position ";
            
        private const string FLEX_SQL = 
            @"WHERE p.position IN ('RB', 'WR', 'TE') ";

        private const string POSITION_SQL = 
            @"WHERE lower(p.position) ILIKE @pos ";

        private const string GROUP_BY_SQL = 
            @"GROUP BY 
                p.player_id, 
                pse.week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                pse.rushing_attempts, 
                pse.rushing_yards, 
                pse.rushing_touchdowns, 
                pse.receiving_targets, 
                pse.receptions, 
                pse.receiving_yards, 
                pse.receiving_touchdowns, 
                pse.return_touchdowns, 
                pse.two_point_conversions, 
                pt.total_touches, 
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

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsAsync(int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStats = new List<PlayerStatsExtDto>();
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
                            flexWeeklyTotalStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByConfAsync(string conf, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByTeamAsync(string team, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByNameAsync(string name, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStats.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAsync(string pos, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStatsByPos = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection (_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStatsByPos.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStatsByPos;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndConfAsync(string pos, string conf, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStatsByPosAndConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection (_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStatsByPosAndConf.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStatsByPosAndConf;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndTeamAsync(string pos, string team, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStatsByPosAndTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection (_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStatsByPosAndTeam.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStatsByPosAndTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getFlexWeeklyTotalStatsByPosAndNameAsync(string pos, string name, int week)
        {
            List<PlayerStatsExtDto> flexWeeklyTotalStatsByPosAndName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection (_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@pos", $"%{pos}%");
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flexWeeklyTotalStatsByPosAndName.Add(MapRowToFlexStat(reader));
                        }
                    }
                }
            }
            return flexWeeklyTotalStatsByPosAndName;
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
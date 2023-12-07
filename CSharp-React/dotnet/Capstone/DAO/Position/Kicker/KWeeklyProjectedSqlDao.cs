using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Kicker
{
    public class KWeeklyProjectedSqlDao : IKWeeklyProjectedDao
    {
        private readonly string _connectionString;
        public KWeeklyProjectedSqlDao(IConfiguration configuration)
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
                ppe.field_goals_made, 
                ppe.field_goals_attempted, 
                CASE 
                    WHEN ppe.field_goals_attempted = 0 THEN 0 
                    ELSE ROUND(ppe.field_goals_made / ppe.field_goals_attempted * 100, 2) 
                END AS field_goal_percentage, 
                ppe.field_goals_made_0_to_19, 
                ppe.field_goals_made_20_to_29, 
                ppe.field_goals_made_30_to_39, 
                ppe.field_goals_made_40_to_49, 
                ppe.field_goals_made_50_plus, 
                ppe.extra_points_made, 
                ppe.extra_points_attempted, 
                CASE 
                    WHEN ppe.extra_points_attempted = 0 THEN 0 
                    ELSE ROUND(ppe.extra_points_made / ppe.extra_points_attempted * 100, 2) 
                END AS extra_point_percentage, 
                ppe.fantasy_points AS fantasy_points_total, 
                ppe.fantasy_points AS fantasy_points_average, 
                t.conference, 
                t.status AS team_status 
            FROM player_projections_ext ppe 
            JOIN players p ON p.player_id = ppe.player_id 
            JOIN teams t ON t.team_id = ppe.team_id 
            WHERE p.position = 'K' 
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
                ppe.field_goals_made, 
                ppe.field_goals_attempted, 
                ppe.field_goals_made_0_to_19, 
                ppe.field_goals_made_20_to_29, 
                ppe.field_goals_made_30_to_39, 
                ppe.field_goals_made_40_to_49, 
                ppe.field_goals_made_50_plus, 
                ppe.extra_points_made, 
                ppe.extra_points_attempted, 
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

        public async Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsAsync(int week)
        {
            List<PlayerStatsExtDto> kWeeklyProjectedStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            kWeeklyProjectedStats.Add(MapRowToKStat(reader));
                        }
                    }
                }
            }
            return kWeeklyProjectedStats;
        }

        public async Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByConfAsync(string conf, int week)
        {
            List<PlayerStatsExtDto> kWeeklyProjectedStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            kWeeklyProjectedStatsByConf.Add(MapRowToKStat(reader));
                        }
                    }
                }
            }
            return kWeeklyProjectedStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByTeamAsync(string team, int week)
        {
            List<PlayerStatsExtDto> kWeeklyProjectedStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            kWeeklyProjectedStatsByTeam.Add(MapRowToKStat(reader));
                        }
                    }
                }
            }
            return kWeeklyProjectedStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getKWeeklyProjectedStatsByNameAsync(string name, int week)
        {
            List<PlayerStatsExtDto> kWeeklyProjectedStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            kWeeklyProjectedStatsByName.Add(MapRowToKStat(reader));
                        }
                    }
                }
            }
            return kWeeklyProjectedStatsByName;
        }

        private PlayerStatsExtDto MapRowToKStat(NpgsqlDataReader reader)
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
                FieldGoalsMade = Convert.ToDouble(reader["field_goals_made"]),
                FieldGoalsAttempted = Convert.ToDouble(reader["field_goals_attempted"]),
                FieldGoalPercentage = Convert.ToDouble(reader["field_goal_percentage"]),
                FieldGoalsMade0to19 = Convert.ToDouble(reader["field_goals_made_0_to_19"]),
                FieldGoalsMade20to29 = Convert.ToDouble(reader["field_goals_made_20_to_29"]),
                FieldGoalsMade30to39 = Convert.ToDouble(reader["field_goals_made_30_to_39"]),
                FieldGoalsMade40to49 = Convert.ToDouble(reader["field_goals_made_40_to_49"]),
                FieldGoalsMade50Plus = Convert.ToDouble(reader["field_goals_made_50_plus"]),
                ExtraPointsMade = Convert.ToDouble(reader["extra_points_made"]),
                ExtraPointsAttempted = Convert.ToDouble(reader["extra_points_attempted"]),
                ExtraPointPercentage = Convert.ToDouble(reader["extra_point_percentage"]),
                FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]),
                FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]),
                Conference = Convert.ToString(reader["conference"]),
                TeamStatus = Convert.ToString(reader["team_status"])
            };
        }
    }
}
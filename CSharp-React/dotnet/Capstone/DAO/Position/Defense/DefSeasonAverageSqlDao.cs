using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Defense
{
    public class DefSeasonAverageSqlDao : IDefSeasonAverageDao
    {
        private readonly string _connectionString;
        public DefSeasonAverageSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"SELECT 
                p.player_id, 
                COUNT(DISTINCT pse.week) AS week,
                p.position,
                t.team,
                p.name,
                p.status,
                p.injury_status,
                ROUND(AVG(pse.defensive_touchdowns), 2) AS defensive_touchdowns,
                ROUND(AVG(pse.special_teams_touchdowns), 2) AS special_teams_touchdowns,
                ROUND(AVG(pse.touchdowns_scored), 2) AS touchdowns_scored,
                ROUND(AVG(pse.fumbles_forced), 2) AS fumbles_forced,
                ROUND(AVG(pse.fumbles_recovered), 2) AS fumbles_recovered,
                ROUND(AVG(pse.interceptions), 2) AS interceptions,
                ROUND(AVG(pse.tackles_for_loss), 2) AS tackles_for_loss,
                ROUND(AVG(pse.quarterback_hits), 2) AS quarterback_hits,
                ROUND(AVG(pse.sacks), 2) AS sacks,
                ROUND(AVG(pse.safeties), 2) AS safeties,
                ROUND(AVG(pse.blocked_kicks), 2) AS blocked_kicks,
                ROUND(AVG(pse.points_allowed), 2) AS points_allowed,
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_total,
                ROUND(AVG(pse.fantasy_points), 2) AS fantasy_points_average,
                t.conference,
                t.status AS team_status
            FROM player_stats_ext pse
            JOIN players p ON p.player_id = pse.player_id
            JOIN teams t ON t.team_id = pse.team_id
            WHERE p.position = 'DEF' ";
        
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
                t.status 
            ORDER BY fantasy_points_average DESC";

        private const string CONF_SQL = 
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL = 
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL = 
            @"AND lower(p.name) ILIKE @name ";

        public async Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsAsync()
        {
            List<PlayerStatsExtDto> defSeasonAverageStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defSeasonAverageStats.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defSeasonAverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByConfAsync(string conf)
        {
            List<PlayerStatsExtDto> defSeasonAverageStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defSeasonAverageStatsByConf.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defSeasonAverageStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByTeamAsync(string team)
        {
            List<PlayerStatsExtDto> defSeasonAverageStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defSeasonAverageStatsByTeam.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defSeasonAverageStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getDefSeasonAverageStatsByNameAsync(string name)
        {
            List<PlayerStatsExtDto> defSeasonAverageStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defSeasonAverageStatsByName.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defSeasonAverageStatsByName;
        }

        private PlayerStatsExtDto MapRowToDefStat(NpgsqlDataReader reader)
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
                DefensiveTouchdowns = Convert.ToDouble(reader["defensive_touchdowns"]),
                SpecialTeamsTouchdowns = Convert.ToDouble(reader["special_teams_touchdowns"]),
                TouchdownsScored = Convert.ToDouble(reader["touchdowns_scored"]),
                FumblesForced = Convert.ToDouble(reader["fumbles_forced"]),
                FumblesRecovered = Convert.ToDouble(reader["fumbles_recovered"]),
                Interceptions = Convert.ToDouble(reader["interceptions"]),
                TacklesForLoss = Convert.ToDouble(reader["tackles_for_loss"]),
                QuarterbackHits = Convert.ToDouble(reader["quarterback_hits"]),
                Sacks = Convert.ToDouble(reader["sacks"]),
                Safeties = Convert.ToDouble(reader["safeties"]),
                BlockedKicks = Convert.ToDouble(reader["blocked_kicks"]),
                PointsAllowed = Convert.ToDouble(reader["points_allowed"]),
                FantasyPointsTotal = Convert.ToDouble(reader["fantasy_points_total"]),
                FantasyPointsAverage = Convert.ToDouble(reader["fantasy_points_average"]),
                Conference = Convert.ToString(reader["conference"]),
                TeamStatus = Convert.ToString(reader["team_status"])
            };
        }
    }
}
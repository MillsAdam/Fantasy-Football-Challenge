using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Defense
{
    public class DefLast4AverageSqlDao : IDefLast4AverageDao
    {
        private readonly string _connectionString;
        private readonly IConfigurationDao _configurationDao;
        public DefLast4AverageSqlDao(IConfiguration configuration, IConfigurationDao configurationDao)
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
            CROSS JOIN StartingWeek sw 
            WHERE p.position = 'DEF' 
                AND pse.week <= sw.week 
                AND pse.week >= sw.week - 3 ";
        
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

        public async Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsAsync()
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> defLast4AverageStats = new List<PlayerStatsExtDto>();
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
                            defLast4AverageStats.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defLast4AverageStats;
        }

        public async Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByConfAsync(string conf)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> defLast4AverageStatsByConf = new List<PlayerStatsExtDto>();
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
                            defLast4AverageStatsByConf.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defLast4AverageStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByTeamAsync(string team)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> defLast4AverageStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defLast4AverageStatsByTeam.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defLast4AverageStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getDefLast4AverageStatsByNameAsync(string name)
        {
            int week = await _configurationDao.GetConfigurationValue("currentWeek");
            List<PlayerStatsExtDto> defLast4AverageStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using(NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week - 1);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using(NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            defLast4AverageStatsByName.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defLast4AverageStatsByName;
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
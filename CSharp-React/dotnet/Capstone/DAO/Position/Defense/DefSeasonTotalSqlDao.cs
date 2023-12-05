using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Defense
{
    public class DefSeasonTotalSqlDao : IDefSeasonTotalDao
    {
        private readonly string _connectionString;
        public DefSeasonTotalSqlDao(IConfiguration configuration)
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
                SUM(pse.defensive_touchdowns) AS defensive_touchdowns,
                SUM(pse.special_teams_touchdowns) AS special_teams_touchdowns,
                SUM(pse.touchdowns_scored) AS touchdowns_scored,
                SUM(pse.fumbles_forced) AS fumbles_forced,
                SUM(pse.fumbles_recovered) AS fumbles_recovered,
                SUM(pse.interceptions) AS interceptions,
                SUM(pse.tackles_for_loss) AS tackles_for_loss,
                SUM(pse.quarterback_hits) AS quarterback_hits,
                SUM(pse.sacks) AS sacks,
                SUM(pse.safeties) AS safeties,
                SUM(pse.blocked_kicks) AS blocked_kicks,
                SUM(pse.points_allowed) AS points_allowed,
                SUM(pse.fantasy_points) AS fantasy_points_total,
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
            ORDER BY fantasy_points_total DESC";

        private const string CONF_SQL = 
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL =
            @"AND lower(p.name) ILIKE @name ";


        public async Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsAsync()
        {
            List<PlayerStatsExtDto> defSeasonTotalStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection))
                {
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        defSeasonTotalStats.Add(MapRowToDefStat(reader));
                    }
                }
            }
            return defSeasonTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsByConfAsync(string conf)
        {
            List<PlayerStatsExtDto> defSeasonTotalStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("conf", $"%{conf}%");
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        defSeasonTotalStatsByConf.Add(MapRowToDefStat(reader));
                    }
                }
            }
            return defSeasonTotalStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsByTeamAsync(string team)
        {
            List<PlayerStatsExtDto> defSeasonTotalStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("team", $"%{team}%");
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        defSeasonTotalStatsByTeam.Add(MapRowToDefStat(reader));
                    }
                }
            }
            return defSeasonTotalStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getDefSeasonTotalStatsByNameAsync(string name)
        {
            List<PlayerStatsExtDto> defSeasonTotalStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("name", $"%{name}%");
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        defSeasonTotalStatsByName.Add(MapRowToDefStat(reader));
                    }
                }
            }
            return defSeasonTotalStatsByName;
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
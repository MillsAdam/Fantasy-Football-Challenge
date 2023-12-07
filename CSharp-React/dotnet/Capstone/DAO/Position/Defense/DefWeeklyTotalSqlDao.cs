using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.Position.Defense
{
    public class DefWeeklyTotalSqlDao : IDefWeeklyTotalDao
    {
        private readonly string _connectionString;
        public DefWeeklyTotalSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        private const string SELECT_SQL = 
            @"SELECT 
                p.player_id, 
                pse.week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                pse.defensive_touchdowns, 
                pse.special_teams_touchdowns, 
                pse.touchdowns_scored, 
                pse.fumbles_forced, 
                pse.fumbles_recovered, 
                pse.interceptions, 
                pse.tackles_for_loss, 
                pse.quarterback_hits, 
                pse.sacks, 
                pse.safeties, 
                pse.blocked_kicks, 
                pse.points_allowed, 
                pse.fantasy_points AS fantasy_points_total, 
                pse.fantasy_points AS fantasy_points_average, 
                t.conference, 
                t.status AS team_status 
            FROM player_stats_ext pse 
            JOIN players p ON p.player_id = pse.player_id 
            JOIN teams t ON t.team_id = pse.team_id 
            WHERE p.position = 'DEF' 
                AND pse.week = @week ";
        
        private const string GROUP_BY_SQL = 
            @"GROUP BY 
                p.player_id, 
                pse.week, 
                p.position, 
                t.team, 
                p.name, 
                p.status, 
                p.injury_status, 
                pse.defensive_touchdowns, 
                pse.special_teams_touchdowns, 
                pse.touchdowns_scored, 
                pse.fumbles_forced, 
                pse.fumbles_recovered, 
                pse.interceptions, 
                pse.tackles_for_loss, 
                pse.quarterback_hits, 
                pse.sacks, 
                pse.safeties, 
                pse.blocked_kicks, 
                pse.points_allowed, 
                pse.fantasy_points, 
                t.conference, 
                t.status 
            ORDER BY pse.fantasy_points DESC ";

        private const string CONF_SQL =
            @"AND lower(t.conference) ILIKE @conf ";

        private const string TEAM_SQL =
            @"AND lower(t.team) ILIKE @team ";

        private const string NAME_SQL = 
            @"AND lower(p.name) ILIKE @name ";
        
        public async Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsAsync(int week)
        {
            List<PlayerStatsExtDto> defWeeklyTotalStats = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defWeeklyTotalStats.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defWeeklyTotalStats;
        }

        public async Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByConfAsync(string conf, int week)
        {
            List<PlayerStatsExtDto> defWeeklyTotalStatsByConf = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + CONF_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@conf", $"%{conf}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defWeeklyTotalStatsByConf.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defWeeklyTotalStatsByConf;
        }

        public async Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByTeamAsync(string team, int week)
        {
            List<PlayerStatsExtDto> defWeeklyTotalStatsByTeam = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + TEAM_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@team", $"%{team}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defWeeklyTotalStatsByTeam.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defWeeklyTotalStatsByTeam;
        }

        public async Task<List<PlayerStatsExtDto>> getDefWeeklyTotalStatsByNameAsync(string name, int week)
        {
            List<PlayerStatsExtDto> defWeeklyTotalStatsByName = new List<PlayerStatsExtDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (NpgsqlCommand command = new NpgsqlCommand(SELECT_SQL + NAME_SQL + GROUP_BY_SQL, connection))
                {
                    command.Parameters.AddWithValue("@week", week);
                    command.Parameters.AddWithValue("@name", $"%{name}%");
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            defWeeklyTotalStatsByName.Add(MapRowToDefStat(reader));
                        }
                    }
                }
            }
            return defWeeklyTotalStatsByName;
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
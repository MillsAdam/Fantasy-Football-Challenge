using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO
{
    public class RosterPlayerSqlDao : IRosterPlayerDao
    {
        private readonly string _connectionString;
        private readonly IFantasyRosterDao _fantasyRosterDao;

        public RosterPlayerSqlDao(IConfiguration configuration, IFantasyRosterDao fantasyRosterDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _fantasyRosterDao = fantasyRosterDao;
        }

        public async Task CreateRosterPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand("INSERT INTO roster_players (roster_id, player_id) VALUES (@roster_id, @player_id);", connection);
                {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                    command.Parameters.AddWithValue("@player_id", playerId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteRosterPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using var transaction = connection.BeginTransaction();
                try {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    using NpgsqlCommand command = new NpgsqlCommand(
                        @"DELETE FROM roster_players 
                        WHERE roster_id = @roster_id AND player_id = @player_id;", connection);
                    {
                        command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                        command.Parameters.AddWithValue("@player_id", playerId);
                        await command.ExecuteNonQueryAsync();
                    }
                    using NpgsqlCommand command2 = new NpgsqlCommand(
                        @"DELETE FROM lineup_players 
                        WHERE player_id = @player_id 
                            AND lineup_id IN (SELECT lineup_id FROM fantasy_lineups WHERE roster_id = @roster_id);", connection);
                    {
                        command2.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                        command2.Parameters.AddWithValue("@player_id", playerId);
                        await command2.ExecuteNonQueryAsync();
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new ApplicationException("An error occurred while updating the roster. Please try again.", e);
                }     
            }
        }

        public async Task<List<RosterPlayer>> GetRosterPlayers()
        {
            List<RosterPlayer> rosterPlayers = new List<RosterPlayer>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, player_id FROM roster_players;", connection);
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        RosterPlayer rosterPlayer = new RosterPlayer();
                        {
                            rosterPlayer.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                            rosterPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                        };
                        rosterPlayers.Add(rosterPlayer);
                    }
                }
            }
            return rosterPlayers;
        }

        public async Task<List<RosterPlayerDto>> GetRosterPlayerDtosByUser(User user)
        {
            if (user.FantasyLeagueId == null)
            {
                return new List<RosterPlayerDto>();
            }

            List<RosterPlayerDto> rosterPlayerDtos = new List<RosterPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        rp.roster_id, 
                        rp.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        COALESCE(ppi.fantasy_points, 0) as proj_fantasy_points, 
                        COALESCE(psi.fantasy_points, 0) as stat_fantasy_points, 
                        t.conference, 
                        t.status as team_status 
                    FROM roster_players rp 
                    JOIN players p ON rp.player_id = p.player_id 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    LEFT JOIN configuration c_week ON c_week.config_key = 'currentWeek' 
                    LEFT JOIN player_projections ppi ON p.player_id = ppi.player_id AND ppi.week = c_week.config_value  
                    LEFT JOIN player_stats psi ON p.player_id = psi.player_id AND psi.week = c_week.config_value  
                    WHERE roster_id = @roster_id 
                    GROUP BY 
                        rp.roster_id, 
                        rp.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        ppi.fantasy_points, 
                        psi.fantasy_points, 
                        t.conference, 
                        t.status 
                    ORDER BY 
                        CASE p.position 
                            WHEN 'QB' THEN 1 
                            WHEN 'RB' THEN 2 
                            WHEN 'WR' THEN 3 
                            WHEN 'TE' THEN 4 
                            WHEN 'K' THEN 5 
                            WHEN 'DEF' THEN 6 
                        ELSE 7 END ASC, 
                        ppi.fantasy_points DESC;", connection);
                {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        RosterPlayerDto rosterPlayerDto = new RosterPlayerDto();
                        {
                            rosterPlayerDto.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                            rosterPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            rosterPlayerDto.Team = Convert.ToString(reader["team"]);
                            rosterPlayerDto.Position = Convert.ToString(reader["position"]);
                            rosterPlayerDto.Name = Convert.ToString(reader["name"]);
                            rosterPlayerDto.Status = Convert.ToString(reader["status"]);
                            rosterPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                            rosterPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                            rosterPlayerDto.FantasyPointsProj = Convert.ToDouble(reader["proj_fantasy_points"]);
                            rosterPlayerDto.FantasyPoints = Convert.ToDouble(reader["stat_fantasy_points"]);
                            rosterPlayerDto.Conference = Convert.ToString(reader["conference"]);
                            rosterPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);

                            rosterPlayerDto.FantasyLineupId = null;
                            rosterPlayerDto.LineupPosition = null;
                        };
                        rosterPlayerDtos.Add(rosterPlayerDto);
                    }
                
                }
            }
            return rosterPlayerDtos;
        }

        public async Task<List<RosterPlayerDto>> GetRosterPlayerDtosByUserId(int userId)
        {
            List<RosterPlayerDto> rosterPlayerDtos = new List<RosterPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        fr.roster_id, 
                        p.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        COALESCE(SUM(
                            CASE 
                                WHEN fl.game_week = 1 
                                THEN (
                                    SELECT SUM(psi.fantasy_points) 
                                    FROM player_stats psi 
                                    WHERE psi.player_id = p.player_id 
                                        AND psi.week = (
                                            SELECT config_value 
                                            FROM configuration 
                                            WHERE config_key = 'lineupWeek1'
                                        )
                                ) 
                                WHEN fl.game_week = 2 
                                THEN (
                                    SELECT SUM(psi.fantasy_points) 
                                    FROM player_stats psi 
                                    WHERE psi.player_id = p.player_id 
                                        AND psi.week = (
                                            SELECT config_value 
                                            FROM configuration 
                                            WHERE config_key = 'lineupWeek2'
                                        )
                                ) 
                                WHEN fl.game_week = 3 
                                THEN (
                                    SELECT SUM(psi.fantasy_points) 
                                    FROM player_stats psi 
                                    WHERE psi.player_id = p.player_id 
                                        AND psi.week = (
                                            SELECT config_value 
                                            FROM configuration 
                                            WHERE config_key = 'lineupWeek3'
                                        )
                                ) 
                                WHEN fl.game_week = 4 
                                THEN (
                                    SELECT SUM(psi.fantasy_points) 
                                    FROM player_stats psi 
                                    WHERE psi.player_id = p.player_id 
                                        AND psi.week = (
                                            SELECT config_value 
                                            FROM configuration 
                                            WHERE config_key = 'lineupWeek4'
                                        )
                                ) 
                                ELSE 0 
                            END
                        ), 0) AS stat_fantasy_points, 
                        t.conference, 
                        t.status as team_status 
                    FROM players p 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN lineup_players lp ON p.player_id = lp.player_id 
                    LEFT JOIN fantasy_lineups fl ON lp.lineup_id = fl.lineup_id 
                    JOIN fantasy_rosters fr ON fl.roster_id = fr.roster_id 
                    WHERE fr.user_id = @user_id 
                    GROUP BY 
                        fr.roster_id, 
                        p.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        t.conference, 
                        t.status 
                    ORDER BY 
                        CASE p.position 
                            WHEN 'QB' THEN 1 
                            WHEN 'RB' THEN 2 
                            WHEN 'WR' THEN 3 
                            WHEN 'TE' THEN 4 
                            WHEN 'K' THEN 5 
                            WHEN 'DEF' THEN 6 
                        ELSE 7 END ASC, 
                        stat_fantasy_points DESC;", connection);
                {
                    command.Parameters.AddWithValue("@user_id", userId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        RosterPlayerDto rosterPlayerDto = new RosterPlayerDto();
                        {
                            rosterPlayerDto.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                            rosterPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            rosterPlayerDto.Team = Convert.ToString(reader["team"]);
                            rosterPlayerDto.Position = Convert.ToString(reader["position"]);
                            rosterPlayerDto.Name = Convert.ToString(reader["name"]);
                            rosterPlayerDto.Status = Convert.ToString(reader["status"]);
                            rosterPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                            rosterPlayerDto.FantasyPoints = reader["stat_fantasy_points"] is DBNull ? 0.0 : Convert.ToDouble(reader["stat_fantasy_points"]);
                            rosterPlayerDto.Conference = Convert.ToString(reader["conference"]);
                            rosterPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);

                            rosterPlayerDto.FantasyLineupId = null;
                            rosterPlayerDto.LineupPosition = null;
                            rosterPlayerDto.FantasyPointsAvg = 0.0;
                            rosterPlayerDto.FantasyPointsProj = 0.0;
                        };
                        rosterPlayerDtos.Add(rosterPlayerDto);
                    }
                }
                return rosterPlayerDtos;
            }
        }

        public async Task<List<RosterPlayer>> GetRosterPlayersByUser(User user)
        {
            List<RosterPlayer> rosterPlayers = new List<RosterPlayer>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, player_id FROM roster_players WHERE roster_id = @roster_id;", connection);
                {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        RosterPlayer rosterPlayer = new RosterPlayer();
                        {
                            rosterPlayer.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                            rosterPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                        };
                        rosterPlayers.Add(rosterPlayer);
                    }
                }
            }
            return rosterPlayers;
        }

        public async Task UpdateRosterPlayer(User user, int oldPlayerId, int newPlayerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"UPDATE roster_players SET player_id = @new_player_id WHERE roster_id = @roster_id AND player_id = @old_player_id;", connection);
                {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                    command.Parameters.AddWithValue("@old_player_id", oldPlayerId);
                    command.Parameters.AddWithValue("@new_player_id", newPlayerId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
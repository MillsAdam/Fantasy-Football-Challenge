using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO
{
    public class LineupPlayerSqlDao : ILineupPlayerDao
    {
        private readonly string _connectionString;
        private readonly IFantasyLineupDao _fantasyLineupDao;
        private readonly IPlayerDao _playerDao;

        public LineupPlayerSqlDao(IConfiguration configuration, IFantasyLineupDao fantasyLineupDao, IPlayerDao playerDao)
        {
            _connectionString = configuration.GetConnectionString("Project");
            _fantasyLineupDao = fantasyLineupDao;
            _playerDao = playerDao;
        }

        public async Task CreateLineupPlayer(User user, int playerId, string lineupPosition)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO lineup_players (lineup_id, player_id, lineup_position) VALUES (@lineup_id, @player_id, @lineup_position);", connection);
                {
                    FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user);
                    string position = await _playerDao.GetPlayerPositionByPlayerIdAsync(playerId);
                    command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                    command.Parameters.AddWithValue("@player_id", playerId);
                    command.Parameters.AddWithValue("@lineup_position", lineupPosition);
                    await command.ExecuteNonQueryAsync();
                
                }
            }
        }

        public async Task DeleteLineupPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand("DELETE FROM lineup_players WHERE lineup_id = @lineup_id AND player_id = @player_id;", connection);
                {
                    FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user);
                    command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                    command.Parameters.AddWithValue("@player_id", playerId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<LineupPlayer>> GetLineupPlayers()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand("SELECT lineup_id, player_id, lineup_position FROM lineup_players;", connection);
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    List<LineupPlayer> lineupPlayers = new List<LineupPlayer>();
                    while (await reader.ReadAsync())
                    {
                        LineupPlayer lineupPlayer = new LineupPlayer();
                        {
                            lineupPlayer.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                            lineupPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                            lineupPlayer.LineupPosition = Convert.ToString(reader["lineup_position"]);
                        };
                        lineupPlayers.Add(lineupPlayer);
                    }
                    return lineupPlayers;
                
                }
            }
        }

        public async Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUser(User user)
        {
            if (user.FantasyLeagueId == null)
            {
                return new List<LineupPlayerDto>();
            }

            List<LineupPlayerDto> lineupPlayerDtos = new List<LineupPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        lp.lineup_id, 
                        lp.player_id, 
                        lp.lineup_position, 
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
                    FROM lineup_players lp 
                    JOIN players p ON lp.player_id = p.player_id 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    LEFT JOIN configuration c_week ON c_week.config_key = 'currentWeek' 
                    LEFT JOIN player_projections ppi ON p.player_id = ppi.player_id AND ppi.week = c_week.config_value  
                    LEFT JOIN player_stats psi ON p.player_id = psi.player_id AND psi.week = c_week.config_value  
                    WHERE lineup_id = @lineup_id 
                    GROUP BY 
                        lp.lineup_id, 
                        lp.player_id, 
                        lp.lineup_position, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        ppi.fantasy_points, 
                        psi.fantasy_points, 
                        t.conference, 
                        t.status 
                    ORDER BY CASE lp.lineup_position 
                        WHEN 'QB1' THEN 1 
                        WHEN 'QB2' THEN 2 
                        WHEN 'RB1' THEN 3 
                        WHEN 'RB2' THEN 4 
                        WHEN 'WR1' THEN 5 
                        WHEN 'WR2' THEN 6 
                        WHEN 'WR3' THEN 7 
                        WHEN 'TE' THEN 8 
                        WHEN 'FLEX' THEN 9 
                        WHEN 'K' THEN 10 
                        WHEN 'DEF' THEN 11 
                    ELSE 12 END ASC;", connection);
                {
                    FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user);
                    command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        LineupPlayerDto lineupPlayerDto = new LineupPlayerDto();
                        {
                            lineupPlayerDto.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                            lineupPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            lineupPlayerDto.LineupPosition = Convert.ToString(reader["lineup_position"]);
                            lineupPlayerDto.Position = Convert.ToString(reader["position"]);
                            lineupPlayerDto.Team = Convert.ToString(reader["team"]);
                            lineupPlayerDto.Name = Convert.ToString(reader["name"]);
                            lineupPlayerDto.Status = Convert.ToString(reader["status"]);
                            lineupPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                            lineupPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                            lineupPlayerDto.FantasyPointsProj = Convert.ToDouble(reader["proj_fantasy_points"]);
                            lineupPlayerDto.FantasyPoints = Convert.ToDouble(reader["stat_fantasy_points"]);
                            lineupPlayerDto.Conference = Convert.ToString(reader["conference"]);
                            lineupPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);

                            lineupPlayerDto.FantasyRosterId = null;
                        };
                        lineupPlayerDtos.Add(lineupPlayerDto);
                    }
                    return lineupPlayerDtos;
                
                }
            }
        }

        public async Task UpdateLineupPlayer(User user, int oldPlayerId, int newPlayerId, string oldLineupPosition, string newLineupPosition)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"UPDATE lineup_players 
                    SET player_id = @new_player_id, lineup_position = @new_lineup_position 
                    WHERE lineup_id = @lineup_id AND player_id = @old_player_id and lineup_position = @old_lineup_position;", connection);
                {
                    FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user);
                    string position = await _playerDao.GetPlayerPositionByPlayerIdAsync(newPlayerId);
                    command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                    command.Parameters.AddWithValue("@old_player_id", oldPlayerId);
                    command.Parameters.AddWithValue("@new_player_id", newPlayerId);
                    command.Parameters.AddWithValue("@old_lineup_position", oldLineupPosition);
                    command.Parameters.AddWithValue("@new_lineup_position", newLineupPosition);
                    await command.ExecuteNonQueryAsync();
                }
                
            }
        }

        public async Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUserAndWeek(User user, int gameWeek)
        {
            List<LineupPlayerDto> lineupPlayerDtos = new List<LineupPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        lp.lineup_id, 
                        lp.player_id, 
                        lp.lineup_position, 
                        p.position, 
                        t.team, 
                        p.name, 
                        psi.status, 
                        psi.injury_status, 
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        COALESCE(ppi.fantasy_points, 0) as proj_fantasy_points, 
                        COALESCE(psi.fantasy_points, 0) as stat_fantasy_points, 
                        t.conference, 
                        t.status as team_status 
                    FROM lineup_players lp 
                    JOIN players p ON lp.player_id = p.player_id 
                    JOIN teams t ON p.team_id = t.team_id 
                    JOIN fantasy_lineups fl ON lp.lineup_id = fl.lineup_id 
                    JOIN fantasy_rosters fr ON fl.roster_id = fr.roster_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    LEFT JOIN configuration c_week ON c_week.config_key = 
                        CASE 
                            WHEN @game_week = 1 THEN 'lineupWeek1' 
                            WHEN @game_week = 2 THEN 'lineupWeek2' 
                            WHEN @game_week = 3 THEN 'lineupWeek3' 
                            WHEN @game_week = 4 THEN 'lineupWeek4'
                        END 
                    LEFT JOIN player_projections ppi ON p.player_id = ppi.player_id AND ppi.week = c_week.config_value  
                    LEFT JOIN player_stats psi ON p.player_id = psi.player_id AND psi.week = c_week.config_value  
                    WHERE fr.user_id = @user_id  
                        AND fl.game_week = @game_week
                    GROUP BY 
                        lp.lineup_id, 
                        lp.player_id, 
                        lp.lineup_position, 
                        p.position, 
                        t.team, 
                        p.name, 
                        psi.status, 
                        psi.injury_status, 
                        ppi.fantasy_points, 
                        psi.fantasy_points, 
                        t.conference, 
                        t.status 
                    ORDER BY CASE lp.lineup_position 
                        WHEN 'QB1' THEN 1 
                        WHEN 'QB2' THEN 2 
                        WHEN 'RB1' THEN 3 
                        WHEN 'RB2' THEN 4 
                        WHEN 'WR1' THEN 5 
                        WHEN 'WR2' THEN 6 
                        WHEN 'WR3' THEN 7 
                        WHEN 'TE' THEN 8 
                        WHEN 'FLEX' THEN 9 
                        WHEN 'K' THEN 10 
                        WHEN 'DEF' THEN 11 
                    ELSE 12 END ASC;", connection);
                {
                    command.Parameters.AddWithValue("@user_id", user.UserId);
                    command.Parameters.AddWithValue("@game_week", gameWeek);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        LineupPlayerDto lineupPlayerDto = new LineupPlayerDto();
                        {
                            lineupPlayerDto.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                            lineupPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            lineupPlayerDto.LineupPosition = Convert.ToString(reader["lineup_position"]);
                            lineupPlayerDto.Position = Convert.ToString(reader["position"]);
                            lineupPlayerDto.Team = Convert.ToString(reader["team"]);
                            lineupPlayerDto.Name = Convert.ToString(reader["name"]);
                            lineupPlayerDto.Status = Convert.ToString(reader["status"]);
                            lineupPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                            lineupPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                            lineupPlayerDto.FantasyPointsProj = Convert.ToDouble(reader["proj_fantasy_points"]);
                            lineupPlayerDto.FantasyPoints = Convert.ToDouble(reader["stat_fantasy_points"]);
                            lineupPlayerDto.Conference = Convert.ToString(reader["conference"]);
                            lineupPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);

                            lineupPlayerDto.FantasyRosterId = null;
                        };
                        lineupPlayerDtos.Add(lineupPlayerDto);
                    }
                    return lineupPlayerDtos;
                
                }
            }
        }

        public async Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUserIdAndWeek(int userId, int gameWeek)
        {
            List<LineupPlayerDto> lineupPlayerDtos = new List<LineupPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        lp.lineup_id, 
                        lp.player_id, 
                        lp.lineup_position, 
                        p.position, 
                        t.team, 
                        p.name, 
                        psi.status, 
                        psi.injury_status, 
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        COALESCE(ppi.fantasy_points, 0) as proj_fantasy_points, 
                        COALESCE(psi.fantasy_points, 0) as stat_fantasy_points, 
                        t.conference, 
                        t.status as team_status 
                    FROM lineup_players lp 
                    JOIN players p ON lp.player_id = p.player_id 
                    JOIN teams t ON p.team_id = t.team_id 
                    JOIN fantasy_lineups fl ON lp.lineup_id = fl.lineup_id 
                    JOIN fantasy_rosters fr ON fl.roster_id = fr.roster_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    LEFT JOIN configuration c_week ON c_week.config_key = 
                        CASE 
                            WHEN @game_week = 1 THEN 'lineupWeek1' 
                            WHEN @game_week = 2 THEN 'lineupWeek2' 
                            WHEN @game_week = 3 THEN 'lineupWeek3' 
                            WHEN @game_week = 4 THEN 'lineupWeek4'
                        END 
                    LEFT JOIN player_projections ppi ON p.player_id = ppi.player_id AND ppi.week = c_week.config_value  
                    LEFT JOIN player_stats psi ON p.player_id = psi.player_id AND psi.week = c_week.config_value  
                    WHERE fr.user_id = @user_id  
                        AND fl.game_week = @game_week
                    GROUP BY 
                        lp.lineup_id, 
                        lp.player_id, 
                        lp.lineup_position, 
                        p.position, 
                        t.team, 
                        p.name, 
                        psi.status, 
                        psi.injury_status, 
                        ppi.fantasy_points, 
                        psi.fantasy_points, 
                        t.conference, 
                        t.status 
                    ORDER BY CASE
                        WHEN lp.lineup_position = 'QB1' THEN 1 
                        WHEN lp.lineup_position = 'QB2' THEN 2 
                        WHEN lp.lineup_position = 'RB1' THEN 3 
                        WHEN lp.lineup_position = 'RB2' THEN 4 
                        WHEN lp.lineup_position = 'WR1' THEN 5 
                        WHEN lp.lineup_position = 'WR2' THEN 6 
                        WHEN lp.lineup_position = 'WR3' THEN 7 
                        WHEN lp.lineup_position = 'TE' THEN 8 
                        WHEN lp.lineup_position = 'FLEX' THEN 9 
                        WHEN lp.lineup_position = 'K' THEN 10 
                        WHEN lp.lineup_position = 'DEF' THEN 11 
                    ELSE 12 END ASC;", connection);
                {
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@game_week", gameWeek);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        LineupPlayerDto lineupPlayerDto = new LineupPlayerDto();
                        {
                            lineupPlayerDto.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                            lineupPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                            lineupPlayerDto.LineupPosition = Convert.ToString(reader["lineup_position"]);
                            lineupPlayerDto.Position = Convert.ToString(reader["position"]);
                            lineupPlayerDto.Team = Convert.ToString(reader["team"]);
                            lineupPlayerDto.Name = Convert.ToString(reader["name"]);
                            lineupPlayerDto.Status = Convert.ToString(reader["status"]);
                            lineupPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                            lineupPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                            lineupPlayerDto.FantasyPointsProj = Convert.ToDouble(reader["proj_fantasy_points"]);
                            lineupPlayerDto.FantasyPoints = Convert.ToDouble(reader["stat_fantasy_points"]);
                            lineupPlayerDto.Conference = Convert.ToString(reader["conference"]);
                            lineupPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);

                            lineupPlayerDto.FantasyRosterId = null;
                        };
                        lineupPlayerDtos.Add(lineupPlayerDto);
                    }
                    return lineupPlayerDtos;
                
                }
            }
        }
    }
}
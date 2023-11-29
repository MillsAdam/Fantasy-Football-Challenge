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

                List<RosterPlayer> rosterPlayers = await GetRosterPlayersByUser(user);
                if (rosterPlayers.Count > 27)
                {
                    throw new InvalidOperationException("Roster already has the maximum number of players.");
                }
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
                using NpgsqlCommand command = new NpgsqlCommand("DELETE FROM roster_players WHERE roster_id = @roster_id AND player_id = @player_id;", connection);
                {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                    command.Parameters.AddWithValue("@player_id", playerId);
                    await command.ExecuteNonQueryAsync();
                
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
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        COALESCE(ppi.fantasy_points, 0) as proj_fantasy_points, 
                        COALESCE(psi.fantasy_points, 0) as stat_fantasy_points 
                    FROM roster_players rp 
                    JOIN players p ON rp.player_id = p.player_id 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    LEFT JOIN configuration c_week ON c_week.config_key = 'current_week' 
                    LEFT JOIN configuration c_season_type ON c_season_type.config_key = 'current_season_type' 
                    LEFT JOIN player_projections ppi ON p.player_id = ppi.player_id AND ppi.week = c_week.config_value AND ppi.season_type = c_season_type.config_value 
                    LEFT JOIN player_stats psi ON p.player_id = psi.player_id AND psi.week = c_week.config_value AND psi.season_type = c_season_type.config_value 
                    WHERE roster_id = @roster_id 
                    GROUP BY rp.roster_id, rp.player_id, p.position, t.team, p.name, ppi.fantasy_points, psi.fantasy_points 
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
                            rosterPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                            rosterPlayerDto.FantasyPointsProj = Convert.ToDouble(reader["proj_fantasy_points"]);
                            rosterPlayerDto.FantasyPoints = Convert.ToDouble(reader["stat_fantasy_points"]);
                        };
                        rosterPlayerDtos.Add(rosterPlayerDto);
                    }
                
                }
            }
            return rosterPlayerDtos;
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
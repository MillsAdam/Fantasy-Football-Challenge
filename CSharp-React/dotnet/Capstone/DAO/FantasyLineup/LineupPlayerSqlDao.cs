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
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO lineup_players (lineup_id, player_id, lineup_position) VALUES (@lineup_id, @player_id, @lineup_position);", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.Parameters.AddWithValue("@lineup_position", lineupPosition);
                command.ExecuteNonQuery();
            }
        }

        public async Task DeleteLineupPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM lineup_players WHERE lineup_id = @lineup_id AND player_id = @player_id;", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<LineupPlayer>> GetLineupPlayers()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT lineup_id, player_id, lineup_position FROM lineup_players;", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                List<LineupPlayer> lineupPlayers = new List<LineupPlayer>();
                while (reader.Read())
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

        public async Task<List<LineupPlayerDto>> GetLineupPlayerDtosByUser(User user)
        {
            List<LineupPlayerDto> lineupPlayerDtos = new List<LineupPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT lp.lineup_id, lp.player_id, lp.lineup_position, p.position, t.team, p.name, pp.fantasy_points " + 
                    "FROM lineup_players lp " +
                    "JOIN players p ON lp.player_id = p.player_id " +
                    "JOIN teams t ON p.team_id = t.team_id " +
                    "JOIN player_projections pp ON p.player_id = pp.player_id " +
                    "WHERE lineup_id = @lineup_id " + 
                    "ORDER BY CASE lp.lineup_position " + 
                        "WHEN 'QB1' THEN 1 " + 
                        "WHEN 'QB2' THEN 2 " +
                        "WHEN 'RB1' THEN 3 " +
                        "WHEN 'RB2' THEN 4 " +
                        "WHEN 'WR1' THEN 5 " + 
                        "WHEN 'WR2' THEN 6 " +
                        "WHEN 'WR3' THEN 7 " +
                        "WHEN 'TE' THEN 8 " + 
                        "WHEN 'FLEX' THEN 9 " +
                        "WHEN 'K' THEN 10 " + 
                        "WHEN 'DEF' THEN 11 " + 
                    "ELSE 12 END ASC;", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LineupPlayerDto lineupPlayerDto = new LineupPlayerDto();
                    {
                        lineupPlayerDto.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                        lineupPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                        lineupPlayerDto.Team = Convert.ToString(reader["team"]);
                        lineupPlayerDto.Position = Convert.ToString(reader["position"]);
                        lineupPlayerDto.Name = Convert.ToString(reader["name"]);
                        lineupPlayerDto.FantasyPoints = Convert.ToDouble(reader["fantasy_points"]);
                        lineupPlayerDto.LineupPosition = Convert.ToString(reader["lineup_position"]);
                    };
                    lineupPlayerDtos.Add(lineupPlayerDto);
                }
                return lineupPlayerDtos;
            }
        }

        public async Task UpdateLineupPlayer(User user, int oldPlayerId, int newPlayerId, string oldLineupPosition, string newLineupPosition)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("UPDATE lineup_players SET player_id = @new_player_id, lineup_position = @new_lineup_position WHERE lineup_id = @lineup_id AND player_id = @old_player_id and lineup_position = @old_lineup_position;", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                string position = await _playerDao.GetPlayerPositionByPlayerIdAsync(newPlayerId);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                command.Parameters.AddWithValue("@old_player_id", oldPlayerId);
                command.Parameters.AddWithValue("@new_player_id", newPlayerId);
                command.Parameters.AddWithValue("@old_lineup_position", oldLineupPosition);
                command.Parameters.AddWithValue("@new_lineup_position", newLineupPosition);
                command.ExecuteNonQuery();
            }
        }
    }
}
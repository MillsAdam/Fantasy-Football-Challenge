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

        public async Task CreateLineupPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO lineup_players (lineup_id, player_id, position) VALUES (@lineup_id, @player_id, @position);", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                string position = await _playerDao.GetPlayerPositionByPlayerIdAsync(playerId);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.Parameters.AddWithValue("@position", position);
                command.ExecuteNonQuery();
            }
        }

        public async Task DeleteLineupPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM lineup_players WHERE lineup_id = @lineup_id AND player_id = @player_id;", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.ExecuteNonQuery();
            }
        }

        public Task<List<LineupPlayer>> GetLineupPlayers()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT lineup_id, player_id, position FROM lineup_players;", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                List<LineupPlayer> lineupPlayers = new List<LineupPlayer>();
                while (reader.Read())
                {
                    LineupPlayer lineupPlayer = new LineupPlayer();
                    {
                        lineupPlayer.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                        lineupPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                        lineupPlayer.Position = Convert.ToString(reader["position"]);
                    };
                    lineupPlayers.Add(lineupPlayer);
                }
                return Task.FromResult(lineupPlayers);
            }
        }

        public Task<List<LineupPlayer>> GetLineupPlayersByUser(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT lineup_id, player_id, position FROM lineup_players WHERE lineup_id = @lineup_id;", connection);
                FantasyLineup fantasyLineup = _fantasyLineupDao.GetFantasyLineupByUser(user, 1).Result;
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                NpgsqlDataReader reader = command.ExecuteReader();
                List<LineupPlayer> lineupPlayers = new List<LineupPlayer>();
                while (reader.Read())
                {
                    LineupPlayer lineupPlayer = new LineupPlayer();
                    {
                        lineupPlayer.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                        lineupPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                        lineupPlayer.Position = Convert.ToString(reader["position"]);
                    };
                    lineupPlayers.Add(lineupPlayer);
                }
                return Task.FromResult(lineupPlayers);
            }
        }

        public async Task UpdateLineupPlayer(User user, int oldPlayerId, int newPlayerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("UPDATE lineup_players SET player_id = @new_player_id, position = @position WHERE lineup_id = @lineup_id AND player_id = @old_player_id;", connection);
                FantasyLineup fantasyLineup = await _fantasyLineupDao.GetFantasyLineupByUser(user, 1);
                string position = await _playerDao.GetPlayerPositionByPlayerIdAsync(newPlayerId);
                command.Parameters.AddWithValue("@lineup_id", fantasyLineup.FantasyLineupId);
                command.Parameters.AddWithValue("@old_player_id", oldPlayerId);
                command.Parameters.AddWithValue("@new_player_id", newPlayerId);
                command.Parameters.AddWithValue("@position", position);
                command.ExecuteNonQuery();
            }
        }
    }
}
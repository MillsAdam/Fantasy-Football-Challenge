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
                if (rosterPlayers.Count >= 27)
                {
                    throw new InvalidOperationException("Roster already has the maximum number of players.");
                }
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO roster_players (roster_id, player_id) VALUES (@roster_id, @player_id);", connection);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.ExecuteNonQuery();
            }
        }

        public async Task DeleteRosterPlayer(User user, int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM roster_players WHERE roster_id = @roster_id AND player_id = @player_id;", connection);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.ExecuteNonQuery();
            }
        }

        public async Task<List<RosterPlayer>> GetRosterPlayers()
        {
            List<RosterPlayer> rosterPlayers = new List<RosterPlayer>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, player_id FROM roster_players;", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RosterPlayer rosterPlayer = new RosterPlayer();
                    {
                        rosterPlayer.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        rosterPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                    };
                    rosterPlayers.Add(rosterPlayer);
                }
            }
            return rosterPlayers;
        }

        // REORDER RETURN TO GO QB, RB, WR, TE, K, DEF
        public async Task<List<RosterPlayerDto>> GetRosterPlayerDtosByUser(User user)
        {
            List<RosterPlayerDto> rosterPlayerDtos = new List<RosterPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT rp.roster_id, rp.player_id, p.position, t.team, p.first_name, p.last_name FROM roster_players rp JOIN players p ON rp.player_id = p.player_id JOIN teams t ON p.team_id = t.team_id WHERE roster_id = @roster_id;", connection);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RosterPlayerDto rosterPlayerDto = new RosterPlayerDto();
                    {
                        rosterPlayerDto.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        rosterPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                        rosterPlayerDto.Team = Convert.ToString(reader["team"]);
                        rosterPlayerDto.Position = Convert.ToString(reader["position"]);
                        rosterPlayerDto.FirstName = Convert.ToString(reader["first_name"]);
                        rosterPlayerDto.LastName = Convert.ToString(reader["last_name"]);
                    };
                    rosterPlayerDtos.Add(rosterPlayerDto);
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT roster_id, player_id FROM roster_players WHERE roster_id = @roster_id;", connection);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RosterPlayer rosterPlayer = new RosterPlayer();
                    {
                        rosterPlayer.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        rosterPlayer.PlayerId = Convert.ToInt32(reader["player_id"]);
                    };
                    rosterPlayers.Add(rosterPlayer);
                }
            }
            return rosterPlayers;
        }

        public async Task UpdateRosterPlayer(User user, int oldPlayerId, int newPlayerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("UPDATE roster_players SET player_id = @new_player_id WHERE roster_id = @roster_id AND player_id = @old_player_id;", connection);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                command.Parameters.AddWithValue("@old_player_id", oldPlayerId);
                command.Parameters.AddWithValue("@new_player_id", newPlayerId);
                command.ExecuteNonQuery();
            }
        }
    }
}
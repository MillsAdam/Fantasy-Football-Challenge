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
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO roster_players (roster_id, player_id) VALUES (@roster_id, @player_id);", connection);
                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                command.Parameters.AddWithValue("@player_id", playerId);
                command.ExecuteNonQuery();
            }
        }

        // public async Task DeleteRosterPlayer(User user, int playerId)
        // {
        //     throw new NotImplementedException();
        // }

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

        // public async Task UpdateRosterPlayer(User user, int playerId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
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
    public class PlayerSqlDao : IPlayerDao
    {
        private readonly string _connectionString;

        public PlayerSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task AddPlayerAsync(PlayerDto playerDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO players (player_id, team_id, name, position, status, injury_status) " + 
                    "VALUES (@player_id, @team_id, @name, @position, @status, @injury_status);", connection);
                command.Parameters.AddWithValue("@player_id", playerDto.PlayerId);
                if (playerDto.TeamId != null)
                {
                    command.Parameters.AddWithValue("@team_id", playerDto.TeamId);
                }
                else
                {
                    command.Parameters.AddWithValue("@team_id", DBNull.Value);
                }
                command.Parameters.AddWithValue("@name", playerDto.Name);
                command.Parameters.AddWithValue("@position", playerDto.Position);
                command.Parameters.AddWithValue("@status", playerDto.Status);
                if (playerDto.InjuryStatus != null)
                {
                    command.Parameters.AddWithValue("@injury_status", playerDto.InjuryStatus);
                }
                else
                {
                    command.Parameters.AddWithValue("@injury_status", DBNull.Value);
                }
                command.ExecuteNonQuery();
            }
        }

        public async Task UpdatePlayerAsync(PlayerDto playerDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    UPDATE players
                    SET team_id = @team_id,
                        name = @name,
                        position = @position,
                        status = @status,
                        injury_status = @injury_status
                    WHERE player_id = @player_id;";

                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerDto.TeamId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@name", playerDto.Name);
                    command.Parameters.AddWithValue("@position", playerDto.Position);
                    command.Parameters.AddWithValue("@status", playerDto.Status ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@injury_status", playerDto.InjuryStatus ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<string> GetPlayerPositionByPlayerIdAsync(int playerId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                NpgsqlCommand command = new NpgsqlCommand("SELECT position FROM players WHERE player_id = @player_id;", connection);
                command.Parameters.AddWithValue("@player_id", playerId);
                NpgsqlDataReader reader = command.ExecuteReader();
                string position = "";
                while (reader.Read())
                {
                    position = Convert.ToString(reader["position"]);
                }
                return position;
            }
        }

        public async Task<List<SearchPlayerDto>> GetPlayerIdByNameAsync(string playerName)
        {
            List<SearchPlayerDto> searchPlayerDtos = new List<SearchPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT p.player_id, t.team, p.name, p.position, p.status, p.injury_status " +
                    "FROM players p " +
                    "JOIN teams t ON p.team_id = t.team_id " + 
                    "WHERE p.name ILIKE @player_name_pattern " +
                    "AND p.position IN ('QB', 'RB', 'WR', 'TE', 'K', 'DEF') " + 
                    "AND p.team_id IS NOT NULL;", connection);
                {
                    string playerNamePattern = "%" + playerName + "%";
                    command.Parameters.AddWithValue("@player_name_pattern", playerNamePattern);

                    using NpgsqlDataReader reader = command.ExecuteReader();
                    {
                        
                        while (await reader.ReadAsync())
                        {
                            SearchPlayerDto searchPlayerDto = new SearchPlayerDto();
                            {
                                searchPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                                searchPlayerDto.Team = Convert.ToString(reader["team"]);
                                searchPlayerDto.Name = Convert.ToString(reader["name"]);
                                searchPlayerDto.Position = Convert.ToString(reader["position"]);
                                searchPlayerDto.Status = Convert.ToString(reader["status"]);
                                if (reader["injury_status"] != DBNull.Value)
                                {
                                    searchPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"]);
                                }
                                else
                                {
                                    searchPlayerDto.InjuryStatus = null;
                                }
                            };
                            searchPlayerDtos.Add(searchPlayerDto);
                        }                    
                    }
                }
            }
            return searchPlayerDtos;
        }

        
    }
}
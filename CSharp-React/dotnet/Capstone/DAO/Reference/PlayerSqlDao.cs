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
                using NpgsqlCommand command = new NpgsqlCommand(@"
                    INSERT INTO players (
                        player_id, team_id, name, position, status, injury_status) 
                    VALUES (
                        @player_id, @team_id, @name, @position, @status, @injury_status);", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerDto.TeamId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@name", playerDto.Name);
                    command.Parameters.AddWithValue("@position", playerDto.Position);
                    command.Parameters.AddWithValue("@status", playerDto.Status);
                    command.Parameters.AddWithValue("@injury_status", playerDto.InjuryStatus ?? (object)DBNull.Value);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpsertPlayerAsync(PlayerDto playerDto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    INSERT INTO players (
                        player_id, team_id, name, position, status, injury_status) 
                    VALUES (
                        @player_id, @team_id, @name, @position, @status, @injury_status)
                    ON CONFLICT (player_id) DO UPDATE
                    SET team_id = @team_id,
                        name = @name,
                        position = @position,
                        status = @status,
                        injury_status = @injury_status;";

                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerDto.PlayerId);
                    command.Parameters.AddWithValue("@team_id", playerDto.TeamId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@name", playerDto.Name);
                    command.Parameters.AddWithValue("@position", playerDto.Position);
                    command.Parameters.AddWithValue("@status", playerDto.Status);
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
                using NpgsqlCommand command = new NpgsqlCommand("SELECT position FROM players WHERE player_id = @player_id;", connection);
                {
                    command.Parameters.AddWithValue("@player_id", playerId);
                    using NpgsqlDataReader reader = command.ExecuteReader();
                    {
                        string position = "";
                        while (await reader.ReadAsync())
                        {
                            position = Convert.ToString(reader["position"]);
                        }
                        return position;
                    }
                }
            }
        }

        public async Task<List<SearchPlayerDto>> GetPlayerIdByNameAsync(string playerName)
        {
            List<SearchPlayerDto> searchPlayerDtos = new List<SearchPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        p.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        t.conference, 
                        t.status as team_status
                    FROM players p 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    WHERE p.name ILIKE @player_name_pattern 
                        AND p.position IN ('QB', 'RB', 'WR', 'TE', 'K', 'DEF') 
                        AND p.team_id IS NOT NULL 
                    GROUP BY p.player_id, p.position, t.team, p.name, p.status, p.injury_status, t.conference, t.status 
                    ORDER BY avg_fantasy_points DESC;", connection);
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
                                searchPlayerDto.Position = Convert.ToString(reader["position"]);
                                searchPlayerDto.Team = Convert.ToString(reader["team"]);
                                searchPlayerDto.Name = Convert.ToString(reader["name"]);
                                searchPlayerDto.Status = Convert.ToString(reader["status"]);
                                searchPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                                searchPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                                searchPlayerDto.Conference = Convert.ToString(reader["conference"]);
                                searchPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);

                                searchPlayerDto.FantasyRosterId = null;
                                searchPlayerDto.FantasyLineupId = null;
                                searchPlayerDto.FantasyPointsProj = null;
                                searchPlayerDto.FantasyPoints = null;
                                searchPlayerDto.LineupPosition = null;
                            };
                            searchPlayerDtos.Add(searchPlayerDto);
                        }                    
                    }
                }
            }
            return searchPlayerDtos;
        }

        public async Task<List<SearchPlayerDto>> GetPlayerIdByTeamAsync(string teamName)
        {
            List<SearchPlayerDto> searchPlayerDtos = new List<SearchPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        p.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        t.conference, 
                        t.status as team_status 
                    FROM players p 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    WHERE t.team = @team 
                        AND p.position IN ('QB', 'RB', 'WR', 'TE', 'K', 'DEF') 
                        AND p.team_id IS NOT NULL 
                    GROUP BY p.player_id, p.position, t.team, p.name, p.status, p.injury_status, t.conference, t.status 
                    ORDER BY avg_fantasy_points DESC;", connection);
                {
                    command.Parameters.AddWithValue("@team", teamName);

                    using NpgsqlDataReader reader = command.ExecuteReader();
                    {
                        
                        while (await reader.ReadAsync())
                        {
                            SearchPlayerDto searchPlayerDto = new SearchPlayerDto();
                            {
                                searchPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                                searchPlayerDto.Position = Convert.ToString(reader["position"]);
                                searchPlayerDto.Team = Convert.ToString(reader["team"]);
                                searchPlayerDto.Name = Convert.ToString(reader["name"]);
                                searchPlayerDto.Status = Convert.ToString(reader["status"]);
                                searchPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                                searchPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                                searchPlayerDto.Conference = Convert.ToString(reader["conference"]);
                                searchPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);
                            };
                            searchPlayerDtos.Add(searchPlayerDto);
                        }                    
                    }
                }
            }
            return searchPlayerDtos;
        }

        public async Task<List<SearchPlayerDto>> GetPlayerIdByPositionAsync(string position)
        {
            List<SearchPlayerDto> searchPlayerDtos = new List<SearchPlayerDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        p.player_id, 
                        p.position, 
                        t.team, 
                        p.name, 
                        p.status, 
                        p.injury_status, 
                        COALESCE(ROUND(AVG(ps.fantasy_points), 2), 0) as avg_fantasy_points, 
                        t.conference, 
                        t.status as team_status 
                    FROM players p 
                    JOIN teams t ON p.team_id = t.team_id 
                    LEFT JOIN player_stats ps ON p.player_id = ps.player_id 
                    WHERE p.position = @position 
                        AND p.position IN ('QB', 'RB', 'WR', 'TE', 'K', 'DEF') 
                        AND p.team_id IS NOT NULL 
                    GROUP BY p.player_id, p.position, t.team, p.name, p.status, p.injury_status, t.conference, t.status 
                    ORDER BY avg_fantasy_points DESC;", connection);
                {
                    command.Parameters.AddWithValue("@position", position);

                    using NpgsqlDataReader reader = command.ExecuteReader();
                    {
                        
                        while (await reader.ReadAsync())
                        {
                            SearchPlayerDto searchPlayerDto = new SearchPlayerDto();
                            {
                                searchPlayerDto.PlayerId = Convert.ToInt32(reader["player_id"]);
                                searchPlayerDto.Position = Convert.ToString(reader["position"]);
                                searchPlayerDto.Team = Convert.ToString(reader["team"]);
                                searchPlayerDto.Name = Convert.ToString(reader["name"]);
                                searchPlayerDto.Status = Convert.ToString(reader["status"]);
                                searchPlayerDto.InjuryStatus = Convert.ToString(reader["injury_status"] ?? (object)DBNull.Value);
                                searchPlayerDto.FantasyPointsAvg = Convert.ToDouble(reader["avg_fantasy_points"]);
                                searchPlayerDto.Conference = Convert.ToString(reader["conference"]);
                                searchPlayerDto.TeamStatus = Convert.ToString(reader["team_status"]);
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
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
    public class FantasyRosterSqlDao : IFantasyRosterDao
    {
        private readonly string _connectionString;

        public FantasyRosterSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task CreateFantasyRoster(User user, string teamName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO fantasy_rosters (user_id, league_id, team_name, total_score) VALUES (@user_id, @league_id, @team_name, @total_score);", connection);
                {
                    command.Parameters.AddWithValue("@user_id", user.UserId);
                    command.Parameters.AddWithValue("@league_id", user.FantasyLeagueId);
                    command.Parameters.AddWithValue("@team_name", teamName);
                    command.Parameters.AddWithValue("@total_score", 0);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<FantasyRosterDto>> GetFantasyRosters(User user)
        {
            if (user.FantasyLeagueId == null)
            {
                return new List<FantasyRosterDto>();
            }

            
            List<FantasyRosterDto> fantasyRosterDtos = new List<FantasyRosterDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT 
                        fr.roster_id, 
                        fr.user_id, 
                        fr.league_id,
                        fr.team_name, 
                        u.username, 
                        fr.total_score, 
                        COALESCE(w1.total_score, 0) AS week_one_score, 
                        COALESCE(w2.total_score, 0) AS week_two_score, 
                        COALESCE(w3.total_score, 0) AS week_three_score, 
                        COALESCE(w4.total_score, 0) AS week_four_score 
                    FROM fantasy_rosters fr 
                    JOIN users u ON fr.user_id = u.user_id 
                    LEFT JOIN 
                        (SELECT roster_id, total_score FROM fantasy_lineups WHERE game_week = 1) w1 ON fr.roster_id = w1.roster_id 
                    LEFT JOIN 
                        (SELECT roster_id, total_score FROM fantasy_lineups WHERE game_week = 2) w2 ON fr.roster_id = w2.roster_id 
                    LEFT JOIN 
                        (SELECT roster_id, total_score FROM fantasy_lineups WHERE game_week = 3) w3 ON fr.roster_id = w3.roster_id 
                    LEFT JOIN 
                        (SELECT roster_id, total_score FROM fantasy_lineups WHERE game_week = 4) w4 ON fr.roster_id = w4.roster_id 
                    WHERE fr.league_id = @league_id
                    ORDER BY fr.total_score DESC, u.username;", connection);
                {
                    command.Parameters.AddWithValue("@league_id", user.FantasyLeagueId);
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        FantasyRosterDto fantasyRosterDto = new FantasyRosterDto();
                        {
                            fantasyRosterDto.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                            fantasyRosterDto.UserId = Convert.ToInt32(reader["user_id"]);
                            fantasyRosterDto.FantasyLeagueId = Convert.ToInt32(reader["league_id"]);
                            fantasyRosterDto.TeamName = Convert.ToString(reader["team_name"]);
                            fantasyRosterDto.Username = Convert.ToString(reader["username"]);
                            fantasyRosterDto.TotalScore = Convert.ToDouble(reader["total_score"]);
                            fantasyRosterDto.Week1Score = Convert.ToDouble(reader["week_one_score"]);
                            fantasyRosterDto.Week2Score = Convert.ToDouble(reader["week_two_score"]);
                            fantasyRosterDto.Week3Score = Convert.ToDouble(reader["week_three_score"]);
                            fantasyRosterDto.Week4Score= Convert.ToDouble(reader["week_four_score"]);
                        };
                        fantasyRosterDtos.Add(fantasyRosterDto);
                    } 
                }
                
            }
            return fantasyRosterDtos;
        }

        public async Task<FantasyRoster> GetFantasyRosterByUser(User user)
        {
            FantasyRoster fantasyRoster = new FantasyRoster();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT roster_id, user_id, league_id, team_name, total_score FROM fantasy_rosters WHERE user_id = @user_id AND league_id = @league_id;", connection);
                {
                    command.Parameters.AddWithValue("@user_id", user.UserId);
                    command.Parameters.AddWithValue("@league_id", user.FantasyLeagueId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        fantasyRoster.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        fantasyRoster.UserId = Convert.ToInt32(reader["user_id"]);
                        fantasyRoster.FantasyLeagueId = Convert.ToInt32(reader["league_id"]);
                        fantasyRoster.TeamName = Convert.ToString(reader["team_name"]);
                        fantasyRoster.TotalScore = Convert.ToDouble(reader["total_score"]);
                    }
                }
            }
            return fantasyRoster;
        }
    }
}
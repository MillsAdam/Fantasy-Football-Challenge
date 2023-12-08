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
    public class FantasyLineupSqlDao : IFantasyLineupDao
    {
        private readonly string connectionString;
        private readonly IFantasyRosterDao _fantasyRosterDao;

        public FantasyLineupSqlDao(IConfiguration configuration, IFantasyRosterDao fantasyRosterDao)
        {
            connectionString = configuration.GetConnectionString("Project");
            _fantasyRosterDao = fantasyRosterDao;
        }

        public async Task CreateFantasyLineup(int fantasyRosterId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                for (int gameWeek = 1; gameWeek <= 4; gameWeek++)
                {
                    using NpgsqlCommand command = new NpgsqlCommand(
                        @"INSERT INTO fantasy_lineups (roster_id, game_week, total_score) VALUES (@roster_id, @game_week, @total_score);", connection);
                    {
                        command.Parameters.AddWithValue("@roster_id", fantasyRosterId);
                        command.Parameters.AddWithValue("@game_week", gameWeek);
                        command.Parameters.AddWithValue("@total_score", 0);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task<FantasyLineup> GetFantasyLineupByUser(User user)
        {
            FantasyLineup fantasyLineup = new FantasyLineup();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT fl.lineup_id, fl.roster_id, fl.game_week, fl.total_score 
                    FROM fantasy_lineups fl 
                    LEFT JOIN configuration c_lineup_week ON c_lineup_week.config_key = 'currentLineupWeek' 
                    WHERE roster_id = @roster_id 
                        AND game_week = c_lineup_week.config_value;", connection);
                {
                    FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                    command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        fantasyLineup.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                        fantasyLineup.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                        fantasyLineup.GameWeek = Convert.ToInt32(reader["game_week"]);
                        fantasyLineup.TotalScore = Convert.ToDouble(reader["total_score"]);
                    }
                    return fantasyLineup;
                }
            }
        }

        public async Task<double> GetWeeklyScoreByUserAndWeek(User user, int gameWeek)
        {
            double weeklyScore = 0.0;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT fl.total_score 
                    FROM fantasy_lineups fl 
                    JOIN fantasy_rosters fr ON fr.roster_id = fl.roster_id
                    WHERE fr.user_id = @user_id 
                        AND fl.game_week = @game_week;", connection);
                {
                    command.Parameters.AddWithValue("@user_id", user.UserId);
                    command.Parameters.AddWithValue("@game_week", gameWeek);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
                    {
                        weeklyScore = Convert.ToDouble(reader["total_score"]);
                    }
                    return weeklyScore;
                }
            }
        }

        // public async Task<FantasyLineup> GetFantasyLineupByUserId(int userId)
        // {
        //     FantasyLineup fantasyLineup = new FantasyLineup();
        //     using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //     {
        //         await connection.OpenAsync();
        //         using NpgsqlCommand command = new NpgsqlCommand(
        //             @"SELECT fl.lineup_id, fl.roster_id, fl.game_week, fl.total_score 
        //             FROM fantasy_lineups fl 
        //             LEFT JOIN configuration c_lineup_week ON c_lineup_week.config_key = 'currentLineupWeek' 
        //             JOIN fantasy_rosters fr ON fr.roster_id = fl.roster_id
        //             WHERE fr.user_id = @user_id 
        //                 AND game_week = c_lineup_week.config_value;", connection);
        //         {
        //             command.Parameters.AddWithValue("@user_id", userId);
        //             NpgsqlDataReader reader = command.ExecuteReader();
        //             while (await reader.ReadAsync())
        //             {
        //                 fantasyLineup.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
        //                 fantasyLineup.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
        //                 fantasyLineup.GameWeek = Convert.ToInt32(reader["game_week"]);
        //                 fantasyLineup.TotalScore = Convert.ToDouble(reader["total_score"]);
        //             }
        //             return fantasyLineup;
        //         }
        //     }
        // }
    }
}
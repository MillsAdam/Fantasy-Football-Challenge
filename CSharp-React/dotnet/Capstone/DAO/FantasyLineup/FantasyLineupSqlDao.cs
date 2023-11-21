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

        public FantasyLineupSqlDao(IConfiguration configuration, IUserDao userDao, IFantasyRosterDao fantasyRosterDao)
        {
            connectionString = configuration.GetConnectionString("Project");
            _fantasyRosterDao = fantasyRosterDao;
        }

        public async Task CreateFantasyLineup(int fantasyRosterId)
        {
            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                for (int gameWeek = 1; gameWeek <= 4; gameWeek++)
                {
                    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("INSERT INTO fantasy_lineups (roster_id, game_week) VALUES (@roster_id, @game_week);", connection);
                    command.Parameters.AddWithValue("@roster_id", fantasyRosterId);
                    command.Parameters.AddWithValue("@game_week", gameWeek);
                    command.ExecuteNonQuery();
                }
            }
        }

        public async Task<FantasyLineup> GetFantasyLineupByUser(User user, int gameWeek)
        {
            FantasyLineup fantasyLineup = new FantasyLineup();
            using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("SELECT lineup_id, roster_id, game_week FROM fantasy_lineups WHERE roster_id = @roster_id AND game_week = @game_week;", connection);

                FantasyRoster fantasyRoster = await _fantasyRosterDao.GetFantasyRosterByUser(user);
                command.Parameters.AddWithValue("@roster_id", fantasyRoster.FantasyRosterId);
                command.Parameters.AddWithValue("@game_week", 1); // Change gameWeek as needed
                Npgsql.NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fantasyLineup.FantasyLineupId = Convert.ToInt32(reader["lineup_id"]);
                    fantasyLineup.FantasyRosterId = Convert.ToInt32(reader["roster_id"]);
                    fantasyLineup.GameWeek = Convert.ToInt32(reader["game_week"]);
                }
                return fantasyLineup;
            }
        }
    }
}
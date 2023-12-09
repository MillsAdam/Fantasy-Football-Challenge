using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.Services
{
    public class ScoreService
    {
        private readonly string _connectionString;

        public ScoreService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }
       public async Task UpdateLineupTotalScores() 
        {
            using (var connection = new NpgsqlConnection(_connectionString)) 
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try 
                    {
                        // Fetch the data first
                        var updateData = new List<(int LineupId, decimal TotalLineupScore)>();
                        var calculateCommand = new NpgsqlCommand(
                            @"SELECT lp.lineup_id, SUM(ps.fantasy_points) AS total_lineup_score 
                            FROM lineup_players lp 
                            JOIN fantasy_lineups fl ON lp.lineup_id = fl.lineup_id
                            LEFT JOIN configuration c_week on c_week.config_key = 
                                CASE 
                                    WHEN fl.game_week = 1 THEN 'lineupWeek1' 
                                    WHEN fl.game_week = 2 THEN 'lineupWeek2' 
                                    WHEN fl.game_week = 3 THEN 'lineupWeek3' 
                                    WHEN fl.game_week = 4 THEN 'lineupWeek4' 
                                END 
                            LEFT JOIN player_stats ps ON lp.player_id = ps.player_id AND ps.week = c_week.config_value 
                            WHERE fl.game_week = (SELECT config_value FROM configuration WHERE config_key = 'currentLineupWeek') 
                            GROUP BY lp.lineup_id", connection);
                        
                        using (var lineupReader = await calculateCommand.ExecuteReaderAsync())
                        {
                            while (await lineupReader.ReadAsync())
                            {
                                int lineupId = lineupReader.GetInt32(lineupReader.GetOrdinal("lineup_id"));
                                decimal totalLineupScore = lineupReader.IsDBNull(lineupReader.GetOrdinal("total_lineup_score")) 
                                    ? 0 : lineupReader.GetDecimal(lineupReader.GetOrdinal("total_lineup_score"));
                                
                                updateData.Add((lineupId, totalLineupScore));
                            }
                        }

                        // Now perform the updates
                        foreach (var (LineupId, TotalLineupScore) in updateData)
                        {
                            var updateCommand = new NpgsqlCommand(
                                "UPDATE fantasy_lineups " +
                                "SET total_score = @totalLineupScore " +
                                "WHERE lineup_id = @lineupId", connection);
                            updateCommand.Parameters.AddWithValue("totalLineupScore", TotalLineupScore);
                            updateCommand.Parameters.AddWithValue("lineupId", LineupId);
                            await updateCommand.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception e)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task UpdateRosterTotalScores()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Fetch the data first
                        var updateData = new List<(int RosterId, decimal TotalRosterScore)>();
                        var calculateCommand = new NpgsqlCommand(
                            "SELECT fr.roster_id, SUM(fl.total_score) AS total_roster_score " +
                            "FROM fantasy_rosters fr " +
                            "JOIN fantasy_lineups fl ON fr.roster_id = fl.roster_id " +
                            "GROUP BY fr.roster_id", connection);

                        using (var rosterReader = await calculateCommand.ExecuteReaderAsync())
                        {
                            while (await rosterReader.ReadAsync())
                            {
                                int rosterId = rosterReader.GetInt32(rosterReader.GetOrdinal("roster_id"));
                                decimal totalRosterScore = rosterReader.IsDBNull(rosterReader.GetOrdinal("total_roster_score"))
                                    ? 0 : rosterReader.GetDecimal(rosterReader.GetOrdinal("total_roster_score"));

                                updateData.Add((rosterId, totalRosterScore));
                            }
                        }

                        // Now perform the updates
                        foreach (var (RosterId, TotalRosterScore) in updateData)
                        {
                            var updateCommand = new NpgsqlCommand(
                                "UPDATE fantasy_rosters " +
                                "SET total_score = @totalRosterScore " +
                                "WHERE roster_id = @rosterId", connection);
                            updateCommand.Parameters.AddWithValue("totalRosterScore", TotalRosterScore);
                            updateCommand.Parameters.AddWithValue("rosterId", RosterId);
                            await updateCommand.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception e)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }
}
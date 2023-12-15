using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Capstone.DAO.FantasyLeague
{
    public class FantasyMemberSqlDao : IFantasyMemberDao
    {
        private readonly string _connectionString;
        public FantasyMemberSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public async Task AddMemberAsync(FantasyMember fantasyMember)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"INSERT INTO fantasy_members (user_id, league_id)
                    VALUES (@userId, @leagueId);", connection);
                {
                    command.Parameters.AddWithValue("@userId", fantasyMember.UserId);
                    command.Parameters.AddWithValue("@leagueId", fantasyMember.FantasyLeagueId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<FantasyMember>> GetFantasyMembersByLeagueIdAsync(int fantasyLeagueId)
        {
            List<FantasyMember> fantasyMembers = new List<FantasyMember>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT
                        user_id,
                        league_id
                    FROM fantasy_members
                    WHERE league_id = @leagueId;", connection);
                {
                    command.Parameters.AddWithValue("@leagueId", fantasyLeagueId);
                    using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        FantasyMember fantasyMember = MapRowToFantasyMember(reader);
                        fantasyMembers.Add(fantasyMember);
                    }
                }
            }
            return fantasyMembers;
        }

        public async Task<List<FantasyLeagueModelDto>> GetFantasyLeaguesByUserIdAsync(User user)
        {
            List<FantasyLeagueModelDto> fantasyLeagues = new List<FantasyLeagueModelDto>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using NpgsqlCommand command = new NpgsqlCommand(
                    @"SELECT
                        fl.league_id,
                        fl.league_name
                    FROM fantasy_members fm
                    JOIN fantasy_leagues fl ON fm.league_id = fl.league_id
                    WHERE fm.user_id = @userId;", connection);
                {
                    command.Parameters.AddWithValue("@userId", user.UserId);
                    using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        FantasyLeagueModelDto fantasyLeague = MapRowToFantasyLeagueModelDto(reader);
                        fantasyLeagues.Add(fantasyLeague);
                    }
                }
            }
            return fantasyLeagues;
        }

        private FantasyMember MapRowToFantasyMember(NpgsqlDataReader reader)
        {
            return new FantasyMember()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                FantasyLeagueId = Convert.ToInt32(reader["league_id"])
            };
        }

        private FantasyLeagueModelDto MapRowToFantasyLeagueModelDto(NpgsqlDataReader reader)
        {
            return new FantasyLeagueModelDto()
            {
                FantasyLeagueId = Convert.ToInt32(reader["league_id"]),
                LeagueName = Convert.ToString(reader["league_name"])
            };
        }
    }
}
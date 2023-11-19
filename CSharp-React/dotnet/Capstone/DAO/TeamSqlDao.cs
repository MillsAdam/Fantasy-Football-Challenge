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
    public class TeamSqlDao : ITeamDao
    {
        private readonly string _connectionString;

        public TeamSqlDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Project");
        }

        public TeamDto AddTeam(Team team)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO teams (team_id, team, city, name, conference, division, full_name, status) VALUES (@team_id, @team, @city, @name, @conference, @division, @full_name, @status);", connection);
                command.Parameters.AddWithValue("@team", team.Key);
                command.Parameters.AddWithValue("@city", team.City);
                command.Parameters.AddWithValue("@name", team.Name);
                command.Parameters.AddWithValue("@conference", team.Conference);
                command.Parameters.AddWithValue("@division", team.Division);
                command.Parameters.AddWithValue("@full_name", team.FullName);
                command.Parameters.AddWithValue("@status", "Active");
                command.ExecuteNonQuery();

                TeamDto teamDto = new TeamDto();
                teamDto.TeamId = team.TeamId;
                teamDto.Team = team.Key;
                teamDto.City = team.City;
                teamDto.Name = team.Name;
                teamDto.Conference = team.Conference;
                teamDto.Division = team.Division;
                teamDto.FullName = team.FullName;
                teamDto.Status = "Active";
                return teamDto;
            }
        }
    }
}
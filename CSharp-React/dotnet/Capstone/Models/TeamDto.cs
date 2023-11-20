using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class TeamDto
    {
        public int TeamId { get; set; } // Team ID
        public string Team { get; set; } // Team Abbreivation (Key)
        public string City { get; set; } // Team City
        public string Name { get; set; } // Team Name
        public string Conference { get; set; } // Team Conference
        public string Division { get; set; } // Team Division
        public string FullName { get; set; } // Team Full Name
        public string Status { get; set; } // Team Status

        public static TeamDto FromTeam(Team team) => new TeamDto
        {
            TeamId = team.TeamId,
            Team = team.Key,
            City = team.City,
            Name = team.Name,
            Conference = team.Conference,
            Division = team.Division,
            FullName = team.FullName,
            Status = "Active"
        };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;

namespace Capstone.Models
{
    public class PlayerDto
    {
        public int PlayerId { get; set; } // Player ID
        public int TeamId { get; set; } // Team ID
        public string FirstName { get; set; } // Player First Name
        public string LastName { get; set; } // Player Last Name
        public string Position { get; set; } // Player Position
        public string PositionCategory { get; set; } // Player Position Category
        public string Status { get; set; } // Player Status
        public string? InjuryStatus { get; set; } // Player Injury Status // Nullable???

        // public static PlayerDto FromPlayer(Player player) => new PlayerDto
        // {
        //     PlayerId = player.PlayerId,
        //     TeamId = player.TeamId,
        //     FirstName = player.FirstName,
        //     LastName = player.LastName,
        //     Position = player.Position,
        //     PositionCategory = player.PositionCategory,
        //     Status = player.Status,
        //     InjuryStatus = player.InjuryStatus
        // };

        public static PlayerDto FromTeam(Team team) => new PlayerDto
        {
            PlayerId = team.PlayerId,
            TeamId = team.TeamId,
            FirstName = team.City,
            LastName = team.Name,
            Position = "DEF",
            PositionCategory = "DEF",
            Status = "Active",
            InjuryStatus = null
        };
    }
}
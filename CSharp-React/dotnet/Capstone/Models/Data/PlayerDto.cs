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
        public int? TeamId { get; set; } // Team ID // Nullable
        public string Name { get; set; } // Player Name
        public string Position { get; set; } // Player Position
        public string Status { get; set; } // Player Status
        public string? InjuryStatus { get; set; } // Player Injury Status // Nullable

        public static PlayerDto FromPlayer(Player player) => new PlayerDto
        {
            PlayerId = player.PlayerID,
            TeamId = player.TeamID,
            Name = player.FirstName + " " + player.LastName,
            Position = player.Position,
            Status = player.Status,
            InjuryStatus = player.InjuryStatus
        };

        public static PlayerDto FromTeam(Team team) => new PlayerDto
        {
            PlayerId = team.PlayerID,
            TeamId = team.TeamID,
            Name = team.City + " " + team.Name,
            Position = "DEF",
            Status = "Active",
            InjuryStatus = null
        };
    }
}
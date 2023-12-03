using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class PlayerStatsDto
    {
        public int PlayerId { get; set; } // Player ID
        public int TeamId { get; set; } // Team ID
        public int Week { get; set; } // Week
        public string Name { get; set; } // Player Name
        public string Position { get; set; } // Player Position
        public string Status { get; set; } // Player Status
        public string? InjuryStatus { get; set; } // Player Injury Status // Nullable
        public double FantasyPoints { get; set; } // Fantasy Points


        private const int PostSeasonType = 3;
        private const int RegularSeasonWeeks = 18;

        public static PlayerStatsDto FromPlayerStats(PlayerStats playerStats) => new PlayerStatsDto
        {
            PlayerId = playerStats.PlayerID,
            TeamId = playerStats.TeamID,
            Week = playerStats.SeasonType == PostSeasonType ? playerStats.Week + RegularSeasonWeeks : playerStats.Week,
            Name = playerStats.Name,
            Position = playerStats.Position,
            Status = playerStats.Played == 1 ? "Active" : "Inactive",
            InjuryStatus = playerStats.InjuryStatus,
            FantasyPoints = playerStats.FantasyPointsPPR
        };

        public static PlayerStatsDto FromDefenseStats(DefenseStats defenseStats) => new PlayerStatsDto
        {
            PlayerId = defenseStats.PlayerID,
            TeamId = defenseStats.TeamID,
            Week = defenseStats.SeasonType == PostSeasonType ? defenseStats.Week + RegularSeasonWeeks : defenseStats.Week,
            Name = defenseStats.Team,
            Position = "DEF",
            Status = "Active",
            InjuryStatus = null,
            FantasyPoints = defenseStats.FantasyPoints
        };

    }
}
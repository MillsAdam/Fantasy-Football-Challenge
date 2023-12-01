using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SearchPlayerDto
    {
        public int PlayerId { get; set; } // Player ID
        public string Position { get; set; } // Player Position
        public string Team { get; set; } // Team Name
        public string Name { get; set; } // Player Name
        public string Status { get; set; } // Player Status
        public string? InjuryStatus { get; set; } // Player Injury Status // Nullable
        public double FantasyPointsAvg { get; set; } // Player Fantasy Points Average
        public string Conference { get; set; } // Player Conference
        public string TeamStatus { get; set; } // Player Team Status
        
        public int? FantasyRosterId { get; set; } // Fantasy Roster ID
        public int? FantasyLineupId { get; set; } // Fantasy Lineup ID
        public double? FantasyPointsProj { get; set; } // Player Fantasy Points Projection
        public double? FantasyPoints { get; set; } // Player Fantasy Points
        public string? LineupPosition { get; set; } // Player Lineup Position

    }
}
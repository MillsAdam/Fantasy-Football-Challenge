using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class RosterPlayerDto
    {
        public int FantasyRosterId { get; set; }
        public int PlayerId { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string? InjuryStatus { get; set; }
        public double FantasyPointsAvg { get; set; }
        public double FantasyPointsProj { get; set; }
        public double FantasyPoints { get; set; }
        public string Conference { get; set; }
        public string TeamStatus { get; set; }
    }
}
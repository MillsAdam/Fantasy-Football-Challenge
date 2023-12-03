using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class FantasyLineup
    {
        public int FantasyLineupId { get; set; }
        public int FantasyRosterId { get; set; }
        public int GameWeek { get; set; }
        public double TotalScore { get; set; }
    }
}
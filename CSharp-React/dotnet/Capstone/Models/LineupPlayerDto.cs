using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class LineupPlayerDto
    {
        public int FantasyLineupId { get; set; }
        public int PlayerId { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Name { get; set; }
        public double FantasyPoints { get; set; }
        public string LineupPosition { get; set; }
    }
}
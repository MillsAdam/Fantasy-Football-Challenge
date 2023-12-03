using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class LineupPlayer
    {
        public int FantasyLineupId { get; set; }
        public int PlayerId { get; set; }
        public string LineupPosition { get; set; }
    }
}
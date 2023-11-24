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
    }
}
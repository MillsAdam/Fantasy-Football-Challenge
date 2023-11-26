using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class FantasyRosterDto
    {
        public int FantasyRosterId { get; set; }
        public int UserId { get; set; }
        public string TeamName { get; set; }
        public string Username { get; set; }
        public double TotalScore { get; set; }
    }
}
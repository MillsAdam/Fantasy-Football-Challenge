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
        public int FantasyLeagueId { get; set; }
        public string TeamName { get; set; }
        public string Username { get; set; }
        public double TotalScore { get; set; }
        public double Week1Score { get; set; }
        public double Week2Score { get; set; }
        public double Week3Score { get; set; }
        public double Week4Score { get; set; }
    }
}
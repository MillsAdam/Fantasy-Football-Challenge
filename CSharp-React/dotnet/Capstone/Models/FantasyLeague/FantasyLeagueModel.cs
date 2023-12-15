using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class FantasyLeagueModel
    {
        public int FantasyLeagueId { get; set; }
        public int UserId { get; set; }
        public string LeagueName { get; set; }
        public string LeaguePasswordHash { get; set; }
        public string LeagueSalt { get; set; }
    }
}
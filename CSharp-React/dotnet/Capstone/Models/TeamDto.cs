using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class TeamDto
    {
        public int TeamId { get; set; } // Team ID
        public string Team { get; set; } // Team Abbreivation
        public string City { get; set; } // Team City
        public string Name { get; set; } // Team Name
        public string Conference { get; set; } // Team Conference
        public string Division { get; set; } // Team Division
        public string FullName { get; set; } // Team Full Name
        public string Status { get; set; } // Team Status
    }
}
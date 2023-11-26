using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capstone.Models
{
    public class Player
    {
        [JsonProperty("PlayerID")]
        public int PlayerID { get; set; }
        [JsonProperty("Team")]
        public string Team { get; set; }
        [JsonProperty("Number")]
        public int? Number { get; set; } // Nullable
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Position")]
        public string Position { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("Height")]
        public string Height { get; set; }
        [JsonProperty("Weight")]
        public int Weight { get; set; }
        [JsonProperty("BirthDate")]
        public DateTime? BirthDate { get; set; } // Nullable
        [JsonProperty("College")]
        public string College { get; set; }
        [JsonProperty("Experience")]
        public int Experience { get; set; }
        [JsonProperty("FantasyPosition")]
        public string FantasyPosition { get; set; }
        [JsonProperty("PositionCategory")]
        public string PositionCategory { get; set; }
        [JsonProperty("PhotoUrl")]
        public string PhotoUrl { get; set; }
        [JsonProperty("ByeWeek")]
        public int? ByeWeek { get; set; } // Nullable
        [JsonProperty("AverageDraftPosition")]
        public double? AverageDraftPosition { get; set; } // Nullable
        [JsonProperty("CollegeDraftTeam")]
        public string CollegeDraftTeam { get; set; }
        [JsonProperty("CollegeDraftYear")]
        public int? CollegeDraftYear { get; set; } // Nullable
        [JsonProperty("CollegeDraftRound")]
        public int? CollegeDraftRound { get; set; } // Nullable
        [JsonProperty("CollegeDraftPick")]
        public int? CollegeDraftPick { get; set; } // Nullable
        [JsonProperty("IsUndraftedFreeAgent")]
        public bool IsUndraftedFreeAgent { get; set; }
        [JsonProperty("FanDuelPlayerID")]
        public int? FanDuelPlayerID { get; set; } // Nullable
        [JsonProperty("DraftKingsPlayerID")]
        public int? DraftKingsPlayerID { get; set; } // Nullable
        [JsonProperty("InjuryStatus")]
        public string InjuryStatus { get; set; }
        [JsonProperty("FanDuelName")]
        public string FanDuelName { get; set; }
        [JsonProperty("DraftKingsName")]
        public string DraftKingsName { get; set; }
        [JsonProperty("TeamID")]
        public int? TeamID { get; set; } // Nullable
    }
}
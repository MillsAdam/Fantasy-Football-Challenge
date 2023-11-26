using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capstone.Models
{
    public class DefenseStats
    {
        [JsonProperty("GameKey")]
        public string GameKey { get; set; }
        [JsonProperty("SeasonType")]
        public int SeasonType { get; set; }
        [JsonProperty("Season")]
        public int Season { get; set; }
        [JsonProperty("Week")]
        public int Week { get; set; }
        [JsonProperty("Date")]
        public DateTime Date { get; set; }
        [JsonProperty("Team")]
        public string Team { get; set; }
        [JsonProperty("Opponent")]
        public string Opponent { get; set; }
        [JsonProperty("PointsAllowed")]
        public double PointsAllowed { get; set; }
        [JsonProperty("TouchdownsScored")]
        public double TouchdownsScored { get; set; }
        [JsonProperty("Sacks")]
        public double Sacks { get; set; }
        [JsonProperty("SackYards")]
        public double SackYards { get; set; }
        [JsonProperty("FumblesForced")]
        public double FumblesForced { get; set; }
        [JsonProperty("FumblesRecovered")]
        public double FumblesRecovered { get; set; }
        [JsonProperty("FumbleReturnTouchdowns")]
        public double FumbleReturnTouchdowns { get; set; }
        [JsonProperty("Interceptions")]
        public double Interceptions { get; set; }
        [JsonProperty("InterceptionReturnTouchdowns")]
        public double InterceptionReturnTouchdowns { get; set; }
        [JsonProperty("BlockedKicks")]
        public double BlockedKicks { get; set; }
        [JsonProperty("Safeties")]
        public double Safeties { get; set; }
        [JsonProperty("PuntReturnTouchdowns")]
        public double PuntReturnTouchdowns { get; set; }
        [JsonProperty("KickReturnTouchdowns")]
        public double KickReturnTouchdowns { get; set; }
        [JsonProperty("BlockedKickReturnTouchdowns")]
        public double BlockedKickReturnTouchdowns { get; set; }
        [JsonProperty("FieldGoalReturnTouchdowns")]
        public double FieldGoalReturnTouchdowns { get; set; }
        [JsonProperty("QuarterbackHits")]
        public double QuarterbackHits { get; set; }
        [JsonProperty("TacklesForLoss")]
        public double TacklesForLoss { get; set; }
        [JsonProperty("DefensiveTouchdowns")]
        public double DefensiveTouchdowns { get; set; }
        [JsonProperty("SpecialTeamsTouchdowns")]
        public double SpecialTeamsTouchdowns { get; set; }
        [JsonProperty("FantasyPoints")]
        public double FantasyPoints { get; set; }
        [JsonProperty("PointsAllowedByDefenseSpecialTeams")]
        public double PointsAllowedByDefenseSpecialTeams { get; set; }
        [JsonProperty("TwoPointConversionReturns")]
        public double TwoPointConversionReturns { get; set; }
        [JsonProperty("FantasyPointsFanDuel")]
        public double FantasyPointsFanDuel { get; set; }
        [JsonProperty("FantasyPointsDraftKings")]
        public double FantasyPointsDraftKings { get; set; }
        [JsonProperty("PlayerID")]
        public int PlayerID { get; set; }
        [JsonProperty("HomeOrAway")]
        public string HomeOrAway { get; set; }
        [JsonProperty("TeamID")]
        public int TeamID { get; set; }
        [JsonProperty("OpponentID")]
        public int OpponentID { get; set; }
        [JsonProperty("ScoreID")]
        public int ScoreID { get; set; }
    }
}
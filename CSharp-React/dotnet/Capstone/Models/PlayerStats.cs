using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Capstone.Models
{
    public class PlayerStats
    {
        [JsonProperty("GameKey")]
        public string GameKey { get; set; }
        [JsonProperty("PlayerID")]
        public int PlayerID { get; set; }
        [JsonProperty("SeasonType")]
        public int SeasonType { get; set; }
        [JsonProperty("Season")]
        public int Season { get; set; }
        [JsonProperty("GameDate")]
        public DateTime GameDate { get; set; }
        [JsonProperty("Week")]
        public int Week { get; set; }
        [JsonProperty("Team")]
        public string Team { get; set; }
        [JsonProperty("Opponent")]
        public string Opponent { get; set; }
        [JsonProperty("HomeOrAway")]
        public string HomeOrAway { get; set; }
        [JsonProperty("Number")]
        public int Number { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Position")]
        public string Position { get; set; }
        [JsonProperty("PositionCategory")]
        public string PositionCategory { get; set; }
        [JsonProperty("Played")]
        public int Played { get; set; }
        [JsonProperty("Started")]
        public int Started { get; set; }
        [JsonProperty("PassingAttempts")]
        public double PassingAttempts { get; set; }
        [JsonProperty("PassingCompletions")]
        public double PassingCompletions { get; set; }
        [JsonProperty("PassingYards")]
        public double PassingYards { get; set; }
        [JsonProperty("PassingCompletionPercentage")]
        public double PassingCompletionPercentage { get; set; }
        [JsonProperty("PassingYardsPerAttempt")]
        public double PassingYardsPerAttempt { get; set; }
        [JsonProperty("PassingYardsPerCompletion")]
        public double PassingYardsPerCompletion { get; set; }
        [JsonProperty("PassingTouchdowns")]
        public double PassingTouchdowns { get; set; }
        [JsonProperty("PassingInterceptions")]
        public double PassingInterceptions { get; set; }
        [JsonProperty("PassingRating")]
        public double PassingRating { get; set; }
        [JsonProperty("PassingLong")]
        public double PassingLong { get; set; }
        [JsonProperty("PassingSacks")]
        public double PassingSacks { get; set; }
        [JsonProperty("PassingSackYards")]
        public double PassingSackYards { get; set; }
        [JsonProperty("RushingAttempts")]
        public double RushingAttempts { get; set; }
        [JsonProperty("RushingYards")]
        public double RushingYards { get; set; }
        [JsonProperty("RushingYardsPerAttempt")]
        public double RushingYardsPerAttempt { get; set; }
        [JsonProperty("RushingTouchdowns")]
        public double RushingTouchdowns { get; set; }
        [JsonProperty("RushingLong")]
        public double RushingLong { get; set; }
        [JsonProperty("ReceivingTargets")]
        public double ReceivingTargets { get; set; }
        [JsonProperty("Receptions")]
        public double Receptions { get; set; }
        [JsonProperty("ReceivingYards")]
        public double ReceivingYards { get; set; }
        [JsonProperty("ReceivingYardsPerReception")]
        public double ReceivingYardsPerReception { get; set; }
        [JsonProperty("ReceivingTouchdowns")]
        public double ReceivingTouchdowns { get; set; }
        [JsonProperty("ReceivingLong")]
        public double ReceivingLong { get; set; }
        [JsonProperty("Fumbles")]
        public double Fumbles { get; set; }
        [JsonProperty("FumblesLost")]
        public double FumblesLost { get; set; }
        [JsonProperty("PuntReturns")]
        public double PuntReturns { get; set; }
        [JsonProperty("PuntReturnYards")]
        public double PuntReturnYards { get; set; }
        [JsonProperty("PuntReturnTouchdowns")]
        public double PuntReturnTouchdowns { get; set; }
        [JsonProperty("KickReturns")]
        public double KickReturns { get; set; }
        [JsonProperty("KickReturnYards")]
        public double KickReturnYards { get; set; }
        [JsonProperty("KickReturnTouchdowns")]
        public double KickReturnTouchdowns { get; set; }
        [JsonProperty("SoloTackles")]
        public double SoloTackles { get; set; }
        [JsonProperty("AssistedTackles")]
        public double AssistedTackles { get; set; }
        [JsonProperty("TacklesForLoss")]
        public double TacklesForLoss { get; set; }
        [JsonProperty("Sacks")]
        public double Sacks { get; set; }
        [JsonProperty("SackYards")]
        public double SackYards { get; set; }
        [JsonProperty("QuarterbackHits")]
        public double QuarterbackHits { get; set; }
        [JsonProperty("PassesDefended")]
        public double PassesDefended { get; set; }
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
        [JsonProperty("FieldGoalsAttempted")]
        public double FieldGoalsAttempted { get; set; }
        [JsonProperty("FieldGoalsMade")]
        public double FieldGoalsMade { get; set; }
        [JsonProperty("ExtraPointsMade")]
        public double ExtraPointsMade { get; set; }
        [JsonProperty("TwoPointConversionPasses")]
        public double TwoPointConversionPasses { get; set; }
        [JsonProperty("TwoPointConversionRuns")]
        public double TwoPointConversionRuns { get; set; }
        [JsonProperty("TwoPointConversionReceptions")]
        public double TwoPointConversionReceptions { get; set; }
        [JsonProperty("FantasyPoints")]
        public double FantasyPoints { get; set; }
        [JsonProperty("FantasyPointsPPR")]
        public double FantasyPointsPPR { get; set; }
        [JsonProperty("FantasyPosition")]
        public string FantasyPosition { get; set; }
        [JsonProperty("PlayerGameID")]
        public int PlayerGameID { get; set; }
        [JsonProperty("ExtraPointsAttempted")]
        public double ExtraPointsAttempted { get; set; }
        [JsonProperty("FantasyPointsFanDuel")]
        public double FantasyPointsFanDuel { get; set; }
        [JsonProperty("FieldGoalsMade0to19")]
        public double? FieldGoalsMade0to19 { get; set; }
        [JsonProperty("FieldGoalsMade20to29")]
        public double? FieldGoalsMade20to29 { get; set; }
        [JsonProperty("FieldGoalsMade30to39")]
        public double? FieldGoalsMade30to39 { get; set; }
        [JsonProperty("FieldGoalsMade40to49")]
        public double? FieldGoalsMade40to49 { get; set; }
        [JsonProperty("FieldGoalsMade50Plus")]
        public double? FieldGoalsMade50Plus { get; set; }
        [JsonProperty("FantasyPointsDraftKings")]
        public double FantasyPointsDraftKings { get; set; }
        [JsonProperty("InjuryStatus")]
        public string? InjuryStatus { get; set; }
        [JsonProperty("TeamID")]
        public int TeamID { get; set; }
        [JsonProperty("OpponentID")]
        public int OpponentID { get; set; }
        [JsonProperty("ScoreID")]
        public int ScoreID { get; set; }
        [JsonProperty("SnapCountsConfirmed")]
        public bool? SnapCountsConfirmed { get; set; }
    }
}
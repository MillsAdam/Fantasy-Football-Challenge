using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.Data
{
    public class PlayerStatsExt
    {
        public int PlayerId { get; set; }
        public int? TeamId { get; set; }
        public int Week { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public string? InjuryStatus { get; set; }
        public double FantasyPoints { get; set; }
        public double PassingCompletions { get; set; }
        public double PassingAttempts { get; set; }
        public double PassingYards { get; set; }
        public double PassingTouchdowns { get; set; }
        public double PassingInterceptions { get; set; }
        public double PassingRating { get; set; }
        public double RushingAttempts { get; set; }
        public double RushingYards { get; set; }
        public double RushingTouchdowns { get; set; }
        public double ReceivingTargets { get; set; }
        public double Receptions { get; set; }
        public double ReceivingYards { get; set; }
        public double ReceivingTouchdowns { get; set; }
        public double ReturnTouchdowns { get; set;}
        public double TwoPointConversions { get; set; }
        public double FumblesLost { get; set; }
        public double FieldGoalsMade { get; set; }
        public double FieldGoalsAttempted { get; set; }
        public double? FieldGoalsMade0to19 { get; set; }
        public double? FieldGoalsMade20to29 { get; set; }
        public double? FieldGoalsMade30to39 { get; set; }
        public double? FieldGoalsMade40to49 { get; set; }
        public double? FieldGoalsMade50Plus { get; set; }
        public double ExtraPointsMade { get; set; }
        public double ExtraPointsAttempted { get; set; }
        public double DefensiveTouchdowns { get; set; }
        public double SpecialTeamsTouchdowns { get; set; }
        public double TouchdownsScored { get; set; }
        public double FumblesForced { get; set; }
        public double FumblesRecovered { get; set; }
        public double Interceptions { get; set; }
        public double TacklesForLoss { get; set; }
        public double QuarterbackHits { get; set; }
        public double Sacks { get; set; }
        public double Safeties { get; set; }
        public double BlockedKicks { get; set; }
        public double PointsAllowed { get; set; } 


        private const int PostSeasonType = 3;
        private const int RegularSeasonWeeks = 18;

        public static PlayerStatsExt FromPlayerStatsExt(PlayerStats playerStats) => new PlayerStatsExt 
        {
            PlayerId = playerStats.PlayerID,
            TeamId = playerStats.TeamID,
            Week = playerStats.SeasonType == PostSeasonType ? playerStats.Week + RegularSeasonWeeks : playerStats.Week,
            Name = playerStats.Name,
            Position = playerStats.Position,
            Status = playerStats.Played == 1 ? "Active" : "Inactive",
            InjuryStatus = playerStats.InjuryStatus,
            FantasyPoints = playerStats.FantasyPointsPPR,
            PassingCompletions = playerStats.PassingCompletions,
            PassingAttempts = playerStats.PassingAttempts,
            PassingYards = playerStats.PassingYards,
            PassingTouchdowns = playerStats.PassingTouchdowns,
            PassingInterceptions = playerStats.PassingInterceptions,
            PassingRating = playerStats.PassingRating,
            RushingAttempts = playerStats.RushingAttempts,
            RushingYards = playerStats.RushingYards,
            RushingTouchdowns = playerStats.RushingTouchdowns,
            ReceivingTargets = playerStats.ReceivingTargets,
            Receptions = playerStats.Receptions,
            ReceivingYards = playerStats.ReceivingYards,
            ReceivingTouchdowns = playerStats.ReceivingTouchdowns,
            ReturnTouchdowns = playerStats.PuntReturnTouchdowns + playerStats.KickReturnTouchdowns,
            TwoPointConversions = playerStats.TwoPointConversionPasses + playerStats.TwoPointConversionRuns + playerStats.TwoPointConversionReceptions,
            FumblesLost = playerStats.FumblesLost,
            FieldGoalsMade = playerStats.FieldGoalsMade,
            FieldGoalsAttempted = playerStats.FieldGoalsAttempted,
            FieldGoalsMade0to19 = playerStats.FieldGoalsMade0to19,
            FieldGoalsMade20to29 = playerStats.FieldGoalsMade20to29,
            FieldGoalsMade30to39 = playerStats.FieldGoalsMade30to39,
            FieldGoalsMade40to49 = playerStats.FieldGoalsMade40to49,
            FieldGoalsMade50Plus = playerStats.FieldGoalsMade50Plus,
            ExtraPointsMade = playerStats.ExtraPointsMade,
            ExtraPointsAttempted = playerStats.ExtraPointsAttempted,
            DefensiveTouchdowns = 0,
            SpecialTeamsTouchdowns = 0,
            TouchdownsScored = 0,
            FumblesForced = 0,
            FumblesRecovered = 0,
            Interceptions = 0,
            TacklesForLoss = 0,
            QuarterbackHits = 0,
            Sacks = 0,
            Safeties = 0,
            BlockedKicks = 0,
            PointsAllowed = 0
        };

        public static PlayerStatsExt FromDefenseStatsExt(DefenseStats defenseStats) => new PlayerStatsExt 
        {
            PlayerId = defenseStats.PlayerID,
            TeamId = defenseStats.TeamID,
            Week = defenseStats.SeasonType == PostSeasonType ? defenseStats.Week + RegularSeasonWeeks : defenseStats.Week,
            Name = defenseStats.Team,
            Position = "DEF",
            Status = "Active",
            InjuryStatus = null,
            FantasyPoints = defenseStats.FantasyPoints,
            PassingCompletions = 0,
            PassingAttempts = 0,
            PassingYards = 0,
            PassingTouchdowns = 0,
            PassingInterceptions = 0,
            PassingRating = 0,
            RushingAttempts = 0,
            RushingYards = 0,
            RushingTouchdowns = 0,
            ReceivingTargets = 0,
            Receptions = 0,
            ReceivingYards = 0,
            ReceivingTouchdowns = 0,
            ReturnTouchdowns = 0,
            TwoPointConversions = 0,
            FumblesLost = 0,
            FieldGoalsMade = 0,
            FieldGoalsAttempted = 0,
            FieldGoalsMade0to19 = 0,
            FieldGoalsMade20to29 = 0,
            FieldGoalsMade30to39 = 0,
            FieldGoalsMade40to49 = 0,
            FieldGoalsMade50Plus = 0,
            ExtraPointsMade = 0,
            ExtraPointsAttempted = 0,
            DefensiveTouchdowns = defenseStats.DefensiveTouchdowns,
            SpecialTeamsTouchdowns = defenseStats.SpecialTeamsTouchdowns,
            TouchdownsScored = defenseStats.TouchdownsScored,
            FumblesForced = defenseStats.FumblesForced,
            FumblesRecovered = defenseStats.FumblesRecovered,
            Interceptions = defenseStats.Interceptions,
            TacklesForLoss = defenseStats.TacklesForLoss,
            QuarterbackHits = defenseStats.QuarterbackHits,
            Sacks = defenseStats.Sacks,
            Safeties = defenseStats.Safeties,
            BlockedKicks = defenseStats.BlockedKicks,
            PointsAllowed = defenseStats.PointsAllowed
        };
    }
}
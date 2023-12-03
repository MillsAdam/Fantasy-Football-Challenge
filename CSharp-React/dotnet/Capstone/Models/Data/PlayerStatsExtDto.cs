using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.Data
{
    public class PlayerStatsExtDto
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int Week { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string? InjuryStatus { get; set; }
        public double FantasyPointsTotal { get; set; }
        public double FantasyPointsAverage { get; set; }
        public double PassingCompletions { get; set; }
        public double PassingAttempts { get; set; }
        public double PassingCompletionPercentage { get; set; }
        public double PassingYards { get; set; }
        public double PassingTouchdowns { get; set; }
        public double PassingInterceptions { get; set; }
        public double PassingRating { get; set; }
        public double RushingAttempts { get; set; }
        public double RushingYards { get; set; }
        public double RushingYardsPerAttempt { get; set; }
        public double RushingTouchdowns { get; set; }
        public double ReceivingTargets { get; set; }
        public double Receptions { get; set; }
        public double ReceivingYards { get; set; }
        public double ReceivingYardsPerReception { get; set; }
        public double ReceivingTouchdowns { get; set; }
        public double ReturnTouchdowns { get; set; }
        public double TwoPointConversions { get; set; }
        public double Usage { get; set; }
        public double FumblesLost { get; set; }
        public double FieldGoalsMade { get; set; }
        public double FieldGoalsAttempted { get; set; }
        public double FieldGoalPercentage { get; set; }
        public double FieldGoalsMade0to19 { get; set; }
        public double FieldGoalsMade20to29 { get; set; }
        public double FieldGoalsMade30to39 { get; set; }
        public double FieldGoalsMade40to49 { get; set; }
        public double FieldGoalsMade50Plus { get; set; }
        public double ExtraPointsMade { get; set; }
        public double ExtraPointsAttempted { get; set; }
        public double ExtraPointPercentage { get; set; }
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
        public string Conference { get; set; }
        public string TeamStatus { get; set; }
    }
}
package com.techelevator.dao;

import com.techelevator.model.Offense;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

@Component
public class JdbcOffenseDao implements OffenseDao {
    private JdbcTemplate jdbcTemplate;
    public JdbcOffenseDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    private String generateSql(String tableName) {
        return "INSERT INTO " + tableName +
                " (GameKey, PlayerID, SeasonType, Season, GameDate, Week, Team, Opponent, " +
                "HomeOrAway, Number, Name, Position, PositionCategory, Played, Started, " +
                "PassingAttempts, PassingCompletions, PassingYards, PassingCompletionPercentage, " +
                "PassingYardsPerAttempt, PassingYardsPerCompletion, PassingTouchdowns, " +
                "PassingInterceptions, PassingRating, PassingLong, PassingSacks, PassingSackYards, " +
                "RushingAttempts, RushingYards, RushingYardsPerAttempt, RushingTouchdowns, " +
                "RushingLong, ReceivingTargets, Receptions, ReceivingYards, ReceivingYardsPerReception, " +
                "ReceivingTouchdowns, ReceivingLong, Fumbles, FumblesLost, PuntReturns, " +
                "PuntReturnYards, PuntReturnTouchdowns, KickReturns, KickReturnYards, " +
                "KickReturnTouchdowns, SoloTackles, AssistedTackles, TacklesForLoss, Sacks, " +
                "SackYards, QuarterbackHits, PassesDefended, FumblesForced, FumblesRecovered, " +
                "FumbleReturnTouchdowns, Interceptions, InterceptionReturnTouchdowns, FieldGoalsAttempted, " +
                "FieldGoalsMade, ExtraPointsMade, TwoPointConversionPasses, TwoPointConversionRuns, " +
                "TwoPointConversionReceptions, FantasyPoints, FantasyPointsPPR, " +
                "FantasyPosition, PlayerGameID, ExtraPointsAttempted, FantasyPointsFanDuel, " +
                "FieldGoalsMade0to19, FieldGoalsMade20to29, FieldGoalsMade30to39, " +
                "FieldGoalsMade40to49, FieldGoalsMade50Plus, FantasyPointsDraftKings, " +
                "InjuryStatus, TeamID, OpponentID, ScoreID, SnapCountsConfirmed) " +
                "VALUES " +
                "(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
    }

    @Override
    public void addOffenseStats(Offense offense) {
        String sql = generateSql("offense_stats_2022");
        jdbcTemplate.update(sql, createObjectArray(offense));
    }

    @Override
    public void addOffenseProj(Offense offense) {
        String sql = generateSql("offense_proj_2022");
        jdbcTemplate.update(sql, createObjectArray(offense));
    }



    private Object[] createObjectArray(Offense offense) {
        return new Object [] {
                offense.getGameKey(), offense.getPlayerID(), offense.getSeasonType(),
                offense.getSeason(), offense.getGameDate(), offense.getWeek(),
                offense.getTeam(), offense.getOpponent(), offense.getHomeOrAway(),
                offense.getNumber(), offense.getName(), offense.getPosition(),
                offense.getPositionCategory(), offense.getPlayed(), offense.getStarted(),
                offense.getPassingAttempts(), offense.getPassingCompletions(), offense.getPassingYards(),
                offense.getPassingCompletionPercentage(), offense.getPassingYardsPerAttempt(), offense.getPassingYardsPerCompletion(),
                offense.getPassingTouchdowns(), offense.getPassingInterceptions(), offense.getPassingRating(),
                offense.getPassingLong(), offense.getPassingSacks(), offense.getPassingSackYards(),
                offense.getRushingAttempts(), offense.getRushingYards(), offense.getRushingYardsPerAttempt(),
                offense.getRushingTouchdowns(), offense.getRushingLong(), offense.getReceivingTargets(),
                offense.getReceptions(), offense.getReceivingYards(), offense.getReceivingYardsPerReception(),
                offense.getReceivingTouchdowns(), offense.getReceivingLong(), offense.getFumbles(),
                offense.getFumblesLost(), offense.getPuntReturns(), offense.getPuntReturnYards(),
                offense.getPuntReturnTouchdowns(), offense.getKickReturns(), offense.getKickReturnYards(),
                offense.getKickReturnTouchdowns(), offense.getSoloTackles(), offense.getAssistedTackles(),
                offense.getTacklesForLoss(), offense.getSacks(), offense.getSackYards(),
                offense.getQuarterbackHits(), offense.getPassesDefended(), offense.getFumblesForced(),
                offense.getFumblesRecovered(), offense.getFumbleReturnTouchdowns(), offense.getInterceptions(),
                offense.getInterceptionReturnTouchdowns(), offense.getFieldGoalsAttempted(), offense.getFieldGoalsMade(),
                offense.getExtraPointsMade(), offense.getTwoPointConversionPasses(), offense.getTwoPointConversionRuns(),
                offense.getTwoPointConversionReceptions(), offense.getFantasyPoints(), offense.getFantasyPointsPPR(),
                offense.getFantasyPosition(), offense.getPlayerGameID(), offense.getExtraPointsAttempted(),
                offense.getFantasyPointsFanDuel(), offense.getFieldGoalsMade0to19(), offense.getFieldGoalsMade20to29(),
                offense.getFieldGoalsMade30to39(), offense.getFieldGoalsMade40to49(), offense.getFieldGoalsMade50Plus(),
                offense.getFantasyPointsDraftKings(), offense.getInjuryStatus(), offense.getTeamID(),
                offense.getOpponentID(), offense.getScoreID(), offense.isSnapCountsConfirmed()

        };
    }
}

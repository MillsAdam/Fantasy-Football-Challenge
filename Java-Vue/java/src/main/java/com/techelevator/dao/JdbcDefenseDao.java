package com.techelevator.dao;


import com.techelevator.model.Defense;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

@Component
public class JdbcDefenseDao implements DefenseDao {
    private JdbcTemplate jdbcTemplate;
    public JdbcDefenseDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    private String generateSql(String tableName) {
        return "INSERT INTO " + tableName +
                " (GameKey, SeasonType, Season, Week, Date, Team, Opponent, PointsAllowed, " +
                "TouchdownsScored, Sacks, SackYards, FumblesForced, FumblesRecovered, " +
                "FumbleReturnTouchdowns, Interceptions, InterceptionReturnTouchdowns, BlockedKicks, " +
                "Safeties, PuntReturnTouchdowns, KickReturnTouchdowns, BlockedKickReturnTouchdowns, " +
                "FieldGoalReturnTouchdowns, QuarterbackHits, TacklesForLoss, DefensiveTouchdowns, " +
                "SpecialTeamsTouchdowns, FantasyPoints, PointsAllowedByDefenseSpecialTeams, " +
                "TwoPointConversionReturns, FantasyPointsFanDuel, FantasyPointsDraftKings, PlayerID, " +
                "HomeOrAway, TeamID, OpponentID, ScoreID) " +
                "VALUES " +
                "(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
    }

    @Override
    public void addDefenseStats(Defense defense) {
        String sql = generateSql("defense_stats");
        jdbcTemplate.update(sql, createObjectArray(defense));
    }

    @Override
    public void addDefenseProj(Defense defense) {
        String sql = generateSql("defense_proj");
        jdbcTemplate.update(sql, createObjectArray(defense));
    }

    private Object[] createObjectArray(Defense defense) {
        return new Object[] {
                defense.getGameKey(), defense.getSeasonType(), defense.getSeason(),
                defense.getWeek(), defense.getDate(), defense.getTeam(),
                defense.getOpponent(), defense.getPointsAllowed(), defense.getTouchdownsScored(),
                defense.getSacks(), defense.getSackYards(), defense.getFumblesForced(),
                defense.getFumblesRecovered(), defense.getFumbleReturnTouchdowns(), defense.getInterceptions(),
                defense.getInterceptionReturnTouchdowns(), defense.getBlockedKicks(), defense.getSafeties(),
                defense.getPuntReturnTouchdowns(), defense.getKickReturnTouchdowns(), defense.getBlockedKickReturnTouchdowns(),
                defense.getFieldGoalReturnTouchdowns(), defense.getQuarterbackHits(), defense.getTacklesForLoss(),
                defense.getDefensiveTouchdowns(), defense.getSpecialTeamsTouchdowns(), defense.getFantasyPoints(),
                defense.getPointsAllowedByDefenseSpecialTeams(), defense.getTwoPointConversionReturns(), defense.getFantasyPointsFanDuel(),
                defense.getFantasyPointsDraftKings(), defense.getPlayerID(), defense.getHomeOrAway(),
                defense.getTeamID(), defense.getOpponentID(), defense.getScoreID()
        };
    }
}

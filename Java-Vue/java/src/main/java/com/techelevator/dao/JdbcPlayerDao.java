package com.techelevator.dao;

import com.techelevator.model.Player;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

@Component
public class JdbcPlayerDao implements PlayerDao{
    private JdbcTemplate jdbcTemplate;
    public JdbcPlayerDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    private String generateSql(String tableName) {
        return "INSERT INTO " + tableName +
                " (PlayerID, Team, Number, FirstName, " +
                "LastName, Position, Status, Height, " +
                "Weight, BirthDate, College, Experience, " +
                "FantasyPosition, PositionCategory, PhotoUrl, ByeWeek, " +
                "AverageDraftPosition, CollegeDraftTeam, CollegeDraftYear, CollegeDraftRound, " +
                "CollegeDraftPick, IsUndraftedFreeAgent, FanDuelPlayerID, DraftKingsPlayerID, " +
                "InjuryStatus, FanDuelName, DraftKingsName, TeamID) " +
                "VALUES " +
                "(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
    }

    @Override
    public void addPlayers(Player player) {
        String sql = generateSql("players");
        jdbcTemplate.update(sql, createObjectArray(player));
    }

    private Object[] createObjectArray(Player player) {
        return new Object[] {
                player.getPlayerID(), player.getTeam(), player.getNumber(),
                player.getFirstName(), player.getLastName(), player.getPosition(),
                player.getStatus(), player.getHeight(), player.getWeight(),
                player.getBirthDate(), player.getCollege(), player.getExperience(),
                player.getFantasyPosition(), player.getPositionCategory(), player.getPhotoUrl(),
                player.getByeWeek(), player.getAverageDraftPosition(), player.getCollegeDraftTeam(),
                player.getCollegeDraftYear(), player.getCollegeDraftRound(), player.getCollegeDraftPick(),
                player.isUndraftedFreeAgent(), player.getFanDuelPlayerID(), player.getDraftKingsPlayerID(),
                player.getInjuryStatus(), player.getFanDuelName(), player.getDraftKingsName(),
                player.getTeamID()
        };
    }
}

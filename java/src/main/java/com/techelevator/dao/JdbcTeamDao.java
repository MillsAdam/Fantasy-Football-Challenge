package com.techelevator.dao;

import com.techelevator.model.Team;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

@Component
public class JdbcTeamDao implements TeamDao{
    private JdbcTemplate jdbcTemplate;
    public JdbcTeamDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    private String generateSql(String tableName) {
        return "INSERT INTO " + tableName +
                " (Key, TeamID, PlayerID, City, " +
                "Name, Conference, Division, FullName, " +
                "StadiumID, ByeWeek, AverageDraftPosition, AverageDraftPositionPPR, " +
                "PrimaryColor, SecondaryColor, TertiaryColor, QuaternaryColor, " +
                "WikipediaLogoUrl, WikipediaWordMarkUrl, DraftKingsName, DraftKingsPlayerID, " +
                "FanDuelName, FanDuelPlayerID, AverageDraftPosition2QB, AverageDraftPositionDynasty) " +
                "VALUES " +
                "(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, " +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
    }
    @Override
    public void addTeams(Team team) {
        String sql = generateSql("teams_2022");
        jdbcTemplate.update(sql, createObjectArray(team));
    }

    private Object[] createObjectArray(Team team) {
        return new Object[] {
                team.getKey(), team.getTeamID(), team.getPlayerID(),
                team.getCity(), team.getName(), team.getConference(),
                team.getDivision(), team.getFullName(), team.getStadiumID(),
                team.getByeWeek(), team.getAverageDraftPosition(), team.getAverageDraftPositionPPR(),
                team.getPrimaryColor(), team.getSecondaryColor(), team.getTertiaryColor(),
                team.getQuaternaryColor(), team.getWikipediaLogoUrl(), team.getWikipediaWordMarkUrl(),
                team.getDraftKingsName(), team.getDraftKingsPlayerID(), team.getFanDuelName(),
                team.getFanDuelPlayerID(), team.getAverageDraftPosition2QB(), team.getAverageDraftPositionDynasty()
        };
    }
}

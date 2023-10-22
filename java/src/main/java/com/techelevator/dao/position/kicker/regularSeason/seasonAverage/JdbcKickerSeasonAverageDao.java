package com.techelevator.dao.position.kicker.regularSeason.seasonAverage;

import com.techelevator.exception.DaoException;
import com.techelevator.model.position.KickerDto;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

@Component
public class JdbcKickerSeasonAverageDao implements KickerSeasonAverageDao{
    private final JdbcTemplate jdbcTemplate;
    DecimalFormat decimalFormat = new DecimalFormat("#.0");


    public JdbcKickerSeasonAverageDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    private static final String SELECT_SQL =
            "SELECT " +
                "o.PlayerID, " +
                "COUNT(DISTINCT o.Week) AS Week, " +
                "o.Team, " +
                "o.Position, " +
                "o.Name, " +
                "AVG(o.FieldGoalsMade) AS FieldGoalsMade, " +
                "AVG(o.FieldGoalsAttempted) AS FieldGoalsAttempted, " +
                "CASE " +
                    "WHEN AVG(o.FieldGoalsAttempted) = 0 THEN 0 " +
                    "ELSE AVG(o.FieldGoalsMade) / AVG(o.FieldGoalsAttempted) * 100.0 " +
                "END AS FieldGoalPercentage, " +
                "AVG(o.FieldGoalsMade0to19) AS FieldGoalsMade0to19, " +
                "AVG(o.FieldGoalsMade20to29) AS FieldGoalsMade20to29, " +
                "AVG(o.FieldGoalsMade30to39) AS FieldGoalsMade30to39, " +
                "AVG(o.FieldGoalsMade40to49) AS FieldGoalsMade40to49, " +
                "AVG(o.FieldGoalsMade50Plus) AS FieldGoalsMade50Plus, " +
                "AVG(o.ExtraPointsMade) AS ExtraPointsMade, " +
                "AVG(o.ExtraPointsAttempted) AS ExtraPointsAttempted, " +
                "CASE " +
                    "WHEN AVG(o.ExtraPointsAttempted) = 0 THEN 0 " +
                    "ELSE AVG(o.ExtraPointsMade) / AVG(o.ExtraPointsAttempted) * 100.0" +
                "END AS ExtraPointPercentage, " +
                "AVG(o.FantasyPointsPPR) AS TotalFantasyPoints, " +
                "AVG(o.FantasyPointsPPR) AS AvgFantasyPoints " +
            "FROM offense_stats_2022 AS o " +
            "JOIN teams_2022 AS t ON o.TeamID = t.TeamID " +
            "WHERE o.Position = 'K' " +
                "AND o.PositionCategory = 'ST' " +
                "AND o.SeasonType = 1 ";
    private static final String GROUP_SQL =
            "GROUP BY o.PlayerID, o.Team, o.Position, o.Name " +
            "ORDER BY TotalFantasyPoints DESC;";
    private static final String CONFERENCE_SQL = "AND lower(t.Conference) ILIKE ? ";
    private static final String TEAM_SQL = "AND lower(o.Team) ILIKE ? ";
    private static final String NAME_SQL = "AND lower(o.Name) ILIKE ? ";



    @Override
    public List<KickerDto> getKickerSeasonAverageStats() {
        List<KickerDto> kickerDtoList = new ArrayList<>();
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + GROUP_SQL);
            while (results.next()) {
                KickerDto kickerDto = mapRowToKickerDto(results);
                kickerDtoList.add(kickerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }
        return kickerDtoList;
    }

    @Override
    public List<KickerDto> getKickerSeasonAverageStatsByConference(String searchTerm) {
        List<KickerDto> kickerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + CONFERENCE_SQL + GROUP_SQL,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                KickerDto kickerDto = mapRowToKickerDto(results);
                kickerDtoList.add(kickerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return kickerDtoList;
    }

    @Override
    public List<KickerDto> getKickerSeasonAverageStatsByTeam(String searchTerm) {
        List<KickerDto> kickerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + TEAM_SQL + GROUP_SQL,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                KickerDto kickerDto = mapRowToKickerDto(results);
                kickerDtoList.add(kickerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return kickerDtoList;
    }

    @Override
    public List<KickerDto> getKickerSeasonAverageStatsByName(String searchTerm) {
        List<KickerDto> kickerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + NAME_SQL + GROUP_SQL,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                KickerDto kickerDto = mapRowToKickerDto(results);
                kickerDtoList.add(kickerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return kickerDtoList;
    }


    private KickerDto mapRowToKickerDto(SqlRowSet rs) {
        KickerDto kickerDto = new KickerDto();
        kickerDto.setPlayerID(rs.getInt("PlayerID"));
        kickerDto.setWeek(rs.getInt("Week"));
        kickerDto.setTeam(rs.getString("Team"));
        kickerDto.setPosition(rs.getString("Position"));
        kickerDto.setName(rs.getString("Name"));
        kickerDto.setFieldGoalsMade(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsMade"))));
        kickerDto.setFieldGoalsAttempted(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsAttempted"))));
        kickerDto.setFieldGoalPercentage(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalPercentage"))));
        kickerDto.setFieldGoalsMade0to19(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsMade0to19"))));
        kickerDto.setFieldGoalsMade20to29(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsMade20to29"))));
        kickerDto.setFieldGoalsMade30to39(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsMade30to39"))));
        kickerDto.setFieldGoalsMade40to49(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsMade40to49"))));
        kickerDto.setFieldGoalsMade50Plus(Double.parseDouble(decimalFormat.format(rs.getDouble("FieldGoalsMade50Plus"))));
        kickerDto.setExtraPointsMade(Double.parseDouble(decimalFormat.format(rs.getDouble("ExtraPointsMade"))));
        kickerDto.setExtraPointsAttempted(Double.parseDouble(decimalFormat.format(rs.getDouble("ExtraPointsAttempted"))));
        kickerDto.setExtraPointPercentage(Double.parseDouble(decimalFormat.format(rs.getDouble("ExtraPointPercentage"))));
        kickerDto.setFantasyPointsTotal(Double.parseDouble(decimalFormat.format(rs.getDouble("TotalFantasyPoints"))));
        kickerDto.setFantasyPointsAverage(Double.parseDouble(decimalFormat.format(rs.getDouble("AvgFantasyPoints"))));
        return kickerDto;
    }
}

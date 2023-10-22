package com.techelevator.dao.position.quarterback.regularSeason.weeklyProjected;

import com.techelevator.exception.DaoException;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

@Component
public class JdbcQuarterbackWeeklyProjectedDao implements QuarterbackWeeklyProjectedDao {
    private final JdbcTemplate jdbcTemplate;
    DecimalFormat decimalFormat = new DecimalFormat("#.0");


    public JdbcQuarterbackWeeklyProjectedDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    private static final String SELECT_SQL =
            "SELECT " +
                "o.PlayerID, " +
                "o.Week, " +
                "o.Team, " +
                "o.Position, " +
                "o.Name, " +
                "SUM(o.PassingCompletions) AS PassingCompletions, " +
                "SUM(o.PassingAttempts) AS PassingAttempts, " +
                "CASE " +
                    "WHEN SUM(o.PassingAttempts) = 0 THEN 0 " +
                    "ELSE SUM(o.PassingCompletions) * 100.0 / SUM(o.PassingAttempts) " +
                "END AS PassingCompletionPercentage, " +
                "SUM(o.PassingYards) AS PassingYards, " +
                "SUM(o.PassingTouchdowns) AS PassingTouchdowns, " +
                "SUM(o.PassingInterceptions) AS PassingInterceptions, " +
                "AVG(o.PassingRating) AS PassingRating, " +
                "SUM(o.RushingAttempts) AS RushingAttempts, " +
                "SUM(o.RushingYards) AS RushingYards, " +
                "SUM(o.RushingTouchdowns) AS RushingTouchdowns, " +
                "SUM(o.TwoPointConversionPasses + o.TwoPointConversionRuns + o.TwoPointConversionReceptions) AS TwoPointConversions, " +
                "SUM(o.FumblesLost) AS FumblesLost, " +
                "SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, " +
                "AVG(o.FantasyPointsPPR) AS AvgFantasyPoints " +
            "FROM offense_proj_2022 AS o " +
            "JOIN teams_2022 AS t ON o.TeamID = t.TeamID " +
            "WHERE o.Position = 'QB' " +
                "AND o.PositionCategory = 'OFF' " +
                "AND o.SeasonType = 1 " +
                "AND o.Week = ? ";
    private static final String GROUP_SQL =
            "GROUP BY o.PlayerID, o.Week, o.Team, o.Position, o.Name " +
            "ORDER BY TotalFantasyPoints DESC;";
    private static final String CONFERENCE_SQL = "AND lower(t.Conference) ILIKE ? ";
    private static final String TEAM_SQL = "AND lower(o.Team) ILIKE ? ";
    private static final String NAME_SQL = "AND lower(o.Name) ILIKE ? ";


    @Override
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStats(int searchWeek) {
        List<QuarterbackDto> quarterbackDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + GROUP_SQL,
                    searchWeek);
            while (results.next()) {
                QuarterbackDto quarterbackDto = mapRowToQuarterbackDto(results);
                quarterbackDtoList.add(quarterbackDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return quarterbackDtoList;
    }

    @Override
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByConference(int searchWeek, String searchTerm) {
        List<QuarterbackDto> quarterbackDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + CONFERENCE_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                QuarterbackDto quarterbackDto = mapRowToQuarterbackDto(results);
                quarterbackDtoList.add(quarterbackDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return quarterbackDtoList;
    }

    @Override
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm) {
        List<QuarterbackDto> quarterbackDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + TEAM_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                QuarterbackDto quarterbackDto = mapRowToQuarterbackDto(results);
                quarterbackDtoList.add(quarterbackDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return quarterbackDtoList;
    }

    @Override
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByName(int searchWeek, String searchTerm) {
        List<QuarterbackDto> quarterbackDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + NAME_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                QuarterbackDto quarterbackDto = mapRowToQuarterbackDto(results);
                quarterbackDtoList.add(quarterbackDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return quarterbackDtoList;
    }


    private QuarterbackDto mapRowToQuarterbackDto(SqlRowSet rs) {
        QuarterbackDto quarterbackDto = new QuarterbackDto();
        quarterbackDto.setPlayerID(rs.getInt("PlayerID"));
        quarterbackDto.setWeek(rs.getInt("Week"));
        quarterbackDto.setTeam(rs.getString("Team"));
        quarterbackDto.setPosition(rs.getString("Position"));
        quarterbackDto.setName(rs.getString("Name"));
        quarterbackDto.setPassingCompletions(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingCompletions"))));
        quarterbackDto.setPassingAttempts(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingAttempts"))));
        quarterbackDto.setPassingCompletionPercentage(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingCompletionPercentage"))));
        quarterbackDto.setPassingYards(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingYards"))));
        quarterbackDto.setPassingTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingTouchdowns"))));
        quarterbackDto.setPassingInterceptions(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingInterceptions"))));
        quarterbackDto.setPassingRating(Double.parseDouble(decimalFormat.format(rs.getDouble("PassingRating"))));
        quarterbackDto.setRushingAttempts(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingAttempts"))));
        quarterbackDto.setRushingYards(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingYards"))));
        quarterbackDto.setRushingTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingTouchdowns"))));
        quarterbackDto.setTwoPointConversions(Double.parseDouble(decimalFormat.format(rs.getDouble("TwoPointConversions"))));
        quarterbackDto.setFumblesLost(Double.parseDouble(decimalFormat.format(rs.getDouble("FumblesLost"))));
        quarterbackDto.setFantasyPointsTotal(Double.parseDouble(decimalFormat.format(rs.getDouble("TotalFantasyPoints"))));
        quarterbackDto.setFantasyPointsAverage(Double.parseDouble(decimalFormat.format(rs.getDouble("AvgFantasyPoints"))));
        return quarterbackDto;
    }
}

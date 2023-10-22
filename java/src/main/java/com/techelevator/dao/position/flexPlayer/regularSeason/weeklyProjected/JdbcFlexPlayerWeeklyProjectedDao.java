package com.techelevator.dao.position.flexPlayer.regularSeason.weeklyProjected;

import com.techelevator.exception.DaoException;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

@Component
public class JdbcFlexPlayerWeeklyProjectedDao implements FlexPlayerWeeklyProjectedDao {
    private final JdbcTemplate jdbcTemplate;
    DecimalFormat decimalFormat = new DecimalFormat("#.0");


    public JdbcFlexPlayerWeeklyProjectedDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    private static final String SELECT_SQL =
            "WITH PlayerTotals AS (" +
                "SELECT " +
                    "Week, " +
                    "Team, " +
                    "Position, " +
                    "SUM(RushingAttempts + ReceivingTargets) AS TotalTouches " +
                "FROM offense_proj_2022 " +
                "WHERE Position IN ('RB', 'WR', 'TE') " +
                    "AND Week = ? " +
                    "AND PositionCategory = 'OFF' " +
                    "AND SeasonType = 1 " +
                "GROUP BY Week, Team, Position" +
            ") " +
            "SELECT " +
                "o.PlayerID, " +
                "o.Week, " +
                "o.Team, " +
                "o.Position, " +
                "o.Name, " +
                "SUM(o.RushingAttempts) AS RushingAttempts, " +
                "SUM(o.RushingYards) AS RushingYards, " +
                "CASE " +
                    "WHEN SUM(o.RushingAttempts) = 0 THEN 0 " +
                    "ELSE SUM(o.RushingYards) / SUM(o.RushingAttempts) " +
                "END AS RushingYardsPerAttempt, " +
                "SUM(o.RushingTouchdowns) AS RushingTouchdowns, " +
                "SUM(o.ReceivingTargets) AS ReceivingTargets, " +
                "SUM(o.Receptions) AS Receptions, " +
                "SUM(o.ReceivingYards) AS ReceivingYards, " +
                "CASE " +
                    "WHEN SUM(o.Receptions) = 0 THEN 0 " +
                    "ELSE SUM(o.ReceivingYards) / SUM(o.Receptions) " +
                "END AS ReceivingYardsPerReception, " +
                "SUM(o.ReceivingTouchdowns) AS ReceivingTouchdowns, " +
                "SUM(o.PuntReturnTouchdowns + o.KickReturnTouchdowns) AS ReturnTouchdowns, " +
                "SUM(o.TwoPointConversionPasses + o.TwoPointConversionRuns + o.TwoPointConversionReceptions) AS TwoPointConversions, " +
                "CASE " +
                    "WHEN pt.TotalTouches = 0 THEN 0 " +
                    "ELSE SUM(o.RushingAttempts + o.ReceivingTargets) * 100.0 / pt.TotalTouches " +
                "END AS Usage, " +
                "SUM(o.FumblesLost) AS FumblesLost, " +
                "SUM(o.FantasyPointsPPR) AS TotalFantasyPoints, " +
                "AVG(o.FantasyPointsPPR) AS AvgFantasyPoints " +
            "FROM offense_proj_2022 AS o " +
            "JOIN PlayerTotals AS pt ON o.Week = pt.Week AND o.Team = pt.Team AND o.Position = pt.Position " +
            "JOIN teams_2022 AS t ON o.TeamID = t.TeamID " +
            "WHERE o.PositionCategory = 'OFF' " +
                "AND o.SeasonType = 1 ";
    private static final String GROUP_SQL =
            "GROUP BY o.PlayerID, o.Week, o.Team, o.Position, o.Name, pt.TotalTouches " +
            "ORDER BY TotalFantasyPoints DESC;";
    private static final String FLEX_SQL = "AND o.Position IN ('RB', 'WR', 'TE') ";
    private static final String POSITION_SQL = "AND lower(o.Position) ILIKE ? ";
    private static final String CONFERENCE_SQL = "AND lower(t.Conference) ILIKE ? ";
    private static final String TEAM_SQL = "AND lower(o.Team) ILIKE ? ";
    private static final String NAME_SQL = "AND lower(o.Name) ILIKE ? ";



    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStats(int searchWeek) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + GROUP_SQL,
                    searchWeek);
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }
        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByConference(int searchWeek, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + CONFERENCE_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByName(int searchWeek, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPosition(int searchWeek, String searchPosition) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchPosition.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }
        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + CONFERENCE_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchPosition.toLowerCase() + "%",
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchPosition.toLowerCase() + "%",
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return flexPlayerDtoList;
    }

    @Override
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_SQL,
                    searchWeek,
                    "%" + searchPosition.toLowerCase() + "%",
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                FlexPlayerDto flexPlayerDto = mapRowToFlexPlayerDto(results);
                flexPlayerDtoList.add(flexPlayerDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return flexPlayerDtoList;
    }


    private FlexPlayerDto mapRowToFlexPlayerDto(SqlRowSet rs) {
        FlexPlayerDto flexPlayerDto = new FlexPlayerDto();
        flexPlayerDto.setPlayerID(rs.getInt("PlayerID"));
        flexPlayerDto.setWeek(rs.getInt("Week"));
        flexPlayerDto.setTeam(rs.getString("Team"));
        flexPlayerDto.setPosition(rs.getString("Position"));
        flexPlayerDto.setName(rs.getString("Name"));
        flexPlayerDto.setRushingAttempts(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingAttempts"))));
        flexPlayerDto.setRushingYards(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingYards"))));
        flexPlayerDto.setRushingYardsPerAttempt(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingYardsPerAttempt"))));
        flexPlayerDto.setRushingTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("RushingTouchdowns"))));
        flexPlayerDto.setReceivingTargets(Double.parseDouble(decimalFormat.format(rs.getDouble("ReceivingTargets"))));
        flexPlayerDto.setReceptions(Double.parseDouble(decimalFormat.format(rs.getDouble("Receptions"))));
        flexPlayerDto.setReceivingYards(Double.parseDouble(decimalFormat.format(rs.getDouble("ReceivingYards"))));
        flexPlayerDto.setReceivingYardsPerReception(Double.parseDouble(decimalFormat.format(rs.getDouble("ReceivingYardsPerReception"))));
        flexPlayerDto.setReceivingTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("ReceivingTouchdowns"))));
        flexPlayerDto.setReturnTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("ReturnTouchdowns"))));
        flexPlayerDto.setTwoPointConversions(Double.parseDouble(decimalFormat.format(rs.getDouble("TwoPointConversions"))));
        flexPlayerDto.setUsage(Double.parseDouble(decimalFormat.format(rs.getDouble("Usage"))));
        flexPlayerDto.setFumblesLost(Double.parseDouble(decimalFormat.format(rs.getDouble("FumblesLost"))));
        flexPlayerDto.setFantasyPointsTotal(Double.parseDouble(decimalFormat.format(rs.getDouble("TotalFantasyPoints"))));
        flexPlayerDto.setFantasyPointsAverage(Double.parseDouble(decimalFormat.format(rs.getDouble("AvgFantasyPoints"))));
        return flexPlayerDto;
    }
}

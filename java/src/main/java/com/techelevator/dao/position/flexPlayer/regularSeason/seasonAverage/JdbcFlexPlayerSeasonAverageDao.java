package com.techelevator.dao.position.flexPlayer.regularSeason.seasonAverage;

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
public class JdbcFlexPlayerSeasonAverageDao implements FlexPlayerSeasonAverageDao {
    private final JdbcTemplate jdbcTemplate;
    DecimalFormat decimalFormat = new DecimalFormat("#.00");


    public JdbcFlexPlayerSeasonAverageDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }



    private static final String SELECT_SQL =
            "WITH PlayerTotals AS (" +
                    "SELECT " +
                        "Team, " +
                        "Position, " +
                        "SUM(AvgTotalTouches) AS TotalTouches " +
                    "FROM (" +
                        "SELECT " +
                            "PlayerID, " +
                            "Team, " +
                            "Position, " +
                            "AVG(RushingAttempts + ReceivingTargets) AS AvgTotalTouches " +
                        "FROM offense_stats_2022 " +
                        "WHERE Position IN ('RB', 'WR', 'TE') " +
                            "AND PositionCategory = 'OFF' " +
                            "AND SeasonType = 1 " +
                        "GROUP BY PlayerID, Team, Position" +
                    ") AS Subquery " +
                    "GROUP BY Team, Position" +
            ") " +
            "SELECT " +
                "o.PlayerID, " +
                "COUNT(DISTINCT o.Week) AS Week, " +
                "o.Team, " +
                "o.Position, " +
                "o.Name, " +
                "AVG(o.RushingAttempts) AS RushingAttempts, " +
                "AVG(o.RushingYards) AS RushingYards, " +
                "CASE " +
                    "WHEN AVG(o.RushingAttempts) = 0 THEN 0 " +
                    "ELSE AVG(o.RushingYards) / AVG(o.RushingAttempts) " +
                "END AS RushingYardsPerAttempt, " +
                "AVG(o.RushingTouchdowns) AS RushingTouchdowns, " +
                "AVG(o.ReceivingTargets) AS ReceivingTargets, " +
                "AVG(o.Receptions) AS Receptions, " +
                "AVG(o.ReceivingYards) AS ReceivingYards, " +
                "CASE " +
                    "WHEN AVG(o.Receptions) = 0 THEN 0 " +
                    "ELSE AVG(o.ReceivingYards) / AVG(o.Receptions) " +
                "END AS ReceivingYardsPerReception, " +
                "AVG(o.ReceivingTouchdowns) AS ReceivingTouchdowns, " +
                "AVG(o.PuntReturnTouchdowns + o.KickReturnTouchdowns) AS ReturnTouchdowns, " +
                "AVG(o.TwoPointConversionPasses + o.TwoPointConversionRuns + o.TwoPointConversionReceptions) AS TwoPointConversions, " +
                "AVG(o.RushingAttempts + o.ReceivingTargets) * 100.0 / AVG(TotalTouches) AS Usage, " +
                "AVG(o.FumblesLost) AS FumblesLost, " +
                "AVG(o.FantasyPointsPPR) AS TotalFantasyPoints, " +
                "AVG(o.FantasyPointsPPR) AS AvgFantasyPoints " +
            "FROM offense_stats_2022 AS o " +
            "JOIN PlayerTotals AS pt ON o.Team = pt.Team AND o.Position = pt.Position " +
            "JOIN teams_2022 AS t ON o.TeamID = t.TeamID " +
            "WHERE o.PositionCategory = 'OFF' " +
                "AND o.SeasonType = 1 ";
    private static final String GROUP_SQL =
            "GROUP BY o.PlayerID, o.Team, o.Position, o.Name " +
            "ORDER BY AvgFantasyPoints DESC;";
    private static final String FLEX_SQL = "AND o.Position IN ('RB', 'WR', 'TE') ";
    private static final String POSITION_SQL = "AND lower(o.Position) ILIKE ? ";
    private static final String CONFERENCE_SQL = "AND lower(t.Conference) ILIKE ? ";
    private static final String TEAM_SQL = "AND lower(o.Team) ILIKE ? ";
    private static final String NAME_SQL = "AND lower(o.Name) ILIKE ? ";


    @Override
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStats() {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + GROUP_SQL);
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByConference(String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + CONFERENCE_SQL + GROUP_SQL,
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByTeam(String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + TEAM_SQL + GROUP_SQL,
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsName(String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + FLEX_SQL + NAME_SQL + GROUP_SQL,
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPosition(String searchPosition) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + GROUP_SQL,
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndConference(String searchPosition, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + CONFERENCE_SQL + GROUP_SQL,
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndTeam(String searchPosition, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + TEAM_SQL + GROUP_SQL,
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
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndName(String searchPosition, String searchTerm) {
        List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + POSITION_SQL + NAME_SQL + GROUP_SQL,
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

package com.techelevator.dao.position.defense.regularSeason.seasonAverage;

import com.techelevator.exception.DaoException;
import com.techelevator.model.position.DefenseDto;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@Component
public class JdbcDefenseSeasonAverageDao implements DefenseSeasonAverageDao{
    private final JdbcTemplate jdbcTemplate;
    DecimalFormat decimalFormat = new DecimalFormat("#.00");


    public JdbcDefenseSeasonAverageDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    private static final String SELECT_SQL =
            "SELECT " +
                "d.PlayerID, " +
                "COUNT(DISTINCT d.Week) AS Week, " +
                "d.Team, " +
                "t.FullName AS Name, " +
                "AVG(d.DefensiveTouchdowns) AS DefensiveTouchdowns, " +
                "AVG(d.SpecialTeamsTouchdowns) AS SpecialTeamsTouchdowns, " +
                "AVG(d.TouchdownsScored) AS TouchdownsScored, " +
                "AVG(d.FumblesForced) AS FumblesForced, " +
                "AVG(d.FumblesRecovered) AS FumblesRecovered, " +
                "AVG(d.Interceptions) AS Interceptions, " +
                "AVG(d.TacklesForLoss) AS TacklesForLoss, " +
                "AVG(d.QuarterbackHits) AS QuarterbackHits, " +
                "AVG(d.Sacks) AS Sacks, " +
                "AVG(d.Safeties) AS Safeties, " +
                "AVG(d.BlockedKicks) AS BlockedKicks, " +
                "AVG(d.PointsAllowed) AS PointsAllowed, " +
                "AVG(d.FantasyPoints) AS TotalFantasyPoints, " +
                "AVG(d.FantasyPoints) AS AvgFantasyPoints " +
            "FROM defense_stats_2022 AS d " +
            "JOIN teams_2022 AS t ON d.TeamID = t.TeamID " +
            "WHERE SeasonType = 1 ";
    private static final String GROUP_SQL =
            "GROUP BY d.PlayerID, d.Team, t.FullName " +
            "ORDER BY TotalFantasyPoints DESC;";
    private static final String CONFERENCE_SQL = "AND lower(t.Conference) ILIKE ? ";
    private static final String TEAM_SQL = "AND lower(d.Team) ILIKE ? ";
    private static final String NAME_SQL = "AND lower(t.FullName) ILIKE ? ";



    @Override
    public List<DefenseDto> getDefenseSeasonAverageStats() {
        List<DefenseDto> defenseDtoList = new ArrayList<>();
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + GROUP_SQL);
            while (results.next()) {
                DefenseDto defenseDto = mapRowToDefenseDto(results);
                defenseDtoList.add(defenseDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }
        return defenseDtoList;
    }

    @Override
    public List<DefenseDto> getDefenseSeasonAverageStatsByConference(String searchTerm) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + CONFERENCE_SQL + GROUP_SQL,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                DefenseDto defenseDto = mapRowToDefenseDto(results);
                defenseDtoList.add(defenseDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }


        return defenseDtoList;
    }

    @Override
    public List<DefenseDto> getDefenseSeasonAverageStatsByTeam(String searchTerm) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + TEAM_SQL + GROUP_SQL,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                DefenseDto defenseDto = mapRowToDefenseDto(results);
                defenseDtoList.add(defenseDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return defenseDtoList;
    }

    @Override
    public List<DefenseDto> getDefenseSeasonAverageStatsByName(String searchTerm) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + NAME_SQL + GROUP_SQL,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                DefenseDto defenseDto = mapRowToDefenseDto(results);
                defenseDtoList.add(defenseDto);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }

        return defenseDtoList;
    }


    private DefenseDto mapRowToDefenseDto(SqlRowSet rs) {
        DefenseDto defenseDto = new DefenseDto();
        defenseDto.setPlayerID(rs.getInt("PlayerID"));
        defenseDto.setWeek(rs.getInt("Week"));
        defenseDto.setTeam(rs.getString("Team"));
        defenseDto.getPosition();
        defenseDto.setName(rs.getString("Name"));
        defenseDto.setDefensiveTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("DefensiveTouchdowns"))));
        defenseDto.setSpecialTeamsTouchdowns(Double.parseDouble(decimalFormat.format(rs.getDouble("SpecialTeamsTouchdowns"))));
        defenseDto.setTouchdownsScored(Double.parseDouble(decimalFormat.format(rs.getDouble("TouchdownsScored"))));
        defenseDto.setFumblesForced(Double.parseDouble(decimalFormat.format(rs.getDouble("FumblesForced"))));
        defenseDto.setFumblesRecovered(Double.parseDouble(decimalFormat.format(rs.getDouble("FumblesRecovered"))));
        defenseDto.setInterceptions(Double.parseDouble(decimalFormat.format(rs.getDouble("Interceptions"))));
        defenseDto.setTacklesForLoss(Double.parseDouble(decimalFormat.format(rs.getDouble("TacklesForLoss"))));
        defenseDto.setQuarterbackHits(Double.parseDouble(decimalFormat.format(rs.getDouble("QuarterbackHits"))));
        defenseDto.setSacks(Double.parseDouble(decimalFormat.format(rs.getDouble("Sacks"))));
        defenseDto.setSafeties(Double.parseDouble(decimalFormat.format(rs.getDouble("Safeties"))));
        defenseDto.setBlockedKicks(Double.parseDouble(decimalFormat.format(rs.getDouble("BlockedKicks"))));
        defenseDto.setPointsAllowed(Double.parseDouble(decimalFormat.format(rs.getDouble("PointsAllowed"))));
        defenseDto.setFantasyPointsTotal(Double.parseDouble(decimalFormat.format(rs.getDouble("TotalFantasyPoints"))));
        defenseDto.setFantasyPointsAverage(Double.parseDouble(decimalFormat.format(rs.getDouble("AvgFantasyPoints"))));
        return defenseDto;
    }
}

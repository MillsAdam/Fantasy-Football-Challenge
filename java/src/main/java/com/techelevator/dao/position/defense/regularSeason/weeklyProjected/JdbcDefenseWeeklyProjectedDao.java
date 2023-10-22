package com.techelevator.dao.position.defense.regularSeason.weeklyProjected;

import com.techelevator.exception.DaoException;
import com.techelevator.model.position.DefenseDto;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

@Component
public class JdbcDefenseWeeklyProjectedDao implements DefenseWeeklyProjectedDao {
    private final JdbcTemplate jdbcTemplate;
    DecimalFormat decimalFormat = new DecimalFormat("#.0");


    public JdbcDefenseWeeklyProjectedDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    private static final String SELECT_SQL =
            "SELECT " +
                "d.PlayerID, " +
                "d.Week, " +
                "d.Team, " +
                "t.FullName AS Name, " +
                "SUM(d.DefensiveTouchdowns) AS DefensiveTouchdowns, " +
                "SUM(d.SpecialTeamsTouchdowns) AS SpecialTeamsTouchdowns, " +
                "SUM(d.TouchdownsScored) AS TouchdownsScored, " +
                "SUM(d.FumblesForced) AS FumblesForced, " +
                "SUM(d.FumblesRecovered) AS FumblesRecovered, " +
                "SUM(d.Interceptions) AS Interceptions, " +
                "SUM(d.TacklesForLoss) AS TacklesForLoss, " +
                "SUM(d.QuarterbackHits) AS QuarterbackHits, " +
                "SUM(d.Sacks) AS Sacks, " +
                "SUM(d.Safeties) AS Safeties, " +
                "SUM(d.BlockedKicks) AS BlockedKicks, " +
                "SUM(d.PointsAllowed) AS PointsAllowed, " +
                "SUM(d.FantasyPoints) AS TotalFantasyPoints, " +
                "AVG(d.FantasyPoints) AS AvgFantasyPoints " +
            "FROM defense_proj_2022 AS d " +
            "JOIN teams_2022 AS t ON d.TeamID = t.TeamID " +
            "WHERE SeasonType = 1 " +
            "AND d.Week = ? ";
    private static final String GROUP_SQL =
            "GROUP BY d.PlayerID, d.Week, d.Team, t.FullName " +
            "ORDER BY TotalFantasyPoints DESC;";
    private static final String CONFERENCE_SQL = "AND lower(t.Conference) ILIKE ? ";
    private static final String TEAM_SQL = "AND lower(d.Team) ILIKE ? ";
    private static final String NAME_SQL = "AND lower(t.FullName) ILIKE ? ";



    @Override
    public List<DefenseDto> getDefenseWeeklyProjectedStats(int searchWeek) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + GROUP_SQL,
                    searchWeek);
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
    public List<DefenseDto> getDefenseWeeklyProjectedStatsByConference(int searchWeek, String searchTerm) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + CONFERENCE_SQL + GROUP_SQL,
                    searchWeek,
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
    public List<DefenseDto> getDefenseWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + TEAM_SQL + GROUP_SQL,
                    searchWeek,
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
    public List<DefenseDto> getDefenseWeeklyProjectedStatsByName(int searchWeek, String searchTerm) {
        List<DefenseDto> defenseDtoList = new ArrayList<>();

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(SELECT_SQL + NAME_SQL + GROUP_SQL,
                    searchWeek,
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

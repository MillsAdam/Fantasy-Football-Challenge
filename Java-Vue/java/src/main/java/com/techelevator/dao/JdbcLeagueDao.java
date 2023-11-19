package com.techelevator.dao;

import com.techelevator.exception.DaoException;
import com.techelevator.model.League;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.sql.Timestamp;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

@Component
public class JdbcLeagueDao implements LeagueDao{
    private JdbcTemplate jdbcTemplate;
    public JdbcLeagueDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    @Override
    public League createLeague(int userId, String leagueName) {
        League newLeague = null;
        String sql = "INSERT INTO leagues (" +
                "user_id, league_name, start_date, end_date) " +
                "VALUES (" +
                "?, ?, ?, ?) " +
                "RETURNING league_id";

        Timestamp startDate = new Timestamp(new Date().getTime());
        Timestamp endDate = new Timestamp(startDate.getTime() + 7 * 24 * 60 * 60 * 1000);

        try {
            int leagueId = jdbcTemplate.queryForObject(sql, int.class,
                    userId, leagueName, startDate, endDate);
            newLeague = getLeagueById(leagueId);
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database", e);
        } catch (DataIntegrityViolationException e) {
            throw new DaoException("Data integrity violation", e);
        }

        return newLeague;
    }

    @Override
    public League getLeagueById(int leagueId) {
        League league = null;
        String sql = "SELECT * FROM leagues WHERE league_id = ?";

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(sql, leagueId);
            if (results.next()) {
                league = mapRowToLeague(results);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database", e);
        } catch (DataIntegrityViolationException e) {
            throw new DaoException("Data integrity violation", e);
        }

        return league;
    }

    @Override
    public List<League> getAllLeagues() {
        List<League> leagueList = new ArrayList<>();
        String sql = "SELECT * FROM leagues";

        SqlRowSet results = jdbcTemplate.queryForRowSet(sql);
        while (results.next()) {
            leagueList.add(mapRowToLeague(results));
        }

        return leagueList;
    }

    @Override
    public void updateLeague(League league) {

    }

    @Override
    public void deleteLeague(int leagueId) {

    }

    public League mapRowToLeague(SqlRowSet rs) {
        League league = new League();
        league.setLeagueId(rs.getInt("league_id"));
        league.setUserId(rs.getInt("user_id"));
        league.setLeagueName(rs.getString("league_name"));
        league.setStartDate(rs.getTimestamp("start_date"));
        league.setEndDate(rs.getTimestamp("end_date"));
        return league;
    }
}

package com.techelevator.dao;

import com.techelevator.exception.DaoException;
import com.techelevator.model.League;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;
import com.techelevator.model.User;

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
    public void createLeague(League league) {
        String sql = "INSERT INTO leagues (" +
                "user_id, leagueName, startDate, endDate) " +
                "VALUES (" +
                "?, ?, ?, ?) " +
                "RETURNING league_id";
        try {
            jdbcTemplate.update(sql, int.class,
                    league.getUserId(), league.getLeagueName(),
                    league.getStartDate(), league.getEndDate());
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database", e);
        } catch (DataIntegrityViolationException e) {
            throw new DaoException("Data integrity violation", e);
        }
    }

    @Override
    public League getLeagueById(int leagueId) {
        return null;
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
        league.setStartDate(rs.getDate("start_date"));
        league.setEndDate(rs.getDate("end_date"));
        return league;
    }
}

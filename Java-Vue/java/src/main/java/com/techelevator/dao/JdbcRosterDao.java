package com.techelevator.dao;

import com.techelevator.exception.DaoException;
import com.techelevator.model.Roster;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
public class JdbcRosterDao implements RosterDao{
    private JdbcTemplate jdbcTemplate;
    public JdbcRosterDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    @Override
    public Roster createRoster(int leagueId, int userId, String rosterName) {
        Roster newRoster = null;
        String sql = "INSERT INTO rosters (" +
                "league_id, user_id, roster_name) " +
                "VALUES (" +
                "?, ?, ?) " +
                "RETURNING roster_id";

        try {
            int rosterId = jdbcTemplate.queryForObject(sql, int.class,
                    leagueId, userId, rosterName);
            newRoster = getRosterById(rosterId);
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database", e);
        } catch (DataIntegrityViolationException e) {
            throw new DaoException("Data integrity violation", e);
        }

        return newRoster;
    }

    @Override
    public Roster getRosterById(int rosterId) {
        Roster roster = null;
        String sql = "SELECT * FROM rosters WHERE roster_id = ?";

        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(sql, rosterId);
            if (results.next()) {
                roster = mapRowToRoster(results);
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database", e);
        } catch (DataIntegrityViolationException e) {
            throw new DaoException("Data integrity violation", e);
        }

        return roster;
    }

    @Override
    public List<Roster> getAllRosters() {
        List<Roster> rosterList = new ArrayList<>();
        String sql = "SELECT * FROM rosters";

        SqlRowSet results = jdbcTemplate.queryForRowSet(sql);
        while (results.next()) {
            rosterList.add(mapRowToRoster(results));
        }

        return rosterList;
    }

    @Override
    public Roster updateRoster(int rosterId, int playerPosition, int playerID) {
        Roster updatedRoster = null;
        String sql = "UPDATE rosters SET player" + playerPosition + " = ? WHERE roster_id = ?";

        try {
            int rowsAffected = jdbcTemplate.update(sql, playerID, rosterId);
            if (rowsAffected == 0) {
                throw new DaoException("Zero rows affected, expected at least one");
            }
            updatedRoster = getRosterById(rosterId);
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database", e);
        } catch (DataIntegrityViolationException e) {
            throw new DaoException("Data integrity violation", e);
        }

        return updatedRoster;
    }


    public Roster mapRowToRoster(SqlRowSet rs) {
        Roster roster = new Roster();
        roster.setRosterId(rs.getInt("roster_id"));
        roster.setLeagueId(rs.getInt("league_id"));
        roster.setUserId(rs.getInt("user_id"));
        roster.setRosterName(rs.getString("roster_name"));
        roster.setPlayer1(rs.getInt("player1"));
        roster.setPlayer2(rs.getInt("player2"));
        roster.setPlayer3(rs.getInt("player3"));
        roster.setPlayer4(rs.getInt("player4"));
        roster.setPlayer5(rs.getInt("player5"));
        roster.setPlayer6(rs.getInt("player6"));
        roster.setPlayer7(rs.getInt("player7"));
        roster.setPlayer8(rs.getInt("player8"));
        roster.setPlayer9(rs.getInt("player9"));
        roster.setPlayer10(rs.getInt("player10"));
        roster.setPlayer11(rs.getInt("player11"));
        roster.setPlayer12(rs.getInt("player12"));
        roster.setPlayer13(rs.getInt("player13"));
        roster.setPlayer14(rs.getInt("player14"));
        roster.setPlayer15(rs.getInt("player15"));
        roster.setPlayer16(rs.getInt("player16"));
        roster.setPlayer17(rs.getInt("player17"));
        roster.setPlayer18(rs.getInt("player18"));
        roster.setPlayer19(rs.getInt("player19"));
        roster.setPlayer20(rs.getInt("player20"));
        roster.setPlayer21(rs.getInt("player21"));
        roster.setPlayer22(rs.getInt("player22"));
        roster.setPlayer23(rs.getInt("player23"));
        roster.setPlayer24(rs.getInt("player24"));
        roster.setPlayer25(rs.getInt("player25"));
        roster.setPlayer26(rs.getInt("player26"));
        roster.setPlayer27(rs.getInt("player27"));
        return roster;
    }
}

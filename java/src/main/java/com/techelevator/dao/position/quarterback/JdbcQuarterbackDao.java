package com.techelevator.dao.position.quarterback;

import com.techelevator.exception.DaoException;
import org.springframework.jdbc.CannotGetJdbcConnectionException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
public class JdbcQuarterbackDao implements QuarterbackDao{
    private JdbcTemplate jdbcTemplate;
    public JdbcQuarterbackDao(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }


    @Override
    public List<String> getQuarterbackNames(String searchTerm) {
        List<String> quarterbackList = new ArrayList<>();
        String sql = "SELECT Name " +
                "FROM offense_stats_2022 " +
                "WHERE PositionCategory = 'OFF' " +
                "AND SeasonType = 1 " +
                "AND lower(Name) ILIKE ?";
        try {
            SqlRowSet results = jdbcTemplate.queryForRowSet(sql,
                    "%" + searchTerm.toLowerCase() + "%");
            while (results.next()) {
                quarterbackList.add(results.getString("Name"));
            }
        } catch (CannotGetJdbcConnectionException e) {
            throw new DaoException("Unable to connect to server or database.");
        }
        return quarterbackList;
    }
}

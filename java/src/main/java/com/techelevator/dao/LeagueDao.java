package com.techelevator.dao;

import com.techelevator.model.League;

import java.util.List;

public interface LeagueDao {
    League createLeague(int userId, String leagueName);
    League getLeagueById(int leagueId);
    List<League> getAllLeagues();
    void updateLeague(League league);
    void deleteLeague(int leagueId);

}

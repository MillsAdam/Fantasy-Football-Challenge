package com.techelevator.dao;

import com.techelevator.model.Roster;

import java.util.List;

public interface RosterDao {
    Roster createRoster(int leagueId, int userId, String rosterName);
    Roster getRosterById(int rosterId);
    List<Roster> getAllRosters();
    Roster updateRoster(int rosterId, int playerPosition, int playerID);
}

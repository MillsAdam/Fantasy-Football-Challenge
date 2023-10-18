package com.techelevator.dao.position.defense.regularSeason.last4Total;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseLast4TotalDao {
    List<DefenseDto> getDefenseLast4TotalStats(int searchWeek); // Last 4 Total Stats
    List<DefenseDto> getDefenseLast4TotalStatsByConference(int searchWeek, String searchTerm); // Last 4 Total Stats by Conference
    List<DefenseDto> getDefenseLast4TotalStatsByTeam(int searchWeek, String searchTerm); // Last 4 Total Stats by Team
    List<DefenseDto> getDefenseLast4TotalStatsByName(int searchWeek, String searchTerm); // Last 4 Total Stats by Name
}

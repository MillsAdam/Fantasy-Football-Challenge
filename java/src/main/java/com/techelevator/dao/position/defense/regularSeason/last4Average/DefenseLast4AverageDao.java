package com.techelevator.dao.position.defense.regularSeason.last4Average;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseLast4AverageDao {
    List<DefenseDto> getDefenseLast4AverageStats(int searchWeek); // Last 4 Average Stats
    List<DefenseDto> getDefenseLast4AverageStatsByConference(int searchWeek, String searchTerm); // Last 4 Average Stats by Conference
    List<DefenseDto> getDefenseLast4AverageStatsByTeam(int searchWeek, String searchTerm); // Last 4 Average Stats by Team
    List<DefenseDto> getDefenseLast4AverageStatsByName(int searchWeek, String searchTerm); // Last 4 Average Stats by Name
}

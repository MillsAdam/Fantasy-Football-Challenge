package com.techelevator.dao.position.defense.last4Average;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseLast4AverageDao {
    List<DefenseDto> getDefenseLast4AverageStats(); // Last 4 Average Stats
    List<DefenseDto> getDefenseLast4AverageStatsByConference(String searchTerm); // Last 4 Average Stats by Conference
    List<DefenseDto> getDefenseLast4AverageStatsByTeam(String searchTerm); // Last 4 Average Stats by Team
    List<DefenseDto> getDefenseLast4AverageStatsByName(String searchTerm); // Last 4 Average Stats by Name
}

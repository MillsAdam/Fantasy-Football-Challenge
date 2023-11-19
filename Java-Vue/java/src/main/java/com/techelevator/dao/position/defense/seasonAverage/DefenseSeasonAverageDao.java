package com.techelevator.dao.position.defense.seasonAverage;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseSeasonAverageDao {
    List<DefenseDto> getDefenseSeasonAverageStats(); // Season Average Stats
    List<DefenseDto> getDefenseSeasonAverageStatsByConference(String searchTerm); // Season Average Stats by Conference
    List<DefenseDto> getDefenseSeasonAverageStatsByTeam(String searchTerm); // Season Average Stats by Team
    List<DefenseDto> getDefenseSeasonAverageStatsByName(String searchTerm); // Season Average Stats By Name
}

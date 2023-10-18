package com.techelevator.dao.position.defense.regularSeason.SeasonTotal;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseSeasonTotalDao {
    List<DefenseDto> getDefenseSeasonTotalStats(); // Season Total Stats
    List<DefenseDto> getDefenseSeasonTotalStatsByConference(String searchTerm); // Season Total Stats by Conference
    List<DefenseDto> getDefenseSeasonTotalStatsByTeam(String searchTerm); // Season Total Stats by Team
    List<DefenseDto> getDefenseSeasonTotalStatsByName(String searchTerm); // Season Total Stats by Name
}

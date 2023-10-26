package com.techelevator.dao.position.defense.seasonProjected;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseSeasonProjectedDao {
    List<DefenseDto> getDefenseSeasonProjectedStats(); // Season Projected Stats
    List<DefenseDto> getDefenseSeasonProjectedStatsByConference(String searchTerm); // Season Projected Stats by Conference
    List<DefenseDto> getDefenseSeasonProjectedStatsByTeam(String searchTerm); // Season Projected Stats by Team
    List<DefenseDto> getDefenseSeasonProjectedStatsByName(String searchTerm); // Season Projected Stats by Name
}

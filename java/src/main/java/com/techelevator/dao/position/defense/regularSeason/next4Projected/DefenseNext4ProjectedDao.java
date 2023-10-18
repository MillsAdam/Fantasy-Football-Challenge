package com.techelevator.dao.position.defense.regularSeason.next4Projected;


import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseNext4ProjectedDao {
    List<DefenseDto> getDefenseNext4ProjectedStats(int searchWeek); // Next 4 Projected Stats
    List<DefenseDto> getDefenseNext4ProjectedStatsByConference(int searchWeek, String searchTerm); // Next 4 Projected Stats by Conference
    List<DefenseDto> getDefenseNext4ProjectedStatsByTeam(int searchWeek, String searchTerm); // Next 4 Projected Stats by Team
    List<DefenseDto> getDefenseNext4ProjectedStatsByName(int searchWeek, String searchTerm); // Next 4 Projected Stats by Name
}

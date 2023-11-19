package com.techelevator.dao.position.defense.next4Projected;


import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseNext4ProjectedDao {
    List<DefenseDto> getDefenseNext4ProjectedStats(); // Next 4 Projected Stats
    List<DefenseDto> getDefenseNext4ProjectedStatsByConference(String searchTerm); // Next 4 Projected Stats by Conference
    List<DefenseDto> getDefenseNext4ProjectedStatsByTeam(String searchTerm); // Next 4 Projected Stats by Team
    List<DefenseDto> getDefenseNext4ProjectedStatsByName(String searchTerm); // Next 4 Projected Stats by Name
}

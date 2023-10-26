package com.techelevator.dao.position.defense.remainingProjected;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseRemainingProjectedDao {
    List<DefenseDto> getDefenseRemainingProjectedStats(); // Remaining Projected Stats
    List<DefenseDto> getDefenseRemainingProjectedStatsByConference(String searchTerm); // Remaining Projected Stats by Conference
    List<DefenseDto> getDefenseRemainingProjectedStatsByTeam(String searchTerm); // Remaining Projected Stats by Team
    List<DefenseDto> getDefenseRemainingProjectedStatsByName(String searchTerm); // Remaining Projected Stats By Name
}

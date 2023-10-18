package com.techelevator.dao.position.defense.regularSeason.remainingProjected;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseRemainingProjectedDao {
    List<DefenseDto> getDefenseRemainingProjectedStats(int searchWeek); // Remaining Projected Stats
    List<DefenseDto> getDefenseRemainingProjectedStatsByConference(int searchWeek, String searchTerm); // Remaining Projected Stats by Conference
    List<DefenseDto> getDefenseRemainingProjectedStatsByTeam(int searchWeek, String searchTerm); // Remaining Projected Stats by Team
    List<DefenseDto> getDefenseRemainingProjectedStatsByName(int searchWeek, String searchTerm); // Remaining Projected Stats By Name
}

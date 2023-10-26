package com.techelevator.dao.position.defense.weeklyProjected;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseWeeklyProjectedDao {
    List<DefenseDto> getDefenseWeeklyProjectedStats(int searchWeek); // Weekly Projected Stats
    List<DefenseDto> getDefenseWeeklyProjectedStatsByConference(int searchWeek, String searchTerm); // Weekly Projected Stats by Conference
    List<DefenseDto> getDefenseWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm); // Weekly Projected Stats by Team
    List<DefenseDto> getDefenseWeeklyProjectedStatsByName(int searchWeek, String searchTerm); // Weekly Projected Stats by Name
}

package com.techelevator.dao.position.defense.regularSeason.weeklyTotal;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseWeeklyTotalDao {
    List<DefenseDto> getDefenseWeeklyTotalStats(int searchWeek); // Weekly Total Stats
    List<DefenseDto> getDefenseWeeklyTotalStatsByConference(int searchWeek, String searchTerm); // Weekly Total Stats by Conference
    List<DefenseDto> getDefenseWeeklyTotalStatsByTeam(int searchWeek, String searchTerm); // Weekly Total Stats by Team
    List<DefenseDto> getDefenseWeeklyTotalStatsByName(int searchWeek, String searchTerm); // Weekly Total Stats by Name
}

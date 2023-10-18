package com.techelevator.dao.position.quarterback.regularSeason.weeklyTotal;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackWeeklyTotalDao {
    List<QuarterbackDto> getQuarterbackWeeklyTotalStats(int searchWeek); // Weekly Total Stats
    List<QuarterbackDto> getQuarterbackWeeklyTotalStatsByConference(int searchWeek, String searchTerm); // Weekly Total Stats by Conference
    List<QuarterbackDto> getQuarterbackWeeklyTotalStatsByTeam(int searchWeek, String searchTerm); // Weekly Total Stats by Team
    List<QuarterbackDto> getQuarterbackWeeklyTotalStatsByName(int searchWeek, String searchTerm); // Weekly Total Stats by Name
}

package com.techelevator.dao.position.quarterback.regularSeason.last4Total;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackLast4TotalDao {
    List<QuarterbackDto> getQuarterbackLast4TotalStats(int searchWeek); // Last 4 Total Stats
    List<QuarterbackDto> getQuarterbackLast4TotalStatsByAndConference(int searchWeek, String searchTerm); // Last 4 Total Stats by Conference
    List<QuarterbackDto> getQuarterbackLast4TotalStatsByTeam(int searchWeek, String searchTerm); // Last 4 Total Stats by Team
    List<QuarterbackDto> getQuarterbackLast4TotalStatsByName(int searchWeek, String searchTerm); // Last 4 Total Stats by Name
}

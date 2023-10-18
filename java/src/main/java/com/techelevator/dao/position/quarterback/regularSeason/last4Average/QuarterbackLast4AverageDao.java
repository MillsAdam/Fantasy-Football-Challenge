package com.techelevator.dao.position.quarterback.regularSeason.last4Average;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackLast4AverageDao {
    List<QuarterbackDto> getQuarterbackLast4AverageStats(int searchWeek); // Last 4 Average Stats
    List<QuarterbackDto> getQuarterbackLast4AverageStatsByConference(int searchWeek, String searchTerm); // Last 4 Average Stats by Conference
    List<QuarterbackDto> getQuarterbackLast4AverageStatsByAndTeam(int searchWeek, String searchTerm); // Last 4 Average Stats by Team
    List<QuarterbackDto> getQuarterbackLast4AverageStatsByAndName(int searchWeek, String searchTerm); // Last 4 Average Stats by Name

}

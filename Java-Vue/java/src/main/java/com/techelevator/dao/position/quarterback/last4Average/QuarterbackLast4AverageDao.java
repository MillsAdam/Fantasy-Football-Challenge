package com.techelevator.dao.position.quarterback.last4Average;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackLast4AverageDao {
    List<QuarterbackDto> getQuarterbackLast4AverageStats(); // Last 4 Average Stats
    List<QuarterbackDto> getQuarterbackLast4AverageStatsByConference(String searchTerm); // Last 4 Average Stats by Conference
    List<QuarterbackDto> getQuarterbackLast4AverageStatsByAndTeam(String searchTerm); // Last 4 Average Stats by Team
    List<QuarterbackDto> getQuarterbackLast4AverageStatsByAndName(String searchTerm); // Last 4 Average Stats by Name

}

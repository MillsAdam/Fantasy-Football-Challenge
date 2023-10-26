package com.techelevator.dao.position.quarterback.last4Total;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackLast4TotalDao {
    List<QuarterbackDto> getQuarterbackLast4TotalStats(); // Last 4 Total Stats
    List<QuarterbackDto> getQuarterbackLast4TotalStatsByAndConference(String searchTerm); // Last 4 Total Stats by Conference
    List<QuarterbackDto> getQuarterbackLast4TotalStatsByTeam(String searchTerm); // Last 4 Total Stats by Team
    List<QuarterbackDto> getQuarterbackLast4TotalStatsByName(String searchTerm); // Last 4 Total Stats by Name
}

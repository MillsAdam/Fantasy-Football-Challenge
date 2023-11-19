package com.techelevator.dao.position.quarterback.seasonAverage;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackSeasonAverageDao {
    List<QuarterbackDto> getQuarterbackSeasonAverageStats(); // Season Average Stats
    List<QuarterbackDto> getQuarterbackSeasonAverageStatsByConference(String searchTerm); // Season Average Stats by Conference
    List<QuarterbackDto> getQuarterbackSeasonAverageStatsByTeam(String searchTerm); // Season Average Stats by Team
    List<QuarterbackDto> getQuarterbackSeasonAverageStatsByName(String searchTerm); // Season Average Stats by Name

}

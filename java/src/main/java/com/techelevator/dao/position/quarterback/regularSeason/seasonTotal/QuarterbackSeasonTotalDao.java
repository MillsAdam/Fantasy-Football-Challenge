package com.techelevator.dao.position.quarterback.regularSeason.seasonTotal;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackSeasonTotalDao {
    List<QuarterbackDto> getQuarterbackSeasonTotalStats(); // Season Total Stats
    List<QuarterbackDto> getQuarterbackSeasonTotalStatsByConference(String searchTerm); // Season Total Stats by Conference
    List<QuarterbackDto> getQuarterbackSeasonTotalStatsByTeam(String searchTerm); // Season Total Stats by Team
    List<QuarterbackDto> getQuarterbackSeasonTotalStatsByName(String searchTerm); // Season Total Stats by Name
}

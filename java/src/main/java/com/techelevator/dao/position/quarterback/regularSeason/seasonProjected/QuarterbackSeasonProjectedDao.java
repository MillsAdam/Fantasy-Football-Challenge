package com.techelevator.dao.position.quarterback.regularSeason.seasonProjected;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackSeasonProjectedDao {
    List<QuarterbackDto> getQuarterbackSeasonProjectedStats(); // Season Projected Stats
    List<QuarterbackDto> getQuarterbackSeasonProjectedStatsByConference(String searchTerm); // Season Projected Stats by Conference
    List<QuarterbackDto> getQuarterbackSeasonProjectedStatsByTeam(String searchTerm); // Season Projected Stats by Team
    List<QuarterbackDto> getQuarterbackSeasonProjectedStatsByName(String searchTerm); // Season Projected Stats by Name
}

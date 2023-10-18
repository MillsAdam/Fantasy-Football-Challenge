package com.techelevator.dao.position.quarterback.regularSeason.next4Projected;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackNext4ProjectedDao {
    List<QuarterbackDto> getQuarterbackNext4ProjectedStats(int searchWeek); // Next 4 Projected Stats
    List<QuarterbackDto> getQuarterbackNext4ProjectedStatsByConference(int searchWeek, String searchTerm); // Next 4 Projected Stats by Conference
    List<QuarterbackDto> getQuarterbackNext4ProjectedStatsByTeam(int searchWeek, String searchTerm); // Next 4 Projected Stats by Team
    List<QuarterbackDto> getQuarterbackNext4ProjectedStatsByName(int searchWeek, String searchTerm); // Next 4 Projected Stats by Name
}

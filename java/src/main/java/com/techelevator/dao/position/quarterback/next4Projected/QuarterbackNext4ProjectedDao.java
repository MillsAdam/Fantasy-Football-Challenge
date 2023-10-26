package com.techelevator.dao.position.quarterback.next4Projected;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackNext4ProjectedDao {
    List<QuarterbackDto> getQuarterbackNext4ProjectedStats(); // Next 4 Projected Stats
    List<QuarterbackDto> getQuarterbackNext4ProjectedStatsByConference(String searchTerm); // Next 4 Projected Stats by Conference
    List<QuarterbackDto> getQuarterbackNext4ProjectedStatsByTeam(String searchTerm); // Next 4 Projected Stats by Team
    List<QuarterbackDto> getQuarterbackNext4ProjectedStatsByName(String searchTerm); // Next 4 Projected Stats by Name
}

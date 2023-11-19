package com.techelevator.dao.position.quarterback.remainingProjected;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackRemainingProjectedDao {
    List<QuarterbackDto> getQuarterbackRemainingProjectedStats(); // Remaining Projected Stats
    List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByConference(String searchTerm); // Remaining Projected Stats by Conference
    List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByTeam(String searchTerm); // Remaining Projected Stats by Team
    List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByName(String searchTerm); // Remaining Projected Stats by Name

}

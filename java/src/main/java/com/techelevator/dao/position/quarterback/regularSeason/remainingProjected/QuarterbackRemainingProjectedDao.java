package com.techelevator.dao.position.quarterback.regularSeason.remainingProjected;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackRemainingProjectedDao {
    List<QuarterbackDto> getQuarterbackRemainingProjectedStats(int searchWeek); // Remaining Projected Stats
    List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByConference(int searchWeek, String searchTerm); // Remaining Projected Stats by Conference
    List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByTeam(int searchWeek, String searchTerm); // Remaining Projected Stats by Team
    List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByName(int searchWeek, String searchTerm); // Remaining Projected Stats by Name

}

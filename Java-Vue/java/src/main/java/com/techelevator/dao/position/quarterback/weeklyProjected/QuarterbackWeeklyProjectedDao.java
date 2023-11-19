package com.techelevator.dao.position.quarterback.weeklyProjected;

import com.techelevator.model.position.QuarterbackDto;

import java.util.List;

public interface QuarterbackWeeklyProjectedDao {
    List<QuarterbackDto> getQuarterbackWeeklyProjectedStats(int searchWeek); // Weekly Projected Stats
    List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByConference(int searchWeek, String searchTerm); //  Weekly Projected Stats by Conference
    List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm); //  Weekly Projected Stats by Team
    List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByName(int searchWeek, String searchTerm); //  Weekly Projected Stats by Name
}

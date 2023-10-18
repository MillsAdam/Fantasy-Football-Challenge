package com.techelevator.dao.position.kicker.regularSeason.weeklyProjected;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerWeeklyProjectedDao {
    List<KickerDto> getKickerWeeklyProjectedStats(int searchWeek); // Weekly Projected Stats
    List<KickerDto> getKickerWeeklyProjectedStatsByConference(int searchWeek, String searchTerm); // Weekly Projected Stats by Conference
    List<KickerDto> getAKickerWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm); // Weekly Projected Stats by Team
    List<KickerDto> getKickerWeeklyProjectedStatsByName(int searchWeek, String searchTerm); // Weekly Projected Stats by Name
}

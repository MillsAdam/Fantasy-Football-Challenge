package com.techelevator.dao.position.kicker.regularSeason.last4Average;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerLast4AverageDao {
    List<KickerDto> getKickerLast4AverageStats(int searchWeek); // Last 4 Average Stats
    List<KickerDto> getKickerLast4AverageStatsByConference(int searchWeek, String searchTerm); // Last 4 Average Stats by Conference
    List<KickerDto> getKickerLast4AverageStatsByTeam(int searchWeek, String searchTerm); // Last 4 Average Stats by Team
    List<KickerDto> getKickerLast4AverageStatsByName(int searchWeek, String searchTerm); // Last 4 Average Stats by Name
}

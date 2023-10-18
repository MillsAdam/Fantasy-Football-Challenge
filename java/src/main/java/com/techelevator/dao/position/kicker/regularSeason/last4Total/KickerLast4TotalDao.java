package com.techelevator.dao.position.kicker.regularSeason.last4Total;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerLast4TotalDao {
    List<KickerDto> getKickerLast4TotalStats(int searchWeek); // Last 4 Total Stats
    List<KickerDto> getKickerLast4TotalStatsByConference(int searchWeek, String searchTerm); // Last 4 Total Stats by Conference
    List<KickerDto> getKickerLast4TotalStatsByTeam(int searchWeek, String searchTerm); // Last 4 Total Stats by Team
    List<KickerDto> getKickerLast4TotalStatsByName(int searchWeek, String searchTerm); // Last 4 Total Stats by Name
}

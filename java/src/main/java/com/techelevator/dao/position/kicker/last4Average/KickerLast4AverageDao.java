package com.techelevator.dao.position.kicker.last4Average;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerLast4AverageDao {
    List<KickerDto> getKickerLast4AverageStats(); // Last 4 Average Stats
    List<KickerDto> getKickerLast4AverageStatsByConference(String searchTerm); // Last 4 Average Stats by Conference
    List<KickerDto> getKickerLast4AverageStatsByTeam(String searchTerm); // Last 4 Average Stats by Team
    List<KickerDto> getKickerLast4AverageStatsByName(String searchTerm); // Last 4 Average Stats by Name
}

package com.techelevator.dao.position.kicker.last4Total;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerLast4TotalDao {
    List<KickerDto> getKickerLast4TotalStats(); // Last 4 Total Stats
    List<KickerDto> getKickerLast4TotalStatsByConference(String searchTerm); // Last 4 Total Stats by Conference
    List<KickerDto> getKickerLast4TotalStatsByTeam(String searchTerm); // Last 4 Total Stats by Team
    List<KickerDto> getKickerLast4TotalStatsByName(String searchTerm); // Last 4 Total Stats by Name
}

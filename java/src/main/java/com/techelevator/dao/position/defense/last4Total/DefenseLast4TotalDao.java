package com.techelevator.dao.position.defense.last4Total;

import com.techelevator.model.position.DefenseDto;

import java.util.List;

public interface DefenseLast4TotalDao {
    List<DefenseDto> getDefenseLast4TotalStats(); // Last 4 Total Stats
    List<DefenseDto> getDefenseLast4TotalStatsByConference(String searchTerm); // Last 4 Total Stats by Conference
    List<DefenseDto> getDefenseLast4TotalStatsByTeam(String searchTerm); // Last 4 Total Stats by Team
    List<DefenseDto> getDefenseLast4TotalStatsByName(String searchTerm); // Last 4 Total Stats by Name
}

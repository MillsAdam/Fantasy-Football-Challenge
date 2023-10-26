package com.techelevator.dao.position.kicker.remainingProjected;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerRemainingProjectedDao {
    List<KickerDto> getKickerRemainingProjectedStats(); // Remaining Projected Stats
    List<KickerDto> getKickerRemainingProjectedStatsByConference(String searchTerm); // Remaining Projected Stats by Conference
    List<KickerDto> getKickerRemainingProjectedStatsByTeam(String searchTerm); // Remaining Projected Stats by Team
    List<KickerDto> getKickerRemainingProjectedStatsByName(String searchTerm); // Remaining Projected Stats by Name
}

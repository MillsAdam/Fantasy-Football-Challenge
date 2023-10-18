package com.techelevator.dao.position.kicker.regularSeason.remainingProjected;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerRemainingProjectedDao {
    List<KickerDto> getKickerRemainingProjectedStats(int searchWeek); // Remaining Projected Stats
    List<KickerDto> getKickerRemainingProjectedStatsByConference(int searchWeek, String searchTerm); // Remaining Projected Stats by Conference
    List<KickerDto> getKickerRemainingProjectedStatsByTeam(int searchWeek, String searchTerm); // Remaining Projected Stats by Team
    List<KickerDto> getKickerRemainingProjectedStatsByName(int searchWeek, String searchTerm); // Remaining Projected Stats by Name
}

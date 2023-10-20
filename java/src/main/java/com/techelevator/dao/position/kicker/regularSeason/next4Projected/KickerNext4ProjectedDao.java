package com.techelevator.dao.position.kicker.regularSeason.next4Projected;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerNext4ProjectedDao {
    List<KickerDto> getKickerNext4ProjectedStats(int searchWeek); // Next 4 Projected Stats
    List<KickerDto> getKickerNext4ProjectedStatsByConference(int searchWeek, String searchTerm); // Next 4 Projected Stats by Conference
    List<KickerDto> getKickerNext4ProjectedStatsByTeam(int searchWeek, String searchTerm); // Next 4 Projected Stats by Team
    List<KickerDto> getKickerNext4ProjectedStatsByName(int searchWeek, String searchTerm); // Next 4 Projected Stats by Name
}

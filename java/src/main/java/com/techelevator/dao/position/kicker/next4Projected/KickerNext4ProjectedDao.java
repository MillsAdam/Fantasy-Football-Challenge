package com.techelevator.dao.position.kicker.next4Projected;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerNext4ProjectedDao {
    List<KickerDto> getKickerNext4ProjectedStats(); // Next 4 Projected Stats
    List<KickerDto> getKickerNext4ProjectedStatsByConference(String searchTerm); // Next 4 Projected Stats by Conference
    List<KickerDto> getKickerNext4ProjectedStatsByTeam(String searchTerm); // Next 4 Projected Stats by Team
    List<KickerDto> getKickerNext4ProjectedStatsByName(String searchTerm); // Next 4 Projected Stats by Name
}

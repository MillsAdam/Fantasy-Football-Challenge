package com.techelevator.dao.position.kicker.seasonProjected;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerSeasonProjectedDao {
    List<KickerDto> getKickerSeasonProjectedStats(); // Season Projected Stats
    List<KickerDto> getKickerSeasonProjectedStatsByConference(String searchTerm); // Season Projected Stats by Conference
    List<KickerDto> getKickerSeasonProjectedStatsByTeam(String searchTerm); // Season Projected Stats by Team
    List<KickerDto> getKickerSeasonProjectedStatsByName(String searchTerm); // Season Projected Stats by Name
}

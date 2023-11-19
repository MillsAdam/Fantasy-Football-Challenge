package com.techelevator.dao.position.kicker.seasonTotal;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerSeasonTotalDao {
    List<KickerDto> getKickerSeasonTotalStats(); // Season Total Stats
    List<KickerDto> getKickerSeasonTotalStatsByConference(String searchTerm); // Season Total Stats by Conference
    List<KickerDto> getKickerSeasonTotalStatsByTeam(String searchTerm); // Season Total Stats by Team
    List<KickerDto> getKickerSeasonTotalStatsByName(String searchTerm); // Season Total Stats by Name
}

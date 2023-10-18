package com.techelevator.dao.position.kicker.regularSeason.seasonAverage;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerSeasonAverageDao {
    List<KickerDto> getKickerSeasonAverageStats(); // Season Average Stats
    List<KickerDto> getKickerSeasonAverageStatsByConference(String searchTerm); // Season Average Stats by Conference
    List<KickerDto> getKickerSeasonAverageStatsByTeam(String searchTerm); // Season Average Stats By Team
    List<KickerDto> getKickerSeasonAverageStatsByName(String searchTerm); // Season Average Stats by Name
}

package com.techelevator.dao.position.flexPlayer.seasonAverage;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerSeasonAverageDao {
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStats(); // Season Average Stats
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByConference(String searchTerm); // Season Average Stats by Conference
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByTeam(String searchTerm); // Season Average Stats by Team
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsName(String searchTerm); // Season Average Stats by Name


    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPosition(String searchPosition); // Season Average Position Stats
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndConference(String searchPosition, String conference); // Season Average Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndTeam(String searchPosition, String conference); // Season Average Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndName(String searchPosition, String conference); // Season Average Position Stats by Name
}

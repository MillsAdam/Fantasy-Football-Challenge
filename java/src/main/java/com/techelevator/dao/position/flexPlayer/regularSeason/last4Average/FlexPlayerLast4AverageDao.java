package com.techelevator.dao.position.flexPlayer.regularSeason.last4Average;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerLast4AverageDao {
    List<FlexPlayerDto> getFlexPlayerLast4AverageStats(int searchWeek); // Last 4 Average Stats
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByConference(int searchWeek, String searchTerm); // Last 4 Average Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByTeam(int searchWeek, String searchTerm); // Last 4 Average Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByName(int searchWeek, String searchTerm); // Last 4 Average Stats by Name

    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPosition(int searchWeek, String searchPosition); // Last 4 Average Position Stats
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm); // Last 4 Average Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm); // Last 4 Average Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm); // Last 4 Average Position Stats by Name
}

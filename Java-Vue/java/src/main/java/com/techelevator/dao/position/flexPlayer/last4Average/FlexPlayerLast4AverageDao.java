package com.techelevator.dao.position.flexPlayer.last4Average;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerLast4AverageDao {
    List<FlexPlayerDto> getFlexPlayerLast4AverageStats(); // Last 4 Average Stats
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByConference(String searchTerm); // Last 4 Average Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByTeam(String searchTerm); // Last 4 Average Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByName(String searchTerm); // Last 4 Average Stats by Name

    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPosition(String searchPosition); // Last 4 Average Position Stats
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndConference(String searchPosition, String searchTerm); // Last 4 Average Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndTeam(String searchPosition, String searchTerm); // Last 4 Average Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndName(String searchPosition, String searchTerm); // Last 4 Average Position Stats by Name
}

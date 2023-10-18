package com.techelevator.dao.position.flexPlayer.regularSeason.last4Total;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerLast4TotalDao {
    List<FlexPlayerDto> getFlexPlayerLast4TotalStats(int searchWeek); // Last 4 Total Stats
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByConference(int searchWeek, String searchTerm); // Last 4 Total Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByTeam(int searchWeek, String searchTerm); // Last 4 Total Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByName(int searchWeek, String searchTerm); // Last 4 Total Stats by Name

    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPosition(int searchWeek, String searchPosition); // Last 4 Total Position Stats
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm); // Last 4 Total Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm); // Last 4 Total Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm); // Last 4 Total Position Stats by Name
}

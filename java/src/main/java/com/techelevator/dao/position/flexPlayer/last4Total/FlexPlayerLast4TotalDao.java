package com.techelevator.dao.position.flexPlayer.last4Total;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerLast4TotalDao {
    List<FlexPlayerDto> getFlexPlayerLast4TotalStats(); // Last 4 Total Stats
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByConference(String searchTerm); // Last 4 Total Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByTeam(String searchTerm); // Last 4 Total Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByName(String searchTerm); // Last 4 Total Stats by Name

    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPosition(String searchPosition); // Last 4 Total Position Stats
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndConference(String searchPosition, String searchTerm); // Last 4 Total Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndTeam(String searchPosition, String searchTerm); // Last 4 Total Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndName(String searchPosition, String searchTerm); // Last 4 Total Position Stats by Name
}

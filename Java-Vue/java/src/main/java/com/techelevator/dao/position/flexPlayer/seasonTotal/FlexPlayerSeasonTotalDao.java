package com.techelevator.dao.position.flexPlayer.seasonTotal;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerSeasonTotalDao {
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStats(); // Season Total Stats
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByConference(String searchTerm); // Season Total Stats by Conference
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByTeam(String searchTerm); // Season Total Stats by Team
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByName(String searchTerm); // Season Total Stats by Name


    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPosition(String searchPosition); // Season Total Position Stats
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPositionAndConference(String searchPosition, String searchTerm); // Season Total Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPositionAndTeam(String searchPosition, String searchTerm); // Season Total Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPositionAndName(String searchPosition, String searchTerm); // Season Total Position Stats by Name
}

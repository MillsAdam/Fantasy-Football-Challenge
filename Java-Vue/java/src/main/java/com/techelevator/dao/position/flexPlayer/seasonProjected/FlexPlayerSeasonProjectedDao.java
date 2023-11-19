package com.techelevator.dao.position.flexPlayer.seasonProjected;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerSeasonProjectedDao {
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStats(); //  Season Projected Stats
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByConference(String searchTerm); // Season Projected Stats by Conference
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByTeam(String searchTerm); // Season Projected Stats by Team
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByName(String searchTerm); // Season Projected Stats by Name


    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPosition(String searchPosition); // Season Projected Position Stats
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPositionAndConference(String searchPosition, String searchTerm); // Season Projected Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPositionAndTeam(String searchPosition, String searchTerm); // Season Projected Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPositionAndName(String searchPosition, String searchTerm); // Season Projected Position Stats by Name
}

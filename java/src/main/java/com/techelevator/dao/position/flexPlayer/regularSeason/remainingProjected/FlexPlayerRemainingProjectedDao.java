package com.techelevator.dao.position.flexPlayer.regularSeason.remainingProjected;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerRemainingProjectedDao {
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStats(int searchWeek); // Remaining Projected Stats
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByConference(int searchWeek, String searchTerm); // Remaining Projected Stats by Conference
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByTeam(int searchWeek, String searchTerm); // Remaining Projected Stats by Team
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByName(int searchWeek, String searchTerm); // Remaining Projected Stats by Name

    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPosition(int searchWeek, String searchPosition); // Remaining Projected Position Stats
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm); // Remaining Projected Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm); // Remaining Projected Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm); // Remaining Projected Position Stats by Name
}

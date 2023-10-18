package com.techelevator.dao.position.flexPlayer.regularSeason.next4Projected;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerNext4ProjectedDao {
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStats(int searchWeek); // Next 4 Projected Stats
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByConference(int searchWeek, String searchTerm); // Next 4 Projected Stats by Conference
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByTeam(int searchWeek, String searchTerm); // Next 4 Projected Stats by Team
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByName(int searchWeek, String searchTerm); // Next 4 Projected Stats by Name

    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPosition(int searchWeek, String searchPosition); // Next 4 Projected Position Stats
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm); // Next 4 Projected Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm); // Next 4 Projected Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm); // Next 4 Projected Position Stats by Name
}

package com.techelevator.dao.position.flexPlayer.next4Projected;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerNext4ProjectedDao {
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStats(); // Next 4 Projected Stats
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByConference(String searchTerm); // Next 4 Projected Stats by Conference
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByTeam(String searchTerm); // Next 4 Projected Stats by Team
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByName(String searchTerm); // Next 4 Projected Stats by Name

    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPosition(String searchPosition); // Next 4 Projected Position Stats
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndConference(String searchPosition, String searchTerm); // Next 4 Projected Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndTeam(String searchPosition, String searchTerm); // Next 4 Projected Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndName(String searchPosition, String searchTerm); // Next 4 Projected Position Stats by Name
}

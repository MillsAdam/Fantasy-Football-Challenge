package com.techelevator.dao.position.flexPlayer.remainingProjected;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerRemainingProjectedDao {
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStats(); // Remaining Projected Stats
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByConference(String searchTerm); // Remaining Projected Stats by Conference
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByTeam(String searchTerm); // Remaining Projected Stats by Team
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByName(String searchTerm); // Remaining Projected Stats by Name

    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPosition(String searchPosition); // Remaining Projected Position Stats
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndConference(String searchPosition, String searchTerm); // Remaining Projected Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndTeam(String searchPosition, String searchTerm); // Remaining Projected Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndName(String searchPosition, String searchTerm); // Remaining Projected Position Stats by Name
}

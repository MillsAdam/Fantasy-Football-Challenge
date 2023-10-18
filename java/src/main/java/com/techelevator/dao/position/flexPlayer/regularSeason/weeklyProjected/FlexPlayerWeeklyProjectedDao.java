package com.techelevator.dao.position.flexPlayer.regularSeason.weeklyProjected;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerWeeklyProjectedDao {
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStats(int searchWeek); // Weekly Projected Stats
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByConference(int searchWeek, String searchTerm); // Weekly Projected Stats by Conference
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByTeam(int searchWeek, String searchTerm); // Weekly Projected Stats By Team
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByName(int searchWeek, String searchTerm); // Weekly Projected Stats by Name

    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPosition(int searchWeek, String searchPosition); // Weekly Projected Position Stats
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm); // Weekly Projected Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm); // Weekly Projected Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm); // Weekly Projected Position Stats by Name
}

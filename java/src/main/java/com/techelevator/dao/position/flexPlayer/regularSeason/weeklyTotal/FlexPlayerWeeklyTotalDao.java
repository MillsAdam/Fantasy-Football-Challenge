package com.techelevator.dao.position.flexPlayer.regularSeason.weeklyTotal;

import com.techelevator.model.position.FlexPlayerDto;

import java.util.List;

public interface FlexPlayerWeeklyTotalDao {
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStats(int searchWeek); // Weekly Total Stats
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByConference(int searchWeek, String searchTerm); // Weekly Total Stats by Conference
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByTeam(int searchWeek, String searchTerm); // Weekly Total Stats by Team
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByName(int searchWeek, String searchTerm); // Weekly Total Stats by Name

    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByPosition(int searchWeek, String searchPosition); // Weekly Total Position Stats
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByPositionAndConference(int searchWeek, String searchPosition, String searchTerm); // Weekly Total Position Stats by Conference
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByPositionAndTeam(int searchWeek, String searchPosition, String searchTerm); // Weekly Total Position Stats by Team
    List<FlexPlayerDto> getFlexPlayerWeeklyTotalStatsByPositionAndName(int searchWeek, String searchPosition, String searchTerm); // Weekly Total Position Stats by Name
}

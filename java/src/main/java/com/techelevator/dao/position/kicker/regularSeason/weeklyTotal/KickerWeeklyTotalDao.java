package com.techelevator.dao.position.kicker.regularSeason.weeklyTotal;

import com.techelevator.model.position.KickerDto;

import java.util.List;

public interface KickerWeeklyTotalDao {
    List<KickerDto> getKickerWeeklyTotalStats(int searchWeek); // Weekly Total Stats
    List<KickerDto> getKickerWeeklyTotalStatsByConference(int searchWeek, String searchTerm); //  Weekly Total Stats by Conference
    List<KickerDto> getKickerWeeklyTotalStatsByTeam(int searchWeek, String searchTerm); //  Weekly Total Stats by Team
    List<KickerDto> getKickerWeeklyTotalStatsByName(int searchWeek, String searchTerm); //  Weekly Total Stats By Name

}

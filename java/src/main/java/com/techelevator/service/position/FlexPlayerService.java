package com.techelevator.service.position;

import com.techelevator.dao.position.flexPlayer.regularSeason.last4Average.FlexPlayerLast4AverageDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.last4Total.FlexPlayerLast4TotalDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.next4Projected.FlexPlayerNext4ProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.remainingProjected.FlexPlayerRemainingProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.seasonAverage.FlexPlayerSeasonAverageDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.seasonProjected.FlexPlayerSeasonProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.seasonTotal.FlexPlayerSeasonTotalDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.weeklyProjected.FlexPlayerWeeklyProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.weeklyTotal.FlexPlayerWeeklyTotalDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.stereotype.Service;

import java.util.Collections;
import java.util.List;

@Service
public class FlexPlayerService {
    private FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao;
    private FlexPlayerSeasonAverageDao flexPlayerSeasonAverageDao;
    private FlexPlayerSeasonProjectedDao flexPlayerSeasonProjectedDao;
    private FlexPlayerLast4TotalDao flexPlayerLast4TotalDao;
    private FlexPlayerLast4AverageDao flexPlayerLast4AverageDao;
    private FlexPlayerNext4ProjectedDao flexPlayerNext4ProjectedDao;
    private FlexPlayerRemainingProjectedDao flexPlayerRemainingProjectedDao;
    private FlexPlayerWeeklyTotalDao flexPlayerWeeklyTotalDao;
    private FlexPlayerWeeklyProjectedDao flexPlayerWeeklyProjectedDao;
    private static final String SEASON = "season";
    private static final String LAST_4 = "last4";
    private static final String NEXT_4 = "next4";
    private static final String REMAINING = "remaining";
    private static final String WEEKLY = "weekly";
    private static final String TOTAL = "total";
    private static final String AVERAGE = "average";
    private static final String PROJECTED = "projected";
    private static final String ALL = "all";
    private static final String CONFERENCE = "conference";
    private static final String TEAM = "team";
    private static final String NAME = "name";


    public FlexPlayerService(FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao,
                             FlexPlayerSeasonAverageDao flexPlayerSeasonAverageDao,
                             FlexPlayerSeasonProjectedDao flexPlayerSeasonProjectedDao,
                             FlexPlayerLast4TotalDao flexPlayerLast4TotalDao,
                             FlexPlayerLast4AverageDao flexPlayerLast4AverageDao,
                             FlexPlayerNext4ProjectedDao flexPlayerNext4ProjectedDao,
                             FlexPlayerRemainingProjectedDao flexPlayerRemainingProjectedDao,
                             FlexPlayerWeeklyTotalDao flexPlayerWeeklyTotalDao,
                             FlexPlayerWeeklyProjectedDao flexPlayerWeeklyProjectedDao) {
        this.flexPlayerSeasonTotalDao = flexPlayerSeasonTotalDao;
        this.flexPlayerSeasonAverageDao = flexPlayerSeasonAverageDao;
        this.flexPlayerSeasonProjectedDao = flexPlayerSeasonProjectedDao;
        this.flexPlayerLast4TotalDao = flexPlayerLast4TotalDao;
        this.flexPlayerLast4AverageDao = flexPlayerLast4AverageDao;
        this.flexPlayerNext4ProjectedDao = flexPlayerNext4ProjectedDao;
        this.flexPlayerRemainingProjectedDao = flexPlayerRemainingProjectedDao;
        this.flexPlayerWeeklyTotalDao = flexPlayerWeeklyTotalDao;
        this.flexPlayerWeeklyProjectedDao = flexPlayerWeeklyProjectedDao;
    }

    public List<FlexPlayerDto> searchFlexPlayerStats(
            String searchInterval,
            String searchPoints,
            String searchCategory,
            String searchTerm,
            Integer searchWeek) {


        List<FlexPlayerDto> results = Collections.emptyList();

        switch(searchInterval) {
            case SEASON:
                results = handleSeasonInterval(searchPoints, searchCategory, searchTerm);
                break;
            case LAST_4:
                results = handleLast4Interval(searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case NEXT_4:
                results = handleNext4Interval(searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case REMAINING:
                results = handleRemainingInterval(searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case WEEKLY:
                results = handleWeeklyInterval(searchPoints, searchCategory, searchTerm, searchWeek);
        }

        return results;
    }



    private List<FlexPlayerDto> handleSeasonInterval(String searchPoints, String searchCategory, String searchTerm) {
        switch(searchPoints) {
            case TOTAL:
                return handleSeasonTotal(searchCategory, searchTerm);
            case AVERAGE:
                return handleSeasonAverage(searchCategory, searchTerm);
            case PROJECTED:
                return handleSeasonProjected(searchCategory, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleLast4Interval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleLast4Total(searchCategory, searchTerm, searchWeek);
            case AVERAGE:
                return handleLast4Average(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleNext4Interval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleNext4Projected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleRemainingInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleRemainingProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleWeeklyInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleWeeklyTotal(searchCategory, searchTerm, searchWeek);
            case PROJECTED:
                return handleWeeklyProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }



    private List<FlexPlayerDto> handleSeasonTotal(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStats();
            case CONFERENCE:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByConference(searchTerm);
            case TEAM:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByTeam(searchTerm);
            case NAME:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleSeasonAverage(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStats();
            case CONFERENCE:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByConference(searchTerm);
            case TEAM:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByTeam(searchTerm);
            case NAME:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleSeasonProjected(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStats();
            case CONFERENCE:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByConference(searchTerm);
            case TEAM:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByTeam(searchTerm);
            case NAME:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleLast4Total(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStats(searchWeek);
            case CONFERENCE:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleLast4Average(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStats(searchWeek);
            case CONFERENCE:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleNext4Projected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStats(searchWeek);
            case CONFERENCE:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleRemainingProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStats(searchWeek);
            case CONFERENCE:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleWeeklyTotal(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStats(searchWeek);
            case CONFERENCE:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleWeeklyProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStats(searchWeek);
            case CONFERENCE:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }
}

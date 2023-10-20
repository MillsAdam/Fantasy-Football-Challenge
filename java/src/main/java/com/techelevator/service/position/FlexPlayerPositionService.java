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
public class FlexPlayerPositionService {
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


    public FlexPlayerPositionService(FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao,
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
            String searchPosition,
            String searchInterval,
            String searchPoints,
            String searchCategory,
            String searchTerm,
            Integer searchWeek) {


        List<FlexPlayerDto> results = Collections.emptyList();

        switch(searchInterval) {
            case SEASON:
                results = handleSeasonInterval(searchPosition, searchPoints, searchCategory, searchTerm);
                break;
            case LAST_4:
                results = handleLast4Interval(searchPosition, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case NEXT_4:
                results = handleNext4Interval(searchPosition, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case REMAINING:
                results = handleRemainingInterval(searchPosition, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case WEEKLY:
                results = handleWeeklyInterval(searchPosition, searchPoints, searchCategory, searchTerm, searchWeek);
        }

        return results;
    }



    private List<FlexPlayerDto> handleSeasonInterval(String searchPosition, String searchPoints, String searchCategory, String searchTerm) {
        switch(searchPoints) {
            case TOTAL:
                return handleSeasonTotal(searchPosition, searchCategory, searchTerm);
            case AVERAGE:
                return handleSeasonAverage(searchPosition, searchCategory, searchTerm);
            case PROJECTED:
                return handleSeasonProjected(searchPosition, searchCategory, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleLast4Interval(String searchPosition, String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleLast4Total(searchPosition, searchCategory, searchTerm, searchWeek);
            case AVERAGE:
                return handleLast4Average(searchPosition, searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleNext4Interval(String searchPosition, String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleNext4Projected(searchPosition, searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleRemainingInterval(String searchPosition, String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleRemainingProjected(searchPosition, searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleWeeklyInterval(String searchPosition, String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleWeeklyTotal(searchPosition, searchCategory, searchTerm, searchWeek);
            case PROJECTED:
                return handleWeeklyProjected(searchPosition, searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }



    private List<FlexPlayerDto> handleSeasonTotal(String searchPosition, String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPosition(searchPosition);
            case CONFERENCE:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndConference(searchPosition, searchTerm);
            case TEAM:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndTeam(searchPosition, searchTerm);
            case NAME:
                return flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndName(searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleSeasonAverage(String searchPosition, String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPosition(searchPosition);
            case CONFERENCE:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndConference(searchPosition, searchTerm);
            case TEAM:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndTeam(searchPosition, searchTerm);
            case NAME:
                return flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndName(searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleSeasonProjected(String searchPosition, String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPosition(searchPosition);
            case CONFERENCE:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndConference(searchPosition, searchTerm);
            case TEAM:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndTeam(searchPosition, searchTerm);
            case NAME:
                return flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndName(searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleLast4Total(String searchPosition, String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPosition(searchWeek, searchPosition);
            case CONFERENCE:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
            case TEAM:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
            case NAME:
                return flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleLast4Average(String searchPosition, String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPosition(searchWeek, searchPosition);
            case CONFERENCE:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
            case TEAM:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
            case NAME:
                return flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleNext4Projected(String searchPosition, String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPosition(searchWeek, searchPosition);
            case CONFERENCE:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
            case TEAM:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
            case NAME:
                return flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleRemainingProjected(String searchPosition, String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPosition(searchWeek, searchPosition);
            case CONFERENCE:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
            case TEAM:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
            case NAME:
                return flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleWeeklyTotal(String searchPosition, String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPosition(searchWeek, searchPosition);
            case CONFERENCE:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
            case TEAM:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
            case NAME:
                return flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<FlexPlayerDto> handleWeeklyProjected(String searchPosition, String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPosition(searchWeek, searchPosition);
            case CONFERENCE:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
            case TEAM:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
            case NAME:
                return flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
        }
        return Collections.emptyList();
    }
}

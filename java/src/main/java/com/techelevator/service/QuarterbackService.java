package com.techelevator.service;

import com.techelevator.dao.position.quarterback.regularSeason.last4Average.QuarterbackLast4AverageDao;
import com.techelevator.dao.position.quarterback.regularSeason.last4Total.QuarterbackLast4TotalDao;
import com.techelevator.dao.position.quarterback.regularSeason.next4Projected.QuarterbackNext4ProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.remainingProjected.QuarterbackRemainingProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.seasonAverage.QuarterbackSeasonAverageDao;
import com.techelevator.dao.position.quarterback.regularSeason.seasonProjected.QuarterbackSeasonProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.seasonTotal.QuarterbackSeasonTotalDao;
import com.techelevator.dao.position.quarterback.regularSeason.weeklyProjected.QuarterbackWeeklyProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.weeklyTotal.QuarterbackWeeklyTotalDao;
import com.techelevator.model.position.QuarterbackDto;

import java.util.Collections;
import java.util.List;

public class QuarterbackService {
    private QuarterbackSeasonTotalDao quarterbackSeasonTotalDao;
    private QuarterbackSeasonAverageDao quarterbackSeasonAverageDao;
    private QuarterbackSeasonProjectedDao quarterbackSeasonProjectedDao;
    private QuarterbackLast4TotalDao quarterbackLast4TotalDao;
    private QuarterbackLast4AverageDao quarterbackLast4AverageDao;
    private QuarterbackNext4ProjectedDao quarterbackNext4ProjectedDao;
    private QuarterbackRemainingProjectedDao quarterbackRemainingProjectedDao;
    private QuarterbackWeeklyTotalDao quarterbackWeeklyTotalDao;
    private QuarterbackWeeklyProjectedDao quarterbackWeeklyProjectedDao;
    private static final String QB = "qb";
    private static final String FLEX = "flex";
    private static final String RB = "rb";
    private static final String WR = "wr";
    private static final String TE = "TE";
    private static final String K = "k";
    private static final String DEF = "def";
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


    public QuarterbackService(QuarterbackSeasonTotalDao quarterbackSeasonTotalDao,
                              QuarterbackSeasonAverageDao quarterbackSeasonAverageDao,
                              QuarterbackSeasonProjectedDao quarterbackSeasonProjectedDao,
                              QuarterbackLast4TotalDao quarterbackLast4TotalDao,
                              QuarterbackLast4AverageDao quarterbackLast4AverageDao,
                              QuarterbackNext4ProjectedDao quarterbackNext4ProjectedDao,
                              QuarterbackRemainingProjectedDao quarterbackRemainingProjectedDao,
                              QuarterbackWeeklyTotalDao quarterbackWeeklyTotalDao,
                              QuarterbackWeeklyProjectedDao quarterbackWeeklyProjectedDao) {
        this.quarterbackSeasonTotalDao = quarterbackSeasonTotalDao;
        this.quarterbackSeasonAverageDao = quarterbackSeasonAverageDao;
        this.quarterbackSeasonProjectedDao = quarterbackSeasonProjectedDao;
        this.quarterbackLast4TotalDao = quarterbackLast4TotalDao;
        this.quarterbackLast4AverageDao = quarterbackLast4AverageDao;
        this.quarterbackNext4ProjectedDao = quarterbackNext4ProjectedDao;
        this.quarterbackRemainingProjectedDao = quarterbackRemainingProjectedDao;
        this.quarterbackWeeklyTotalDao = quarterbackWeeklyTotalDao;
        this.quarterbackWeeklyProjectedDao = quarterbackWeeklyProjectedDao;
    }

    public List<QuarterbackDto> searchQuarterbackStats(
            String searchPosition,
            String searchInterval,
            String searchPoints,
            String searchCategory,
            String searchTerm,
            Integer searchWeek) {

        if (!searchPosition.equalsIgnoreCase(QB)) {
            return Collections.emptyList();
        }

        List<QuarterbackDto> results = Collections.emptyList();

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


    private List<QuarterbackDto> handleSeasonInterval(String searchPoints, String searchCategory, String searchTerm) {
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

    private List<QuarterbackDto> handleLast4Interval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleLast4Total(searchCategory, searchTerm, searchWeek);
            case AVERAGE:
                return handleLast4Average(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleNext4Interval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleNext4Projected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleRemainingInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleRemainingProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleWeeklyInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleWeeklyTotal(searchCategory, searchTerm, searchWeek);
            case PROJECTED:
                return handleWeeklyProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }



    private List<QuarterbackDto> handleSeasonTotal(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStats();
            case CONFERENCE:
                return quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByConference(searchTerm);
            case TEAM:
                return quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByTeam(searchTerm);
            case NAME:
                return quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleSeasonAverage(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStats();
            case CONFERENCE:
                return quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByConference(searchTerm);
            case TEAM:
                return quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByTeam(searchTerm);
            case NAME:
                return quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleSeasonProjected(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStats();
            case CONFERENCE:
                return quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByConference(searchTerm);
            case TEAM:
                return quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByTeam(searchTerm);
            case NAME:
                return quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleLast4Total(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return quarterbackLast4TotalDao.getQuarterbackLast4TotalStats(searchWeek);
            case CONFERENCE:
                return quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByAndConference(searchWeek, searchTerm);
            case TEAM:
                return quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleLast4Average(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return quarterbackLast4AverageDao.getQuarterbackLast4AverageStats(searchWeek);
            case CONFERENCE:
                return quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByAndTeam(searchWeek, searchTerm);
            case NAME:
                return quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByAndName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleNext4Projected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStats(searchWeek);
            case CONFERENCE:
                return quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleRemainingProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStats(searchWeek);
            case CONFERENCE:
                return quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleWeeklyTotal(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStats(searchWeek);
            case CONFERENCE:
                return quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<QuarterbackDto> handleWeeklyProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStats(searchWeek);
            case CONFERENCE:
                return quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }
}

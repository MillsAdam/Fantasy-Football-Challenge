package com.techelevator.service.position;

import com.techelevator.dao.position.kicker.regularSeason.last4Average.KickerLast4AverageDao;
import com.techelevator.dao.position.kicker.regularSeason.last4Total.KickerLast4TotalDao;
import com.techelevator.dao.position.kicker.regularSeason.next4Projected.KickerNext4ProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.remainingProjected.KickerRemainingProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.seasonAverage.KickerSeasonAverageDao;
import com.techelevator.dao.position.kicker.regularSeason.seasonProjected.KickerSeasonProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.seasonTotal.KickerSeasonTotalDao;
import com.techelevator.dao.position.kicker.regularSeason.weeklyProjected.KickerWeeklyProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.weeklyTotal.KickerWeeklyTotalDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.stereotype.Service;

import java.util.Collections;
import java.util.List;

@Service
public class KickerService {
    private KickerSeasonTotalDao kickerSeasonTotalDao;
    private KickerSeasonAverageDao kickerSeasonAverageDao;
    private KickerSeasonProjectedDao kickerSeasonProjectedDao;
    private KickerLast4TotalDao kickerLast4TotalDao;
    private KickerLast4AverageDao kickerLast4AverageDao;
    private KickerNext4ProjectedDao kickerNext4ProjectedDao;
    private KickerRemainingProjectedDao kickerRemainingProjectedDao;
    private KickerWeeklyTotalDao kickerWeeklyTotalDao;
    private KickerWeeklyProjectedDao kickerWeeklyProjectedDao;
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


    public KickerService(KickerSeasonTotalDao kickerSeasonTotalDao,
                         KickerSeasonAverageDao kickerSeasonAverageDao,
                         KickerSeasonProjectedDao kickerSeasonProjectedDao,
                         KickerLast4TotalDao kickerLast4TotalDao,
                         KickerLast4AverageDao kickerLast4AverageDao,
                         KickerNext4ProjectedDao kickerNext4ProjectedDao,
                         KickerRemainingProjectedDao kickerRemainingProjectedDao,
                         KickerWeeklyTotalDao kickerWeeklyTotalDao,
                         KickerWeeklyProjectedDao kickerWeeklyProjectedDao) {
        this.kickerSeasonTotalDao = kickerSeasonTotalDao;
        this.kickerSeasonAverageDao = kickerSeasonAverageDao;
        this.kickerSeasonProjectedDao = kickerSeasonProjectedDao;
        this.kickerLast4TotalDao = kickerLast4TotalDao;
        this.kickerLast4AverageDao = kickerLast4AverageDao;
        this.kickerNext4ProjectedDao = kickerNext4ProjectedDao;
        this.kickerRemainingProjectedDao = kickerRemainingProjectedDao;
        this.kickerWeeklyTotalDao = kickerWeeklyTotalDao;
        this.kickerWeeklyProjectedDao = kickerWeeklyProjectedDao;
    }

    public List<KickerDto> searchKickerStats(
            String searchInterval,
            String searchPoints,
            String searchCategory,
            String searchTerm,
            Integer searchWeek) {


        List<KickerDto> results = Collections.emptyList();

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



    private List<KickerDto> handleSeasonInterval(String searchPoints, String searchCategory, String searchTerm) {
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

    private List<KickerDto> handleLast4Interval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleLast4Total(searchCategory, searchTerm, searchWeek);
            case AVERAGE:
                return handleLast4Average(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleNext4Interval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleNext4Projected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleRemainingInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleRemainingProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleWeeklyInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleWeeklyTotal(searchCategory, searchTerm, searchWeek);
            case PROJECTED:
                return handleWeeklyProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }



    private List<KickerDto> handleSeasonTotal(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return kickerSeasonTotalDao.getKickerSeasonTotalStats();
            case CONFERENCE:
                return kickerSeasonTotalDao.getKickerSeasonTotalStatsByConference(searchTerm);
            case TEAM:
                return kickerSeasonTotalDao.getKickerSeasonTotalStatsByTeam(searchTerm);
            case NAME:
                return kickerSeasonTotalDao.getKickerSeasonTotalStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleSeasonAverage(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return kickerSeasonAverageDao.getKickerSeasonAverageStats();
            case CONFERENCE:
                return kickerSeasonAverageDao.getKickerSeasonAverageStatsByConference(searchTerm);
            case TEAM:
                return kickerSeasonAverageDao.getKickerSeasonAverageStatsByTeam(searchTerm);
            case NAME:
                return kickerSeasonAverageDao.getKickerSeasonAverageStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleSeasonProjected(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return kickerSeasonProjectedDao.getKickerSeasonProjectedStats();
            case CONFERENCE:
                return kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByConference(searchTerm);
            case TEAM:
                return kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByTeam(searchTerm);
            case NAME:
                return kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleLast4Total(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return kickerLast4TotalDao.getKickerLast4TotalStats(searchWeek);
            case CONFERENCE:
                return kickerLast4TotalDao.getKickerLast4TotalStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return kickerLast4TotalDao.getKickerLast4TotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return kickerLast4TotalDao.getKickerLast4TotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleLast4Average(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return kickerLast4AverageDao.getKickerLast4AverageStats(searchWeek);
            case CONFERENCE:
                return kickerLast4AverageDao.getKickerLast4AverageStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return kickerLast4AverageDao.getKickerLast4AverageStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return kickerLast4AverageDao.getKickerLast4AverageStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleNext4Projected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return kickerNext4ProjectedDao.getKickerNext4ProjectedStats(searchWeek);
            case CONFERENCE:
                return kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleRemainingProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return kickerRemainingProjectedDao.getKickerRemainingProjectedStats(searchWeek);
            case CONFERENCE:
                return kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleWeeklyTotal(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return kickerWeeklyTotalDao.getKickerWeeklyTotalStats(searchWeek);
            case CONFERENCE:
                return kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<KickerDto> handleWeeklyProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return kickerWeeklyProjectedDao.getKickerWeeklyProjectedStats(searchWeek);
            case CONFERENCE:
                return kickerWeeklyProjectedDao.getKickerWeeklyProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return kickerWeeklyProjectedDao.getAKickerWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return kickerWeeklyProjectedDao.getKickerWeeklyProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }
}

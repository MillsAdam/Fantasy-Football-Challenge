package com.techelevator.service.position;

import com.techelevator.dao.position.defense.SeasonTotal.DefenseSeasonTotalDao;
import com.techelevator.dao.position.defense.last4Average.DefenseLast4AverageDao;
import com.techelevator.dao.position.defense.last4Total.DefenseLast4TotalDao;
import com.techelevator.dao.position.defense.next4Projected.DefenseNext4ProjectedDao;
import com.techelevator.dao.position.defense.remainingProjected.DefenseRemainingProjectedDao;
import com.techelevator.dao.position.defense.seasonAverage.DefenseSeasonAverageDao;
import com.techelevator.dao.position.defense.seasonProjected.DefenseSeasonProjectedDao;
import com.techelevator.dao.position.defense.weeklyProjected.DefenseWeeklyProjectedDao;
import com.techelevator.dao.position.defense.weeklyTotal.DefenseWeeklyTotalDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.stereotype.Service;

import java.util.Collections;
import java.util.List;

@Service
public class DefenseService {
    private DefenseSeasonTotalDao defenseSeasonTotalDao;
    private DefenseSeasonAverageDao defenseSeasonAverageDao;
    private DefenseSeasonProjectedDao defenseSeasonProjectedDao;
    private DefenseLast4TotalDao defenseLast4TotalDao;
    private DefenseLast4AverageDao defenseLast4AverageDao;
    private DefenseNext4ProjectedDao defenseNext4ProjectedDao;
    private DefenseRemainingProjectedDao defenseRemainingProjectedDao;
    private DefenseWeeklyTotalDao defenseWeeklyTotalDao;
    private DefenseWeeklyProjectedDao defenseWeeklyProjectedDao;
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


    public DefenseService(DefenseSeasonTotalDao defenseSeasonTotalDao,
                          DefenseSeasonAverageDao defenseSeasonAverageDao,
                          DefenseSeasonProjectedDao defenseSeasonProjectedDao,
                          DefenseLast4TotalDao defenseLast4TotalDao,
                          DefenseLast4AverageDao defenseLast4AverageDao,
                          DefenseNext4ProjectedDao defenseNext4ProjectedDao,
                          DefenseRemainingProjectedDao defenseRemainingProjectedDao,
                          DefenseWeeklyTotalDao defenseWeeklyTotalDao,
                          DefenseWeeklyProjectedDao defenseWeeklyProjectedDao) {
        this.defenseSeasonTotalDao = defenseSeasonTotalDao;
        this.defenseSeasonAverageDao = defenseSeasonAverageDao;
        this.defenseSeasonProjectedDao = defenseSeasonProjectedDao;
        this.defenseLast4TotalDao = defenseLast4TotalDao;
        this.defenseLast4AverageDao = defenseLast4AverageDao;
        this.defenseNext4ProjectedDao = defenseNext4ProjectedDao;
        this.defenseRemainingProjectedDao = defenseRemainingProjectedDao;
        this.defenseWeeklyTotalDao = defenseWeeklyTotalDao;
        this.defenseWeeklyProjectedDao = defenseWeeklyProjectedDao;
    }

    public List<DefenseDto> searchDefenseStats(
            String searchInterval,
            String searchPoints,
            String searchCategory,
            String searchTerm,
            Integer searchWeek) {


        List<DefenseDto> results = Collections.emptyList();

        switch(searchInterval) {
            case SEASON:
                results = handleSeasonInterval(searchPoints, searchCategory, searchTerm);
                break;
            case LAST_4:
                results = handleLast4Interval(searchPoints, searchCategory, searchTerm);
                break;
            case NEXT_4:
                results = handleNext4Interval(searchPoints, searchCategory, searchTerm);
                break;
            case REMAINING:
                results = handleRemainingInterval(searchPoints, searchCategory, searchTerm);
                break;
            case WEEKLY:
                results = handleWeeklyInterval(searchPoints, searchCategory, searchTerm, searchWeek);
        }

        return results;
    }



    private List<DefenseDto> handleSeasonInterval(String searchPoints, String searchCategory, String searchTerm) {
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

    private List<DefenseDto> handleLast4Interval(String searchPoints, String searchCategory, String searchTerm) {
        switch(searchPoints) {
            case TOTAL:
                return handleLast4Total(searchCategory, searchTerm);
            case AVERAGE:
                return handleLast4Average(searchCategory, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleNext4Interval(String searchPoints, String searchCategory, String searchTerm) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleNext4Projected(searchCategory, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleRemainingInterval(String searchPoints, String searchCategory, String searchTerm) {
        if (searchPoints.equalsIgnoreCase(PROJECTED)) {
            return handleRemainingProjected(searchCategory, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleWeeklyInterval(String searchPoints, String searchCategory, String searchTerm, int searchWeek) {
        switch(searchPoints) {
            case TOTAL:
                return handleWeeklyTotal(searchCategory, searchTerm, searchWeek);
            case PROJECTED:
                return handleWeeklyProjected(searchCategory, searchTerm, searchWeek);
        }
        return Collections.emptyList();
    }



    private List<DefenseDto> handleSeasonTotal(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseSeasonTotalDao.getDefenseSeasonTotalStats();
            case CONFERENCE:
                return defenseSeasonTotalDao.getDefenseSeasonTotalStatsByConference(searchTerm);
            case TEAM:
                return defenseSeasonTotalDao.getDefenseSeasonTotalStatsByTeam(searchTerm);
            case NAME:
                return defenseSeasonTotalDao.getDefenseSeasonTotalStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleSeasonAverage(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseSeasonAverageDao.getDefenseSeasonAverageStats();
            case CONFERENCE:
                return defenseSeasonAverageDao.getDefenseSeasonAverageStatsByConference(searchTerm);
            case TEAM:
                return defenseSeasonAverageDao.getDefenseSeasonAverageStatsByTeam(searchTerm);
            case NAME:
                return defenseSeasonAverageDao.getDefenseSeasonAverageStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleSeasonProjected(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseSeasonProjectedDao.getDefenseSeasonProjectedStats();
            case CONFERENCE:
                return defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByConference(searchTerm);
            case TEAM:
                return defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByTeam(searchTerm);
            case NAME:
                return defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleLast4Total(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseLast4TotalDao.getDefenseLast4TotalStats();
            case CONFERENCE:
                return defenseLast4TotalDao.getDefenseLast4TotalStatsByConference(searchTerm);
            case TEAM:
                return defenseLast4TotalDao.getDefenseLast4TotalStatsByTeam(searchTerm);
            case NAME:
                return defenseLast4TotalDao.getDefenseLast4TotalStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleLast4Average(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseLast4AverageDao.getDefenseLast4AverageStats();
            case CONFERENCE:
                return defenseLast4AverageDao.getDefenseLast4AverageStatsByConference(searchTerm);
            case TEAM:
                return defenseLast4AverageDao.getDefenseLast4AverageStatsByTeam(searchTerm);
            case NAME:
                return defenseLast4AverageDao.getDefenseLast4AverageStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleNext4Projected(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseNext4ProjectedDao.getDefenseNext4ProjectedStats();
            case CONFERENCE:
                return defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByConference(searchTerm);
            case TEAM:
                return defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByTeam(searchTerm);
            case NAME:
                return defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleRemainingProjected(String searchCategory, String searchTerm) {
        switch(searchCategory) {
            case ALL:
                return defenseRemainingProjectedDao.getDefenseRemainingProjectedStats();
            case CONFERENCE:
                return defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByConference(searchTerm);
            case TEAM:
                return defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByTeam(searchTerm);
            case NAME:
                return defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByName(searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleWeeklyTotal(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return defenseWeeklyTotalDao.getDefenseWeeklyTotalStats(searchWeek);
            case CONFERENCE:
                return defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }

    private List<DefenseDto> handleWeeklyProjected(String searchCategory, String searchTerm, Integer searchWeek) {
        switch(searchCategory) {
            case ALL:
                return defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStats(searchWeek);
            case CONFERENCE:
                return defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStatsByConference(searchWeek, searchTerm);
            case TEAM:
                return defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
            case NAME:
                return defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStatsByName(searchWeek, searchTerm);
        }
        return Collections.emptyList();
    }
}

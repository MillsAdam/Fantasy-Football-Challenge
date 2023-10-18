package com.techelevator.controller.position.quarterback.regularSeason;

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
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.ArrayList;
import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats")
public class QuarterbackController {
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


    public QuarterbackController(QuarterbackSeasonTotalDao quarterbackSeasonTotalDao,
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


    @RequestMapping(path="/qb/search", method = RequestMethod.GET)
    public List<QuarterbackDto> searchQuarterbackStats(
            @RequestParam("Position") String searchPosition, // qb, flex, rb, wr, te, k, def
            @RequestParam("Interval") String searchInterval, // season, last4, next4, remaining, weekly
            @RequestParam("Points") String searchPoints, // total, average, projected
            @RequestParam("Category") String searchCategory, // all, conference, team, name
            @RequestParam(value = "Term", required = false) String searchTerm,
            @RequestParam(value = "Week", required = false) Integer searchWeek) {

        List<QuarterbackDto> quarterbackDtoList = new ArrayList<>();

        if (searchPosition.equalsIgnoreCase(QB)) {
            if (searchInterval.equalsIgnoreCase(SEASON)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByName(searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(LAST_4)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByAndConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByAndTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByAndName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(NEXT_4)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackNext4ProjectedDao.getQuarterbackNext4ProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(REMAINING)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(WEEKLY)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            }

            if (quarterbackDtoList.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
            }
        }

        return quarterbackDtoList;
    }

}

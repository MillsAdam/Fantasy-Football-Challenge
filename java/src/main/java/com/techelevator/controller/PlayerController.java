package com.techelevator.controller;

import com.techelevator.dao.position.defense.regularSeason.SeasonTotal.DefenseSeasonTotalDao;
import com.techelevator.dao.position.defense.regularSeason.last4Average.DefenseLast4AverageDao;
import com.techelevator.dao.position.defense.regularSeason.last4Total.DefenseLast4TotalDao;
import com.techelevator.dao.position.defense.regularSeason.next4Projected.DefenseNext4ProjectedDao;
import com.techelevator.dao.position.defense.regularSeason.remainingProjected.DefenseRemainingProjectedDao;
import com.techelevator.dao.position.defense.regularSeason.seasonAverage.DefenseSeasonAverageDao;
import com.techelevator.dao.position.defense.regularSeason.seasonProjected.DefenseSeasonProjectedDao;
import com.techelevator.dao.position.defense.regularSeason.weeklyProjected.DefenseWeeklyProjectedDao;
import com.techelevator.dao.position.defense.regularSeason.weeklyTotal.DefenseWeeklyTotalDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.last4Average.FlexPlayerLast4AverageDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.last4Total.FlexPlayerLast4TotalDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.next4Projected.FlexPlayerNext4ProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.remainingProjected.FlexPlayerRemainingProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.seasonAverage.FlexPlayerSeasonAverageDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.seasonProjected.FlexPlayerSeasonProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.seasonTotal.FlexPlayerSeasonTotalDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.weeklyProjected.FlexPlayerWeeklyProjectedDao;
import com.techelevator.dao.position.flexPlayer.regularSeason.weeklyTotal.FlexPlayerWeeklyTotalDao;
import com.techelevator.dao.position.kicker.regularSeason.last4Average.KickerLast4AverageDao;
import com.techelevator.dao.position.kicker.regularSeason.last4Total.KickerLast4TotalDao;
import com.techelevator.dao.position.kicker.regularSeason.next4Projected.KickerNext4ProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.remainingProjected.KickerRemainingProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.seasonAverage.KickerSeasonAverageDao;
import com.techelevator.dao.position.kicker.regularSeason.seasonProjected.KickerSeasonProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.seasonTotal.KickerSeasonTotalDao;
import com.techelevator.dao.position.kicker.regularSeason.weeklyProjected.KickerWeeklyProjectedDao;
import com.techelevator.dao.position.kicker.regularSeason.weeklyTotal.KickerWeeklyTotalDao;
import com.techelevator.dao.position.quarterback.regularSeason.last4Average.QuarterbackLast4AverageDao;
import com.techelevator.dao.position.quarterback.regularSeason.last4Total.QuarterbackLast4TotalDao;
import com.techelevator.dao.position.quarterback.regularSeason.next4Projected.QuarterbackNext4ProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.remainingProjected.QuarterbackRemainingProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.seasonAverage.QuarterbackSeasonAverageDao;
import com.techelevator.dao.position.quarterback.regularSeason.seasonProjected.QuarterbackSeasonProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.seasonTotal.QuarterbackSeasonTotalDao;
import com.techelevator.dao.position.quarterback.regularSeason.weeklyProjected.QuarterbackWeeklyProjectedDao;
import com.techelevator.dao.position.quarterback.regularSeason.weeklyTotal.QuarterbackWeeklyTotalDao;
import com.techelevator.model.position.DefenseDto;
import com.techelevator.model.position.FlexPlayerDto;
import com.techelevator.model.position.KickerDto;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.ArrayList;
import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats")
public class PlayerController {
    private QuarterbackSeasonTotalDao quarterbackSeasonTotalDao;
    private QuarterbackSeasonAverageDao quarterbackSeasonAverageDao;
    private QuarterbackSeasonProjectedDao quarterbackSeasonProjectedDao;
    private QuarterbackLast4TotalDao quarterbackLast4TotalDao;
    private QuarterbackLast4AverageDao quarterbackLast4AverageDao;
    private QuarterbackNext4ProjectedDao quarterbackNext4ProjectedDao;
    private QuarterbackRemainingProjectedDao quarterbackRemainingProjectedDao;
    private QuarterbackWeeklyTotalDao quarterbackWeeklyTotalDao;
    private QuarterbackWeeklyProjectedDao quarterbackWeeklyProjectedDao;
    private FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao;
    private FlexPlayerSeasonAverageDao flexPlayerSeasonAverageDao;
    private FlexPlayerSeasonProjectedDao flexPlayerSeasonProjectedDao;
    private FlexPlayerLast4TotalDao flexPlayerLast4TotalDao;
    private FlexPlayerLast4AverageDao flexPlayerLast4AverageDao;
    private FlexPlayerNext4ProjectedDao flexPlayerNext4ProjectedDao;
    private FlexPlayerRemainingProjectedDao flexPlayerRemainingProjectedDao;
    private FlexPlayerWeeklyTotalDao flexPlayerWeeklyTotalDao;
    private FlexPlayerWeeklyProjectedDao flexPlayerWeeklyProjectedDao;
    private KickerSeasonTotalDao kickerSeasonTotalDao;
    private KickerSeasonAverageDao kickerSeasonAverageDao;
    private KickerSeasonProjectedDao kickerSeasonProjectedDao;
    private KickerLast4TotalDao kickerLast4TotalDao;
    private KickerLast4AverageDao kickerLast4AverageDao;
    private KickerNext4ProjectedDao kickerNext4ProjectedDao;
    private KickerRemainingProjectedDao kickerRemainingProjectedDao;
    private KickerWeeklyTotalDao kickerWeeklyTotalDao;
    private KickerWeeklyProjectedDao kickerWeeklyProjectedDao;
    private DefenseSeasonTotalDao defenseSeasonTotalDao;
    private DefenseSeasonAverageDao defenseSeasonAverageDao;
    private DefenseSeasonProjectedDao defenseSeasonProjectedDao;
    private DefenseLast4TotalDao defenseLast4TotalDao;
    private DefenseLast4AverageDao defenseLast4AverageDao;
    private DefenseNext4ProjectedDao defenseNext4ProjectedDao;
    private DefenseRemainingProjectedDao defenseRemainingProjectedDao;
    private DefenseWeeklyTotalDao defenseWeeklyTotalDao;
    private DefenseWeeklyProjectedDao defenseWeeklyProjectedDao;
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


    public PlayerController(QuarterbackSeasonTotalDao quarterbackSeasonTotalDao,
                            QuarterbackSeasonAverageDao quarterbackSeasonAverageDao,
                            QuarterbackSeasonProjectedDao quarterbackSeasonProjectedDao,
                            QuarterbackLast4TotalDao quarterbackLast4TotalDao,
                            QuarterbackLast4AverageDao quarterbackLast4AverageDao,
                            QuarterbackNext4ProjectedDao quarterbackNext4ProjectedDao,
                            QuarterbackRemainingProjectedDao quarterbackRemainingProjectedDao,
                            QuarterbackWeeklyTotalDao quarterbackWeeklyTotalDao,
                            QuarterbackWeeklyProjectedDao quarterbackWeeklyProjectedDao,
                            FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao,
                            FlexPlayerSeasonAverageDao flexPlayerSeasonAverageDao,
                            FlexPlayerSeasonProjectedDao flexPlayerSeasonProjectedDao,
                            FlexPlayerLast4TotalDao flexPlayerLast4TotalDao,
                            FlexPlayerLast4AverageDao flexPlayerLast4AverageDao,
                            FlexPlayerNext4ProjectedDao flexPlayerNext4ProjectedDao,
                            FlexPlayerRemainingProjectedDao flexPlayerRemainingProjectedDao,
                            FlexPlayerWeeklyTotalDao flexPlayerWeeklyTotalDao,
                            FlexPlayerWeeklyProjectedDao flexPlayerWeeklyProjectedDao,
                            KickerSeasonTotalDao kickerSeasonTotalDao,
                            KickerSeasonAverageDao kickerSeasonAverageDao,
                            KickerSeasonProjectedDao kickerSeasonProjectedDao,
                            KickerLast4TotalDao kickerLast4TotalDao,
                            KickerLast4AverageDao kickerLast4AverageDao,
                            KickerNext4ProjectedDao kickerNext4ProjectedDao,
                            KickerRemainingProjectedDao kickerRemainingProjectedDao,
                            KickerWeeklyTotalDao kickerWeeklyTotalDao,
                            KickerWeeklyProjectedDao kickerWeeklyProjectedDao,
                            DefenseSeasonTotalDao defenseSeasonTotalDao,
                            DefenseSeasonAverageDao defenseSeasonAverageDao,
                            DefenseSeasonProjectedDao defenseSeasonProjectedDao,
                            DefenseLast4TotalDao defenseLast4TotalDao,
                            DefenseLast4AverageDao defenseLast4AverageDao,
                            DefenseNext4ProjectedDao defenseNext4ProjectedDao,
                            DefenseRemainingProjectedDao defenseRemainingProjectedDao,
                            DefenseWeeklyTotalDao defenseWeeklyTotalDao,
                            DefenseWeeklyProjectedDao defenseWeeklyProjectedDao) {
        this.quarterbackSeasonTotalDao = quarterbackSeasonTotalDao;
        this.quarterbackSeasonAverageDao = quarterbackSeasonAverageDao;
        this.quarterbackSeasonProjectedDao = quarterbackSeasonProjectedDao;
        this.quarterbackLast4TotalDao = quarterbackLast4TotalDao;
        this.quarterbackLast4AverageDao = quarterbackLast4AverageDao;
        this.quarterbackNext4ProjectedDao = quarterbackNext4ProjectedDao;
        this.quarterbackRemainingProjectedDao = quarterbackRemainingProjectedDao;
        this.quarterbackWeeklyTotalDao = quarterbackWeeklyTotalDao;
        this.quarterbackWeeklyProjectedDao = quarterbackWeeklyProjectedDao;
        this.flexPlayerSeasonTotalDao = flexPlayerSeasonTotalDao;
        this.flexPlayerSeasonAverageDao = flexPlayerSeasonAverageDao;
        this.flexPlayerSeasonProjectedDao = flexPlayerSeasonProjectedDao;
        this.flexPlayerLast4TotalDao = flexPlayerLast4TotalDao;
        this.flexPlayerLast4AverageDao = flexPlayerLast4AverageDao;
        this.flexPlayerNext4ProjectedDao = flexPlayerNext4ProjectedDao;
        this.flexPlayerRemainingProjectedDao = flexPlayerRemainingProjectedDao;
        this.flexPlayerWeeklyTotalDao = flexPlayerWeeklyTotalDao;
        this.flexPlayerWeeklyProjectedDao = flexPlayerWeeklyProjectedDao;
        this.kickerSeasonTotalDao = kickerSeasonTotalDao;
        this.kickerSeasonAverageDao = kickerSeasonAverageDao;
        this.kickerSeasonProjectedDao = kickerSeasonProjectedDao;
        this.kickerLast4TotalDao = kickerLast4TotalDao;
        this.kickerLast4AverageDao = kickerLast4AverageDao;
        this.kickerNext4ProjectedDao = kickerNext4ProjectedDao;
        this.kickerRemainingProjectedDao = kickerRemainingProjectedDao;
        this.kickerWeeklyTotalDao = kickerWeeklyTotalDao;
        this.kickerWeeklyProjectedDao = kickerWeeklyProjectedDao;
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

    @RequestMapping(path="/search", method = RequestMethod.GET)
    public Object searchStats(
            @RequestParam("Position") String searchPosition, // qb, flex, rb, wr, te, k, def
            @RequestParam("Interval") String searchInterval, // season, last4, next4, remaining, weekly
            @RequestParam("Points") String searchPoints, // total, average, projected
            @RequestParam("Category") String searchCategory, // all, conference, team, name
            @RequestParam(value = "Term", required = false) String searchTerm,
            @RequestParam(value = "Week", required = false) Integer searchWeek) {


        if (searchPosition.equalsIgnoreCase(QB)) {
            List<QuarterbackDto> quarterbackDtoList = new ArrayList<>();

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

            return quarterbackDtoList;
        } else if (searchPosition.equalsIgnoreCase(FLEX)) {
            List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

            if (searchInterval.equalsIgnoreCase(SEASON)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByName(searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(LAST_4)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(NEXT_4)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(REMAINING)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(WEEKLY)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            }

            if (flexPlayerDtoList.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
            }

            return flexPlayerDtoList;
        } else if (searchPosition.equalsIgnoreCase(RB) || searchPosition.equalsIgnoreCase(WR) || searchPosition.equalsIgnoreCase(TE)) {
            List<FlexPlayerDto> flexPlayerDtoList = new ArrayList<>();

            if (searchInterval.equalsIgnoreCase(SEASON)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPosition(searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndConference(searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndTeam(searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndName(searchPosition, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPosition(searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndConference(searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndTeam(searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndName(searchPosition, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPosition(searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndConference(searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndTeam(searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndName(searchPosition, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(LAST_4)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPosition(searchWeek, searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPosition(searchWeek, searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(NEXT_4)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPosition(searchWeek, searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(REMAINING)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPosition(searchWeek, searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(WEEKLY)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPosition(searchWeek, searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerWeeklyTotalDao.getFlexPlayerWeeklyTotalStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPosition(searchWeek, searchPosition);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndConference(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndTeam(searchWeek, searchPosition, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndName(searchWeek, searchPosition, searchTerm);
                    }

                }
            }

            if (flexPlayerDtoList.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
            }

            return flexPlayerDtoList;
        } else if (searchPosition.equalsIgnoreCase(K)) {
            List<KickerDto> kickerDtoList = new ArrayList<>();

            if (searchInterval.equalsIgnoreCase(SEASON)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByName(searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(LAST_4)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerLast4AverageDao.getKickerLast4AverageStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerLast4AverageDao.getKickerLast4AverageStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerLast4AverageDao.getKickerLast4AverageStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerLast4AverageDao.getKickerLast4AverageStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(NEXT_4)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerNext4ProjectedDao.geKickerNext4ProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(REMAINING)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(WEEKLY)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        kickerDtoList = kickerWeeklyProjectedDao.getKickerWeeklyProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        kickerDtoList = kickerWeeklyProjectedDao.getKickerWeeklyProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        kickerDtoList = kickerWeeklyProjectedDao.getAKickerWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        kickerDtoList = kickerWeeklyProjectedDao.getKickerWeeklyProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            }

            if (kickerDtoList.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
            }

            return kickerDtoList;
        } else if (searchPosition.equalsIgnoreCase(DEF)) {
            List<DefenseDto> defenseDtoList = new ArrayList<>();

            if (searchInterval.equalsIgnoreCase(SEASON)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStatsByName(searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStats();
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByConference(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByTeam(searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByName(searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(LAST_4)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(AVERAGE)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(NEXT_4)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(REMAINING)) {
                if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            } else if (searchInterval.equalsIgnoreCase(WEEKLY)) {
                if (searchPoints.equalsIgnoreCase(TOTAL)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByName(searchWeek, searchTerm);
                    }

                } else if (searchPoints.equalsIgnoreCase(PROJECTED)) {

                    if (searchCategory.equalsIgnoreCase(ALL)) {
                        defenseDtoList = defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStats(searchWeek);
                    } else if (searchCategory.equalsIgnoreCase(CONFERENCE)) {
                        defenseDtoList = defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStatsByConference(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(TEAM)) {
                        defenseDtoList = defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStatsByTeam(searchWeek, searchTerm);
                    } else if (searchCategory.equalsIgnoreCase(NAME)) {
                        defenseDtoList = defenseWeeklyProjectedDao.getDefenseWeeklyProjectedStatsByName(searchWeek, searchTerm);
                    }

                }
            }

            if (defenseDtoList.isEmpty()) {
                throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
            }

            return defenseDtoList;
        }

        return null;
    }

}

package com.techelevator.service;

import com.techelevator.service.position.*;
import com.techelevator.service.position.QuarterbackService;
import com.techelevator.service.position.FlexPlayerService;
import com.techelevator.service.position.FlexPlayerPositionService;
import com.techelevator.service.position.KickerService;
import com.techelevator.service.position.DefenseService;
import org.springframework.stereotype.Service;


import java.util.Collections;
import java.util.List;

@Service
public class PlayerStatsService {
    private QuarterbackService quarterbackService;
    private FlexPlayerService flexPlayerService;
    private FlexPlayerPositionService flexPlayerPositionService;
    private KickerService kickerService;
    private DefenseService defenseService;
    private static final String QB = "qb";
    private static final String FLEX = "flex";
    private static final String RB = "rb";
    private static final String WR = "wr";
    private static final String TE = "TE";
    private static final String K = "k";
    private static final String DEF = "def";

    public PlayerStatsService(QuarterbackService quarterbackService,
                              FlexPlayerService flexPlayerService,
                              FlexPlayerPositionService flexPlayerPositionService,
                              KickerService kickerService,
                              DefenseService defenseService) {
        this.quarterbackService = quarterbackService;
        this.flexPlayerService = flexPlayerService;
        this.flexPlayerPositionService = flexPlayerPositionService;
        this.kickerService = kickerService;
        this.defenseService = defenseService;
    }

    public List<?> searchPlayerStats(
            String searchPosition,
            String searchInterval,
            String searchPoints,
            String searchCategory,
            String searchTerm,
            Integer searchWeek) {

        List<?> results = Collections.emptyList();

        switch(searchPosition) {
            case QB:
                results = quarterbackService.searchQuarterbackStats(searchInterval, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case FLEX:
                results = flexPlayerService.searchFlexPlayerStats(searchInterval, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case RB:
            case WR:
            case TE:
                results = flexPlayerPositionService.searchFlexPlayerStats(searchPosition, searchInterval, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case K:
                results = kickerService.searchKickerStats(searchInterval, searchPoints, searchCategory, searchTerm, searchWeek);
                break;
            case DEF:
                results = defenseService.searchDefenseStats(searchInterval, searchPoints, searchCategory, searchTerm, searchWeek);
        }

        return results;
    }
}

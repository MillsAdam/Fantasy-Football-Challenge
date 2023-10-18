package com.techelevator.controller.position.quarterback.regularSeason;


import com.techelevator.dao.position.quarterback.regularSeason.seasonTotal.QuarterbackSeasonTotalDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.parameters.P;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/season/total")
public class QuarterbackSeasonTotalController {
    private QuarterbackSeasonTotalDao quarterbackSeasonTotalDao;
    public QuarterbackSeasonTotalController(QuarterbackSeasonTotalDao quarterbackSeasonTotalDao) {
        this.quarterbackSeasonTotalDao = quarterbackSeasonTotalDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonTotalStats() {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStats();
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonTotalStatsByConference(@PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByConference(conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonTotalStatsByTeam(@PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByTeam(team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonTotalStatsByName(@PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonTotalDao.getQuarterbackSeasonTotalStatsByName(name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

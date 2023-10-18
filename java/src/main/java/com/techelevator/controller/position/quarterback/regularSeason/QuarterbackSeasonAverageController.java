package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.seasonAverage.QuarterbackSeasonAverageDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/season/average")
public class QuarterbackSeasonAverageController {
    private QuarterbackSeasonAverageDao quarterbackSeasonAverageDao;
    public QuarterbackSeasonAverageController(QuarterbackSeasonAverageDao quarterbackSeasonAverageDao) {
        this.quarterbackSeasonAverageDao = quarterbackSeasonAverageDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonAverageStats() {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStats();
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonAverageStatsByConference(@PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByConference(conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonAverageStatsByTeam(@PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByTeam(team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonAverageStatsByName(@PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonAverageDao.getQuarterbackSeasonAverageStatsByName(name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

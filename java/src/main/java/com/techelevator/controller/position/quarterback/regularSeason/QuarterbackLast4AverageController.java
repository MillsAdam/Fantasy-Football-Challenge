package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.last4Average.QuarterbackLast4AverageDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/last4/average")
public class QuarterbackLast4AverageController {
    private QuarterbackLast4AverageDao quarterbackLast4AverageDao;
    public QuarterbackLast4AverageController(QuarterbackLast4AverageDao quarterbackLast4AverageDao) {
        this.quarterbackLast4AverageDao = quarterbackLast4AverageDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4AverageStats(@PathVariable int week) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStats(week);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4AverageStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByConference(week, conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4AverageStatsByAndTeam(@PathVariable int week, @PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByAndTeam(week, team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4AverageStatsByAndName(@PathVariable int week, @PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4AverageDao.getQuarterbackLast4AverageStatsByAndName(week, name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

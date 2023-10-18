package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.weeklyTotal.QuarterbackWeeklyTotalDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/weekly/total")
public class QuarterbackWeeklyTotalController {
    private QuarterbackWeeklyTotalDao quarterbackWeeklyTotalDao;
    public QuarterbackWeeklyTotalController(QuarterbackWeeklyTotalDao quarterbackWeeklyTotalDao) {
        this.quarterbackWeeklyTotalDao = quarterbackWeeklyTotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyTotalStats(@PathVariable int week) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStats(week);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyTotalStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByConference(week, conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyTotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByTeam(week, team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyTotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyTotalDao.getQuarterbackWeeklyTotalStatsByName(week, name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

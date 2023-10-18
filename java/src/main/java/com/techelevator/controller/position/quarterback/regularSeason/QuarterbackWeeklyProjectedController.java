package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.weeklyProjected.QuarterbackWeeklyProjectedDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/weekly/proj")
public class QuarterbackWeeklyProjectedController {
    private QuarterbackWeeklyProjectedDao quarterbackWeeklyProjectedDao;
    public QuarterbackWeeklyProjectedController(QuarterbackWeeklyProjectedDao quarterbackWeeklyProjectedDao) {
        this.quarterbackWeeklyProjectedDao = quarterbackWeeklyProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStats(@PathVariable int week) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStats(week);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByConference(week, conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByTeam(week, team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackWeeklyProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackWeeklyProjectedDao.getQuarterbackWeeklyProjectedStatsByName(week, name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

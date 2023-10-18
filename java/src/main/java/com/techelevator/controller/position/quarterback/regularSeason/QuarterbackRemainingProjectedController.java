package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.remainingProjected.QuarterbackRemainingProjectedDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/remaining/proj")
public class QuarterbackRemainingProjectedController {
    private QuarterbackRemainingProjectedDao quarterbackRemainingProjectedDao;
    public QuarterbackRemainingProjectedController(QuarterbackRemainingProjectedDao quarterbackRemainingProjectedDao) {
        this.quarterbackRemainingProjectedDao = quarterbackRemainingProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackRemainingProjectedStats(@PathVariable int week) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStats(week);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByConference(week, conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByTeam(week, team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackRemainingProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackRemainingProjectedDao.getQuarterbackRemainingProjectedStatsByName(week, name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

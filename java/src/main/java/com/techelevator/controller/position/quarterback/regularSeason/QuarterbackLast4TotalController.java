package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.last4Total.QuarterbackLast4TotalDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/last4/total")
public class QuarterbackLast4TotalController {
    private QuarterbackLast4TotalDao quarterbackLast4TotalDao;
    public QuarterbackLast4TotalController(QuarterbackLast4TotalDao quarterbackLast4TotalDao) {
        this.quarterbackLast4TotalDao = quarterbackLast4TotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4TotalStats(@PathVariable int week) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStats(week);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4TotalStatsByAndConference(@PathVariable int week, @PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByAndConference(week, conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4TotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByTeam(week, team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackLast4TotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackLast4TotalDao.getQuarterbackLast4TotalStatsByName(week, name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

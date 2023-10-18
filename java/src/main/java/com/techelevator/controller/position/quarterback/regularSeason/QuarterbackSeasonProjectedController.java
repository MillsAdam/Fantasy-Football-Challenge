package com.techelevator.controller.position.quarterback.regularSeason;

import com.techelevator.dao.position.quarterback.regularSeason.seasonProjected.QuarterbackSeasonProjectedDao;
import com.techelevator.model.position.QuarterbackDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/qb/season/proj")
public class QuarterbackSeasonProjectedController {
    private QuarterbackSeasonProjectedDao quarterbackSeasonProjectedDao;
    public QuarterbackSeasonProjectedController(QuarterbackSeasonProjectedDao quarterbackSeasonProjectedDao) {
        this.quarterbackSeasonProjectedDao = quarterbackSeasonProjectedDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonProjectedStats() {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStats();
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonProjectedStatsByConference(@PathVariable String conference) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByConference(conference);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonProjectedStatsByTeam(@PathVariable String team) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByTeam(team);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<QuarterbackDto> getQuarterbackSeasonProjectedStatsByName(@PathVariable String name) {
        List<QuarterbackDto> quarterbackDtoList = quarterbackSeasonProjectedDao.getQuarterbackSeasonProjectedStatsByName(name);
        if (quarterbackDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return quarterbackDtoList;
    }
}

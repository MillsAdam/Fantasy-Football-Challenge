package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.weeklyTotal.KickerWeeklyTotalDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/week/total")
public class KickerWeeklyTotalController {
    private KickerWeeklyTotalDao kickerWeeklyTotalDao;
    public KickerWeeklyTotalController(KickerWeeklyTotalDao kickerWeeklyTotalDao) {
        this.kickerWeeklyTotalDao = kickerWeeklyTotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyTotalStats(@PathVariable int week) {
        List<KickerDto> kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStats(week);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyTotalStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByConference(week, conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyTotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByTeam(week, team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyTotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerWeeklyTotalDao.getKickerWeeklyTotalStatsByName(week, name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

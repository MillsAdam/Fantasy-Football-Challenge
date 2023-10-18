package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.weeklyProjected.KickerWeeklyProjectedDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/week/proj")
public class KickerWeeklyProjectedController {
    private KickerWeeklyProjectedDao kickerWeeklyProjectedDao;
    public KickerWeeklyProjectedController(KickerWeeklyProjectedDao kickerWeeklyProjectedDao) {
        this.kickerWeeklyProjectedDao = kickerWeeklyProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyProjectedStats(@PathVariable int week) {
        List<KickerDto> kickerDtoList = kickerWeeklyProjectedDao.getKickerWeeklyProjectedStats(week);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerWeeklyProjectedDao.getKickerWeeklyProjectedStatsByConference(week, conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getAKickerWeeklyProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerWeeklyProjectedDao.getAKickerWeeklyProjectedStatsByTeam(week, team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerWeeklyProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerWeeklyProjectedDao.getKickerWeeklyProjectedStatsByName(week, name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

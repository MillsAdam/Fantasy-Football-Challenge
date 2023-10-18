package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.remainingProjected.KickerRemainingProjectedDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/remaining/proj")
public class KickerRemainingProjectedController {
    private KickerRemainingProjectedDao kickerRemainingProjectedDao;
    public KickerRemainingProjectedController(KickerRemainingProjectedDao kickerRemainingProjectedDao) {
        this.kickerRemainingProjectedDao = kickerRemainingProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<KickerDto> getKickerRemainingProjectedStats(@PathVariable int week) {
        List<KickerDto> kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStats(week);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerRemainingProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByConference(week, conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerRemainingProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByTeam(week, team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerRemainingProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerRemainingProjectedDao.getKickerRemainingProjectedStatsByName(week, name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

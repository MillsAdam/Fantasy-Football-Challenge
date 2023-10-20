package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.next4Projected.KickerNext4ProjectedDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/next4/proj")
public class KickerNext4ProjectedController {
    private KickerNext4ProjectedDao kickerNext4ProjectedDao;
    public KickerNext4ProjectedController(KickerNext4ProjectedDao kickerNext4ProjectedDao) {
        this.kickerNext4ProjectedDao = kickerNext4ProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<KickerDto> geKickerNext4ProjectedStats(@PathVariable int week) {
        List<KickerDto> kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStats(week);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerNext4ProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByConference(week, conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerNext4ProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByTeam(week, team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerNext4ProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerNext4ProjectedDao.getKickerNext4ProjectedStatsByName(week, name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

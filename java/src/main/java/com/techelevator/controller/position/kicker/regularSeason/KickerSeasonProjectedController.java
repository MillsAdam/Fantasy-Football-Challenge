package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.seasonProjected.KickerSeasonProjectedDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/season/proj")
public class KickerSeasonProjectedController {
    private KickerSeasonProjectedDao kickerSeasonProjectedDao;
    public KickerSeasonProjectedController(KickerSeasonProjectedDao kickerSeasonProjectedDao) {
        this.kickerSeasonProjectedDao = kickerSeasonProjectedDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonProjectedStats() {
        List<KickerDto> kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStats();
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonProjectedStatsByConference(@PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByConference(conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonProjectedStatsByTeam(@PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByTeam(team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonProjectedStatsByName(@PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerSeasonProjectedDao.getKickerSeasonProjectedStatsByName(name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

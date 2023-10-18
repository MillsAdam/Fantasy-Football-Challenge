package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.seasonAverage.KickerSeasonAverageDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/season/average")
public class KickerSeasonAverageController {
    private KickerSeasonAverageDao kickerSeasonAverageDao;
    public KickerSeasonAverageController(KickerSeasonAverageDao kickerSeasonAverageDao) {
        this.kickerSeasonAverageDao = kickerSeasonAverageDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonAverageStats() {
        List<KickerDto> kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStats();
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonAverageStatsByConference(@PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStatsByConference(conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonAverageStatsByTeam(@PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStatsByTeam(team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonAverageStatsByName(@PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerSeasonAverageDao.getKickerSeasonAverageStatsByName(name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

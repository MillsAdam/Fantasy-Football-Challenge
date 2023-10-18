package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.seasonTotal.KickerSeasonTotalDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/season/total")
public class KickerSeasonTotalController {
    private KickerSeasonTotalDao kickerSeasonTotalDao;
    public KickerSeasonTotalController(KickerSeasonTotalDao kickerSeasonTotalDao) {
        this.kickerSeasonTotalDao = kickerSeasonTotalDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonTotalStats() {
        List<KickerDto> kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStats();
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonTotalStatsByConference(@PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStatsByConference(conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonTotalStatsByTeam(@PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStatsByTeam(team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerSeasonTotalStatsByName(@PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerSeasonTotalDao.getKickerSeasonTotalStatsByName(name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

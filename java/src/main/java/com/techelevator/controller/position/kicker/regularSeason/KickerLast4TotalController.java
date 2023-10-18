package com.techelevator.controller.position.kicker.regularSeason;

import com.techelevator.dao.position.kicker.regularSeason.last4Total.KickerLast4TotalDao;
import com.techelevator.model.position.KickerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/k/last4/total")
public class KickerLast4TotalController {
    private KickerLast4TotalDao kickerLast4TotalDao;
    public KickerLast4TotalController(KickerLast4TotalDao kickerLast4TotalDao) {
        this.kickerLast4TotalDao = kickerLast4TotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<KickerDto> getKickerLast4TotalStats(@PathVariable int week) {
        List<KickerDto> kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStats(week);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<KickerDto> getKickerLast4TotalStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<KickerDto> kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStatsByConference(week, conference);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<KickerDto> getKickerLast4TotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<KickerDto> kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStatsByTeam(week, team);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<KickerDto> getKickerLast4TotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<KickerDto> kickerDtoList = kickerLast4TotalDao.getKickerLast4TotalStatsByName(week, name);
        if (kickerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Kickers Not Found.");
        }
        return kickerDtoList;
    }
}

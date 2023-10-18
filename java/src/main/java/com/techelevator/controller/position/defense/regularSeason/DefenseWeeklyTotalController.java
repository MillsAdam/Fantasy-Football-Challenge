package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.weeklyTotal.DefenseWeeklyTotalDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/week/total")
public class DefenseWeeklyTotalController {
    private DefenseWeeklyTotalDao defenseWeeklyTotalDao;
    public DefenseWeeklyTotalController(DefenseWeeklyTotalDao defenseWeeklyTotalDao) {
        this.defenseWeeklyTotalDao = defenseWeeklyTotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseWeeklyTotalStats(@PathVariable int week) {
        List<DefenseDto> defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStats(week);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseWeeklyTotalStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByConference(week, conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseWeeklyTotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByTeam(week, team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseWeeklyTotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseWeeklyTotalDao.getDefenseWeeklyTotalStatsByName(week, name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

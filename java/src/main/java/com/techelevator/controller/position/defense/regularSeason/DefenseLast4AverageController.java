package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.last4Average.DefenseLast4AverageDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/last4/average")
public class DefenseLast4AverageController {
    private DefenseLast4AverageDao defenseLast4AverageDao;
    public DefenseLast4AverageController(DefenseLast4AverageDao defenseLast4AverageDao) {
        this.defenseLast4AverageDao = defenseLast4AverageDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4AverageStats(@PathVariable int week) {
        List<DefenseDto> defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStats(week);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4AverageStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStatsByConference(week, conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4AverageStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStatsByTeam(week, team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4AverageStatsByName(@PathVariable int week, @PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseLast4AverageDao.getDefenseLast4AverageStatsByName(week, name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

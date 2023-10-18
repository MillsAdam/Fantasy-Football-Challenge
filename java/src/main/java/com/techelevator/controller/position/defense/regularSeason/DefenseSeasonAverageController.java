package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.seasonAverage.DefenseSeasonAverageDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/season/average")
public class DefenseSeasonAverageController {
    private DefenseSeasonAverageDao defenseSeasonAverageDao;
    public DefenseSeasonAverageController(DefenseSeasonAverageDao defenseSeasonAverageDao) {
        this.defenseSeasonAverageDao = defenseSeasonAverageDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonAverageStats() {
        List<DefenseDto> defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStats();
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonAverageStatsByConference(@PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStatsByConference(conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonAverageStatsByTeam(@PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStatsByTeam(team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonAverageStatsByName(@PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseSeasonAverageDao.getDefenseSeasonAverageStatsByName(name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

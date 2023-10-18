package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.seasonProjected.DefenseSeasonProjectedDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/season/proj")
public class DefenseSeasonProjectedController {
    private DefenseSeasonProjectedDao defenseSeasonProjectedDao;
    public DefenseSeasonProjectedController(DefenseSeasonProjectedDao defenseSeasonProjectedDao) {
        this.defenseSeasonProjectedDao = defenseSeasonProjectedDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonProjectedStats() {
        List<DefenseDto> defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStats();
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonProjectedStatsByConference(@PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByConference(conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonProjectedStatsByTeam(@PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByTeam(team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonProjectedStatsByName(@PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseSeasonProjectedDao.getDefenseSeasonProjectedStatsByName(name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.remainingProjected.DefenseRemainingProjectedDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/remaining/proj")
public class DefenseRemainingProjectedController {
    private DefenseRemainingProjectedDao defenseRemainingProjectedDao;
    public DefenseRemainingProjectedController(DefenseRemainingProjectedDao defenseRemainingProjectedDao) {
        this.defenseRemainingProjectedDao = defenseRemainingProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseRemainingProjectedStats(@PathVariable int week) {
        List<DefenseDto> defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStats(week);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseRemainingProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByConference(week, conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseRemainingProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByTeam(week, team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseRemainingProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseRemainingProjectedDao.getDefenseRemainingProjectedStatsByName(week, name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

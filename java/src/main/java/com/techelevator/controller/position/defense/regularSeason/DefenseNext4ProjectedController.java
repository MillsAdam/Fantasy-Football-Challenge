package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.next4Projected.DefenseNext4ProjectedDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/next4/proj")
public class DefenseNext4ProjectedController {
    private DefenseNext4ProjectedDao defenseNext4ProjectedDao;
    public DefenseNext4ProjectedController(DefenseNext4ProjectedDao defenseNext4ProjectedDao) {
        this.defenseNext4ProjectedDao = defenseNext4ProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseNext4ProjectedStats(@PathVariable int week) {
        List<DefenseDto> defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStats(week);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseNext4ProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByConference(week, conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseNext4ProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByTeam(week, team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseNext4ProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseNext4ProjectedDao.getDefenseNext4ProjectedStatsByName(week, name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

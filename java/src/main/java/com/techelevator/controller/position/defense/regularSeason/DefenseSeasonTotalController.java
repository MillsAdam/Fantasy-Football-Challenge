package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.SeasonTotal.DefenseSeasonTotalDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/season/total")
public class DefenseSeasonTotalController {
    private DefenseSeasonTotalDao defenseSeasonTotalDao;
    public DefenseSeasonTotalController(DefenseSeasonTotalDao defenseSeasonTotalDao) {
        this.defenseSeasonTotalDao = defenseSeasonTotalDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonTotalStats() {
        List<DefenseDto> defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStats();
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonTotalStatsByConference(@PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStatsByConference(conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonTotalStatsByTeam(@PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStatsByTeam(team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseSeasonTotalStatsByName(@PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseSeasonTotalDao.getDefenseSeasonTotalStatsByName(name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

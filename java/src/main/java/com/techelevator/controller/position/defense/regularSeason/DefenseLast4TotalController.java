package com.techelevator.controller.position.defense.regularSeason;

import com.techelevator.dao.position.defense.regularSeason.last4Total.DefenseLast4TotalDao;
import com.techelevator.model.position.DefenseDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/def/last4/total")
public class DefenseLast4TotalController {
    private DefenseLast4TotalDao defenseLast4TotalDao;
    public DefenseLast4TotalController(DefenseLast4TotalDao defenseLast4TotalDao) {
        this.defenseLast4TotalDao = defenseLast4TotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4TotalStats(@PathVariable int week) {
        List<DefenseDto> defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStats(week);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Defenses Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4TotalStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<DefenseDto> defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStatsByConference(week, conference);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4TotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<DefenseDto> defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStatsByTeam(week, team);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<DefenseDto> getDefenseLast4TotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<DefenseDto> defenseDtoList = defenseLast4TotalDao.getDefenseLast4TotalStatsByName(week, name);
        if (defenseDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Quarterbacks Not Found.");
        }
        return defenseDtoList;
    }
}

package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.last4Average.FlexPlayerLast4AverageDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/last4/average")
public class FlexPlayerLast4AverageController {
    private FlexPlayerLast4AverageDao flexPlayerLast4AverageDao;
    public FlexPlayerLast4AverageController(FlexPlayerLast4AverageDao flexPlayerLast4AverageDao) {
        this.flexPlayerLast4AverageDao = flexPlayerLast4AverageDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStats(@PathVariable int week) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStats(week);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByConference(week, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByTeam(week, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByName(@PathVariable int week, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByName(week, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{week}/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPosition(@PathVariable int week, @PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPosition(week, position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndConference(@PathVariable int week, @PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndConference(week, position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndTeam(@PathVariable int week, @PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndTeam(week, position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4AverageStatsByPositionAndName(@PathVariable int week, @PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4AverageDao.getFlexPlayerLast4AverageStatsByPositionAndName(week, position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.seasonAverage.FlexPlayerSeasonAverageDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/season/average")
public class FlexPlayerSeasonAverageController {
    private FlexPlayerSeasonAverageDao flexPlayerSeasonAverageDao;
    public FlexPlayerSeasonAverageController(FlexPlayerSeasonAverageDao flexPlayerSeasonAverageDao) {
        this.flexPlayerSeasonAverageDao = flexPlayerSeasonAverageDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStats() {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStats();
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByConference(@PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByConference(conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByTeam(@PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByTeam(team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsName(@PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsName(name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPosition(@PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPosition(position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndConference(@PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndConference(position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndTeam(@PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndTeam(position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonAverageStatsByPositionAndName(@PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonAverageDao.getFlexPlayerSeasonAverageStatsByPositionAndName(position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

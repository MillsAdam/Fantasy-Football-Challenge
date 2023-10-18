package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.seasonProjected.FlexPlayerSeasonProjectedDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/season/proj")
public class FlexPlayerSeasonProjectedController {
    private FlexPlayerSeasonProjectedDao flexPlayerSeasonProjectedDao;
    public FlexPlayerSeasonProjectedController(FlexPlayerSeasonProjectedDao flexPlayerSeasonProjectedDao) {
        this.flexPlayerSeasonProjectedDao = flexPlayerSeasonProjectedDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStats() {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStats();
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByConference(@PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByConference(conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByTeam(@PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByTeam(team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByName(@PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByName(name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPosition(@PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPosition(position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPositionAndConference(@PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndConference(position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPositionAndTeam(@PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndTeam(position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonProjectedStatsByPositionAndName(@PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonProjectedDao.getFlexPlayerSeasonProjectedStatsByPositionAndName(position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

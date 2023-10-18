package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.seasonTotal.FlexPlayerSeasonTotalDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/season/total")
public class FlexPlayerSeasonTotalController {
    private FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao;
    public FlexPlayerSeasonTotalController(FlexPlayerSeasonTotalDao flexPlayerSeasonTotalDao) {
        this.flexPlayerSeasonTotalDao = flexPlayerSeasonTotalDao;
    }

    @RequestMapping(method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStats() {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStats();
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByConference(@PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByConference(conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByTeam(@PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByTeam(team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByName(@PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByName(name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPosition(@PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPosition(position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPositionAndConference(@PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndConference(position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPositionAndTeam(@PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndTeam(position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerSeasonTotalStatsByPositionAndName(@PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerSeasonTotalDao.getFlexPlayerSeasonTotalStatsByPositionAndName(position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

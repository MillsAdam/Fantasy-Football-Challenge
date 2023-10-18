package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.last4Total.FlexPlayerLast4TotalDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/last4/total")
public class FlexPlayerLast4TotalController {
    private FlexPlayerLast4TotalDao flexPlayerLast4TotalDao;
    public FlexPlayerLast4TotalController(FlexPlayerLast4TotalDao flexPlayerLast4TotalDao) {
        this.flexPlayerLast4TotalDao = flexPlayerLast4TotalDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStats(@PathVariable int week) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStats(week);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByConference(week, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByTeam(week, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByName(@PathVariable int week, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByName(week, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{week}/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPosition(@PathVariable int week, @PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPosition(week, position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndConference(@PathVariable int week, @PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndConference(week, position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndTeam(@PathVariable int week, @PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndTeam(week, position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerLast4TotalStatsByPositionAndName(@PathVariable int week, @PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerLast4TotalDao.getFlexPlayerLast4TotalStatsByPositionAndName(week, position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

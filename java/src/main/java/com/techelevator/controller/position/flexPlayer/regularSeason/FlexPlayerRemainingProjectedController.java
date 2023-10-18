package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.remainingProjected.FlexPlayerRemainingProjectedDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/remaining/proj")
public class FlexPlayerRemainingProjectedController {
    private FlexPlayerRemainingProjectedDao flexPlayerRemainingProjectedDao;
    public FlexPlayerRemainingProjectedController(FlexPlayerRemainingProjectedDao flexPlayerRemainingProjectedDao) {
        this.flexPlayerRemainingProjectedDao = flexPlayerRemainingProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStats(@PathVariable int week) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStats(week);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByConference(week, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByTeam(week, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByName(week, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{week}/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPosition(@PathVariable int week, @PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPosition(week, position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndConference(@PathVariable int week, @PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndConference(week, position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndTeam(@PathVariable int week, @PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndTeam(week, position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerRemainingProjectedStatsByPositionAndName(@PathVariable int week, @PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerRemainingProjectedDao.getFlexPlayerRemainingProjectedStatsByPositionAndName(week, position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

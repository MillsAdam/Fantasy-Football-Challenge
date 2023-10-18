package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.weeklyProjected.FlexPlayerWeeklyProjectedDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/week/proj")
public class FlexPlayerWeeklyProjectedController {
    private FlexPlayerWeeklyProjectedDao flexPlayerWeeklyProjectedDao;
    public FlexPlayerWeeklyProjectedController(FlexPlayerWeeklyProjectedDao flexPlayerWeeklyProjectedDao) {
        this.flexPlayerWeeklyProjectedDao = flexPlayerWeeklyProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStats(@PathVariable int week) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStats(week);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByConference(week, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByTeam(week, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByName(week, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{week}/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPosition(@PathVariable int week, @PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPosition(week, position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndConference(@PathVariable int week, @PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndConference(week, position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndTeam(@PathVariable int week, @PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndTeam(week, position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerWeeklyProjectedStatsByPositionAndName(@PathVariable int week, @PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerWeeklyProjectedDao.getFlexPlayerWeeklyProjectedStatsByPositionAndName(week, position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

package com.techelevator.controller.position.flexPlayer.regularSeason;

import com.techelevator.dao.position.flexPlayer.regularSeason.next4Projected.FlexPlayerNext4ProjectedDao;
import com.techelevator.model.position.FlexPlayerDto;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats/flex/next4/proj")
public class FlexPlayerNext4ProjectedController {
    private FlexPlayerNext4ProjectedDao flexPlayerNext4ProjectedDao;
    public FlexPlayerNext4ProjectedController(FlexPlayerNext4ProjectedDao flexPlayerNext4ProjectedDao) {
        this.flexPlayerNext4ProjectedDao = flexPlayerNext4ProjectedDao;
    }

    @RequestMapping(path="/{week}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStats(@PathVariable int week) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStats(week);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByConference(@PathVariable int week, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByConference(week, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByTeam(@PathVariable int week, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByTeam(week, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByName(@PathVariable int week, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByName(week, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }


    @RequestMapping(path="/{week}/{position}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPosition(@PathVariable int week, @PathVariable String position) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPosition(week, position);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/conference/{conference}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndConference(@PathVariable int week, @PathVariable String position, @PathVariable String conference) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndConference(week, position, conference);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/team/{team}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndTeam(@PathVariable int week, @PathVariable String position, @PathVariable String team) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndTeam(week, position, team);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }

    @RequestMapping(path="/{week}/{position}/name/{name}", method = RequestMethod.GET)
    public List<FlexPlayerDto> getFlexPlayerNext4ProjectedStatsByPositionAndName(@PathVariable int week, @PathVariable String position, @PathVariable String name) {
        List<FlexPlayerDto> flexPlayerDtoList = flexPlayerNext4ProjectedDao.getFlexPlayerNext4ProjectedStatsByPositionAndName(week, position, name);
        if (flexPlayerDtoList == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Flex Players Not Found.");
        }
        return flexPlayerDtoList;
    }
}

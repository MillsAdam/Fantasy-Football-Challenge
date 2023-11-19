package com.techelevator.controller;

import com.techelevator.dao.LeagueDao;
import com.techelevator.dao.RosterDao;
import com.techelevator.dao.UserDao;
import com.techelevator.model.Roster;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import javax.validation.Valid;
import java.security.Principal;
import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("/rosters")
public class RosterController {
    private RosterDao rosterDao;
    private LeagueDao leagueDao;
    private UserDao userDao;

    public RosterController(RosterDao rosterDao, LeagueDao leagueDao, UserDao userDao) {
        this.rosterDao = rosterDao;
        this.leagueDao = leagueDao;
        this.userDao = userDao;
    }

    @ResponseStatus(HttpStatus.CREATED)
    @RequestMapping(path="/create", method = RequestMethod.POST)
    public Roster createRoster(@RequestParam("rosterName") String rosterName,
                               @RequestParam("leagueId") int leagueId,
                               Principal principal) {
        String username = principal.getName();
        int userId = userDao.findIdByUsername(username);

        return rosterDao.createRoster(userId, leagueId, rosterName);
    }

    @GetMapping(path="/{rosterId}")
    public Roster getRosterById(@Valid @PathVariable int rosterId) {
        Roster roster = rosterDao.getRosterById(rosterId);
        if (roster == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Roster Not Found");
        } else {
            return roster;
        }
    }

    @GetMapping()
    public List<Roster> getAllRosters() {
        return rosterDao.getAllRosters();
    }

    @ResponseStatus(HttpStatus.OK)
    @RequestMapping(path="/{rosterId}/add-player", method = RequestMethod.POST)
    public Roster addPlayerToRoster(@PathVariable int rosterId,
                                    @RequestParam("playerPosition") int playerPosition,
                                    @RequestParam("playerID") int playerID) {
        Roster updatedRoster = rosterDao.updateRoster(rosterId, playerPosition, playerID);
        return updatedRoster;
    }


}

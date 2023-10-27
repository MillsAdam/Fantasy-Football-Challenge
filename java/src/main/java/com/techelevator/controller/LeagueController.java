package com.techelevator.controller;

import com.techelevator.dao.LeagueDao;
import com.techelevator.dao.UserDao;
import com.techelevator.model.League;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import javax.validation.Valid;
import java.security.Principal;
import java.sql.Timestamp;
import java.util.Date;
import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("/leagues")
public class LeagueController {
    private LeagueDao leagueDao;
    private UserDao userDao;

    public LeagueController(LeagueDao leagueDao, UserDao userDao) {
        this.leagueDao = leagueDao;
        this.userDao = userDao;
    }

    @ResponseStatus(HttpStatus.CREATED)
    @RequestMapping(path="/create", method = RequestMethod.POST)
    public League createLeague(@RequestParam("leagueName") String leagueName, Principal principal) {
        String username = principal.getName();
        int userId = userDao.findIdByUsername(username);

        java.sql.Timestamp startDate = new Timestamp(new Date().getTime());
        java.sql.Timestamp endDate = new Timestamp(startDate.getTime() + 7 * 24 * 60 * 60 * 1000);

        return leagueDao.createLeague(userId, leagueName);
    }

    @GetMapping(path="/{leagueId}")
    public League getLeagueById(@Valid @PathVariable int leagueId) {
        League league = leagueDao.getLeagueById(leagueId);
        if (league == null) {
            throw new ResponseStatusException(HttpStatus.NOT_FOUND, "League Not Found");
        } else {
            return league;
        }
    }

    @GetMapping()
    public List<League> getAllLeagues() {
        return leagueDao.getAllLeagues();
    }
}

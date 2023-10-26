package com.techelevator.controller;

import com.techelevator.dao.LeagueDao;
import com.techelevator.dao.UserDao;
import com.techelevator.model.League;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.security.Principal;
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
    public void createLeague(@Valid @RequestBody League league, Principal principal) {
        leagueDao.createLeague(league);
    }

    @GetMapping()
    public List<League> getAllLeagues() {
        return leagueDao.getAllLeagues();
    }
}

package com.techelevator.controller;

import com.techelevator.service.PlayerStatsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats")
public class PlayerStatsController {
    private PlayerStatsService playerStatsService;
    @Autowired
    public PlayerStatsController(PlayerStatsService playerStatsService) {
        this.playerStatsService = playerStatsService;
    }

    @GetMapping("/search")
    public List<?> searchPlayerStats(
            @RequestParam("Position") String searchPosition, // qb, flex, rb, wr, te, k, def
            @RequestParam("Interval") String searchInterval, // season, last4, next4, remaining, weekly
            @RequestParam("Points") String searchPoints, // total, average, projected
            @RequestParam("Category") String searchCategory, // all, conference, team, name
            @RequestParam(value = "Term", required = false) String searchTerm,
            @RequestParam(value = "Week", required = false) Integer searchWeek) {
        return playerStatsService.searchPlayerStats(
                searchPosition,
                searchInterval,
                searchPoints,
                searchCategory,
                searchTerm,
                searchWeek
        );
    }
}

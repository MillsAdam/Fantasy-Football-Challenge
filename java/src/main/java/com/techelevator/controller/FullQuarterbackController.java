package com.techelevator.controller;

import com.techelevator.model.position.QuarterbackDto;
import com.techelevator.service.FullQuarterbackService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin
@RestController
@RequestMapping("stats")
public class FullQuarterbackController {
    private FullQuarterbackService fullQuarterbackService;
    @Autowired
    public FullQuarterbackController(FullQuarterbackService fullQuarterbackService) {
        this.fullQuarterbackService = fullQuarterbackService;
    }

    @GetMapping("/qb/search")
    public List<QuarterbackDto> searchQuarterbackStats(
            @RequestParam("Position") String searchPosition, // qb, flex, rb, wr, te, k, def
            @RequestParam("Interval") String searchInterval, // season, last4, next4, remaining, weekly
            @RequestParam("Points") String searchPoints, // total, average, projected
            @RequestParam("Category") String searchCategory, // all, conference, team, name
            @RequestParam(value = "Term", required = false) String searchTerm,
            @RequestParam(value = "Week", required = false) Integer searchWeek) {
        return fullQuarterbackService.searchQuarterbackStats(
                searchPosition,
                searchInterval,
                searchPoints,
                searchCategory,
                searchTerm,
                searchWeek
        );
    }
}

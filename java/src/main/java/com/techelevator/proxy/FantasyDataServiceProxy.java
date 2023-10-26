package com.techelevator.proxy;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.techelevator.dao.DefenseDao;
import com.techelevator.dao.OffenseDao;
import com.techelevator.dao.PlayerDao;
import com.techelevator.dao.TeamDao;
import com.techelevator.model.Defense;
import com.techelevator.model.Offense;
import com.techelevator.model.Player;
import com.techelevator.model.Team;
import com.techelevator.service.FantasyDataService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.util.List;

@RestController
@CrossOrigin
@RequestMapping("/add")
public class FantasyDataServiceProxy {

    @Autowired
    private FantasyDataService fantasyDataService = new FantasyDataService();
    @Autowired
    private OffenseDao offenseDao;
    @Autowired
    private DefenseDao defenseDao;
    @Autowired
    private TeamDao teamDao;
    @Autowired
    private PlayerDao playerDao;


    int currentWeek = Integer.parseInt(System.getProperty("CURRENT_WEEK"));

    public FantasyDataServiceProxy(FantasyDataService fantasyDataService, OffenseDao offenseDao, DefenseDao defenseDao, TeamDao teamDao, PlayerDao playerDao) {
        this.fantasyDataService = fantasyDataService;
        this.offenseDao = offenseDao;
        this.defenseDao = defenseDao;
        this.teamDao = teamDao;
        this.playerDao = playerDao;
    }


    // Add stats by single week
    // playerType = offense / defense
    // statType = stats / proj
    // seasonType = reg / post
    // week = 1-18 for regular season, 1-4 for post season
    @RequestMapping(path="/{playerType}/{statType}/{seasonType}/{week}", method=RequestMethod.POST)
    public ResponseEntity<String> addStatsFromExternalAPIbyWeek(@PathVariable String playerType, @PathVariable String statType, @PathVariable String seasonType, @PathVariable int week) {
        ResponseEntity<String> statsFromAPI = fantasyDataService.getStatsFromExternalAPIByWeek(playerType, statType, seasonType, week);

        if (playerType.equals("offense")) {
            List<Offense> parsedOffenseStats = parseOffenseFromAPI(statsFromAPI);

            if (statType.equals("stats")) {
                for (Offense offense : parsedOffenseStats) {
                    offenseDao.addOffenseStats(offense);
                }
            } else if (statType.equals("proj")) {
                for (Offense offense : parsedOffenseStats) {
                    offenseDao.addOffenseProj(offense);
                }
            }

            return ResponseEntity.ok("Offense stats inserted into the database.");
        } else if (playerType.equals("defense")) {
            List<Defense> parsedDefenseStats = parseDefenseFromAPI(statsFromAPI);

            if (statType.equals("stats")) {
                for (Defense defense : parsedDefenseStats) {
                    defenseDao.addDefenseStats(defense);
                }
            } else if (statType.equals("proj")) {
                for (Defense defense : parsedDefenseStats) {
                    defenseDao.addDefenseProj(defense);
                }
            }

            return ResponseEntity.ok("Defense stats inserted into the database.");
        }

        return ResponseEntity.badRequest().body("Invalid Player Type.");
    }

    // Parse Offense and Defense Stats From API
    private List<Offense> parseOffenseFromAPI(ResponseEntity<String> offenseStatsResponse) {
        String offenseStatsJson = offenseStatsResponse.getBody();
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(offenseStatsJson, new TypeReference<List<Offense>>() {});
        } catch (IOException e) {
            throw new RuntimeException("Error offense stats JSON" + e.getMessage(), e);
        }
    }

    private List<Defense> parseDefenseFromAPI(ResponseEntity<String> defenseStatsResponse) {
        String defenseStatsJson = defenseStatsResponse.getBody();
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(defenseStatsJson, new TypeReference<List<Defense>>() {});
        } catch (IOException e) {
            throw new RuntimeException("Error defense stats JSON" + e.getMessage(), e);
        }
    }



    // Add Batch Teams
    // /batch/teams
    @RequestMapping(path="/batch/teams", method = RequestMethod.POST)
    public ResponseEntity<String> addTeamsFromExternalAPI() {
        try {
            ResponseEntity<String> teamsFromAPI = fantasyDataService.getTeamsFromExternalAPI();
            List<Team> parsedTeams = parseTeamFromAPI(teamsFromAPI);
            for (Team team : parsedTeams) {
                teamDao.addTeams(team);
            }
            return ResponseEntity.ok("Teams inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    // parsed Team and Stadium Detail from API
    private List<Team> parseTeamFromAPI(ResponseEntity<String> teamsResponse) {
        String teamsJson = teamsResponse.getBody();
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(teamsJson, new TypeReference<List<Team>>() {});
        } catch (IOException e) {
            throw new RuntimeException("Error parsing teams JSON" + e.getMessage(), e);
        }
    }



    // Add Batch Teams
    // /batch/teams
    @RequestMapping(path="/batch/players", method = RequestMethod.POST)
    public ResponseEntity<String> addPlayersFromExternalAPI() {
        try {
            ResponseEntity<String> playersFromAPI = fantasyDataService.getPlayersFromExternalAPI();
            List<Player> parsedPlayers = parsePlayersFromAPI(playersFromAPI);
            for (Player player : parsedPlayers) {
                playerDao.addPlayers(player);
            }
            return ResponseEntity.ok("Players inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    // parsed Team and Stadium Detail from API
    private List<Player> parsePlayersFromAPI(ResponseEntity<String> playersResponse) {
        String playersJson = playersResponse.getBody();
        ObjectMapper objectMapper = new ObjectMapper();
        try {
            return objectMapper.readValue(playersJson, new TypeReference<List<Player>>() {});
        } catch (IOException e) {
            throw new RuntimeException("Error parsing players JSON" + e.getMessage(), e);
        }
    }





    // Add Batch Offense
    // /batch/offense
    // stats or proj
    // reg or post
    @RequestMapping(path="/batch/offense/stats/reg", method=RequestMethod.POST)
    public ResponseEntity<String> addOffenseStatsRegFromAPI() {
        try {
            for (int week = 1; week <= currentWeek; week++) {
                ResponseEntity<String> offenseStatsFromAPI = fantasyDataService.getOffenseStatsRegFromExternalAPI(week);
                List<Offense> parsedOffenseStats = parseOffenseFromAPI(offenseStatsFromAPI);
                for (Offense offense : parsedOffenseStats) {
                    offenseDao.addOffenseStats(offense);
                }
            }
            return ResponseEntity.ok("Player stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    @RequestMapping(path="/batch/offense/stats/post", method=RequestMethod.POST)
    public ResponseEntity<String> addOffenseStatsPostFromAPI() {
        try {
            for (int week = 1; week <= 4; week++) {
                ResponseEntity<String> offenseStatsFromAPI = fantasyDataService.getOffenseStatsPostFromExternalAPI(week);
                List<Offense> parsedOffenseStats = parseOffenseFromAPI(offenseStatsFromAPI);
                for (Offense offense : parsedOffenseStats) {
                    offenseDao.addOffenseStats(offense);
                }
            }
            return ResponseEntity.ok("Player stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    @RequestMapping(path="/batch/offense/proj/reg", method=RequestMethod.POST)
    public ResponseEntity<String> addOffenseProjRegFromAPI() {
        try {
            for (int week = 1; week <= 18; week++) {
                ResponseEntity<String> offenseProjFromAPI = fantasyDataService.getOffenseProjRegFromExternalAPI(week);
                List<Offense> parsedOffenseProj = parseOffenseFromAPI(offenseProjFromAPI);
                for (Offense offense : parsedOffenseProj) {
                    offenseDao.addOffenseProj(offense);
                }
            }
            return ResponseEntity.ok("Player stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    @RequestMapping(path="/batch/offense/proj/post", method=RequestMethod.POST)
    public ResponseEntity<String> addOffenseProjPostFromAPI() {
        try {
            for (int week = 1; week <= 4; week++) {
                ResponseEntity<String> offenseProjFromAPI = fantasyDataService.getOffenseProjPostFromExternalAPI(week);
                List<Offense> parsedOffenseProj = parseOffenseFromAPI(offenseProjFromAPI);
                for (Offense offense : parsedOffenseProj) {
                    offenseDao.addOffenseProj(offense);
                }
            }
            return ResponseEntity.ok("Player stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }


    // Add Batch Offense
    // /batch/defense
    // stats or proj
    // reg or post
    @RequestMapping(path="/batch/defense/stats/reg", method=RequestMethod.POST)
    public ResponseEntity<String> addDefenseStatsRegFromAPI() {
        try {
            for (int week = 1; week <= currentWeek; week++) {
                ResponseEntity<String> defenseStatsFromAPI = fantasyDataService.getDefenseStatsRegFromExternalAPI(week);
                List<Defense> parsedPlayerStats = parseDefenseFromAPI(defenseStatsFromAPI);
                for (Defense defense : parsedPlayerStats) {
                    defenseDao.addDefenseStats(defense);
                }
            }
            return ResponseEntity.ok("Defense stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    @RequestMapping(path="/batch/defense/stats/post", method=RequestMethod.POST)
    public ResponseEntity<String> addDefenseStatsPostFromAPI() {
        try {
            for (int week = 1; week <= 4; week++) {
                ResponseEntity<String> defenseStatsFromAPI = fantasyDataService.getDefenseStatsPostFromExternalAPI(week);
                List<Defense> parsedPlayerStats = parseDefenseFromAPI(defenseStatsFromAPI);
                for (Defense defense : parsedPlayerStats) {
                    defenseDao.addDefenseStats(defense);
                }
            }
            return ResponseEntity.ok("Defense stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    @RequestMapping(path="/batch/defense/proj/reg", method=RequestMethod.POST)
    public ResponseEntity<String> addDefenseProjRegFromAPI() {
        try {
            for (int week = 1; week <= 18; week++) {
                ResponseEntity<String> defenseProjFromAPI = fantasyDataService.getDefenseProjRegFromExternalAPI(week);
                List<Defense> parsedPlayerProj = parseDefenseFromAPI(defenseProjFromAPI);
                for (Defense defense : parsedPlayerProj) {
                    defenseDao.addDefenseProj(defense);
                }
            }
            return ResponseEntity.ok("Defense stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    @RequestMapping(path="/batch/defense/proj/post", method=RequestMethod.POST)
    public ResponseEntity<String> addDefenseProjPostFromAPI() {
        try {
            for (int week = 1; week <= 4; week++) {
                ResponseEntity<String> defenseProjFromAPI = fantasyDataService.getDefenseProjPostFromExternalAPI(week);
                List<Defense> parsedPlayerProj = parseDefenseFromAPI(defenseProjFromAPI);
                for (Defense defense : parsedPlayerProj) {
                    defenseDao.addDefenseProj(defense);
                }
            }
            return ResponseEntity.ok("Defense stats inserted into the database.");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

}

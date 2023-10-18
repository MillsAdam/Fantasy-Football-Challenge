package com.techelevator.service;

import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.*;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

import java.util.ArrayList;
import java.util.List;

@Service
public class FantasyDataService {

    private static final String API_BASE_URL = "https://api.sportsdata.io/api/nfl/fantasy/json/";
    private static final String API_KEY = "d9c343f71fad4e1dbb63f512b9bcdbcd";
    private static final String PLAYER_GAME_STATS_BY_WEEK = "PlayerGameStatsByWeek/";
    private static final String FANTASY_DEFENSE_BY_GAME = "FantasyDefenseByGame/";
    private static final String PLAYER_GAME_PROJECTION_STATS_BY_WEEK = "PlayerGameProjectionStatsByWeek/";
    private static final String FANTASY_DEFENSE_PROJECTIONS_BY_GAME = "FantasyDefenseProjectionsByGame/";
    private static final String REG = "2022REG/";
    private static final String POST = "2022POST/";
    private static final String TEAMS = "Teams/";
    private final RestTemplate restTemplate = new RestTemplate();


    public ResponseEntity<String> getStatsFromExternalAPIByWeek(String playerType, String statType, String seasonType, int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        String url = null;

        if (playerType.equals("offense")) {
            if (statType.equals("stats")) {
                url = API_BASE_URL + PLAYER_GAME_STATS_BY_WEEK;
            } else if (statType.equals("proj")) {
                url = API_BASE_URL + PLAYER_GAME_PROJECTION_STATS_BY_WEEK;
            }
        } else if (playerType.equals("defense")) {
            if (statType.equals("stats")) {
                url = API_BASE_URL + FANTASY_DEFENSE_BY_GAME;
            } else if (statType.equals("proj")) {
                url = API_BASE_URL + FANTASY_DEFENSE_PROJECTIONS_BY_GAME;
            }
        }

        if (seasonType.equals("reg")) {
            url += REG + week;
        } else if (seasonType.equals("post")) {
            url += POST + week;
        }

        ResponseEntity<String> responseEntity = restTemplate.exchange(
                url,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }


    // Teams
    public ResponseEntity<String> getTeamsFromExternalAPI() {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + TEAMS,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }


    // Batch Offense
    public ResponseEntity<String> getOffenseStatsRegFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
            ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + PLAYER_GAME_STATS_BY_WEEK + "/" + REG + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
            );
        return responseEntity;
    }

    public ResponseEntity<String> getOffenseStatsPostFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + PLAYER_GAME_STATS_BY_WEEK + "/" + POST + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }

    public ResponseEntity<String> getOffenseProjRegFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + PLAYER_GAME_PROJECTION_STATS_BY_WEEK + "/" + REG + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }

    public ResponseEntity<String> getOffenseProjPostFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + PLAYER_GAME_PROJECTION_STATS_BY_WEEK + "/" + POST + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }


    // Batch Defense
    public ResponseEntity<String> getDefenseStatsRegFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + FANTASY_DEFENSE_BY_GAME + "/" + REG + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }

    public ResponseEntity<String> getDefenseStatsPostFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + FANTASY_DEFENSE_BY_GAME + "/" + POST + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }

    public ResponseEntity<String> getDefenseProjRegFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + FANTASY_DEFENSE_PROJECTIONS_BY_GAME + "/" + REG + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }

    public ResponseEntity<String> getDefenseProjPostFromExternalAPI(int week) {
        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.setContentType(MediaType.APPLICATION_JSON);
        httpHeaders.set("Ocp-Apim-Subscription-Key", API_KEY);
        HttpEntity<String> entity = new HttpEntity<>(httpHeaders);
        ResponseEntity<String> responseEntity = restTemplate.exchange(
                API_BASE_URL + FANTASY_DEFENSE_PROJECTIONS_BY_GAME + "/" + POST + "/" + week,
                HttpMethod.GET,
                entity,
                new ParameterizedTypeReference<String>() {}
        );
        return responseEntity;
    }
}

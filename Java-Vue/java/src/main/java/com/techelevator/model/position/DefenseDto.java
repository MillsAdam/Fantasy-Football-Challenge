package com.techelevator.model.position;


import java.util.HashMap;
import java.util.Map;

public class DefenseDto {
    private int playerID;
    private int week;
    private String team;
    private String position;
    private String name;
    private double defensiveTouchdowns;
    private double specialTeamsTouchdowns;
    private double touchdownsScored;
    private double fumblesForced;
    private double fumblesRecovered;
    private double interceptions;
    private double tacklesForLoss;
    private double quarterbackHits;
    private double sacks;
    private double safeties;
    private double blockedKicks;
    private double pointsAllowed;
    private double fantasyPointsTotal;
    private double fantasyPointsAverage;

    private static final Map<String, String> teamNames = new HashMap<>();
    static {
        teamNames.put("ARI", "Arizona Cardinals");
        teamNames.put("ATL", "Atlanta Falcons");
        teamNames.put("BAL", "Baltimore Ravens");
        teamNames.put("BUF", "Buffalo Bills");
        teamNames.put("CAR", "Carolina Panthers");
        teamNames.put("CHI", "Chicago Bears");
        teamNames.put("CIN", "Cincinnati Bengals");
        teamNames.put("CLE", "Cleveland Browns");
        teamNames.put("DAL", "Dallas Cowboys");
        teamNames.put("DEN", "Denver Broncos");
        teamNames.put("DET", "Detroit Lions");
        teamNames.put("GB", "Green Bay Packers");
        teamNames.put("HOU", "Houston Texans");
        teamNames.put("IND", "Indianapolis Colts");
        teamNames.put("JAX", "Jacksonville Jaguars");
        teamNames.put("KC", "Kansas City Chiefs");
        teamNames.put("LAC", "Los Angeles Chargers");
        teamNames.put("LAR", "Los Angeles Rams");
        teamNames.put("LV", "Las Vegas Raiders");
        teamNames.put("MIA", "Miami Dolphins");
        teamNames.put("MIN", "Minnesota Vikings");
        teamNames.put("NE", "New England Patriots");
        teamNames.put("NO", "New Orleans Saints");
        teamNames.put("NYG", "New York Giants");
        teamNames.put("NYJ", "New York Jets");
        teamNames.put("PHI", "Philadelphia Eagles");
        teamNames.put("PIT", "Pittsburgh Steelers");
        teamNames.put("SEA", "Seattle Seahawks");
        teamNames.put("SF", "San Francisco 49ers");
        teamNames.put("TB", "Tampa Bay Buccaneers");
        teamNames.put("TEN", "Tennessee Titans");
        teamNames.put("WAS", "Washington Commanders");
    }


    public DefenseDto() {
    }


    public DefenseDto(int playerID, int week, String team, String position, String name, double defensiveTouchdowns, double specialTeamsTouchdowns, double touchdownsScored, double fumblesForced, double fumblesRecovered, double interceptions, double tacklesForLoss, double quarterbackHits, double sacks, double safeties, double blockedKicks, double pointsAllowed, double fantasyPointsTotal, double fantasyPointsAverage) {
        this.playerID = playerID;
        this.week = week;
        this.team = team;
        this.position = position;
        this.name = name;
        this.defensiveTouchdowns = defensiveTouchdowns;
        this.specialTeamsTouchdowns = specialTeamsTouchdowns;
        this.touchdownsScored = touchdownsScored;
        this.fumblesForced = fumblesForced;
        this.fumblesRecovered = fumblesRecovered;
        this.interceptions = interceptions;
        this.tacklesForLoss = tacklesForLoss;
        this.quarterbackHits = quarterbackHits;
        this.sacks = sacks;
        this.safeties = safeties;
        this.blockedKicks = blockedKicks;
        this.pointsAllowed = pointsAllowed;
        this.fantasyPointsTotal = fantasyPointsTotal;
        this.fantasyPointsAverage = fantasyPointsAverage;
    }


    public int getPlayerID() {
        return playerID;
    }

    public void setPlayerID(int playerID) {
        this.playerID = playerID;
    }

    public int getWeek() {
        return week;
    }

    public void setWeek(int week) {
        this.week = week;
    }

    public String getTeam() {
        return team;
    }

    public void setTeam(String team) {
        this.team = team;
    }

    public String getPosition() {
        return "DEF";
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public String getName() { return name; }

    public void setName(String name) {
        this.name = name;
    }

    public double getDefensiveTouchdowns() {
        return defensiveTouchdowns;
    }

    public void setDefensiveTouchdowns(double defensiveTouchdowns) {
        this.defensiveTouchdowns = defensiveTouchdowns;
    }

    public double getSpecialTeamsTouchdowns() {
        return specialTeamsTouchdowns;
    }

    public void setSpecialTeamsTouchdowns(double specialTeamsTouchdowns) {
        this.specialTeamsTouchdowns = specialTeamsTouchdowns;
    }

    public double getTouchdownsScored() {
        return touchdownsScored;
    }

    public void setTouchdownsScored(double touchdownsScored) {
        this.touchdownsScored = touchdownsScored;
    }

    public double getFumblesForced() {
        return fumblesForced;
    }

    public void setFumblesForced(double fumblesForced) {
        this.fumblesForced = fumblesForced;
    }

    public double getFumblesRecovered() {
        return fumblesRecovered;
    }

    public void setFumblesRecovered(double fumblesRecovered) {
        this.fumblesRecovered = fumblesRecovered;
    }

    public double getInterceptions() {
        return interceptions;
    }

    public void setInterceptions(double interceptions) {
        this.interceptions = interceptions;
    }

    public double getTacklesForLoss() {
        return tacklesForLoss;
    }

    public void setTacklesForLoss(double tacklesForLoss) {
        this.tacklesForLoss = tacklesForLoss;
    }

    public double getQuarterbackHits() {
        return quarterbackHits;
    }

    public void setQuarterbackHits(double quarterbackHits) {
        this.quarterbackHits = quarterbackHits;
    }

    public double getSacks() {
        return sacks;
    }

    public void setSacks(double sacks) {
        this.sacks = sacks;
    }

    public double getSafeties() {
        return safeties;
    }

    public void setSafeties(double safeties) {
        this.safeties = safeties;
    }

    public double getBlockedKicks() {
        return blockedKicks;
    }

    public void setBlockedKicks(double blockedKicks) {
        this.blockedKicks = blockedKicks;
    }

    public double getPointsAllowed() {
        return pointsAllowed;
    }

    public void setPointsAllowed(double pointsAllowed) {
        this.pointsAllowed = pointsAllowed;
    }

    public double getFantasyPointsTotal() {
        return fantasyPointsTotal;
    }

    public void setFantasyPointsTotal(double fantasyPointsTotal) {
        this.fantasyPointsTotal = fantasyPointsTotal;
    }

    public double getFantasyPointsAverage() {
        return fantasyPointsAverage;
    }

    public void setFantasyPointsAverage(double fantasyPointsAverage) {
        this.fantasyPointsAverage = fantasyPointsAverage;
    }


    @Override
    public String toString() {
        return "DefenseDto{" +
                "playerID=" + playerID +
                ", week=" + week +
                ", team='" + team + '\'' +
                ", position='" + position + '\'' +
                ", name='" + name + '\'' +
                ", defensiveTouchdowns=" + defensiveTouchdowns +
                ", specialTeamsTouchdowns=" + specialTeamsTouchdowns +
                ", touchdownsScored=" + touchdownsScored +
                ", fumblesForced=" + fumblesForced +
                ", fumblesRecovered=" + fumblesRecovered +
                ", interceptions=" + interceptions +
                ", tacklesForLoss=" + tacklesForLoss +
                ", quarterbackHits=" + quarterbackHits +
                ", sacks=" + sacks +
                ", safeties=" + safeties +
                ", blockedKicks=" + blockedKicks +
                ", pointsAllowed=" + pointsAllowed +
                ", fantasyPointsTotal=" + fantasyPointsTotal +
                ", fantasyPointsAverage=" + fantasyPointsAverage +
                '}';
    }
}

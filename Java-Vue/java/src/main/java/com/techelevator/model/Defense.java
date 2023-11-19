package com.techelevator.model;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.sql.Timestamp;

public class Defense {
    @JsonProperty("GameKey")
    private String gameKey;
    @JsonProperty("SeasonType")
    private int seasonType;
    @JsonProperty("Season")
    private int season;
    @JsonProperty("Week")
    private int week;
    @JsonProperty("Date")
    private Timestamp date;
    @JsonProperty("Team")
    private String team;
    @JsonProperty("Opponent")
    private String opponent;
    @JsonProperty("PointsAllowed")
    private double pointsAllowed;
    @JsonProperty("TouchdownsScored")
    private double touchdownsScored;
    @JsonProperty("Sacks")
    private double sacks;
    @JsonProperty("SackYards")
    private double sackYards;
    @JsonProperty("FumblesForced")
    private double fumblesForced;
    @JsonProperty("FumblesRecovered")
    private double fumblesRecovered;
    @JsonProperty("FumbleReturnTouchdowns")
    private double fumbleReturnTouchdowns;
    @JsonProperty("Interceptions")
    private double interceptions;
    @JsonProperty("InterceptionReturnTouchdowns")
    private double interceptionReturnTouchdowns;
    @JsonProperty("BlockedKicks")
    private double blockedKicks;
    @JsonProperty("Safeties")
    private double safeties;
    @JsonProperty("PuntReturnTouchdowns")
    private double puntReturnTouchdowns;
    @JsonProperty("KickReturnTouchdowns")
    private double kickReturnTouchdowns;
    @JsonProperty("BlockedKickReturnTouchdowns")
    private double blockedKickReturnTouchdowns;
    @JsonProperty("FieldGoalReturnTouchdowns")
    private double fieldGoalReturnTouchdowns;
    @JsonProperty("QuarterbackHits")
    private double quarterbackHits;
    @JsonProperty("TacklesForLoss")
    private double tacklesForLoss;
    @JsonProperty("DefensiveTouchdowns")
    private double defensiveTouchdowns;
    @JsonProperty("SpecialTeamsTouchdowns")
    private double specialTeamsTouchdowns;
    @JsonProperty("FantasyPoints")
    private double fantasyPoints;
    @JsonProperty("PointsAllowedByDefenseSpecialTeams")
    private double pointsAllowedByDefenseSpecialTeams;
    @JsonProperty("TwoPointConversionReturns")
    private double twoPointConversionReturns;
    @JsonProperty("FantasyPointsFanDuel")
    private double fantasyPointsFanDuel;
    @JsonProperty("FantasyPointsDraftKings")
    private double fantasyPointsDraftKings;
    @JsonProperty("PlayerID")
    private int playerID;
    @JsonProperty("HomeOrAway")
    private String homeOrAway;
    @JsonProperty("TeamID")
    private int teamID;
    @JsonProperty("OpponentID")
    private int opponentID;
    @JsonProperty("ScoreID")
    private int scoreID;


    public Defense() {
    }


    public Defense(String gameKey, int seasonType, int season, int week, Timestamp date, String team, String opponent, double pointsAllowed, double touchdownsScored, double sacks, double sackYards, double fumblesForced, double fumblesRecovered, double fumbleReturnTouchdowns, double interceptions, double interceptionReturnTouchdowns, double blockedKicks, double safeties, double puntReturnTouchdowns, double kickReturnTouchdowns, double blockedKickReturnTouchdowns, double fieldGoalReturnTouchdowns, double quarterbackHits, double tacklesForLoss, double defensiveTouchdowns, double specialTeamsTouchdowns, double fantasyPoints, double pointsAllowedByDefenseSpecialTeams, double twoPointConversionReturns, double fantasyPointsFanDuel, double fantasyPointsDraftKings, int playerID, String homeOrAway, int teamID, int opponentID, int scoreID) {
        this.gameKey = gameKey;
        this.seasonType = seasonType;
        this.season = season;
        this.week = week;
        this.date = date;
        this.team = team;
        this.opponent = opponent;
        this.pointsAllowed = pointsAllowed;
        this.touchdownsScored = touchdownsScored;
        this.sacks = sacks;
        this.sackYards = sackYards;
        this.fumblesForced = fumblesForced;
        this.fumblesRecovered = fumblesRecovered;
        this.fumbleReturnTouchdowns = fumbleReturnTouchdowns;
        this.interceptions = interceptions;
        this.interceptionReturnTouchdowns = interceptionReturnTouchdowns;
        this.blockedKicks = blockedKicks;
        this.safeties = safeties;
        this.puntReturnTouchdowns = puntReturnTouchdowns;
        this.kickReturnTouchdowns = kickReturnTouchdowns;
        this.blockedKickReturnTouchdowns = blockedKickReturnTouchdowns;
        this.fieldGoalReturnTouchdowns = fieldGoalReturnTouchdowns;
        this.quarterbackHits = quarterbackHits;
        this.tacklesForLoss = tacklesForLoss;
        this.defensiveTouchdowns = defensiveTouchdowns;
        this.specialTeamsTouchdowns = specialTeamsTouchdowns;
        this.fantasyPoints = fantasyPoints;
        this.pointsAllowedByDefenseSpecialTeams = pointsAllowedByDefenseSpecialTeams;
        this.twoPointConversionReturns = twoPointConversionReturns;
        this.fantasyPointsFanDuel = fantasyPointsFanDuel;
        this.fantasyPointsDraftKings = fantasyPointsDraftKings;
        this.playerID = playerID;
        this.homeOrAway = homeOrAway;
        this.teamID = teamID;
        this.opponentID = opponentID;
        this.scoreID = scoreID;
    }


    public String getGameKey() {
        return gameKey;
    }

    public void setGameKey(String gameKey) {
        this.gameKey = gameKey;
    }

    public int getSeasonType() {
        return seasonType;
    }

    public void setSeasonType(int seasonType) {
        this.seasonType = seasonType;
    }

    public int getSeason() {
        return season;
    }

    public void setSeason(int season) {
        this.season = season;
    }

    public int getWeek() {
        return week;
    }

    public void setWeek(int week) {
        this.week = week;
    }

    public Timestamp getDate() {
        return date;
    }

    public void setDate(Timestamp date) {
        this.date = date;
    }

    public String getTeam() {
        return team;
    }

    public void setTeam(String team) {
        this.team = team;
    }

    public String getOpponent() {
        return opponent;
    }

    public void setOpponent(String opponent) {
        this.opponent = opponent;
    }

    public double getPointsAllowed() {
        return pointsAllowed;
    }

    public void setPointsAllowed(double pointsAllowed) {
        this.pointsAllowed = pointsAllowed;
    }

    public double getTouchdownsScored() {
        return touchdownsScored;
    }

    public void setTouchdownsScored(double touchdownsScored) {
        this.touchdownsScored = touchdownsScored;
    }

    public double getSacks() {
        return sacks;
    }

    public void setSacks(double sacks) {
        this.sacks = sacks;
    }

    public double getSackYards() {
        return sackYards;
    }

    public void setSackYards(double sackYards) {
        this.sackYards = sackYards;
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

    public double getFumbleReturnTouchdowns() {
        return fumbleReturnTouchdowns;
    }

    public void setFumbleReturnTouchdowns(double fumbleReturnTouchdowns) {
        this.fumbleReturnTouchdowns = fumbleReturnTouchdowns;
    }

    public double getInterceptions() {
        return interceptions;
    }

    public void setInterceptions(double interceptions) {
        this.interceptions = interceptions;
    }

    public double getInterceptionReturnTouchdowns() {
        return interceptionReturnTouchdowns;
    }

    public void setInterceptionReturnTouchdowns(double interceptionReturnTouchdowns) {
        this.interceptionReturnTouchdowns = interceptionReturnTouchdowns;
    }

    public double getBlockedKicks() {
        return blockedKicks;
    }

    public void setBlockedKicks(double blockedKicks) {
        this.blockedKicks = blockedKicks;
    }

    public double getSafeties() {
        return safeties;
    }

    public void setSafeties(double safeties) {
        this.safeties = safeties;
    }

    public double getPuntReturnTouchdowns() {
        return puntReturnTouchdowns;
    }

    public void setPuntReturnTouchdowns(double puntReturnTouchdowns) {
        this.puntReturnTouchdowns = puntReturnTouchdowns;
    }

    public double getKickReturnTouchdowns() {
        return kickReturnTouchdowns;
    }

    public void setKickReturnTouchdowns(double kickReturnTouchdowns) {
        this.kickReturnTouchdowns = kickReturnTouchdowns;
    }

    public double getBlockedKickReturnTouchdowns() {
        return blockedKickReturnTouchdowns;
    }

    public void setBlockedKickReturnTouchdowns(double blockedKickReturnTouchdowns) {
        this.blockedKickReturnTouchdowns = blockedKickReturnTouchdowns;
    }

    public double getFieldGoalReturnTouchdowns() {
        return fieldGoalReturnTouchdowns;
    }

    public void setFieldGoalReturnTouchdowns(double fieldGoalReturnTouchdowns) {
        this.fieldGoalReturnTouchdowns = fieldGoalReturnTouchdowns;
    }

    public double getQuarterbackHits() {
        return quarterbackHits;
    }

    public void setQuarterbackHits(double quarterbackHits) {
        this.quarterbackHits = quarterbackHits;
    }

    public double getTacklesForLoss() {
        return tacklesForLoss;
    }

    public void setTacklesForLoss(double tacklesForLoss) {
        this.tacklesForLoss = tacklesForLoss;
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

    public double getFantasyPoints() {
        return fantasyPoints;
    }

    public void setFantasyPoints(double fantasyPoints) {
        this.fantasyPoints = fantasyPoints;
    }

    public double getPointsAllowedByDefenseSpecialTeams() {
        return pointsAllowedByDefenseSpecialTeams;
    }

    public void setPointsAllowedByDefenseSpecialTeams(double pointsAllowedByDefenseSpecialTeams) {
        this.pointsAllowedByDefenseSpecialTeams = pointsAllowedByDefenseSpecialTeams;
    }

    public double getTwoPointConversionReturns() {
        return twoPointConversionReturns;
    }

    public void setTwoPointConversionReturns(double twoPointConversionReturns) {
        this.twoPointConversionReturns = twoPointConversionReturns;
    }

    public double getFantasyPointsFanDuel() {
        return fantasyPointsFanDuel;
    }

    public void setFantasyPointsFanDuel(double fantasyPointsFanDuel) {
        this.fantasyPointsFanDuel = fantasyPointsFanDuel;
    }

    public double getFantasyPointsDraftKings() {
        return fantasyPointsDraftKings;
    }

    public void setFantasyPointsDraftKings(double fantasyPointsDraftKings) {
        this.fantasyPointsDraftKings = fantasyPointsDraftKings;
    }

    public int getPlayerID() {
        return playerID;
    }

    public void setPlayerID(int playerID) {
        this.playerID = playerID;
    }

    public String getHomeOrAway() {
        return homeOrAway;
    }

    public void setHomeOrAway(String homeOrAway) {
        this.homeOrAway = homeOrAway;
    }

    public int getTeamID() {
        return teamID;
    }

    public void setTeamID(int teamID) {
        this.teamID = teamID;
    }

    public int getOpponentID() {
        return opponentID;
    }

    public void setOpponentID(int opponentID) {
        this.opponentID = opponentID;
    }

    public int getScoreID() {
        return scoreID;
    }

    public void setScoreID(int scoreID) {
        this.scoreID = scoreID;
    }


    @Override
    public String toString() {
        return "DefenseStats{" +
                "gameKey='" + gameKey + '\'' +
                ", seasonType=" + seasonType +
                ", season=" + season +
                ", week=" + week +
                ", date=" + date +
                ", team='" + team + '\'' +
                ", opponent='" + opponent + '\'' +
                ", pointsAllowed=" + pointsAllowed +
                ", touchdownsScored=" + touchdownsScored +
                ", sacks=" + sacks +
                ", sackYards=" + sackYards +
                ", fumblesForced=" + fumblesForced +
                ", fumblesRecovered=" + fumblesRecovered +
                ", fumbleReturnTouchdowns=" + fumbleReturnTouchdowns +
                ", interceptions=" + interceptions +
                ", interceptionReturnTouchdowns=" + interceptionReturnTouchdowns +
                ", blockedKicks=" + blockedKicks +
                ", safeties=" + safeties +
                ", puntReturnTouchdowns=" + puntReturnTouchdowns +
                ", kickReturnTouchdowns=" + kickReturnTouchdowns +
                ", blockedKickReturnTouchdowns=" + blockedKickReturnTouchdowns +
                ", fieldGoalReturnTouchdowns=" + fieldGoalReturnTouchdowns +
                ", quarterbackHits=" + quarterbackHits +
                ", tacklesForLoss=" + tacklesForLoss +
                ", defensiveTouchdowns=" + defensiveTouchdowns +
                ", specialTeamsTouchdowns=" + specialTeamsTouchdowns +
                ", fantasyPoints=" + fantasyPoints +
                ", pointsAllowedByDefenseSpecialTeams=" + pointsAllowedByDefenseSpecialTeams +
                ", twoPointConversionReturns=" + twoPointConversionReturns +
                ", fantasyPointsFanDuel=" + fantasyPointsFanDuel +
                ", fantasyPointsDraftKings=" + fantasyPointsDraftKings +
                ", playerID=" + playerID +
                ", homeOrAway='" + homeOrAway + '\'' +
                ", teamID=" + teamID +
                ", opponentID=" + opponentID +
                ", scoreID=" + scoreID +
                '}';
    }
}

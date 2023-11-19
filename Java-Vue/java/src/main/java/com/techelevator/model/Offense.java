package com.techelevator.model;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.sql.Timestamp;


public class Offense {
    @JsonProperty("GameKey")
    private String gameKey;
    @JsonProperty("PlayerID")
    private int playerID;
    @JsonProperty("SeasonType")
    private int seasonType;
    @JsonProperty("Season")
    private int season;
    @JsonProperty("GameDate")
    private Timestamp gameDate;
    @JsonProperty("Week")
    private int week;
    @JsonProperty("Team")
    private String team;
    @JsonProperty("Opponent")
    private String opponent;
    @JsonProperty("HomeOrAway")
    private String homeOrAway;
    @JsonProperty("Number")
    private int number;
    @JsonProperty("Name")
    private String name;
    @JsonProperty("Position")
    private String position;
    @JsonProperty("PositionCategory")
    private String positionCategory;
    @JsonProperty("Played")
    private int played;
    @JsonProperty("Started")
    private int started;
    @JsonProperty("PassingAttempts")
    private double passingAttempts;
    @JsonProperty("PassingCompletions")
    private double passingCompletions;
    @JsonProperty("PassingYards")
    private double passingYards;
    @JsonProperty("PassingCompletionPercentage")
    private double passingCompletionPercentage;
    @JsonProperty("PassingYardsPerAttempt")
    private double passingYardsPerAttempt;
    @JsonProperty("PassingYardsPerCompletion")
    private double passingYardsPerCompletion;
    @JsonProperty("PassingTouchdowns")
    private double passingTouchdowns;
    @JsonProperty("PassingInterceptions")
    private double passingInterceptions;
    @JsonProperty("PassingRating")
    private double passingRating;
    @JsonProperty("PassingLong")
    private double passingLong;
    @JsonProperty("PassingSacks")
    private double passingSacks;
    @JsonProperty("PassingSackYards")
    private double passingSackYards;
    @JsonProperty("RushingAttempts")
    private double rushingAttempts;
    @JsonProperty("RushingYards")
    private double rushingYards;
    @JsonProperty("RushingYardsPerAttempt")
    private double rushingYardsPerAttempt;
    @JsonProperty("RushingTouchdowns")
    private double rushingTouchdowns;
    @JsonProperty("RushingLong")
    private double rushingLong;
    @JsonProperty("ReceivingTargets")
    private double receivingTargets;
    @JsonProperty("Receptions")
    private double receptions;
    @JsonProperty("ReceivingYards")
    private double receivingYards;
    @JsonProperty("ReceivingYardsPerReception")
    private double receivingYardsPerReception;
    @JsonProperty("ReceivingTouchdowns")
    private double receivingTouchdowns;
    @JsonProperty("ReceivingLong")
    private double receivingLong;
    @JsonProperty("Fumbles")
    private double fumbles;
    @JsonProperty("FumblesLost")
    private double fumblesLost;
    @JsonProperty("PuntReturns")
    private double puntReturns;
    @JsonProperty("PuntReturnYards")
    private double puntReturnYards;
    @JsonProperty("PuntReturnTouchdowns")
    private double puntReturnTouchdowns;
    @JsonProperty("KickReturns")
    private double kickReturns;
    @JsonProperty("KickReturnYards")
    private double kickReturnYards;
    @JsonProperty("KickReturnTouchdowns")
    private double kickReturnTouchdowns;
    @JsonProperty("SoloTackles")
    private double soloTackles;
    @JsonProperty("AssistedTackles")
    private double assistedTackles;
    @JsonProperty("TacklesForLoss")
    private double tacklesForLoss;
    @JsonProperty("Sacks")
    private double sacks;
    @JsonProperty("SackYards")
    private double sackYards;
    @JsonProperty("QuarterbackHits")
    private double quarterbackHits;
    @JsonProperty("PassesDefended")
    private double passesDefended;
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
    @JsonProperty("FieldGoalsAttempted")
    private double fieldGoalsAttempted;
    @JsonProperty("FieldGoalsMade")
    private double fieldGoalsMade;
    @JsonProperty("ExtraPointsMade")
    private double extraPointsMade;
    @JsonProperty("TwoPointConversionPasses")
    private double twoPointConversionPasses;
    @JsonProperty("TwoPointConversionRuns")
    private double twoPointConversionRuns;
    @JsonProperty("TwoPointConversionReceptions")
    private double twoPointConversionReceptions;
    @JsonProperty("FantasyPoints")
    private double fantasyPoints;
    @JsonProperty("FantasyPointsPPR")
    private double fantasyPointsPPR;
    @JsonProperty("FantasyPosition")
    private String fantasyPosition;
    @JsonProperty("PlayerGameID")
    private int playerGameID;
    @JsonProperty("ExtraPointsAttempted")
    private double extraPointsAttempted;
    @JsonProperty("FantasyPointsFanDuel")
    private double fantasyPointsFanDuel;
    @JsonProperty("FieldGoalsMade0to19")
    private double fieldGoalsMade0to19;
    @JsonProperty("FieldGoalsMade20to29")
    private double fieldGoalsMade20to29;
    @JsonProperty("FieldGoalsMade30to39")
    private double fieldGoalsMade30to39;
    @JsonProperty("FieldGoalsMade40to49")
    private double fieldGoalsMade40to49;
    @JsonProperty("FieldGoalsMade50Plus")
    private double fieldGoalsMade50Plus;
    @JsonProperty("FantasyPointsDraftKings")
    private double fantasyPointsDraftKings;
    @JsonProperty("InjuryStatus")
    private String injuryStatus;
    @JsonProperty("TeamID")
    private int teamID;
    @JsonProperty("OpponentID")
    private int opponentID;
    @JsonProperty("ScoreID")
    private int scoreID;
    @JsonProperty("SnapCountsConfirmed")
    private boolean snapCountsConfirmed;


    public Offense() {
    }

    public Offense(String gameKey, int playerID, int seasonType, int season, Timestamp gameDate, int week, String team, String opponent, String homeOrAway, int number, String name, String position, String positionCategory, int played, int started, double passingAttempts, double passingCompletions, double passingYards, double passingCompletionPercentage, double passingYardsPerAttempt, double passingYardsPerCompletion, double passingTouchdowns, double passingInterceptions, double passingRating, double passingLong, double passingSacks, double passingSackYards, double rushingAttempts, double rushingYards, double rushingYardsPerAttempt, double rushingTouchdowns, double rushingLong, double receivingTargets, double receptions, double receivingYards, double receivingYardsPerReception, double receivingTouchdowns, double receivingLong, double fumbles, double fumblesLost, double puntReturns, double puntReturnYards, double puntReturnTouchdowns, double kickReturns, double kickReturnYards, double kickReturnTouchdowns, double soloTackles, double assistedTackles, double tacklesForLoss, double sacks, double sackYards, double quarterbackHits, double passesDefended, double fumblesForced, double fumblesRecovered, double fumbleReturnTouchdowns, double interceptions, double interceptionReturnTouchdowns, double fieldGoalsAttempted, double fieldGoalsMade, double extraPointsMade, double twoPointConversionPasses, double twoPointConversionRuns, double twoPointConversionReceptions, double fantasyPoints, double fantasyPointsPPR, String fantasyPosition, int playerGameID, double extraPointsAttempted, double fantasyPointsFanDuel, double fieldGoalsMade0to19, double fieldGoalsMade20to29, double fieldGoalsMade30to39, double fieldGoalsMade40to49, double fieldGoalsMade50Plus, double fantasyPointsDraftKings, String injuryStatus, int teamID, int opponentID, int scoreID, boolean snapCountsConfirmed) {
        this.gameKey = gameKey;
        this.playerID = playerID;
        this.seasonType = seasonType;
        this.season = season;
        this.gameDate = gameDate;
        this.week = week;
        this.team = team;
        this.opponent = opponent;
        this.homeOrAway = homeOrAway;
        this.number = number;
        this.name = name;
        this.position = position;
        this.positionCategory = positionCategory;
        this.played = played;
        this.started = started;
        this.passingAttempts = passingAttempts;
        this.passingCompletions = passingCompletions;
        this.passingYards = passingYards;
        this.passingCompletionPercentage = passingCompletionPercentage;
        this.passingYardsPerAttempt = passingYardsPerAttempt;
        this.passingYardsPerCompletion = passingYardsPerCompletion;
        this.passingTouchdowns = passingTouchdowns;
        this.passingInterceptions = passingInterceptions;
        this.passingRating = passingRating;
        this.passingLong = passingLong;
        this.passingSacks = passingSacks;
        this.passingSackYards = passingSackYards;
        this.rushingAttempts = rushingAttempts;
        this.rushingYards = rushingYards;
        this.rushingYardsPerAttempt = rushingYardsPerAttempt;
        this.rushingTouchdowns = rushingTouchdowns;
        this.rushingLong = rushingLong;
        this.receivingTargets = receivingTargets;
        this.receptions = receptions;
        this.receivingYards = receivingYards;
        this.receivingYardsPerReception = receivingYardsPerReception;
        this.receivingTouchdowns = receivingTouchdowns;
        this.receivingLong = receivingLong;
        this.fumbles = fumbles;
        this.fumblesLost = fumblesLost;
        this.puntReturns = puntReturns;
        this.puntReturnYards = puntReturnYards;
        this.puntReturnTouchdowns = puntReturnTouchdowns;
        this.kickReturns = kickReturns;
        this.kickReturnYards = kickReturnYards;
        this.kickReturnTouchdowns = kickReturnTouchdowns;
        this.soloTackles = soloTackles;
        this.assistedTackles = assistedTackles;
        this.tacklesForLoss = tacklesForLoss;
        this.sacks = sacks;
        this.sackYards = sackYards;
        this.quarterbackHits = quarterbackHits;
        this.passesDefended = passesDefended;
        this.fumblesForced = fumblesForced;
        this.fumblesRecovered = fumblesRecovered;
        this.fumbleReturnTouchdowns = fumbleReturnTouchdowns;
        this.interceptions = interceptions;
        this.interceptionReturnTouchdowns = interceptionReturnTouchdowns;
        this.fieldGoalsAttempted = fieldGoalsAttempted;
        this.fieldGoalsMade = fieldGoalsMade;
        this.extraPointsMade = extraPointsMade;
        this.twoPointConversionPasses = twoPointConversionPasses;
        this.twoPointConversionRuns = twoPointConversionRuns;
        this.twoPointConversionReceptions = twoPointConversionReceptions;
        this.fantasyPoints = fantasyPoints;
        this.fantasyPointsPPR = fantasyPointsPPR;
        this.fantasyPosition = fantasyPosition;
        this.playerGameID = playerGameID;
        this.extraPointsAttempted = extraPointsAttempted;
        this.fantasyPointsFanDuel = fantasyPointsFanDuel;
        this.fieldGoalsMade0to19 = fieldGoalsMade0to19;
        this.fieldGoalsMade20to29 = fieldGoalsMade20to29;
        this.fieldGoalsMade30to39 = fieldGoalsMade30to39;
        this.fieldGoalsMade40to49 = fieldGoalsMade40to49;
        this.fieldGoalsMade50Plus = fieldGoalsMade50Plus;
        this.fantasyPointsDraftKings = fantasyPointsDraftKings;
        this.injuryStatus = injuryStatus;
        this.teamID = teamID;
        this.opponentID = opponentID;
        this.scoreID = scoreID;
        this.snapCountsConfirmed = snapCountsConfirmed;
    }


    public String getGameKey() {
        return gameKey;
    }

    public void setGameKey(String gameKey) {
        this.gameKey = gameKey;
    }

    public int getPlayerID() {
        return playerID;
    }

    public void setPlayerID(int playerID) {
        this.playerID = playerID;
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

    public Timestamp getGameDate() {
        return gameDate;
    }

    public void setGameDate(Timestamp gameDate) {
        this.gameDate = gameDate;
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

    public String getOpponent() {
        return opponent;
    }

    public void setOpponent(String opponent) {
        this.opponent = opponent;
    }

    public String getHomeOrAway() {
        return homeOrAway;
    }

    public void setHomeOrAway(String homeOrAway) {
        this.homeOrAway = homeOrAway;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPosition() {
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public String getPositionCategory() {
        return positionCategory;
    }

    public void setPositionCategory(String positionCategory) {
        this.positionCategory = positionCategory;
    }

    public int getPlayed() {
        return played;
    }

    public void setPlayed(int played) {
        this.played = played;
    }

    public int getStarted() {
        return started;
    }

    public void setStarted(int started) {
        this.started = started;
    }

    public double getPassingAttempts() {
        return passingAttempts;
    }

    public void setPassingAttempts(double passingAttempts) {
        this.passingAttempts = passingAttempts;
    }

    public double getPassingCompletions() {
        return passingCompletions;
    }

    public void setPassingCompletions(double passingCompletions) {
        this.passingCompletions = passingCompletions;
    }

    public double getPassingYards() {
        return passingYards;
    }

    public void setPassingYards(double passingYards) {
        this.passingYards = passingYards;
    }

    public double getPassingCompletionPercentage() {
        return passingCompletionPercentage;
    }

    public void setPassingCompletionPercentage(double passingCompletionPercentage) {
        this.passingCompletionPercentage = passingCompletionPercentage;
    }

    public double getPassingYardsPerAttempt() {
        return passingYardsPerAttempt;
    }

    public void setPassingYardsPerAttempt(double passingYardsPerAttempt) {
        this.passingYardsPerAttempt = passingYardsPerAttempt;
    }

    public double getPassingYardsPerCompletion() {
        return passingYardsPerCompletion;
    }

    public void setPassingYardsPerCompletion(double passingYardsPerCompletion) {
        this.passingYardsPerCompletion = passingYardsPerCompletion;
    }

    public double getPassingTouchdowns() {
        return passingTouchdowns;
    }

    public void setPassingTouchdowns(double passingTouchdowns) {
        this.passingTouchdowns = passingTouchdowns;
    }

    public double getPassingInterceptions() {
        return passingInterceptions;
    }

    public void setPassingInterceptions(double passingInterceptions) {
        this.passingInterceptions = passingInterceptions;
    }

    public double getPassingRating() {
        return passingRating;
    }

    public void setPassingRating(double passingRating) {
        this.passingRating = passingRating;
    }

    public double getPassingLong() {
        return passingLong;
    }

    public void setPassingLong(double passingLong) {
        this.passingLong = passingLong;
    }

    public double getPassingSacks() {
        return passingSacks;
    }

    public void setPassingSacks(double passingSacks) {
        this.passingSacks = passingSacks;
    }

    public double getPassingSackYards() {
        return passingSackYards;
    }

    public void setPassingSackYards(double passingSackYards) {
        this.passingSackYards = passingSackYards;
    }

    public double getRushingAttempts() {
        return rushingAttempts;
    }

    public void setRushingAttempts(double rushingAttempts) {
        this.rushingAttempts = rushingAttempts;
    }

    public double getRushingYards() {
        return rushingYards;
    }

    public void setRushingYards(double rushingYards) {
        this.rushingYards = rushingYards;
    }

    public double getRushingYardsPerAttempt() {
        return rushingYardsPerAttempt;
    }

    public void setRushingYardsPerAttempt(double rushingYardsPerAttempt) {
        this.rushingYardsPerAttempt = rushingYardsPerAttempt;
    }

    public double getRushingTouchdowns() {
        return rushingTouchdowns;
    }

    public void setRushingTouchdowns(double rushingTouchdowns) {
        this.rushingTouchdowns = rushingTouchdowns;
    }

    public double getRushingLong() {
        return rushingLong;
    }

    public void setRushingLong(double rushingLong) {
        this.rushingLong = rushingLong;
    }

    public double getReceivingTargets() {
        return receivingTargets;
    }

    public void setReceivingTargets(double receivingTargets) {
        this.receivingTargets = receivingTargets;
    }

    public double getReceptions() {
        return receptions;
    }

    public void setReceptions(double receptions) {
        this.receptions = receptions;
    }

    public double getReceivingYards() {
        return receivingYards;
    }

    public void setReceivingYards(double receivingYards) {
        this.receivingYards = receivingYards;
    }

    public double getReceivingYardsPerReception() {
        return receivingYardsPerReception;
    }

    public void setReceivingYardsPerReception(double receivingYardsPerReception) {
        this.receivingYardsPerReception = receivingYardsPerReception;
    }

    public double getReceivingTouchdowns() {
        return receivingTouchdowns;
    }

    public void setReceivingTouchdowns(double receivingTouchdowns) {
        this.receivingTouchdowns = receivingTouchdowns;
    }

    public double getReceivingLong() {
        return receivingLong;
    }

    public void setReceivingLong(double receivingLong) {
        this.receivingLong = receivingLong;
    }

    public double getFumbles() {
        return fumbles;
    }

    public void setFumbles(double fumbles) {
        this.fumbles = fumbles;
    }

    public double getFumblesLost() {
        return fumblesLost;
    }

    public void setFumblesLost(double fumblesLost) {
        this.fumblesLost = fumblesLost;
    }

    public double getPuntReturns() {
        return puntReturns;
    }

    public void setPuntReturns(double puntReturns) {
        this.puntReturns = puntReturns;
    }

    public double getPuntReturnYards() {
        return puntReturnYards;
    }

    public void setPuntReturnYards(double puntReturnYards) {
        this.puntReturnYards = puntReturnYards;
    }

    public double getPuntReturnTouchdowns() {
        return puntReturnTouchdowns;
    }

    public void setPuntReturnTouchdowns(double puntReturnTouchdowns) {
        this.puntReturnTouchdowns = puntReturnTouchdowns;
    }

    public double getKickReturns() {
        return kickReturns;
    }

    public void setKickReturns(double kickReturns) {
        this.kickReturns = kickReturns;
    }

    public double getKickReturnYards() {
        return kickReturnYards;
    }

    public void setKickReturnYards(double kickReturnYards) {
        this.kickReturnYards = kickReturnYards;
    }

    public double getKickReturnTouchdowns() {
        return kickReturnTouchdowns;
    }

    public void setKickReturnTouchdowns(double kickReturnTouchdowns) {
        this.kickReturnTouchdowns = kickReturnTouchdowns;
    }

    public double getSoloTackles() {
        return soloTackles;
    }

    public void setSoloTackles(double soloTackles) {
        this.soloTackles = soloTackles;
    }

    public double getAssistedTackles() {
        return assistedTackles;
    }

    public void setAssistedTackles(double assistedTackles) {
        this.assistedTackles = assistedTackles;
    }

    public double getTacklesForLoss() {
        return tacklesForLoss;
    }

    public void setTacklesForLoss(double tacklesForLoss) {
        this.tacklesForLoss = tacklesForLoss;
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

    public double getQuarterbackHits() {
        return quarterbackHits;
    }

    public void setQuarterbackHits(double quarterbackHits) {
        this.quarterbackHits = quarterbackHits;
    }

    public double getPassesDefended() {
        return passesDefended;
    }

    public void setPassesDefended(double passesDefended) {
        this.passesDefended = passesDefended;
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

    public double getFieldGoalsAttempted() {
        return fieldGoalsAttempted;
    }

    public void setFieldGoalsAttempted(double fieldGoalsAttempted) {
        this.fieldGoalsAttempted = fieldGoalsAttempted;
    }

    public double getFieldGoalsMade() {
        return fieldGoalsMade;
    }

    public void setFieldGoalsMade(double fieldGoalsMade) {
        this.fieldGoalsMade = fieldGoalsMade;
    }

    public double getExtraPointsMade() {
        return extraPointsMade;
    }

    public void setExtraPointsMade(double extraPointsMade) {
        this.extraPointsMade = extraPointsMade;
    }

    public double getTwoPointConversionPasses() {
        return twoPointConversionPasses;
    }

    public void setTwoPointConversionPasses(double twoPointConversionPasses) {
        this.twoPointConversionPasses = twoPointConversionPasses;
    }

    public double getTwoPointConversionRuns() {
        return twoPointConversionRuns;
    }

    public void setTwoPointConversionRuns(double twoPointConversionRuns) {
        this.twoPointConversionRuns = twoPointConversionRuns;
    }

    public double getTwoPointConversionReceptions() {
        return twoPointConversionReceptions;
    }

    public void setTwoPointConversionReceptions(double twoPointConversionReceptions) {
        this.twoPointConversionReceptions = twoPointConversionReceptions;
    }

    public double getFantasyPoints() {
        return fantasyPoints;
    }

    public void setFantasyPoints(double fantasyPoints) {
        this.fantasyPoints = fantasyPoints;
    }

    public double getFantasyPointsPPR() {
        return fantasyPointsPPR;
    }

    public void setFantasyPointsPPR(double fantasyPointsPPR) {
        this.fantasyPointsPPR = fantasyPointsPPR;
    }

    public String getFantasyPosition() {
        return fantasyPosition;
    }

    public void setFantasyPosition(String fantasyPosition) {
        this.fantasyPosition = fantasyPosition;
    }

    public int getPlayerGameID() {
        return playerGameID;
    }

    public void setPlayerGameID(int playerGameID) {
        this.playerGameID = playerGameID;
    }

    public double getExtraPointsAttempted() {
        return extraPointsAttempted;
    }

    public void setExtraPointsAttempted(double extraPointsAttempted) {
        this.extraPointsAttempted = extraPointsAttempted;
    }

    public double getFantasyPointsFanDuel() {
        return fantasyPointsFanDuel;
    }

    public void setFantasyPointsFanDuel(double fantasyPointsFanDuel) {
        this.fantasyPointsFanDuel = fantasyPointsFanDuel;
    }

    public double getFieldGoalsMade0to19() {
        return fieldGoalsMade0to19;
    }

    public void setFieldGoalsMade0to19(double fieldGoalsMade0to19) {
        this.fieldGoalsMade0to19 = fieldGoalsMade0to19;
    }

    public double getFieldGoalsMade20to29() {
        return fieldGoalsMade20to29;
    }

    public void setFieldGoalsMade20to29(double fieldGoalsMade20to29) {
        this.fieldGoalsMade20to29 = fieldGoalsMade20to29;
    }

    public double getFieldGoalsMade30to39() {
        return fieldGoalsMade30to39;
    }

    public void setFieldGoalsMade30to39(double fieldGoalsMade30to39) {
        this.fieldGoalsMade30to39 = fieldGoalsMade30to39;
    }

    public double getFieldGoalsMade40to49() {
        return fieldGoalsMade40to49;
    }

    public void setFieldGoalsMade40to49(double fieldGoalsMade40to49) {
        this.fieldGoalsMade40to49 = fieldGoalsMade40to49;
    }

    public double getFieldGoalsMade50Plus() {
        return fieldGoalsMade50Plus;
    }

    public void setFieldGoalsMade50Plus(double fieldGoalsMade50Plus) {
        this.fieldGoalsMade50Plus = fieldGoalsMade50Plus;
    }

    public double getFantasyPointsDraftKings() {
        return fantasyPointsDraftKings;
    }

    public void setFantasyPointsDraftKings(double fantasyPointsDraftKings) {
        this.fantasyPointsDraftKings = fantasyPointsDraftKings;
    }

    public String getInjuryStatus() {
        return injuryStatus;
    }

    public void setInjuryStatus(String injuryStatus) {
        this.injuryStatus = injuryStatus;
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

    public boolean isSnapCountsConfirmed() {
        return snapCountsConfirmed;
    }

    public void setSnapCountsConfirmed(boolean snapCountsConfirmed) {
        this.snapCountsConfirmed = snapCountsConfirmed;
    }


    @Override
    public String toString() {
        return "PlayerStats{" +
                "gameKey='" + gameKey + '\'' +
                ", playerID=" + playerID +
                ", seasonType=" + seasonType +
                ", season=" + season +
                ", gameDate=" + gameDate +
                ", week=" + week +
                ", team='" + team + '\'' +
                ", opponent='" + opponent + '\'' +
                ", homeOrAway='" + homeOrAway + '\'' +
                ", number=" + number +
                ", name='" + name + '\'' +
                ", position='" + position + '\'' +
                ", positionCategory='" + positionCategory + '\'' +
                ", played=" + played +
                ", started=" + started +
                ", passingAttempts=" + passingAttempts +
                ", passingCompletions=" + passingCompletions +
                ", passingYards=" + passingYards +
                ", passingCompletionPercentage=" + passingCompletionPercentage +
                ", passingYardsPerAttempt=" + passingYardsPerAttempt +
                ", passingYardsPerCompletion=" + passingYardsPerCompletion +
                ", passingTouchdowns=" + passingTouchdowns +
                ", passingInterceptions=" + passingInterceptions +
                ", passingRating=" + passingRating +
                ", passingLong=" + passingLong +
                ", passingSacks=" + passingSacks +
                ", passingSackYards=" + passingSackYards +
                ", rushingAttempts=" + rushingAttempts +
                ", rushingYards=" + rushingYards +
                ", rushingYardsPerAttempt=" + rushingYardsPerAttempt +
                ", rushingTouchdowns=" + rushingTouchdowns +
                ", rushingLong=" + rushingLong +
                ", receivingTargets=" + receivingTargets +
                ", receptions=" + receptions +
                ", receivingYards=" + receivingYards +
                ", receivingYardsPerReception=" + receivingYardsPerReception +
                ", receivingTouchdowns=" + receivingTouchdowns +
                ", receivingLong=" + receivingLong +
                ", fumbles=" + fumbles +
                ", fumblesLost=" + fumblesLost +
                ", puntReturns=" + puntReturns +
                ", puntReturnYards=" + puntReturnYards +
                ", puntReturnTouchdowns=" + puntReturnTouchdowns +
                ", kickReturns=" + kickReturns +
                ", kickReturnYards=" + kickReturnYards +
                ", kickReturnTouchdowns=" + kickReturnTouchdowns +
                ", soloTackles=" + soloTackles +
                ", assistedTackles=" + assistedTackles +
                ", tacklesForLoss=" + tacklesForLoss +
                ", sacks=" + sacks +
                ", sackYards=" + sackYards +
                ", quarterbackHits=" + quarterbackHits +
                ", passesDefended=" + passesDefended +
                ", fumblesForced=" + fumblesForced +
                ", fumblesRecovered=" + fumblesRecovered +
                ", fumbleReturnTouchdowns=" + fumbleReturnTouchdowns +
                ", interceptions=" + interceptions +
                ", interceptionReturnTouchdowns=" + interceptionReturnTouchdowns +
                ", fieldGoalsAttempted=" + fieldGoalsAttempted +
                ", fieldGoalsMade=" + fieldGoalsMade +
                ", extraPointsMade=" + extraPointsMade +
                ", twoPointConversionPasses=" + twoPointConversionPasses +
                ", twoPointConversionRuns=" + twoPointConversionRuns +
                ", twoPointConversionReceptions=" + twoPointConversionReceptions +
                ", fantasyPoints=" + fantasyPoints +
                ", fantasyPointsPPR=" + fantasyPointsPPR +
                ", fantasyPosition='" + fantasyPosition + '\'' +
                ", playerGameID=" + playerGameID +
                ", extraPointsAttempted=" + extraPointsAttempted +
                ", fantasyPointsFanDuel=" + fantasyPointsFanDuel +
                ", fieldGoalsMade0to19=" + fieldGoalsMade0to19 +
                ", fieldGoalsMade20to29=" + fieldGoalsMade20to29 +
                ", fieldGoalsMade30to39=" + fieldGoalsMade30to39 +
                ", fieldGoalsMade40to49=" + fieldGoalsMade40to49 +
                ", fieldGoalsMade50Plus=" + fieldGoalsMade50Plus +
                ", fantasyPointsDraftKings=" + fantasyPointsDraftKings +
                ", injuryStatus='" + injuryStatus + '\'' +
                ", teamID=" + teamID +
                ", opponentID=" + opponentID +
                ", scoreID=" + scoreID +
                ", snapCountsConfirmed=" + snapCountsConfirmed +
                '}';
    }
}

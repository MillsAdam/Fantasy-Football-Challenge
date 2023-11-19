package com.techelevator.model.position;


public class QuarterbackDto {
    private int playerID;
    private int week;
    private String team;
    private String position;
    private String name;
    private double passingCompletions;
    private double passingAttempts;
    private double passingCompletionPercentage;
    private double passingYards;
    private double passingTouchdowns;
    private double passingInterceptions;
    private double passingRating;
    private double rushingAttempts;
    private double rushingYards;
    private double rushingTouchdowns;
    private double twoPointConversions;
    private double fumblesLost;
    private double fantasyPointsTotal;
    private double fantasyPointsAverage;


    public QuarterbackDto() {
    }

    public QuarterbackDto(int playerID, int week, String team, String position, String name, double passingCompletions, double passingAttempts, double passingCompletionPercentage, double passingYards, double passingTouchdowns, double passingInterceptions, double passingRating, double rushingAttempts, double rushingYards, double rushingTouchdowns, double twoPointConversions, double fumblesLost, double fantasyPointsTotal, double fantasyPointsAverage) {
        this.playerID = playerID;
        this.week = week;
        this.team = team;
        this.position = position;
        this.name = name;
        this.passingCompletions = passingCompletions;
        this.passingAttempts = passingAttempts;
        this.passingCompletionPercentage = passingCompletionPercentage;
        this.passingYards = passingYards;
        this.passingTouchdowns = passingTouchdowns;
        this.passingInterceptions = passingInterceptions;
        this.passingRating = passingRating;
        this.rushingAttempts = rushingAttempts;
        this.rushingYards = rushingYards;
        this.rushingTouchdowns = rushingTouchdowns;
        this.twoPointConversions = twoPointConversions;
        this.fumblesLost = fumblesLost;
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
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getPassingCompletions() {
        return passingCompletions;
    }

    public void setPassingCompletions(double passingCompletions) {
        this.passingCompletions = passingCompletions;
    }

    public double getPassingAttempts() {
        return passingAttempts;
    }

    public void setPassingAttempts(double passingAttempts) {
        this.passingAttempts = passingAttempts;
    }

    public double getPassingCompletionPercentage() {
        return passingCompletionPercentage;
    }

    public void setPassingCompletionPercentage(double passingCompletionPercentage) {
        this.passingCompletionPercentage = passingCompletionPercentage;
    }

    public double getPassingYards() {
        return passingYards;
    }

    public void setPassingYards(double passingYards) {
        this.passingYards = passingYards;
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

    public double getRushingTouchdowns() {
        return rushingTouchdowns;
    }

    public void setRushingTouchdowns(double rushingTouchdowns) {
        this.rushingTouchdowns = rushingTouchdowns;
    }

    public double getTwoPointConversions() {
        return twoPointConversions;
    }

    public void setTwoPointConversions(double twoPointConversions) {
        this.twoPointConversions = twoPointConversions;
    }

    public double getFumblesLost() {
        return fumblesLost;
    }

    public void setFumblesLost(double fumblesLost) {
        this.fumblesLost = fumblesLost;
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
        return "QuarterbackDto{" +
                "playerID=" + playerID +
                ", week=" + week +
                ", team='" + team + '\'' +
                ", position='" + position + '\'' +
                ", name='" + name + '\'' +
                ", passingCompletions=" + passingCompletions +
                ", passingAttempts=" + passingAttempts +
                ", passingCompletionPercentage=" + passingCompletionPercentage +
                ", passingYards=" + passingYards +
                ", passingTouchdowns=" + passingTouchdowns +
                ", passingInterceptions=" + passingInterceptions +
                ", passingRating=" + passingRating +
                ", rushingAttempts=" + rushingAttempts +
                ", rushingYards=" + rushingYards +
                ", rushingTouchdowns=" + rushingTouchdowns +
                ", twoPointConversions=" + twoPointConversions +
                ", fumblesLost=" + fumblesLost +
                ", fantasyPointsTotal=" + fantasyPointsTotal +
                ", fantasyPointsAverage=" + fantasyPointsAverage +
                '}';
    }
}

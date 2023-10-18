package com.techelevator.model.position;

public class FlexPlayerDto {
    private int playerID;
    private int week;
    private String team;
    private String position;
    private String name;
    private double rushingAttempts;
    private double rushingYards;
    private double rushingYardsPerAttempt;
    private double rushingTouchdowns;
    private double receivingTargets;
    private double receptions;
    private double receivingYards;
    private double receivingYardsPerReception;
    private double receivingTouchdowns;
    private double returnTouchdowns;
    private double twoPointConversions;
    private double usage;
    private double fumblesLost;
    private double fantasyPointsTotal;
    private double fantasyPointsAverage;


    public FlexPlayerDto() {
    }

    public FlexPlayerDto(int playerID, int week, String team, String position, String name, double rushingAttempts, double rushingYards, double rushingYardsPerAttempt, double rushingTouchdowns, double receivingTargets, double receptions, double receivingYards, double receivingYardsPerReception, double receivingTouchdowns, double returnTouchdowns, double twoPointConversions, double usage, double fumblesLost, double fantasyPointsTotal, double fantasyPointsAverage) {
        this.playerID = playerID;
        this.week = week;
        this.team = team;
        this.position = position;
        this.name = name;
        this.rushingAttempts = rushingAttempts;
        this.rushingYards = rushingYards;
        this.rushingYardsPerAttempt = rushingYardsPerAttempt;
        this.rushingTouchdowns = rushingTouchdowns;
        this.receivingTargets = receivingTargets;
        this.receptions = receptions;
        this.receivingYards = receivingYards;
        this.receivingYardsPerReception = receivingYardsPerReception;
        this.receivingTouchdowns = receivingTouchdowns;
        this.returnTouchdowns = returnTouchdowns;
        this.twoPointConversions = twoPointConversions;
        this.usage = usage;
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

    public double getReturnTouchdowns() {
        return returnTouchdowns;
    }

    public void setReturnTouchdowns(double returnTouchdowns) {
        this.returnTouchdowns = returnTouchdowns;
    }

    public double getTwoPointConversions() {
        return twoPointConversions;
    }

    public void setTwoPointConversions(double twoPointConversions) {
        this.twoPointConversions = twoPointConversions;
    }

    public double getUsage() {
        return usage;
    }

    public void setUsage(double usage) {
        this.usage = usage;
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
        return "FlexPlayerDto{" +
                "playerID=" + playerID +
                ", week=" + week +
                ", team='" + team + '\'' +
                ", position='" + position + '\'' +
                ", name='" + name + '\'' +
                ", rushingAttempts=" + rushingAttempts +
                ", rushingYards=" + rushingYards +
                ", rushingYardsPerAttempt=" + rushingYardsPerAttempt +
                ", rushingTouchdowns=" + rushingTouchdowns +
                ", receivingTargets=" + receivingTargets +
                ", receptions=" + receptions +
                ", receivingYards=" + receivingYards +
                ", receivingYardsPerReception=" + receivingYardsPerReception +
                ", receivingTouchdowns=" + receivingTouchdowns +
                ", returnTouchdowns=" + returnTouchdowns +
                ", twoPointConversions=" + twoPointConversions +
                ", usage=" + usage +
                ", fumblesLost=" + fumblesLost +
                ", fantasyPointsTotal=" + fantasyPointsTotal +
                ", fantasyPointsAverage=" + fantasyPointsAverage +
                '}';
    }
}

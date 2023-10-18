package com.techelevator.model.position;


public class KickerDto {
    private int playerID;
    private int week;
    private String team;
    private String position;
    private String name;
    private double fieldGoalsMade;
    private double fieldGoalsAttempted;
    private double fieldGoalPercentage;
    private double fieldGoalsMade0to19;
    private double fieldGoalsMade20to29;
    private double fieldGoalsMade30to39;
    private double fieldGoalsMade40to49;
    private double fieldGoalsMade50Plus;
    private double extraPointsMade;
    private double extraPointsAttempted;
    private double extraPointPercentage;
    private double fantasyPointsTotal;
    private double fantasyPointsAverage;


    public KickerDto() {
    }


    public KickerDto(int playerID, int week, String team, String position, String name, double fieldGoalsMade, double fieldGoalsAttempted, double fieldGoalPercentage, double fieldGoalsMade0to19, double fieldGoalsMade20to29, double fieldGoalsMade30to39, double fieldGoalsMade40to49, double fieldGoalsMade50Plus, double extraPointsMade, double extraPointsAttempted, double extraPointPercentage, double fantasyPointsTotal, double fantasyPointsAverage) {
        this.playerID = playerID;
        this.week = week;
        this.team = team;
        this.position = position;
        this.name = name;
        this.fieldGoalsMade = fieldGoalsMade;
        this.fieldGoalsAttempted = fieldGoalsAttempted;
        this.fieldGoalPercentage = fieldGoalPercentage;
        this.fieldGoalsMade0to19 = fieldGoalsMade0to19;
        this.fieldGoalsMade20to29 = fieldGoalsMade20to29;
        this.fieldGoalsMade30to39 = fieldGoalsMade30to39;
        this.fieldGoalsMade40to49 = fieldGoalsMade40to49;
        this.fieldGoalsMade50Plus = fieldGoalsMade50Plus;
        this.extraPointsMade = extraPointsMade;
        this.extraPointsAttempted = extraPointsAttempted;
        this.extraPointPercentage = extraPointPercentage;
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

    public double getFieldGoalsMade() {
        return fieldGoalsMade;
    }

    public void setFieldGoalsMade(double fieldGoalsMade) {
        this.fieldGoalsMade = fieldGoalsMade;
    }

    public double getFieldGoalsAttempted() {
        return fieldGoalsAttempted;
    }

    public void setFieldGoalsAttempted(double fieldGoalsAttempted) {
        this.fieldGoalsAttempted = fieldGoalsAttempted;
    }

    public double getFieldGoalPercentage() {
        return fieldGoalPercentage;
    }

    public void setFieldGoalPercentage(double fieldGoalPercentage) {
        this.fieldGoalPercentage = fieldGoalPercentage;
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

    public double getExtraPointsMade() {
        return extraPointsMade;
    }

    public void setExtraPointsMade(double extraPointsMade) {
        this.extraPointsMade = extraPointsMade;
    }

    public double getExtraPointsAttempted() {
        return extraPointsAttempted;
    }

    public void setExtraPointsAttempted(double extraPointsAttempted) {
        this.extraPointsAttempted = extraPointsAttempted;
    }

    public double getExtraPointPercentage() {
        return extraPointPercentage;
    }

    public void setExtraPointPercentage(double extraPointPercentage) {
        this.extraPointPercentage = extraPointPercentage;
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
        return "KickerDto{" +
                "playerID=" + playerID +
                ", week=" + week +
                ", team='" + team + '\'' +
                ", position='" + position + '\'' +
                ", name='" + name + '\'' +
                ", fieldGoalsMade=" + fieldGoalsMade +
                ", fieldGoalsAttempted=" + fieldGoalsAttempted +
                ", fieldGoalPercentage=" + fieldGoalPercentage +
                ", fieldGoalsMade0to19=" + fieldGoalsMade0to19 +
                ", fieldGoalsMade20to29=" + fieldGoalsMade20to29 +
                ", fieldGoalsMade30to39=" + fieldGoalsMade30to39 +
                ", fieldGoalsMade40to49=" + fieldGoalsMade40to49 +
                ", fieldGoalsMade50Plus=" + fieldGoalsMade50Plus +
                ", extraPointsMade=" + extraPointsMade +
                ", extraPointsAttempted=" + extraPointsAttempted +
                ", extraPointPercentage=" + extraPointPercentage +
                ", fantasyPointsTotal=" + fantasyPointsTotal +
                ", fantasyPointsAverage=" + fantasyPointsAverage +
                '}';
    }
}

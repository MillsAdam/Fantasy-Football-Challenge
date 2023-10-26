package com.techelevator.model;


import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.Date;

public class Player {
    @JsonProperty("PlayerID")
    private int playerID;
    @JsonProperty("Team")
    private String team;
    @JsonProperty("Number")
    private int number;
    @JsonProperty("FirstName")
    private String firstName;
    @JsonProperty("LastName")
    private String lastName;
    @JsonProperty("Position")
    private String position;
    @JsonProperty("Status")
    private String status;
    @JsonProperty("Height")
    private String height;
    @JsonProperty("Weight")
    private int weight;
    @JsonProperty("BirthDate")
    private Date birthDate;
    @JsonProperty("College")
    private String college;
    @JsonProperty("Experience")
    private int experience;
    @JsonProperty("FantasyPosition")
    private String fantasyPosition;
    @JsonProperty("PositionCategory")
    private String positionCategory;
    @JsonProperty("PhotoUrl")
    private String photoUrl;
    @JsonProperty("ByeWeek")
    private int byeWeek;
    @JsonProperty("AverageDraftPosition")
    private double averageDraftPosition;
    @JsonProperty("CollegeDraftTeam")
    private String collegeDraftTeam;
    @JsonProperty("CollegeDraftYear")
    private int collegeDraftYear;
    @JsonProperty("CollegeDraftRound")
    private int collegeDraftRound;
    @JsonProperty("CollegeDraftPick")
    private int collegeDraftPick;
    @JsonProperty("IsUndraftedFreeAgent")
    private boolean isUndraftedFreeAgent;
    @JsonProperty("FanDuelPlayerID")
    private int fanDuelPlayerID;
    @JsonProperty("DraftKingsPlayerID")
    private int draftKingsPlayerID;
    @JsonProperty("InjuryStatus")
    private String injuryStatus;
    @JsonProperty("FanDuelName")
    private String fanDuelName;
    @JsonProperty("DraftKingsName")
    private String draftKingsName;
    @JsonProperty("TeamID")
    private int teamID;


    public Player() {}


    public Player(int playerID, String team, int number, String firstName, String lastName, String position, String status, String height, int weight, Date birthDate, String college, int experience, String fantasyPosition, String positionCategory, String photoUrl, int byeWeek, double averageDraftPosition, String collegeDraftTeam, int collegeDraftYear, int collegeDraftRound, int collegeDraftPick, boolean isUndraftedFreeAgent, int fanDuelPlayerID, int draftKingsPlayerID, String injuryStatus, String fanDuelName, String draftKingsName, int teamID) {
        this.playerID = playerID;
        this.team = team;
        this.number = number;
        this.firstName = firstName;
        this.lastName = lastName;
        this.position = position;
        this.status = status;
        this.height = height;
        this.weight = weight;
        this.birthDate = birthDate;
        this.college = college;
        this.experience = experience;
        this.fantasyPosition = fantasyPosition;
        this.positionCategory = positionCategory;
        this.photoUrl = photoUrl;
        this.byeWeek = byeWeek;
        this.averageDraftPosition = averageDraftPosition;
        this.collegeDraftTeam = collegeDraftTeam;
        this.collegeDraftYear = collegeDraftYear;
        this.collegeDraftRound = collegeDraftRound;
        this.collegeDraftPick = collegeDraftPick;
        this.isUndraftedFreeAgent = isUndraftedFreeAgent;
        this.fanDuelPlayerID = fanDuelPlayerID;
        this.draftKingsPlayerID = draftKingsPlayerID;
        this.injuryStatus = injuryStatus;
        this.fanDuelName = fanDuelName;
        this.draftKingsName = draftKingsName;
        this.teamID = teamID;
    }


    public int getPlayerID() {
        return playerID;
    }

    public void setPlayerID(int playerID) {
        this.playerID = playerID;
    }

    public String getTeam() {
        return team;
    }

    public void setTeam(String team) {
        this.team = team;
    }

    public int getNumber() {
        return number;
    }

    public void setNumber(int number) {
        this.number = number;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getPosition() {
        return position;
    }

    public void setPosition(String position) {
        this.position = position;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getHeight() {
        return height;
    }

    public void setHeight(String height) {
        this.height = height;
    }

    public int getWeight() {
        return weight;
    }

    public void setWeight(int weight) {
        this.weight = weight;
    }

    public Date getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(Date birthDate) {
        this.birthDate = birthDate;
    }

    public String getCollege() {
        return college;
    }

    public void setCollege(String college) {
        this.college = college;
    }

    public int getExperience() {
        return experience;
    }

    public void setExperience(int experience) {
        this.experience = experience;
    }

    public String getFantasyPosition() {
        return fantasyPosition;
    }

    public void setFantasyPosition(String fantasyPosition) {
        this.fantasyPosition = fantasyPosition;
    }

    public String getPositionCategory() {
        return positionCategory;
    }

    public void setPositionCategory(String positionCategory) {
        this.positionCategory = positionCategory;
    }

    public String getPhotoUrl() {
        return photoUrl;
    }

    public void setPhotoUrl(String photoUrl) {
        this.photoUrl = photoUrl;
    }

    public int getByeWeek() {
        return byeWeek;
    }

    public void setByeWeek(int byeWeek) {
        this.byeWeek = byeWeek;
    }

    public double getAverageDraftPosition() {
        return averageDraftPosition;
    }

    public void setAverageDraftPosition(double averageDraftPosition) {
        this.averageDraftPosition = averageDraftPosition;
    }

    public String getCollegeDraftTeam() {
        return collegeDraftTeam;
    }

    public void setCollegeDraftTeam(String collegeDraftTeam) {
        this.collegeDraftTeam = collegeDraftTeam;
    }

    public int getCollegeDraftYear() {
        return collegeDraftYear;
    }

    public void setCollegeDraftYear(int collegeDraftYear) {
        this.collegeDraftYear = collegeDraftYear;
    }

    public int getCollegeDraftRound() {
        return collegeDraftRound;
    }

    public void setCollegeDraftRound(int collegeDraftRound) {
        this.collegeDraftRound = collegeDraftRound;
    }

    public int getCollegeDraftPick() {
        return collegeDraftPick;
    }

    public void setCollegeDraftPick(int collegeDraftPick) {
        this.collegeDraftPick = collegeDraftPick;
    }

    public boolean isUndraftedFreeAgent() {
        return isUndraftedFreeAgent;
    }

    public void setUndraftedFreeAgent(boolean undraftedFreeAgent) {
        isUndraftedFreeAgent = undraftedFreeAgent;
    }

    public int getFanDuelPlayerID() {
        return fanDuelPlayerID;
    }

    public void setFanDuelPlayerID(int fanDuelPlayerID) {
        this.fanDuelPlayerID = fanDuelPlayerID;
    }

    public int getDraftKingsPlayerID() {
        return draftKingsPlayerID;
    }

    public void setDraftKingsPlayerID(int draftKingsPlayerID) {
        this.draftKingsPlayerID = draftKingsPlayerID;
    }

    public String getInjuryStatus() {
        return injuryStatus;
    }

    public void setInjuryStatus(String injuryStatus) {
        this.injuryStatus = injuryStatus;
    }

    public String getFanDuelName() {
        return fanDuelName;
    }

    public void setFanDuelName(String fanDuelName) {
        this.fanDuelName = fanDuelName;
    }

    public String getDraftKingsName() {
        return draftKingsName;
    }

    public void setDraftKingsName(String draftKingsName) {
        this.draftKingsName = draftKingsName;
    }

    public int getTeamID() {
        return teamID;
    }

    public void setTeamID(int teamID) {
        this.teamID = teamID;
    }


    @Override
    public String toString() {
        return "Player{" +
                "playerID=" + playerID +
                ", team='" + team + '\'' +
                ", number=" + number +
                ", firstName='" + firstName + '\'' +
                ", lastName='" + lastName + '\'' +
                ", position='" + position + '\'' +
                ", status='" + status + '\'' +
                ", height='" + height + '\'' +
                ", weight=" + weight +
                ", birthDate=" + birthDate +
                ", college='" + college + '\'' +
                ", experience=" + experience +
                ", fantasyPosition='" + fantasyPosition + '\'' +
                ", positionCategory='" + positionCategory + '\'' +
                ", photoUrl='" + photoUrl + '\'' +
                ", byeWeek=" + byeWeek +
                ", averageDraftPosition=" + averageDraftPosition +
                ", collegeDraftTeam='" + collegeDraftTeam + '\'' +
                ", collegeDraftYear=" + collegeDraftYear +
                ", collegeDraftRound=" + collegeDraftRound +
                ", collegeDraftPick=" + collegeDraftPick +
                ", isUndraftedFreeAgent=" + isUndraftedFreeAgent +
                ", fanDuelPlayerID=" + fanDuelPlayerID +
                ", draftKingsPlayerID=" + draftKingsPlayerID +
                ", injuryStatus='" + injuryStatus + '\'' +
                ", fanDuelName='" + fanDuelName + '\'' +
                ", draftKingsName='" + draftKingsName + '\'' +
                ", teamID=" + teamID +
                '}';
    }
}

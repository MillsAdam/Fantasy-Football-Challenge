package com.techelevator.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Team {
    @JsonProperty("Key")
    private String key;
    @JsonProperty("TeamID")
    private int teamID;
    @JsonProperty("PlayerID")
    private int playerID;
    @JsonProperty("City")
    private String city;
    @JsonProperty("Name")
    private String name;
    @JsonProperty("Conference")
    private String conference;
    @JsonProperty("Division")
    private String division;
    @JsonProperty("FullName")
    private String fullName;
    @JsonProperty("StadiumID")
    private int stadiumID;
    @JsonProperty("ByeWeek")
    private int byeWeek;
    @JsonProperty("AverageDraftPosition")
    private double averageDraftPosition;
    @JsonProperty("AverageDraftPositionPPR")
    private double averageDraftPositionPPR;
    @JsonProperty("PrimaryColor")
    private String primaryColor;
    @JsonProperty("SecondaryColor")
    private String secondaryColor;
    @JsonProperty("TertiaryColor")
    private String tertiaryColor;
    @JsonProperty("QuaternaryColor")
    private String quaternaryColor;
    @JsonProperty("WikipediaLogoUrl")
    private String wikipediaLogoUrl;
    @JsonProperty("WikipediaWordMarkUrl")
    private String wikipediaWordMarkUrl;
    @JsonProperty("DraftKingsName")
    private String draftKingsName;
    @JsonProperty("DraftKingsPlayerID")
    private int draftKingsPlayerID;
    @JsonProperty("FanDuelName")
    private String fanDuelName;
    @JsonProperty("FanDuelPlayerID")
    private int fanDuelPlayerID;
    @JsonProperty("AverageDraftPosition2QB")
    private double averageDraftPosition2QB;
    @JsonProperty("AverageDraftPositionDynasty")
    private double averageDraftPositionDynasty;


    public Team() {
    }


    public Team(String key, int teamID, int playerID, String city, String name, String conference, String division, String fullName, int stadiumID, int byeWeek, double averageDraftPosition, double averageDraftPositionPPR, String primaryColor, String secondaryColor, String tertiaryColor, String quaternaryColor, String wikipediaLogoUrl, String wikipediaWordMarkUrl, String draftKingsName, int draftKingsPlayerID, String fanDuelName, int fanDuelPlayerID, double averageDraftPosition2QB, double averageDraftPositionDynasty) {
        this.key = key;
        this.teamID = teamID;
        this.playerID = playerID;
        this.city = city;
        this.name = name;
        this.conference = conference;
        this.division = division;
        this.fullName = fullName;
        this.stadiumID = stadiumID;
        this.byeWeek = byeWeek;
        this.averageDraftPosition = averageDraftPosition;
        this.averageDraftPositionPPR = averageDraftPositionPPR;
        this.primaryColor = primaryColor;
        this.secondaryColor = secondaryColor;
        this.tertiaryColor = tertiaryColor;
        this.quaternaryColor = quaternaryColor;
        this.wikipediaLogoUrl = wikipediaLogoUrl;
        this.wikipediaWordMarkUrl = wikipediaWordMarkUrl;
        this.draftKingsName = draftKingsName;
        this.draftKingsPlayerID = draftKingsPlayerID;
        this.fanDuelName = fanDuelName;
        this.fanDuelPlayerID = fanDuelPlayerID;
        this.averageDraftPosition2QB = averageDraftPosition2QB;
        this.averageDraftPositionDynasty = averageDraftPositionDynasty;
    }


    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }

    public int getTeamID() {
        return teamID;
    }

    public void setTeamID(int teamID) {
        this.teamID = teamID;
    }

    public int getPlayerID() {
        return playerID;
    }

    public void setPlayerID(int playerID) {
        this.playerID = playerID;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getConference() {
        return conference;
    }

    public void setConference(String conference) {
        this.conference = conference;
    }

    public String getDivision() {
        return division;
    }

    public void setDivision(String division) {
        this.division = division;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public int getStadiumID() {
        return stadiumID;
    }

    public void setStadiumID(int stadiumID) {
        this.stadiumID = stadiumID;
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

    public double getAverageDraftPositionPPR() {
        return averageDraftPositionPPR;
    }

    public void setAverageDraftPositionPPR(double averageDraftPositionPPR) {
        this.averageDraftPositionPPR = averageDraftPositionPPR;
    }

    public String getPrimaryColor() {
        return primaryColor;
    }

    public void setPrimaryColor(String primaryColor) {
        this.primaryColor = primaryColor;
    }

    public String getSecondaryColor() {
        return secondaryColor;
    }

    public void setSecondaryColor(String secondaryColor) {
        this.secondaryColor = secondaryColor;
    }

    public String getTertiaryColor() {
        return tertiaryColor;
    }

    public void setTertiaryColor(String tertiaryColor) {
        this.tertiaryColor = tertiaryColor;
    }

    public String getQuaternaryColor() {
        return quaternaryColor;
    }

    public void setQuaternaryColor(String quaternaryColor) {
        this.quaternaryColor = quaternaryColor;
    }

    public String getWikipediaLogoUrl() {
        return wikipediaLogoUrl;
    }

    public void setWikipediaLogoUrl(String wikipediaLogoUrl) {
        this.wikipediaLogoUrl = wikipediaLogoUrl;
    }

    public String getWikipediaWordMarkUrl() {
        return wikipediaWordMarkUrl;
    }

    public void setWikipediaWordMarkUrl(String wikipediaWordMarkUrl) {
        this.wikipediaWordMarkUrl = wikipediaWordMarkUrl;
    }

    public String getDraftKingsName() {
        return draftKingsName;
    }

    public void setDraftKingsName(String draftKingsName) {
        this.draftKingsName = draftKingsName;
    }

    public int getDraftKingsPlayerID() {
        return draftKingsPlayerID;
    }

    public void setDraftKingsPlayerID(int draftKingsPlayerID) {
        this.draftKingsPlayerID = draftKingsPlayerID;
    }

    public String getFanDuelName() {
        return fanDuelName;
    }

    public void setFanDuelName(String fanDuelName) {
        this.fanDuelName = fanDuelName;
    }

    public int getFanDuelPlayerID() {
        return fanDuelPlayerID;
    }

    public void setFanDuelPlayerID(int fanDuelPlayerID) {
        this.fanDuelPlayerID = fanDuelPlayerID;
    }

    public double getAverageDraftPosition2QB() {
        return averageDraftPosition2QB;
    }

    public void setAverageDraftPosition2QB(double averageDraftPosition2QB) {
        this.averageDraftPosition2QB = averageDraftPosition2QB;
    }

    public double getAverageDraftPositionDynasty() {
        return averageDraftPositionDynasty;
    }

    public void setAverageDraftPositionDynasty(double averageDraftPositionDynasty) {
        this.averageDraftPositionDynasty = averageDraftPositionDynasty;
    }


    @Override
    public String toString() {
        return "Team{" +
                "key='" + key + '\'' +
                ", teamID=" + teamID +
                ", playerID=" + playerID +
                ", city='" + city + '\'' +
                ", name='" + name + '\'' +
                ", conference='" + conference + '\'' +
                ", division='" + division + '\'' +
                ", fullName='" + fullName + '\'' +
                ", stadiumID=" + stadiumID +
                ", byeWeek=" + byeWeek +
                ", averageDraftPosition=" + averageDraftPosition +
                ", averageDraftPositionPPR=" + averageDraftPositionPPR +
                ", primaryColor='" + primaryColor + '\'' +
                ", secondaryColor='" + secondaryColor + '\'' +
                ", tertiaryColor='" + tertiaryColor + '\'' +
                ", quaternaryColor='" + quaternaryColor + '\'' +
                ", wikipediaLogoUrl='" + wikipediaLogoUrl + '\'' +
                ", wikipediaWordMarkUrl='" + wikipediaWordMarkUrl + '\'' +
                ", draftKingsName='" + draftKingsName + '\'' +
                ", draftKingsPlayerID=" + draftKingsPlayerID +
                ", fanDuelName='" + fanDuelName + '\'' +
                ", fanDuelPlayerID=" + fanDuelPlayerID +
                ", averageDraftPosition2QB=" + averageDraftPosition2QB +
                ", averageDraftPositionDynasty=" + averageDraftPositionDynasty +
                '}';
    }
}

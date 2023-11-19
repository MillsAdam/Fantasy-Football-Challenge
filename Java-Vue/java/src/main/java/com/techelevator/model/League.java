package com.techelevator.model;

import java.sql.Timestamp;

public class League {
    private int leagueId;
    private int userId;
    private String leagueName;
    private Timestamp startDate;
    private Timestamp endDate;


    public League() {}


    public League(int leagueId, int userId, String leagueName, Timestamp startDate, Timestamp endDate) {
        this.leagueId = leagueId;
        this.userId = userId;
        this.leagueName = leagueName;
        this.startDate = startDate;
        this.endDate = endDate;
    }


    public int getLeagueId() {
        return leagueId;
    }

    public void setLeagueId(int leagueId) {
        this.leagueId = leagueId;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getLeagueName() {
        return leagueName;
    }

    public void setLeagueName(String leagueName) {
        this.leagueName = leagueName;
    }

    public Timestamp getStartDate() { return startDate; }

    public void setStartDate(Timestamp startDate) { this.startDate = startDate; }

    public Timestamp getEndDate() { return endDate; }

    public void setEndDate(Timestamp endDate) {
        this.endDate = endDate;
    }


    @Override
    public String toString() {
        return "League{" +
                "leagueId=" + leagueId +
                ", userId=" + userId +
                ", leagueName='" + leagueName + '\'' +
                ", startDate=" + startDate +
                ", endDate=" + endDate +
                '}';
    }
}

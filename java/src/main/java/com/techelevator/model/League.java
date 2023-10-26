package com.techelevator.model;

import java.util.Date;

public class League {
    private int leagueId;
    private int userId;
    private String leagueName;
    private Date startDate;
    private Date endDate;


    public League() {}


    public League(int leagueId, int userId, String leagueName, Date startDate, Date endDate) {
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

    public Date getStartDate() {
        return new Date(System.currentTimeMillis());
    }

    public void setStartDate(Date startDate) {
        this.startDate = startDate;
    }

    public Date getEndDate() {
        return new Date(System.currentTimeMillis() + 7 * 24 * 60 * 60 * 1000);
    }

    public void setEndDate(Date endDate) {
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

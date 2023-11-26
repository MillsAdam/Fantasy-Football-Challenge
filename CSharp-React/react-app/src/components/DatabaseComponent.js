import React, { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";

function DatabaseComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    async function createTeams(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newTeams = await DatabaseService.createTeams();
                if (newTeams) {
                    console.log("Teams created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create teams');
        }
        setIsLoading(false);
    }

    async function createPlayers(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayers = await DatabaseService.createPlayers();
                if (newPlayers) {
                    console.log("Players created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create players');
        }
        setIsLoading(false);
    }

    async function createPlayerStats(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerStats = await DatabaseService.createPlayerStats();
                if (newPlayerStats) {
                    console.log("Player stats created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player stats');
        }
        setIsLoading(false);
    }

    async function createPlayerProjections(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerProjections = await DatabaseService.createPlayerProjections();
                if (newPlayerProjections) {
                    console.log("Player projections created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player projections');
        }
        setIsLoading(false);
    }

    async function updateLineupScores(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedLineupScores = await DatabaseService.updateLineupScores();
                if (updatedLineupScores) {
                    console.log("Lineup scores updated");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update lineup scores');
        }
        setIsLoading(false);
    }

    async function updateRosterScores(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedRosterScores = await DatabaseService.updateRosterScores();
                if (updatedRosterScores) {
                    console.log("Roster scores updated");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update roster scores');
        }
        setIsLoading(false);
    }

    return (
        <div>
            <h1>Database Component</h1>
            <>
                <form onSubmit={createTeams}>
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Teams"}</button>
                </form>
                <form onSubmit={createPlayers}>
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Players"}</button>
                </form>
                <form onSubmit={createPlayerStats}>
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Player Stats"}</button>
                </form>
                <form onSubmit={createPlayerProjections}>
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Player Projections"}</button>
                </form>
                <form onSubmit={updateLineupScores}>
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Update Lineup Scores"}</button>
                </form>
                <form onSubmit={updateRosterScores}>
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Update Roster Scores"}</button>
                </form>
            </>
            {error && <p>{error}</p>}
        </div>
    )

}

export default DatabaseComponent;
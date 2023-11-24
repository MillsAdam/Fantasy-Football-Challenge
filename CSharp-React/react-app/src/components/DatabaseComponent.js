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
            </>
            {error && <p>{error}</p>}
        </div>
    )

}

export default DatabaseComponent;
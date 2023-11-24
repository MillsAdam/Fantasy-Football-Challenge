import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import "../styles/LeagueComponent.css";

function RosterComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosterPlayers, setRosterPlayers] = useState([]);
    // const [userHasTeam, setUserHasTeam] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [playerId, setPlayerId] = useState("");
    // const [oldPlayerId, setOldPlayerId] = useState("");
    // const [newPlayerId, setNewPlayerId] = useState("");
    const [searchTerm, setSearchTerm] = useState("");
    const [searchPlayerId, setSearchPlayerId] = useState(null);

    useEffect(() => {
        async function getRosterPlayers() {
            setIsLoading(true);
            try {
                const rosterPlayersData = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(rosterPlayersData);
            } catch (error) {
                console.error('An error occurred: ', error);
                setError('Failed to get roster players');
            }
            setIsLoading(false);
        }

        if (currentUser && currentUser.userId) {
            getRosterPlayers();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser]);

    async function createRosterPlayer(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const newRosterPlayer = await RosterService.createRosterPlayer(playerId, authToken);
            if (newRosterPlayer) {
                const updatedRosterPlayers = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(updatedRosterPlayers);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create roster player');
        }
        setIsLoading(false);
    }

    async function searchPlayers() {
        setIsLoading(true);
        setError(null);
        try {
            const playerId = await DatabaseService.searchPlayers(searchTerm);
            setSearchPlayerId(playerId);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to search players');
        }
        setIsLoading(false);
    }

    async function handleAddPlayerToRoster(playerId){
        setIsLoading(true);
        setError(null);
        try {
            const newRosterPlayer = await RosterService.createRosterPlayer(playerId, authToken);
            if (newRosterPlayer) {
                const updatedRosterPlayers = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(updatedRosterPlayers);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create roster player');
        }
        setIsLoading(false);
    }

    return (
        <div>
            <h1>Roster Component</h1>
            <div>
                <input type="text" value={searchTerm} onChange={(e) => setSearchTerm(e.target.value)} />
                <button onClick={searchPlayers} disabled={isLoading}>Search Players</button>
            </div>
            <>
                <form onSubmit={createRosterPlayer}>
                    <label htmlFor="playerId">Player ID</label>
                    <input type="text" id="playerId" name="playerId" value={playerId} onChange={(e) => setPlayerId(e.target.value)} />
                    <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Add Player"}</button>
                </form>
            </>
            <div>
                {searchPlayerId && (
                    <div>
                        Player ID: {searchPlayerId}
                        <button onClick={() => handleAddPlayerToRoster(searchPlayerId)}>Add Player To Roster</button>
                    </div>
                )}
            </div>
            <>
                <div className="table-container">
                    <table className="table">
                        <thead>
                            <tr>
                                <th>Position</th>
                                <th>Team</th>
                                <th>Player</th>
                            </tr>
                        </thead>
                        <tbody>
                            {rosterPlayers.map((rosterPlayer, index) => (
                                <tr key={index}>
                                    <td>{rosterPlayer.position}</td>
                                    <td>{rosterPlayer.team}</td>
                                    <td>{rosterPlayer.firstName} {rosterPlayer.lastName}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </>
            {error && <p>{error}</p>}
        </div>
    )
}

export default RosterComponent;
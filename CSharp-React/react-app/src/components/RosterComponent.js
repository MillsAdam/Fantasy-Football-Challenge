import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import "../styles/RosterComponent.css";

function RosterComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosterPlayers, setRosterPlayers] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState("");
    const [searchPlayer, setSearchPlayer] = useState([]);

    useEffect(() => {
        async function getRosterPlayers() {
            setIsLoading(true);
            try {
                const rosterPlayersData = await RosterService.getRosterPlayersByUser(authToken);
                console.log(rosterPlayersData);
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

    async function searchPlayers() {
        setIsLoading(true);
        setError(null);
        try {
            const searchData = await DatabaseService.searchPlayers(searchTerm);
            console.log(searchData);
            setSearchPlayer(searchData);
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
                setSearchTerm("");
                setSearchPlayer([]);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create roster player');
        }
        setIsLoading(false);
    }

    async function handleRemovePlayerFromRoster(playerId) {
        setIsLoading(true);
        setError(null);
        try {
            const removedRosterPlayer = await RosterService.deleteRosterPlayer(playerId, authToken);
            if (removedRosterPlayer) {
                const updatedRosterPlayers = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(updatedRosterPlayers);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to remove roster player');
        }
        setIsLoading(false);
    }

    return (
        <div className="component-container">
            <h1>Roster Component</h1>
            <div>
                <input className="search-input" type="text" value={searchTerm} placeholder="Enter Name" onChange={(e) => setSearchTerm(e.target.value)} />
                <button className="search-button" onClick={searchPlayers} disabled={isLoading}>Search Players</button>
            </div>
            {isLoading ? (
                <p>Loading...</p>
            ) : (
                searchPlayer.length > 0 && (
                    <div>
                        <h2>Search Results</h2>
                        <div className="table-container">
                            <table>
                                <thead>
                                    <tr>
                                        <th>Add</th>
                                        <th>Pos</th>
                                        <th>Team</th>
                                        <th>Player</th>
                                        <th>Avg</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {searchPlayer.map((player, index) => (
                                        <tr key={index}>
                                            <td>
                                                <button className="add-remove-button" onClick={() => handleAddPlayerToRoster(player.playerId)}>+</button>
                                            </td>
                                            <td>{player.position}</td>
                                            <td>{player.team}</td>
                                            <td>{player.name}</td>
                                            <td>{player.fantasyPointsAvg}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>
                )
            )}
            <>
                <h2>My Roster</h2>
                {rosterPlayers.length > 0 && (
                    <div className="table-container">
                        <table>
                            <thead>
                                <tr>
                                    <th>Remove</th>
                                    <th>Pos</th>
                                    <th>Team</th>
                                    <th>Player</th>
                                    <th>Avg</th>
                                    <th>Proj</th>
                                </tr>
                            </thead>
                            <tbody>
                                {rosterPlayers.map((rosterPlayer, index) => (
                                    <tr key={index}>
                                        <td>
                                            <button className="add-remove-button" onClick={() => handleRemovePlayerFromRoster(rosterPlayer.playerId)}>-</button>
                                        </td>
                                        <td>{rosterPlayer.position}</td>
                                        <td>{rosterPlayer.team}</td>
                                        <td>{rosterPlayer.name}</td>
                                        <td>{rosterPlayer.fantasyPointsAvg}</td>
                                        <td>{rosterPlayer.fantasyPointsProj}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </>
            {error && <p>{error}</p>}
        </div>
    )
}

export default RosterComponent;
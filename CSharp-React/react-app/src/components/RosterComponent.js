import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import { AuthContext } from "../context/AuthContext";
import { useConfig } from "../context/ConfigContext";
import DatabaseService from "../services/DatabaseService";
import styles from "../styles/RosterComponent.module.css";
import { positionOptions, teamNameDisplayOptions } from "../constants/RosterConstants";

function RosterComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosterPlayers, setRosterPlayers] = useState([]);
    const [activeRosterPlayers, setActiveRosterPlayers] = useState([]);
    const isRosterFull = rosterPlayers.length >= 27;
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [searchName, setSearchName] = useState("");
    const [activeTeamNameOptions, setActiveTeamNameOptions] = useState([]);
    const [selectedTeamName, setSelectedTeamName] = useState("");
    const [selectedPosition, setSelectedPosition] = useState("");
    const [searchPlayer, setSearchPlayer] = useState([]);
    const [activeSearchMethod, setActiveSearchMethod] = useState("");
    const { configurations } = useConfig();
    const [isRosterLocked, setIsRosterLocked] = useState(false);
    const [playerIndexMap, setPlayerIndexMap] = useState({});


    useEffect(() => {
        async function getRosterPlayers() {
            setIsLoading(true);
            try {
                const rosterPlayersData = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(rosterPlayersData);
                const activeRosterPlayers = rosterPlayersData.filter(player => player.teamStatus === 'Active');
                setActiveRosterPlayers(activeRosterPlayers);
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

    useEffect(() => {
        async function getActiveTeamNameOptions() {
            setIsLoading(true);
            try {
                const activeTeamNameOptionsData = await DatabaseService.getActiveTeams();
                const sortedTeamNames = activeTeamNameOptionsData.map(team => team.team).sort();
                setActiveTeamNameOptions(sortedTeamNames);
            } catch (error) {
                console.error('An error occurred: ', error);
                setError('Failed to get active team name options');
            }
            setIsLoading(false);
        }

        getActiveTeamNameOptions();
    }, []);

    useEffect(() => {
        const rosterLockConfig = configurations.find(config => config.configKey === 'lockRosters');
        if (rosterLockConfig) {
            setIsRosterLocked(rosterLockConfig.configValue === 1);
        }
    }, [configurations, isRosterLocked]);

    async function startSearchByName(e) {
        setActiveSearchMethod("name");
        searchPlayersName(e);
    }

    async function startSearchByTeam(e) {
        setActiveSearchMethod("team");
        searchPlayersTeam(e);
    }

    async function startSearchByPosition(e) {
        setActiveSearchMethod("position");
        searchPlayersPosition(e);
    }

    async function clearSearch(e) {
        e.preventDefault();
        setSearchName("");
        setSelectedTeamName("");
        setSelectedPosition("");
        setSearchPlayer([]);
        setActiveSearchMethod("");
    }

    async function searchPlayersName(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const searchData = await DatabaseService.searchPlayersName(searchName);
            const filteredSearchData = searchData.filter(player => 
                player.teamStatus === 'Active' &&
                !rosterPlayers.some(rosterPlayer => rosterPlayer.playerId === player.playerId)
            );
            setSearchPlayer(filteredSearchData);
            setPlayerIndexMap(filteredSearchData.reduce((acc, player, index) => {
                acc[player.playerId] = index;
                return acc;
            }, {}));
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to search players');
        }
        setIsLoading(false);
    }

    async function searchPlayersTeam(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const searchData = await DatabaseService.searchPlayersTeam(selectedTeamName);
            const filteredSearchData = searchData.filter(player => 
                player.teamStatus === 'Active' &&
                !rosterPlayers.some(rosterPlayer => rosterPlayer.playerId === player.playerId)
            );
            setSearchPlayer(filteredSearchData);
            setPlayerIndexMap(filteredSearchData.reduce((acc, player, index) => {
                acc[player.playerId] = index;
                return acc;
            }, {}));
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to search players');
        }
        setIsLoading(false);
    }

    async function searchPlayersPosition(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const searchData = await DatabaseService.searchPlayersPosition(selectedPosition);
            const filteredSearchData = searchData.filter(player => 
                player.teamStatus === 'Active' &&
                !rosterPlayers.some(rosterPlayer => rosterPlayer.playerId === player.playerId)
            );
            setSearchPlayer(filteredSearchData);
            setPlayerIndexMap(filteredSearchData.reduce((acc, player, index) => {
                acc[player.playerId] = index;
                return acc;
            }, {}));
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to search players');
        }
        setIsLoading(false);
    }

    async function handleAddPlayerToRoster(e, playerId) {
        e.preventDefault();
        if (isRosterLocked) {
            alert('Roster is locked');
            return;
        }
        if (isRosterFull) {
            alert('Roster is full');
            return;
        }
        setIsLoading(true);
        setError(null);
        try {
            const newRosterPlayer = await RosterService.createRosterPlayer(playerId, authToken);
            if (newRosterPlayer) {
                const updatedRosterPlayers = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(updatedRosterPlayers);
                const activeRosterPlayers = updatedRosterPlayers.filter(player => player.teamStatus === 'Active');
                setActiveRosterPlayers(activeRosterPlayers);
                const updatedSearchPlayer = searchPlayer.filter(player => player.playerId !== playerId);
                setSearchPlayer(updatedSearchPlayer);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create roster player');
        }
        setIsLoading(false);
    }

    async function handleRemovePlayerFromRoster(e, playerId) {
        e.preventDefault();
        if (isRosterLocked) {
            alert('Roster is locked');
            return;
        }
        setIsLoading(true);
        setError(null);
        try {
            const playerToRemove = rosterPlayers.find(player => player.playerId === playerId);
            const removedRosterPlayer = await RosterService.deleteRosterPlayer(playerId, authToken);
            if (removedRosterPlayer) {
                const updatedRosterPlayers = await RosterService.getRosterPlayersByUser(authToken);
                setRosterPlayers(updatedRosterPlayers);
                const activeRosterPlayers = updatedRosterPlayers.filter(player => player.teamStatus === 'Active');
                setActiveRosterPlayers(activeRosterPlayers);
                
                const playerIndex = playerIndexMap[playerId];
                if (playerToRemove) {
                    setSearchPlayer(prevPlayers => {
                        const newPlayers = [...prevPlayers];
                        newPlayers.splice(playerIndex, 0, playerToRemove);
                        return newPlayers;
                    });
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to remove roster player');
        }
        setIsLoading(false);
    }

    return (
        <div>
            {isLoading? (<p>Loading...</p>) : (
                <div className={styles.pageContainer}>
                    <div className={styles.componentContainer}>
                        <h2>Search Players</h2>
                        <div>
                            <input 
                                className={styles.searchInput} 
                                type="text" 
                                value={searchName} 
                                placeholder="Enter Name" 
                                onChange={(e) => setSearchName(e.target.value)} 
                                disabled={activeSearchMethod && activeSearchMethod !== "name"}
                            />
                            <button 
                                className={styles.searchButton} 
                                onClick={startSearchByName} 
                                disabled={isLoading || (activeSearchMethod && activeSearchMethod !=="name")}
                            >
                                Search by Name
                            </button>
                        </div>
                        <div>
                            <select 
                                className={styles.searchSelect} 
                                value={selectedTeamName} 
                                onChange={(e) => setSelectedTeamName(e.target.value)} 
                                disabled={activeSearchMethod && activeSearchMethod !== "team"}
                            >
                                <option value="" disabled hidden>Select Team</option>
                                {activeTeamNameOptions.map(teamName => (
                                    <option key={teamName} value={teamName}>{teamNameDisplayOptions[teamName] || teamName}</option>
                                ))}
                            </select>
                            <button 
                                className={styles.searchButton}
                                onClick={startSearchByTeam} 
                                disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "team")}
                            >
                                Search by Team
                            </button>
                        </div>
                        <div>
                            <select 
                                className={styles.searchSelect} 
                                value={selectedPosition} 
                                onChange={(e) => setSelectedPosition(e.target.value)} 
                                disabled={activeSearchMethod && activeSearchMethod !== "position"}
                            >
                                <option value="" disabled hidden>Select Position</option>
                                {positionOptions.map((position, index) => (
                                    <option key={index} value={position}>{position}</option>
                                ))}
                            </select>
                            <button 
                                className={styles.searchButton} 
                                onClick={startSearchByPosition} 
                                disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "position")}
                            >
                                Search by Position
                            </button>
                        </div>
                        <button className={styles.clearButton} onClick={clearSearch} disabled={isLoading}>Clear Search</button>
                        {isLoading ? (<p>Loading...</p>) : (
                            searchPlayer.length > 0 && (
                                <div>
                                    <h2>Search Results</h2>
                                    <div className="table-container">
                                        <table>
                                            <thead>
                                                <tr>
                                                    <th>Add</th>
                                                    <th>Conf</th>
                                                    <th>Team</th>
                                                    <th>Pos</th>
                                                    <th>Inj</th>
                                                    <th>Player</th>
                                                    <th>Avg</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {searchPlayer.map((player, index) => (
                                                    <tr key={index}>
                                                        <td>
                                                            <button 
                                                                className="add-remove-button" 
                                                                onClick={(e) => handleAddPlayerToRoster(e, player.playerId)} 
                                                                // disabled={isRosterFull}
                                                            >
                                                                +
                                                            </button>
                                                        </td>
                                                        <td>{player.conference}</td>
                                                        <td>{player.team}</td>
                                                        <td>{player.position}</td>
                                                        <td className={
                                                            player.injuryStatus === 'P' || player.injuryStatus === null ? 'green-highlight' :
                                                            ["Q"].includes(player.injuryStatus?.charAt(0)) ? 'yellow-highlight' :
                                                            ["D", "O"].includes(player.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
                                                        }>
                                                            {player.injuryStatus ? player.injuryStatus.charAt(0) : 'A'}
                                                        </td>
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
                    </div>
                    
                    <div className={styles.componentContainer}>
                        <h2>My Roster</h2>
                        {activeRosterPlayers.length > 0 && (
                            <div className="table-container">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Remove</th>
                                            <th>Conf</th>
                                            <th>Team</th>
                                            <th>Pos</th>
                                            <th>Inj</th>
                                            <th>Player</th>
                                            <th>Avg</th>
                                            <th>Proj</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {activeRosterPlayers.map((rosterPlayer, index) => (
                                            <tr key={index}>
                                                <td>{index+1}</td>
                                                <td>
                                                    <button 
                                                        className="add-remove-button" 
                                                        onClick={(e) => handleRemovePlayerFromRoster(e, rosterPlayer.playerId)}
                                                    >
                                                        -
                                                    </button>
                                                </td>
                                                <td>{rosterPlayer.conference}</td>
                                                <td>{rosterPlayer.team}</td>
                                                <td>{rosterPlayer.position}</td>
                                                <td className={
                                                    rosterPlayer.injuryStatus === 'P' || rosterPlayer.injuryStatus === null ? 'green-highlight' :
                                                    ["Q"].includes(rosterPlayer.injuryStatus?.charAt(0)) ? 'yellow-highlight' :
                                                    ["D", "O"].includes(rosterPlayer.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
                                                }>
                                                    {rosterPlayer.injuryStatus ? rosterPlayer.injuryStatus.charAt(0) : 'A'}
                                                </td>
                                                <td>{rosterPlayer.name}</td>
                                                <td>{rosterPlayer.fantasyPointsAvg}</td>
                                                <td>{rosterPlayer.fantasyPointsProj}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        )}
                    </div>
                </div>
            )}
            
            <div className="message-container">
                {error && <p>{error}</p>}
            </div>
        </div>  
    )
}

export default RosterComponent;
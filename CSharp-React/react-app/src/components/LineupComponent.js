import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import LineupService from "../services/LineupService";
// import DatabaseService from "../services/DatabaseService";
import { AuthContext } from "../context/AuthContext";
import { useConfig } from "../context/ConfigContext";
import "../styles/LineupComponent.css";
import { allLineupPositions, positionSpecificOptions, gameWeekOptions } from "../constants/LineupConstants";


function LineupComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [activeRosterPlayers, setActiveRosterPlayers] = useState([]);
    const [lineupPlayers, setLineupPlayers] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [lineupOptions, setLineupOptions] = useState([]);
    const { configurations } = useConfig();
    const [isLineupLocked, setIsLineupLocked] = useState(false);
    const [selectedGameWeek, setSelectedGameWeek] = useState("");
    const [weeklyLineupPlayers, setWeeklyLineupPlayers] = useState([]);
    const [weeklyScore, setWeeklyScore] = useState("");

    useEffect(() => {
        async function getRosterPlayers() {
            setIsLoading(true);
            try {
                const rosterPlayersData = await RosterService.getRosterPlayersByUser(authToken);
                const activeRosterPlayersData = rosterPlayersData.filter(player => player.teamStatus === 'Active');
                const lineupPlayerIds = new Set(lineupPlayers.map(p => p.playerId));
                const filteredRosterPlayers = activeRosterPlayersData.filter(p => !lineupPlayerIds.has(p.playerId));
                setActiveRosterPlayers(filteredRosterPlayers);
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
    }, [authToken, currentUser, lineupPlayers]);

    useEffect(() => {
        async function getLineupPlayers() {
            setIsLoading(true);
            try {
                const lineupPlayersData = await LineupService.getLineupPlayersByUser(authToken);
                setLineupPlayers(lineupPlayersData);
            } catch (error) {
                console.error('An error occurred: ', error);
                setError('Failed to get lineup players');
            }
            setIsLoading(false);
        }

        if (currentUser && currentUser.userId) {
            getLineupPlayers();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser]);

    useEffect(() => {
        const lineupLockConfig = configurations.find(config => config.configKey === 'lockLineups');
        if (lineupLockConfig) {
            setIsLineupLocked(lineupLockConfig.configValue === 1);
        }
    }, [configurations, isLineupLocked]);

    useEffect(() => {
        const takenPositions = lineupPlayers.map((lineupPlayer) => lineupPlayer.lineupPosition);
        const availablePositions = allLineupPositions.filter((position) => !takenPositions.includes(position));
        setLineupOptions(availablePositions);
    }, [lineupPlayers]);

    function getFilteredLineupOptions(playerPosition) {
        const specificOptions = positionSpecificOptions[playerPosition] || [];
        return lineupOptions.filter(option => specificOptions.includes(option));
    }

    function clearGameWeek(e) {
        e.preventDefault();
        setSelectedGameWeek("");
        setWeeklyLineupPlayers([]);
    }

    async function handleAddPlayerToLineup(e, playerId, lineupPosition) {
        e.preventDefault();
        if (isLineupLocked) {
            alert('Lineups are locked');
            return;
        }
        setIsLoading(true);
        setError(null);
        try {
            const newLineupPlayer = await LineupService.createLineupPlayer(playerId, lineupPosition, authToken);
            if (newLineupPlayer) {
                const updatedLineupPlayers = await LineupService.getLineupPlayersByUser(authToken);
                setLineupPlayers(updatedLineupPlayers);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create lineup player');
        }
        setIsLoading(false);
    }

    async function handleRemovePlayerFromLineup(e, playerId) {
        e.preventDefault();
        if (isLineupLocked) {
            alert('Lineups are locked');
            return;
        }
        setIsLoading(true);
        setError(null);
        try {
            const removedLineupPlayer = await LineupService.deleteLineupPlayer(playerId, authToken);
            if (removedLineupPlayer) {
                const updatedLineupPlayers = await LineupService.getLineupPlayersByUser(authToken);
                setLineupPlayers(updatedLineupPlayers);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to remove lineup player');
        }
        setIsLoading(false);
    }

    async function getLineupPlayersByWeek(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const lineupPlayersData = await LineupService.getLineupPlayersByWeek(authToken, selectedGameWeek);
            setWeeklyLineupPlayers(lineupPlayersData);
            const weeklyScoreData = await LineupService.getWeeklyScoreByWeek(authToken, selectedGameWeek);
            setWeeklyScore(weeklyScoreData);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to get lineup players');
        }
        setIsLoading(false);
    }

    return (
        <div>
            {isLoading ? (<p>Loading...</p>) : (
                <div className="page-container">
                    <div className="component-container">
                        <h2>My Lineup</h2>
                        {lineupPlayers.length > 0 && (
                            <div className="table-container">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Remove</th>
                                            <th>Conf</th>
                                            <th>Team</th>
                                            <th>Pos</th>
                                            <th>Inj</th>
                                            <th>Player</th>
                                            <th>Proj</th>
                                            <th>Points</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {lineupPlayers.map((lineupPlayer, index) => (
                                            <tr key={index}>
                                                <td>
                                                    <button className="add-remove-button" onClick={(e) => handleRemovePlayerFromLineup(e, lineupPlayer.playerId)}>-</button>
                                                </td>
                                                <td>{lineupPlayer.conference}</td>
                                                <td>{lineupPlayer.team}</td>
                                                <td>{lineupPlayer.lineupPosition}</td>
                                                <td className={
                                                    lineupPlayer.injuryStatus === 'P' || lineupPlayer.injuryStatus === null ? 'green-highlight' :
                                                    ["Q"].includes(lineupPlayer.injuryStatus?.charAt(0)) ? 'yellow-highlight' :
                                                    ["D", "O"].includes(lineupPlayer.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
                                                }>
                                                    {lineupPlayer.injuryStatus ? lineupPlayer.injuryStatus.charAt(0) : 'A'}
                                                </td>
                                                <td>{lineupPlayer.name}</td>
                                                <td>{lineupPlayer.fantasyPointsProj}</td>
                                                <td>{lineupPlayer.fantasyPoints}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        )}
                    </div>

                    <div className="component-container">
                        <h2>My Roster</h2>
                        {activeRosterPlayers.length > 0 && (
                            <div className="table-container">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>Lineup</th>
                                            <th>Add</th>
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
                                                <td>
                                                    <select className="lineup-select" id={`lineup-position-${index}`}>
                                                        {getFilteredLineupOptions(rosterPlayer.position).map((option) => (
                                                            <option key={option} value={option}>{option}</option>
                                                        ))}
                                                    </select>
                                                </td>
                                                <td>
                                                    <button className="add-remove-button" onClick={(e) => handleAddPlayerToLineup(e, rosterPlayer.playerId, document.getElementById(`lineup-position-${index}`).value)}>+</button>
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
                    
                    <div className="component-container">
                        <h2>Weekly Lineup</h2>
                        <form onSubmit={getLineupPlayersByWeek}>
                            <select 
                                className="game-week-select"
                                value={selectedGameWeek} 
                                onChange={(e) => setSelectedGameWeek(e.target.value)}
                            >
                                <option value="" disabled hidden>Select Week</option>
                                {gameWeekOptions.map((option) => (
                                    <option key={option} value={option}>Week {option}</option>
                                ))}
                            </select>
                            <button type="submit" disabled={isLoading || selectedGameWeek === ""}>Submit</button>
                            <button type="button" onClick={clearGameWeek} disabled={isLoading || selectedGameWeek === ""}>Clear</button>
                        </form>
                        {weeklyLineupPlayers.length > 0 && (
                            <div>
                                <div>
                                    <h3>Weekly Score: {weeklyScore}</h3>
                                </div>
                                <div className="table-container">
                                    <table>
                                        <thead>
                                            <tr>
                                                <th>Conf</th>
                                                <th>Team</th>
                                                <th>Pos</th>
                                                <th>Inj</th>
                                                <th>Player</th>
                                                <th>Points</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {weeklyLineupPlayers.map((lineupPlayer, index) => (
                                                <tr key={index}>
                                                    <td>{lineupPlayer.conference}</td>
                                                    <td>{lineupPlayer.team}</td>
                                                    <td>{lineupPlayer.lineupPosition}</td>
                                                    <td className={
                                                        lineupPlayer.injuryStatus === 'P' || lineupPlayer.injuryStatus === null ? 'green-highlight' :
                                                        ["Q"].includes(lineupPlayer.injuryStatus?.charAt(0)) ? 'yellow-highlight' :
                                                        ["D", "O"].includes(lineupPlayer.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
                                                    }>
                                                        {lineupPlayer.injuryStatus ? lineupPlayer.injuryStatus.charAt(0) : 'A'}
                                                    </td>
                                                    <td>{lineupPlayer.name}</td>
                                                    <td>{lineupPlayer.fantasyPoints}</td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </table>
                                </div>
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

export default LineupComponent;
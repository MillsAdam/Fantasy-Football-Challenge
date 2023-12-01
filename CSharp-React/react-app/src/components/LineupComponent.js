import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import LineupService from "../services/LineupService";
import { AuthContext } from "../context/AuthContext";
import "../styles/LineupComponent.css";

const ALL_LINEUP_POSITIONS = ['QB1', 'QB2', 'RB1', 'RB2', 'WR1', 'WR2', 'WR3', 'TE', 'FLEX', 'K', 'DEF'];
const POSITION_SPECIFIC_OPTIONS = {
    QB: ['QB1', 'QB2'],
    RB: ['RB1', 'RB2', 'FLEX'],
    WR: ['WR1', 'WR2', 'WR3', 'FLEX'],
    TE: ['TE', 'FLEX'],
    K: ['K'],
    DEF: ['DEF']
}


function LineupComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    // const [rosterPlayers, setRosterPlayers] = useState([]);
    const [activeRosterPlayers, setActiveRosterPlayers] = useState([]);
    const [lineupPlayers, setLineupPlayers] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [lineupOptions, setLineupOptions] = useState([]);

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
        const takenPositions = lineupPlayers.map((lineupPlayer) => lineupPlayer.lineupPosition);
        const availablePositions = ALL_LINEUP_POSITIONS.filter((position) => !takenPositions.includes(position));
        setLineupOptions(availablePositions);
    }, [lineupPlayers]);

    function getFilteredLineupOptions(playerPosition) {
        const specificOptions = POSITION_SPECIFIC_OPTIONS[playerPosition] || [];
        return lineupOptions.filter(option => specificOptions.includes(option));
    }

    async function handleAddPlayerToLineup(e, playerId, lineupPosition) {
        e.preventDefault();
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
                                                    ["Q", "D", "O"].includes(lineupPlayer.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
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
                                                    ["Q", "D", "O"].includes(rosterPlayer.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
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

export default LineupComponent;
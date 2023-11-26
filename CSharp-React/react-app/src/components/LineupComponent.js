import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import LineupService from "../services/LineupService";
import { AuthContext } from "../context/AuthContext";
import "../styles/LeagueComponent.css";
// import LineupPositionModal from "./LineupPositionModal";

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
    const [rosterPlayers, setRosterPlayers] = useState([]);
    const [lineupPlayers, setLineupPlayers] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    // const [isModalOpen, setIsModalOpen] = useState(false);
    // const [currentPlayerId, setCurrentPlayerId] = useState(null);
    const [lineupOptions, setLineupOptions] = useState([]);

    useEffect(() => {
        async function getRosterPlayers() {
            setIsLoading(true);
            try {
                const rosterPlayersData = await RosterService.getRosterPlayersByUser(authToken);
                const lineupPlayerIds = new Set(lineupPlayers.map(p => p.playerId));
                const filteredRosterPlayers = rosterPlayersData.filter(p => !lineupPlayerIds.has(p.playerId));
                setRosterPlayers(filteredRosterPlayers);
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

    async function handleAddPlayerToLineup(playerId, lineupPosition){
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

    async function handleRemovePlayerFromLineup(playerId) {
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

    // const openModal = (playerId) => {
    //     setCurrentPlayerId(playerId);
    //     setIsModalOpen(true);
    // }

    // const closeModal = () => {
    //     setIsModalOpen(false);
    // }

    // const handleModalSubmit = async (lineupPosition) => {
    //     await handleAddPlayerToLineup(currentPlayerId, lineupPosition);
    // }

    return (
        <div>
            <h1>Lineup Component</h1>
            {isLoading ? (
                <p>Loading...</p>
            ) : (
                <div className="lineup-container">
                    <div className="roster">
                        <h2>My Roster</h2>
                        {rosterPlayers.length > 0 && (
                            <div className="table-container">
                                <table className="table">
                                    <thead>
                                        <tr>
                                            <th>Remove</th>
                                            <th>Position</th>
                                            <th>Team</th>
                                            <th>Player</th>
                                            <th>Projection</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {rosterPlayers.map((rosterPlayer, index) => (
                                            <tr key={index}>
                                                <td>
                                                    <select id={`lineup-position-${index}`}>
                                                        {getFilteredLineupOptions(rosterPlayer.position).map((option) => (
                                                            <option key={option} value={option}>{option}</option>
                                                        ))}
                                                    </select>
                                                    <button onClick={() => handleAddPlayerToLineup(rosterPlayer.playerId, document.getElementById(`lineup-position-${index}`).value)}>+</button>
                                                </td>
                                                {/* <td>
                                                    <button onClick={() => openModal(rosterPlayer.playerId)}>+</button>
                                                </td> */}
                                                <td>{rosterPlayer.position}</td>
                                                <td>{rosterPlayer.team}</td>
                                                <td>{rosterPlayer.name}</td>
                                                <td>{rosterPlayer.fantasyPointsProj}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        )}
                    </div>

                    <div className="lineup">
                        <h2>My Lineup</h2>
                        {lineupPlayers.length > 0 && (
                            <div className="table-container">
                                <table className="table">
                                    <thead>
                                        <tr>
                                            <th>Remove</th>
                                            <th>Position</th>
                                            <th>Team</th>
                                            <th>Player</th>
                                            <th>Projection</th>
                                            <th>Points</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {lineupPlayers.map((lineupPlayer, index) => (
                                            <tr key={index}>
                                                {/* <td>
                                                    <select id={`lineup-position-${index}`}>
                                                        <option value="QB1">QB1</option>
                                                        <option value="QB2">QB2</option>
                                                        <option value="RB1">RB1</option>
                                                        <option value="RB2">RB2</option>
                                                        <option value="WR1">WR1</option>
                                                        <option value="WR2">WR2</option>
                                                        <option value="WR3">WR3</option>
                                                        <option value="TE1">TE1</option>
                                                        <option value="FLEX">FLEX</option>
                                                        <option value="K">K</option>
                                                        <option value="DEF">DEF</option>
                                                    </select>
                                                    <button onClick={() => handleRemovePlayerFromLineup(lineupPlayer.playerId, document.getElementById(`lineup-position-${index}`).value)}>-</button>
                                                </td> */}
                                                <td>
                                                    <button onClick={() => handleRemovePlayerFromLineup(lineupPlayer.playerId)}>-</button>
                                                </td>
                                                <td>{lineupPlayer.lineupPosition}</td>
                                                <td>{lineupPlayer.team}</td>
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
                </div>
            )}
            {error && <p>{error}</p>}
            {/* <LineupPositionModal isOpen={isModalOpen} onClose={closeModal} onSubmit={handleModalSubmit} /> */}
        </div>
    )
}

export default LineupComponent;
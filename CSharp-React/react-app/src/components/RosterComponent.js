import React, { useState, useContext, useEffect } from "react";
import RosterService from "../services/RosterService";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import "../styles/RosterComponent.css";

const positionOptions = ['QB', 'RB', 'WR', 'TE', 'K', 'DEF'];
const teamNameOptions = [
    'ARI', 'ATL', 'BAL', 'BUF', 'CAR', 'CHI', 'CIN', 'CLE', 
    'DAL', 'DEN', 'DET', 'GB', 'HOU', 'IND', 'JAX', 'KC',
    'MIA', 'MIN', 'NE', 'NO', 'NYG', 'NYJ', 'LV', 'PHI',
    'PIT', 'LAC', 'SEA', 'SF', 'LAR', 'TB', 'TEN', 'WAS'
];
const teamNameDisplayNames = {
    'ARI': 'Arizona Cardinals',
    'ATL': 'Atlanta Falcons',
    'BAL': 'Baltimore Ravens',
    'BUF': 'Buffalo Bills',
    'CAR': 'Carolina Panthers',
    'CHI': 'Chicago Bears',
    'CIN': 'Cincinnati Bengals',
    'CLE': 'Cleveland Browns',
    'DAL': 'Dallas Cowboys',
    'DEN': 'Denver Broncos',
    'DET': 'Detroit Lions',
    'GB': 'Green Bay Packers',
    'HOU': 'Houston Texans',
    'IND': 'Indianapolis Colts',
    'JAX': 'Jacksonville Jaguars',
    'KC': 'Kansas City Chiefs',
    'MIA': 'Miami Dolphins',
    'MIN': 'Minnesota Vikings',
    'NE': 'New England Patriots',
    'NO': 'New Orleans Saints',
    'NYG': 'New York Giants',
    'NYJ': 'New York Jets',
    'LV': 'Las Vegas Raiders',
    'PHI': 'Philadelphia Eagles',
    'PIT': 'Pittsburgh Steelers',
    'LAC': 'Los Angeles Chargers',
    'SEA': 'Seattle Seahawks',
    'SF': 'San Francisco 49ers',
    'LAR': 'Los Angeles Rams',
    'TB': 'Tampa Bay Buccaneers',
    'TEN': 'Tennessee Titans',
    'WAS': 'Washington Football Team'
};

function RosterComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosterPlayers, setRosterPlayers] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [searchName, setSearchName] = useState("");
    const [selectedTeamName, setSelectedTeamName] = useState("");
    const [selectedPosition, setSelectedPosition] = useState("");
    const [searchPlayer, setSearchPlayer] = useState([]);
    const [activeSearchMethod, setActiveSearchMethod] = useState("");

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

    function startSearchByName(e) {
        setActiveSearchMethod("name");
        searchPlayersName(e);
    }

    function startSearchByTeam(e) {
        setActiveSearchMethod("team");
        searchPlayersTeam(e);
    }

    function startSearchByPosition(e) {
        setActiveSearchMethod("position");
        searchPlayersPosition(e);
    }

    function clearSearch() {
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
            console.log(searchData);
            setSearchPlayer(searchData);
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
            console.log(searchData);
            setSearchPlayer(searchData);
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
        <div>
            <div className="page-container">
                <div className="component-container">
                    <h2>Search Players</h2>
                    <div>
                        <input 
                            className="search-input" 
                            type="text" value={searchName} 
                            placeholder="Enter Name" 
                            onChange={(e) => setSearchName(e.target.value)} 
                            disabled={activeSearchMethod && activeSearchMethod !== "name"}
                        />
                        <button 
                            className="search-button" 
                            onClick={startSearchByName} 
                            disabled={isLoading || (activeSearchMethod && activeSearchMethod !=="name")}
                        >
                            Search by Name
                        </button>
                    </div>
                    <div>
                        <select 
                            className="search-select" 
                            value={selectedTeamName} 
                            onChange={(e) => setSelectedTeamName(e.target.value)} 
                            disabled={activeSearchMethod && activeSearchMethod !== "team"}
                        >
                            <option value="" disabled hidden>Select Team</option>
                            {teamNameOptions.map((teamName, index) => (
                                <option key={index} value={teamName}>{teamNameDisplayNames[teamName]}</option>
                            ))}
                        </select>
                        <button 
                            className="search-button" 
                            onClick={startSearchByTeam} 
                            disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "team")}
                        >
                            Search by Team
                        </button>
                    </div>
                    <div>
                        <select 
                            className="search-select" 
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
                            className="search-button" 
                            onClick={startSearchByPosition} 
                            disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "position")}
                        >
                            Search by Position
                        </button>
                    </div>
                    <button className="clear-button" onClick={clearSearch} disabled={isLoading}>Clear Search</button>
                    {isLoading ? (<p>Loading...</p>) : (
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
                </div>
                
                <div className="component-container">
                    <h2>My Roster</h2>
                    {rosterPlayers.length > 0 && (
                        <div className="table-container">
                            <table>
                                <thead>
                                    <tr>
                                        <th>#</th>
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
                                            <td>{index+1}</td>
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
                </div>
            </div>
            <div className="message-container">
                {error && <p>{error}</p>}
            </div>
        </div>  
    )
}

export default RosterComponent;
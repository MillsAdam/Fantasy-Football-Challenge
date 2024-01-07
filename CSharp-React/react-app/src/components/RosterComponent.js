import React, { useState, useContext, useEffect, useCallback } from "react";
import RosterService from "../services/RosterService";
import LeagueService from "../services/LeagueService";
import DatabaseService from "../services/DatabaseService";
import { AuthContext } from "../context/AuthContext";
import { useConfig } from "../context/ConfigContext";
import { positionOptions, teamNameDisplayOptions } from "../constants/RosterConstants";
import NavigationBar from "./NavigationBar";

function RosterComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [teamName, setTeamName] = useState("");
    const [userHasTeam, setUserHasTeam] = useState(false);
    const [rosterPlayers, setRosterPlayers] = useState([]);
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
    const [currentLeagueId, setCurrentLeagueId] = useState(null);

    
    const fetchCurrentLeagueId = useCallback(async () => {
        setIsLoading(true);
        try {
            const leagueId = await LeagueService.getCurrentLeagueId(authToken);
            setCurrentLeagueId(leagueId);
        } catch (error) {
            console.error("Error fetching current league: ", error);
            setError(error);
        }
        setIsLoading(false);
    }, [authToken]);

    useEffect(() => {
        if (authToken && currentUser && currentUser.userId) {
            fetchCurrentLeagueId();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser, fetchCurrentLeagueId]);

    const checkUserTeam = useCallback(async () => {
        setIsLoading(true);
        try {
            const rostersData = await LeagueService.getFantasyRosters(authToken);
            const userRoster = rostersData.find(roster => roster.userId === currentUser.userId);
            if (userRoster) {
                setUserHasTeam(true);
            } else {
                setUserHasTeam(false);
            }
        } catch (error) {
            console.error('Error occurred checking team status: ', error);
            setError('Failed to check user team status');
        }
        setIsLoading(false);
    }, [authToken, currentUser.userId]);

    useEffect(() => {
        if (authToken && currentUser && currentUser.userId) {
            checkUserTeam();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser, checkUserTeam]);

    async function createRoster(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const newRoster = await LeagueService.createRoster(teamName, authToken);
            if (newRoster) {
                await LeagueService.getFantasyRosters(authToken);
                setUserHasTeam(true);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create League Roster');
        }
        setIsLoading(false);
    }


    const getRosterPlayers = useCallback(async () => {
        setIsLoading(true);
        try {
            const rosterPlayersData = await RosterService.getRosterPlayersByUser(authToken);
            setRosterPlayers(rosterPlayersData);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to get roster players');
        }
        setIsLoading(false);
    }, [authToken]);

    useEffect(() => {
        if (authToken && currentUser && currentUser.userId) {
            getRosterPlayers();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser, getRosterPlayers]);

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
        e.preventDefault();
        setActiveSearchMethod("name");
        searchPlayersName(e);
    }

    async function startSearchByTeam(e) {
        e.preventDefault();
        setActiveSearchMethod("team");
        searchPlayersTeam(e);
    }

    async function startSearchByPosition(e) {
        e.preventDefault();
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
        <div className="flex flex-col min-h-screen">
            <NavigationBar />
            {userHasTeam && (
                <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                    {!isRosterLocked && 
                        <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                            <div className="mb-4 text-xl text-primary">
                                Search Players
                            </div>
                            <div className="flex flex-row justify-between align-center flex-nowrap mb-4">
                                <input 
                                    className="input input-accent input-bordered input-sm md:input-md w-45"
                                    type="text" 
                                    value={searchName} 
                                    placeholder="Enter Name" 
                                    onChange={(e) => setSearchName(e.target.value)} 
                                    disabled={(activeSearchMethod && activeSearchMethod !== "name") || selectedTeamName || selectedPosition}
                                />
                                <button
                                    className="btn btn-primary btn-sm md:btn-md w-45" 
                                    onClick={startSearchByName} 
                                    disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "name") || searchName === ""}
                                >
                                    Search by Name
                                </button>
                            </div>
                            <div className="flex flex-row justify-between align-center flex-nowrap mb-4">
                                <select 
                                    className="select select-accent select-sm md:select-md w-45"
                                    value={selectedTeamName} 
                                    onChange={(e) => setSelectedTeamName(e.target.value)} 
                                    disabled={(activeSearchMethod && activeSearchMethod !== "team") || searchName || selectedPosition}
                                >
                                    <option value="" disabled hidden>Select Team</option>
                                    {activeTeamNameOptions.map(teamName => (
                                        <option key={teamName} value={teamName}>{teamNameDisplayOptions[teamName] || teamName}</option>
                                    ))}
                                </select>
                                <button 
                                    className="btn btn-primary btn-sm md:btn-md w-45" 
                                    onClick={startSearchByTeam} 
                                    disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "team") || selectedTeamName === ""}
                                >
                                    Search by Team
                                </button>
                            </div>
                            <div className="flex flex-row justify-between align-center flex-nowrap mb-4">
                                <select 
                                    className="select select-accent select-sm md:select-md w-45" 
                                    value={selectedPosition} 
                                    onChange={(e) => setSelectedPosition(e.target.value)} 
                                    disabled={(activeSearchMethod && activeSearchMethod !== "position") || searchName || selectedTeamName}
                                >
                                    <option value="" disabled hidden>Select Position</option>
                                    {positionOptions.map((position, index) => (
                                        <option key={index} value={position}>{position}</option>
                                    ))}
                                </select>
                                <button 
                                    className="btn btn-primary btn-sm md:btn-md w-45" 
                                    onClick={startSearchByPosition} 
                                    disabled={isLoading || (activeSearchMethod && activeSearchMethod !== "position") || selectedPosition === ""}
                                >
                                    Search by Position
                                </button>
                            </div>
                            <button 
                                className="btn btn-secondary btn-sm md:btn-md w-full w-full mb-4" 
                                onClick={clearSearch} 
                                disabled={isLoading || (!searchName && !selectedTeamName && !selectedPosition)}
                            >
                                Clear Search
                            </button>
                            {!isLoading && (
                                searchPlayer.length > 0 && (
                                    <div>
                                        <div className="mb-4 text-success">
                                            Search Results
                                        </div>
                                        <div className="overflow-auto">
                                            <table className="table table-xs table-pin-rows">
                                                <thead>
                                                    <tr className="bg-base-300">
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
                                                        <tr key={index} className="bg-neutral hover:bg-info-content">
                                                            <td>
                                                                <button 
                                                                    className="btn btn-primary btn-outline btn-xs" 
                                                                    onClick={(e) => handleAddPlayerToRoster(e, player.playerId)} 
                                                                    disabled={isRosterFull || isRosterLocked}
                                                                >
                                                                    +
                                                                </button>
                                                            </td>
                                                            <td>{player.conference}</td>
                                                            <td>{player.team}</td>
                                                            <td>{player.position}</td>
                                                            <td className={
                                                                ["P"].includes(player.injuryStatus?.charAt(0)) ? 'text-green-500' : 
                                                                ["Q"].includes(player.injuryStatus?.charAt(0)) ? 'text-yellow-500' : 
                                                                ["D", "O"].includes(player.injuryStatus?.charAt(0)) ? 'text-red-500' : ''
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
                    }
                    
                    {!isRosterLocked && 
                        <div className="divider lg:divider-horizontal divider-vertical"></div>
                    }
                    
                    
                    <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                        <div className="mb-4 text-xl text-primary">
                            My Roster
                        </div>
                        {rosterPlayers.length > 0 && (
                            <div className="overflow-auto">
                                <table className="table table-xs table-pin-rows">
                                    <thead>
                                        <tr className="bg-base-300">
                                            <th>#</th>
                                            <th>Rem</th>
                                            <th>Conf</th>
                                            <th>Team</th>
                                            <th>Pos</th>
                                            <th>Inj</th>
                                            <th>Player</th>
                                            <th>Avg</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {rosterPlayers.map((rosterPlayer, index) => (
                                            <tr key={index} className="bg-neutral hover:bg-info-content">
                                                <td>{index+1}</td>
                                                <td>
                                                    <button 
                                                        className="btn btn-secondary btn-outline btn-xs" 
                                                        onClick={(e) => handleRemovePlayerFromRoster(e, rosterPlayer.playerId)}
                                                        disabled={isRosterLocked}
                                                    >
                                                        -
                                                    </button>
                                                </td>
                                                <td>{rosterPlayer.conference}</td>
                                                <td>{rosterPlayer.team}</td>
                                                <td>{rosterPlayer.position}</td>
                                                <td className={
                                                    ["P"].includes(rosterPlayer.injuryStatus?.charAt(0)) ? 'text-green-500' : 
                                                    ["Q"].includes(rosterPlayer.injuryStatus?.charAt(0)) ? 'text-yellow-500' : 
                                                    ["D", "O"].includes(rosterPlayer.injuryStatus?.charAt(0)) ? 'text-red-500' : ''
                                                }>
                                                    {rosterPlayer.injuryStatus ? rosterPlayer.injuryStatus.charAt(0) : 'A'}
                                                </td>
                                                <td>{rosterPlayer.name}</td>
                                                <td>{rosterPlayer.fantasyPointsAvg}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        )}
                    </div>
                </div>
            )}
            {!currentLeagueId && !userHasTeam && !isLoading && (
                <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                    <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                        <div className="">
                            Join a League to create a Roster
                        </div>
                    </div>
                </div>
            )}
            {currentLeagueId > 0 && !userHasTeam && !isLoading && (
                <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                    <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                        <div className="mb-4 text-xl text-primary">
                            Create League Roster
                        </div>
                        <form onSubmit={createRoster}>
                            <label>Team Name</label>
                            <input 
                                className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                                type="text" 
                                value={teamName} 
                                onChange={(e) => setTeamName(e.target.value)} />
                            <button 
                                className="btn btn-primary btn-sm md:btn-md w-full mb-4" 
                                type="submit" 
                                disabled={isLoading}>{isLoading ? "Loading..." : "Create League Roster"}</button>
                        </form>
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
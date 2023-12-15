import React, { useState, useContext, useEffect, useCallback } from "react";
import LeagueService from "../services/LeagueService";
import { AuthContext } from "../context/AuthContext";
import NavigationBar from "./NavigationBar";

function LeagueComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [leagueName, setLeagueName] = useState("");
    const [leaguePassword, setLeaguePassword] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [errorMessage, setErrorMessage] = useState('');
    const [successMessage, setSuccessMessage] = useState('');
    const [searchQuery, setSearchQuery] = useState("");
    const [searchResults, setSearchResults] = useState([]);
    const [joinLeagueId, setJoinLeagueId] = useState(null);
    const [joinLeaguePassword, setJoinLeaguePassword] = useState("");
    const [myLeagues, setMyLeagues] = useState([]);
    const [currentLeagueId, setCurrentLeagueId] = useState(null);

    const fetchMyLeagues = useCallback(async () => {
        setIsLoading(true);
        try {
            const leagues = await LeagueService.getFantasyLeagues(authToken);
            setMyLeagues(leagues || []);
        } catch (error) {
            console.error("Error fetching leagues: ", error);
            setError(error);
        }
        setIsLoading(false);
    }, [authToken]);

    useEffect(() => {
        if (authToken && currentUser && currentUser.userId) {
            fetchMyLeagues();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser, fetchMyLeagues]);

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

    const displaySuccessMessage = (message) => {
		setSuccessMessage(message);
		setTimeout(() => {
			setSuccessMessage("");
		}, 3000); // Clear the message after 3 seconds
	};

    const displayErrorMessage = (message) => {
		setErrorMessage(message);
		setTimeout(() => {
			setErrorMessage("");
		}, 3000); // Clear the message after 3 seconds
	};

    async function handleCreateLeague(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        setErrorMessage("");
        try {
            await LeagueService.registerFantasyLeague(leagueName, leaguePassword, authToken);
            displaySuccessMessage("Successfully created fantasy league!");
            setLeagueName("");
            setLeaguePassword("");
            fetchCurrentLeagueId();
            fetchMyLeagues();
        } catch (error) {
            if (error.response && error.response.status === 409) {
                console.error("League name is already taken.", error)
                setError(error);
                displayErrorMessage("League name is already taken.");
            } else {
                console.error("Failed to create league. Please try again.", error);
                setError(error);
                displayErrorMessage("Failed to create league. Please try again.");
            }
        }
        setIsLoading(false);
    }

    async function handleSearchLeagues(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        setErrorMessage("");
        try {
            const results = await LeagueService.searchFantasyLeagues(searchQuery, authToken);
            const myLeagueIds = myLeagues.map((league) => league.fantasyLeagueId);
            const leaguesToJoin = results.filter(league => !myLeagueIds.includes(league.fantasyLeagueId));
            if (leaguesToJoin.length === 0) {
                displayErrorMessage("No leagues found or available to join under that name.");
                setSearchResults([]);
            } else {
                setSearchResults(leaguesToJoin);
            }
            
        } catch (error) {
            console.error("Error searching leagues: ", error);
            setError(error);
            displayErrorMessage("Failed to search for leagues.  Please try again.");
        }
        setIsLoading(false);
    }

    async function handleJoinLeague(e, leagueId) {
        e.preventDefault();
        setJoinLeagueId(leagueId);
        setJoinLeaguePassword("");
        setError(null);
    }

    async function submitJoinLeague(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        setErrorMessage("");
        try {
            await LeagueService.joinFantasyLeague(joinLeagueId, joinLeaguePassword, authToken);
            displaySuccessMessage("Successfully joined fantasy league!");
            setJoinLeagueId(null);
            setJoinLeaguePassword("");
            fetchCurrentLeagueId();
            fetchMyLeagues();
        } catch (error) {
            console.error("Error joining league: ", error);
            setError(error);
            displayErrorMessage("Failed to join league.  Please try again.");
        }
        setIsLoading(false);
    }

    async function handleSetCurrentLeague(e, leagueId) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        setErrorMessage("");
        try {
            await LeagueService.setCurrentLeague(leagueId, authToken);
            setCurrentLeagueId(leagueId);
            displaySuccessMessage("Successfully set current fantasy league!");
        } catch (error) {
            console.error("Error setting league: ", error);
            setError(error);
            displayErrorMessage("Failed to set current league.  Please try again.");
        }
        setIsLoading(false);
    }


    return (
        <div className="flex flex-col min-h-screen">
            <NavigationBar />
            <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                {errorMessage && (<div className="text-error">{errorMessage}</div>)}
                {successMessage && (<div className="text-success">{successMessage}</div>)}
            </div>
            
            <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                    <div className="mb-4 text-xl text-primary">
                        Create Fantasy League
                    </div>
                    <form onSubmit={handleCreateLeague}>
                        <label>League Name</label>
                        <input 
                            className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                            type="text" 
                            placeholder="Enter League Name" 
                            value={leagueName} 
                            onChange={(e) => setLeagueName(e.target.value)} />
                        <label>League Password</label>
                        <input 
                            className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                            type="password" 
                            placeholder="Enter League Password" 
                            value={leaguePassword} 
                            onChange={(e) => setLeaguePassword(e.target.value)} />
                        <button 
                            className="btn btn-primary btn-sm md:btn-md w-full mb-4" 
                            type="submit" 
                            disabled={isLoading}>{isLoading ? "Loading..." : "Create Fantasy League"}</button>
                    </form>
                </div>

                <div className="divider lg:divider-horizontal divider-vertical"></div>

                <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                    <div className="mb-4 text-xl text-primary">
                        Join Fantasy League
                    </div>
                    <form onSubmit={handleSearchLeagues}>
                        <label>Search Leagues</label>
                        <input 
                            className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                            type="text" 
                            placeholder="Enter League Name" 
                            onChange={(e) => setSearchQuery(e.target.value)} />
                        <button 
                            className="btn btn-primary btn-sm md:btn-md w-full mb-4" 
                            type="submit"
                            disabled={isLoading}>{isLoading ? "Loading..." : "Search Fantasy Leagues"}</button>
                    </form>
                    {searchResults.length > 0 && (
                        <div>
                            <div className="mb-4 text-success">
                                Search Results
                            </div>
                            <div className="overflow-auto">
                                <table className="table table-xs table-pin-rows mb-4">
                                    <thead>
                                        <tr className="bg-base-300">
                                            <th>Add</th>
                                            <th>League Name</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {searchResults.map((league) => (
                                            <tr key={league.fantasyLeagueId} className="bg-neutral hover:bg-info-content">
                                                <td>
                                                    <button 
                                                        className="btn btn-primary btn-outline btn-xs"
                                                        onClick={(e) => handleJoinLeague(e, league.fantasyLeagueId)}>Add</button>
                                                </td>
                                                <td>{league.leagueName}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                            {joinLeagueId && (
                                <div>
                                    <form onSubmit={submitJoinLeague}>
                                        <label>League Password</label>
                                        <input 
                                            className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                                            type="password" 
                                            placeholder="Enter League Password" 
                                            value={joinLeaguePassword} 
                                            onChange={(e) => setJoinLeaguePassword(e.target.value)} />
                                        {error && (<div className="text-error">{error.message}</div>)}
                                        <button 
                                            className="btn btn-primary btn-sm md:btn-md w-full mb-4" 
                                            type="submit" 
                                            disabled={isLoading}>{isLoading ? "Loading..." : "Join Fantasy League"}</button>
                                    </form>
                                </div>
                            )}
                        </div>
                    )}
                </div>

                <div className="divider lg:divider-horizontal divider-vertical"></div>

                <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                    <div className="mb-4 text-xl text-primary">
                        My Leagues
                    </div>
                    <div className="overflow-auto">
                        <table className="table table-xs table-pin-rows">
                            <thead>
                                <tr className="bg-base-300">
                                    <th>Set</th>
                                    <th>League Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                {myLeagues.map((league) => (
                                    <tr key={league.fantasyLeagueId} className={`bg-neutral hover:bg-info-content ${league.fantasyLeagueId === currentLeagueId ? 'text-primary' : ''}`}>
                                        <td>
                                            <button 
                                                className="btn btn-primary btn-outline btn-xs"
                                                onClick={(e) => handleSetCurrentLeague(e, league.fantasyLeagueId)} 
                                                disabled={league.fantasyLeagueId === currentLeagueId}>Set</button>
                                        </td>
                                        <td>{league.leagueName}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            
        </div>
    )
}

export default LeagueComponent;
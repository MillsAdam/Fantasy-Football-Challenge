import React, { useState, useContext, useEffect, useCallback } from "react";
import LeagueService from "../services/LeagueService";
import LineupService from "../services/LineupService";
import RosterService from "../services/RosterService";
import { AuthContext } from "../context/AuthContext";
import NavigationBar from "./NavigationBar";

function LeaderboardComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosters, setRosters] = useState([]);
    const [userHasTeam, setUserHasTeam] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [selectedUserId, setSelectedUserId] = useState(null);
    const [selectedTeamName, setSelectedTeamName] = useState(null);
    const [selectedWeeklyScore, setSelectedWeeklyScore] = useState(null);
    const [selectedTotalScore, setSelectedTotalScore] = useState(null);
    const [selectedWeek, setSelectedWeek] = useState(null);
    const [lineup, setLIneup] = useState([]);
    const [roster, setRoster] = useState([]);
    const [isRosterVisible, setIsRosterVisible] = useState(false);
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
                setRosters(rostersData);
            } else {
                setUserHasTeam(false);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
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

    const handleUserClick = async (userId, teamName, totalScore) => {
        if (selectedUserId === userId) {
            setSelectedUserId(null);
            setIsRosterVisible(false);
            setSelectedTeamName(null);
            setSelectedTotalScore(null);
            setSelectedWeek(null);
            setRoster([]);
            setLIneup([]);
            return;
        } else {
            setSelectedUserId(userId);
            setSelectedTeamName(teamName);
            setSelectedTotalScore(totalScore);
            setIsRosterVisible(false);
            setSelectedWeek(null);
            setRoster([]);
            setLIneup([]);
        }
    };

    const handleWeekSelection = async (week) => {
        if (selectedWeek === week) {
            setSelectedWeek(null);
            setSelectedWeeklyScore(null);
            setLIneup([]);
            return;
        } else {
            setSelectedWeek(week);
            await fetchUserLineup(selectedUserId, week);
        }
    };

    const handleRosterSelection = async() => {
        // might change this since roster.length can return a blank array
        if (roster.length !== 0) {
            setRoster([]);
            setIsRosterVisible(false)
            return;
        } else {
            await fetchUserRoster(selectedUserId);
            setIsRosterVisible(true);
        }
    };

    async function fetchUserLineup(userId, week) {
        setIsLoading(true);
        try {
            const lineupData = await LineupService.getLineupPlayersByUserIdAndWeek(userId, week);
            const userRoster = rosters.find(roster => roster.userId === userId);
            const weeklyScore = userRoster ? userRoster[`week${week}Score`] : 0;
            setLIneup(lineupData);
            setSelectedWeeklyScore(weeklyScore);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to fetch user lineup');
        }
        setIsLoading(false);
    }

    async function fetchUserRoster(userId) {
        setIsLoading(true);
        try {
            const rosterData = await RosterService.getRosterPlayersByUserId(userId);
            setRoster(rosterData);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to fetch user roster');
        }
        setIsLoading(false);
    }

    return (
        <div className="flex flex-col min-h-screen">
            <NavigationBar />
                <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                    <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                        {!currentLeagueId && userHasTeam === false && (
                            <div>
                                Join a League and create a Roster to view Leaderboard
                            </div>
                        )}
                        {currentLeagueId > 0 && userHasTeam === false && (
                            <div>
                                Create a Roster to view Leaderboard
                            </div>
                        )}
                        {userHasTeam && (
                            <div >
                                <div className="mb-4">
                                    <p className="text-xl text-primary">Leaderboard</p>
                                </div>
                                <div className="overflow-auto">
                                    <table className="table table-xs table-pin-rows">
                                        <thead>
                                            <tr className="bg-base-300">
                                                <th>Rank</th>
                                                <th>User</th>
                                                <th>Team</th>
                                                <th>W1</th>
                                                <th>W2</th>
                                                <th>W3</th>
                                                <th>W4</th>
                                                <th>Points</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {rosters.map((roster, index) => (
                                                <tr key={index} className="bg-neutral hover:bg-info-content" onClick={() => handleUserClick(roster.userId, roster.teamName, roster.totalScore)}>
                                                    <td>{index + 1}</td>
                                                    <td>{roster.username}</td>
                                                    <td>{roster.teamName}</td>
                                                    <td>{roster.week1Score}</td>
                                                    <td>{roster.week2Score}</td>
                                                    <td>{roster.week3Score}</td>
                                                    <td>{roster.week4Score}</td>
                                                    <td>{roster.totalScore}</td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </table>
                                </div>
                                
                            </div>
                        )}
                        {selectedUserId && (
                            <div>
                                <div className="flex my-4 items-center justify-evenly">
                                    <div>
                                        <p className="text-info">{selectedTeamName}</p>
                                    </div>
                                    <div className="flex flex-col">
                                        <div className="form-control">
                                            <label className="flex cursor-pointer label">
                                                <span className="label-text mr-2 md:mr-4">Roster</span>
                                                <input 
                                                    type="checkbox" 
                                                    className="toggle toggle-info ml-2 md:ml-4" 
                                                    checked={isRosterVisible} 
                                                    onChange={() => handleRosterSelection(selectedUserId)}
                                                    disabled={isLoading}
                                                />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                
                                {roster.length !== 0 && (
                                    <div className="mb-4">
                                        <div className="mb-4 flex items-center justify-center flex-row">
                                            <div className="mr-2">
                                                Total Score:
                                            </div>
                                            <div className="ml-2 text-success">
                                                {selectedTotalScore}
                                            </div>
                                        </div>
                                        <div className="overflow-auto">
                                            <table className="table table-xs table-pin-rows">
                                                <thead>
                                                    <tr className="bg-base-300">
                                                        <th>Conf</th>
                                                        <th>Team</th>
                                                        <th>Pos</th>
                                                        <th>Player</th>
                                                        <th>Points</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {roster.map((rosterPlayer, index) => (
                                                        <tr key={index} className="bg-neutral hover:bg-info-content">
                                                            <td>{rosterPlayer.conference}</td>
                                                            <td>{rosterPlayer.team}</td>
                                                            <td>{rosterPlayer.position}</td>
                                                            <td>{rosterPlayer.name}</td>
                                                            <td>{rosterPlayer.fantasyPoints}</td>
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                )}

                                <div role="tablist" className="tabs tabs-lifted mb-4">
                                    {[1, 2, 3, 4].map(week => (
                                        <button 
                                            role="tab" 
                                            className={`tab ${selectedWeek === week ? 'tab-active text-info font-bold' : ''}`}
                                            key={week} 
                                            onClick={() => handleWeekSelection(week)} 
                                            disabled={isLoading}
                                        >
                                            Week {week}
                                        </button>
                                    ))}
                                </div>
                                
                                {selectedWeek && (
                                    <div>
                                        <div className="mb-4 flex flex-row items-center justify-center">
                                            <div className="mr-2">
                                                Week {selectedWeek} Score:
                                            </div>
                                            <div className="ml-2 text-success">
                                                {selectedWeeklyScore}
                                            </div>
                                        </div>
                                        <div className="overflow-auto">
                                            <table className="table table-xs table-pin-rows">
                                                <thead>
                                                    <tr className="bg-base-300">
                                                        <th>Conf</th>
                                                        <th>Team</th>
                                                        <th>Pos</th>
                                                        <th>Player</th>
                                                        <th>Points</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {lineup.map((lineupPlayer, index) => (
                                                        <tr key={index} className="bg-neutral hover:bg-info-content">
                                                            <td>{lineupPlayer.conference}</td>
                                                            <td>{lineupPlayer.team}</td>
                                                            <td>{lineupPlayer.lineupPosition}</td>
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
                        )}
                    </div>
                </div>
            <div className="message-container">
                {error && <p>{error}</p>}
            </div>
        </div>
       
    )
}

export default LeaderboardComponent;

import React, { useState, useContext, useEffect } from "react";
import LeagueService from "../services/LeagueService";
import LineupService from "../services/LineupService";
import RosterService from "../services/RosterService";
import { AuthContext } from "../context/AuthContext";

function LeagueComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosters, setRosters] = useState([]);
    const [userHasTeam, setUserHasTeam] = useState(false);
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
    const [isLineupVisible, setIsLineupVisible] = useState(false);

    useEffect(() => {
        async function checkUserTeam() {
            setIsLoading(true);
            try {
                const rostersData = await LeagueService.getFantasyRosters();
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
        }

        if (currentUser && currentUser.userId) {
            checkUserTeam();
        } else {
            setError('User not found');
        }
    }, [authToken, currentUser]);

    const handleUserClick = async (userId, teamName, totalScore) => {
        if (selectedUserId === userId) {
            setSelectedUserId(null);
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
            fetchUserLineup(selectedUserId, week);
        }
    };

    const handleRosterSelection = async() => {
        if (roster.length !== 0) {
            setRoster([]);
            setIsRosterVisible(false)
            return;
        } else {
            fetUserRoster(selectedUserId);
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

    async function fetUserRoster(userId) {
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
        <div>
            {isLoading ? (<p>Loading...</p>) : (
                <div className="flex md:flex-row md:justify-between md:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                    <div className="flex-1 w-full p-4">
                        {!userHasTeam && (
                            <div>
                                Create a Roster to view Leaderboard
                            </div>
                        )}
                        {userHasTeam && (
                            <div >
                                <div className="mb-4">
                                    Leaderboard
                                </div>
                                <div className="overflow-auto">
                                    <table className="table table-xs table-pin-rows">
                                        <thead>
                                            <tr>
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
                                                <tr key={index} className="hover" onClick={() => handleUserClick(roster.userId, roster.teamName, roster.totalScore)}>
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
                                        <strong>{selectedTeamName}</strong>
                                    </div>
                                    <div className="flex flex-col">
                                        <div className="form-control">
                                            <label className="flex cursor-pointer label items-center justify-center">
                                                <span className="label-text">Roster</span>
                                                <input 
                                                    type="checkbox" 
                                                    className="toggle toggle-info" 
                                                    checked={isRosterVisible} 
                                                    onChange={() => handleRosterSelection(selectedUserId)}
                                                />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                
                                {roster.length !== 0 && (
                                    <div className="mb-4">
                                        <div className="mb-4">
                                            Total Score: <strong>{selectedTotalScore}</strong>
                                        </div>
                                        <div className="overflow-auto">
                                            <table className="table table-xs table-pin-rows">
                                                <thead>
                                                    <tr>
                                                        <th>Conf</th>
                                                        <th>Team</th>
                                                        <th>Pos</th>
                                                        <th>Player</th>
                                                        <th>Points</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {roster.map((rosterPlayer, index) => (
                                                        <tr key={index} className="hover">
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
                                
                                
                                <div className="flex flex-row justify-between align-center flex-nowrap">
                                    {[1, 2, 3, 4].map(week => (
                                        <button 
                                            className="btn btn-info btn-outline btn-xs sm:btn-sm my-4" 
                                            style={{ width: '21%' }}
                                            type="button" 
                                            key={week} 
                                            onClick={() => handleWeekSelection(week)}
                                        >
                                            Week {week}
                                        </button>
                                    ))}
                                </div>
                                
                                {selectedWeek && (
                                    <div>
                                        <div className="mb-4">
                                            Week {selectedWeek} Score: <strong>{selectedWeeklyScore}</strong>
                                        </div>
                                        <div className="overflow-auto">
                                            <table className="table table-xs table-pin-rows">
                                                <thead>
                                                    <tr>
                                                        <th>Conf</th>
                                                        <th>Team</th>
                                                        <th>Pos</th>
                                                        <th>Player</th>
                                                        <th>Points</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {lineup.map((lineupPlayer, index) => (
                                                        <tr key={index} className="hover">
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
                
            )}
            <div className="message-container">
                {error && <p>{error}</p>}
            </div>
        </div>
       
    )
}

export default LeagueComponent;

import React, { useState, useContext, useEffect } from "react";
import LeagueService from "../services/LeagueService";
import LineupService from "../services/LineupService";
import { AuthContext } from "../context/AuthContext";
import "../styles/LeagueComponent.css";

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
            setLIneup([]);
            return;
        } else {
            setSelectedUserId(userId);
            setSelectedTeamName(teamName);
            setSelectedTotalScore(totalScore);
            setSelectedWeek(null);
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

    return (
        <div>
            {isLoading ? (<p>Loading...</p>) : (
                <div className="page-container">
                    <div className="league-component-container">
                        {!userHasTeam && (
                            <div>
                                <h2>Create a Roster to view Leaderboard</h2>
                            </div>
                        )}
                        {userHasTeam && (
                            <div className="table-container">
                                <div>
                                    <h2>Leaderboard</h2>
                                </div>
                                <table>
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
                                            <tr key={index} onClick={() => handleUserClick(roster.userId, roster.teamName, roster.totalScore)}>
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
                        )}
                        {selectedUserId && (
                            <div>
                                <div>
                                    <h3>{selectedTeamName}</h3>
                                    <p>Total Score: {selectedTotalScore}</p>
                                </div>
                                {[1, 2, 3, 4].map(week => (
                                    <button className="league-button" key={week} type="button" onClick={() => handleWeekSelection(week)}>
                                        Week {week}
                                    </button>
                                ))}
                                {selectedWeek && (
                                    <div>
                                        <div>
                                            <p>Week {selectedWeek} Score: {selectedWeeklyScore}</p>
                                        </div>
                                        <div className="table-container">
                                            <table>
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
                                                        <tr key={index}>
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

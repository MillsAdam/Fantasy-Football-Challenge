import React, { useState, useContext, useEffect } from "react";
import LeagueService from "../services/LeagueService";
import { AuthContext } from "../context/AuthContext";
import "../styles/LeagueComponent.css";

function LeagueComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [rosters, setRosters] = useState([]);
    const [userHasTeam, setUserHasTeam] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

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

    return (
        <div>
            {isLoading ? (<p>Loading...</p>) : (
                <div className="component-container">
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
                                        <tr key={index}>
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
                </div>
            )}
            <div className="message-container">
                {error && <p>{error}</p>}
            </div>
        </div>
       
    )
}

export default LeagueComponent;

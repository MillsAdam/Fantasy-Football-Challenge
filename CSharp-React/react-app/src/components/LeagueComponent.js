import React, { useState, useContext, useEffect } from "react";
import LeagueService from "../services/LeagueService";
import { AuthContext } from "../context/AuthContext";
import "../styles/LeagueComponent.css";

function LeagueComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [teamName, setTeamName] = useState("");
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
    }, [currentUser]);

    async function createRoster(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const newRoster = await LeagueService.createRoster(teamName, authToken);
            if (newRoster) {
                const updatedRosters = await LeagueService.getFantasyRosters();
                setRosters(updatedRosters);
                setUserHasTeam(true);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create League Roster');
        }
        setIsLoading(false);
    }

    return (
        <div className="component-container">
            <h1>League Component</h1>
            {!userHasTeam && (
                <>
                    <form onSubmit={createRoster}>
                        <label htmlFor="teamName">Team Name</label>
                        <input type="text" id="teamName" name="teamName" value={teamName} onChange={(e) => setTeamName(e.target.value)} />
                        <button type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create League Roster"}</button>
                    </form>
                </>
            )}
            {userHasTeam && (
                <>
                    <div className="table-container">
                        <table>
                            <thead>
                                <tr>
                                    <th>User</th>
                                    <th>Team</th>
                                    <th>Points</th>
                                </tr>
                            </thead>
                            <tbody>
                                {rosters.map((roster, index) => (
                                    <tr key={index}>
                                        <td>{roster.username}</td>
                                        <td>{roster.teamName}</td>
                                        <td>{roster.totalScore}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </>
            )}
            {error && <p>{error}</p>}
        </div>
    )
}

export default LeagueComponent;

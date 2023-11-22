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

        checkUserTeam();
    }, [currentUser]);

    async function createRoster(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const data = await LeagueService.createRoster(teamName, authToken);
            setRosters([...rosters, data]);
            setUserHasTeam(true);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create League Roster');
        }
        setIsLoading(false);
    }

    return (
        <div>
            <h1>League Component</h1>
            {!userHasTeam && (
                <>
                    <form onSubmit={createRoster}>
                        <input
                            type="text"
                            value={teamName}
                            onChange={(e) => setTeamName(e.target.value)}
                        />
                        <button type="submit" disabled={isLoading}>
                            {isLoading ? "Loading..." : "Create League Roster"}
                        </button>
                    </form>
                </>
            )}
            {userHasTeam && (
                <>
                    <div className="table-container">
                        <table className="table">
                            <thead>
                                <tr>
                                    <th>Roster ID</th>
                                    <th>User ID</th>
                                    <th>Team Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                {rosters.map((roster, index) => (
                                    <tr key={index}>
                                        <td>{roster.fantasyRosterId}</td>
                                        <td>{roster.userId}</td>
                                        <td>{roster.teamName}</td>
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

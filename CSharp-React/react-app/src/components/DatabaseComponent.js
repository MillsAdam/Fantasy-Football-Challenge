import React, { useState, useContext, useEffect, useCallback } from "react";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import "../styles/DatabaseComponent.css";

const configValueOptions = {
    'current_week': [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
    'current_lineup_week': [1, 2, 3, 4],
    'lock_rosters': [1, 2],
    'lock_lineups': [1, 2],
    'lineup_week_one': [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
    'lineup_week_two': [1, 2, 3, 4, 5, 6, 7, 8, 9 , 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
    'lineup_week_three': [1, 2, 3, 4, 5, 6, 7, 8, 9 , 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
    'lineup_week_four': [1, 2, 3, 4, 5, 6, 7, 8, 9 , 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22],
};
const configKeyDisplayNames = {
    'current_week': 'Current Week',
    'current_lineup_week': 'Current Lineup Week',
    'lock_rosters': 'Lock Rosters',
    'lock_lineups': 'Lock Lineups',
    'lineup_week_one': 'Lineup Week One',
    'lineup_week_two': 'Lineup Week Two',
    'lineup_week_three': 'Lineup Week Three',
    'lineup_week_four': 'Lineup Week Four',
};
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

function DatabaseComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [dynamicConfigKeyOptions, setDynamicConfigKeyOptions] = useState([]);
    const [selectedConfigKey, setSelectedConfigKey] = useState("");
    const [selectedConfigValue, setSelectedConfigValue] = useState("");
    const [configurations, setConfigurations] = useState([]);
    const [dynamicTeamNameOptions, setDynamicTeamNameOptions] = useState([]);
    const [selectedTeamName, setSelectedTeamName] = useState("");
    const [teams, setTeams] = useState([]);
    const [successMessage, setSuccessMessage] = useState("");
    const [loadingMessage, setLoadingMessage] = useState("");
    const [isTeamsTableVisible, setIsTeamsTableVisible] = useState(false);
    const [isConfigTableVisible, setIsConfigTableVisible] = useState(false);
    
    const displaySuccessMessage = (message) => {
        setSuccessMessage(message);
        setTimeout(() => {
            setSuccessMessage("");
        }, 3000); // Clear the message after 5 seconds
    }

    const toggleTeamsTableVisibility = () => {
        setIsTeamsTableVisible(!isTeamsTableVisible);
    }

    const toggleConfigTableVisibility = () => {
        setIsConfigTableVisible(!isConfigTableVisible);
    }

    async function createTeams(e) {
        e.preventDefault();
        setLoadingMessage("Creating Teams...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newTeams = await DatabaseService.createTeams();
                if (newTeams) {
                    displaySuccessMessage("Teams created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create teams');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function createPlayers(e) {
        e.preventDefault();
        setLoadingMessage("Creating Players...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayers = await DatabaseService.createPlayers();
                if (newPlayers) {
                    displaySuccessMessage("Players created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create players');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function upsertPlayers(e) {
        e.preventDefault();
        setLoadingMessage("Upserting Players...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedPlayers = await DatabaseService.upsertPlayers();
                if (updatedPlayers) {
                    displaySuccessMessage("Players updated successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update players');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function createPlayerStats(e) {
        e.preventDefault();
        setLoadingMessage("Creating Player Stats...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerStats = await DatabaseService.createPlayerStats();
                const newPlayerStatsExt = await DatabaseService.createPlayerStatsExt();
                if (newPlayerStats && newPlayerStatsExt) {
                    displaySuccessMessage("Player stats created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player stats');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function upsertPlayerStatsByWeek(e) {
        e.preventDefault();
        setLoadingMessage("Upserting Player Stats By Week...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerStatsByWeek = await DatabaseService.upsertPlayerStatsByWeek();
                const newPlayerStatsByWeekExt = await DatabaseService.upsertPlayerStatsExtByWeek();
                if (newPlayerStatsByWeek && newPlayerStatsByWeekExt) {
                    displaySuccessMessage("Player stats by week created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player stats by week');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function createPlayerProjections(e) {
        e.preventDefault();
        setLoadingMessage("Creating Player Projections...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerProjections = await DatabaseService.createPlayerProjections();
                const newPlayerProjectionsExt = await DatabaseService.createPlayerProjectionsExt();
                if (newPlayerProjections && newPlayerProjectionsExt) {
                    displaySuccessMessage("Player projections created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player projections');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function upsertPlayerProjectionsByWeek(e) {
        e.preventDefault();
        setLoadingMessage("Upserting Player Projections...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedPlayerProjections = await DatabaseService.upsertPlayerProjectionsByWeek();
                const updatedPlayerProjectionsExt = await DatabaseService.upsertPlayerProjectionsExtByWeek();
                if (updatedPlayerProjections && updatedPlayerProjectionsExt) {
                    displaySuccessMessage("Player projections updated successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update player projections');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function updateLineupScores(e) {
        e.preventDefault();
        setLoadingMessage("Updating Lineup Scores...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedLineupScores = await DatabaseService.updateLineupScores();
                if (updatedLineupScores) {
                    displaySuccessMessage("Lineup scores updated successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update lineup scores');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function updateRosterScores(e) {
        e.preventDefault();
        setLoadingMessage("Updating Roster Scores...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedRosterScores = await DatabaseService.updateRosterScores();
                if (updatedRosterScores) {
                    displaySuccessMessage("Roster scores updated successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update roster scores');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function updateConfiguration(e) {
        e.preventDefault();
        setLoadingMessage("Updating Configuration...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const configuration = {
                    ConfigKey: selectedConfigKey,
                    ConfigValue: parseInt(selectedConfigValue, 10)
                };
                const updatedConfiguration = await DatabaseService.updateConfiguration(configuration);
                if (updatedConfiguration) {
                    await getConfiguration();
                    displaySuccessMessage("Configuration updated successfully")
                    setSelectedConfigKey("");
                    setSelectedConfigValue("");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update configuration');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    const getConfiguration = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const fetchedConfiguration = await DatabaseService.getConfiguration();
                if (fetchedConfiguration) {
                    const sortedConfigurations = fetchedConfiguration.sort((a, b) => {
                        return a.configKey.localeCompare(b.configKey);
                    });
                    const sortedConfigKeys = fetchedConfiguration.map(config => config.configKey).sort();
                    setConfigurations(sortedConfigurations);
                    setDynamicConfigKeyOptions(sortedConfigKeys);
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to retrieve configuration');
        }
        setIsLoading(false);
    }, [authToken, currentUser.role]);

    

    async function ToggleTeamStatus(e) {
        e.preventDefault();
        setLoadingMessage(`Updating ${selectedTeamName} Status...`);
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedTeam = await DatabaseService.ToggleTeamStatus(selectedTeamName);
                if (updatedTeam) {
                    await getTeams();
                    displaySuccessMessage(`${selectedTeamName} status updated successfully`)
                    setSelectedTeamName("");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError(`Failed to toggle ${selectedTeamName} status`);
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    const getTeams = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const fetchedTeams = await DatabaseService.getTeams();
                if (fetchedTeams) {
                    const sortedTeams = fetchedTeams.sort((a, b) => {
                        return a.team.localeCompare(b.team);
                    });
                    const sortedTeamNames = fetchedTeams.map(team => team.team).sort();
                    setTeams(sortedTeams);
                    setDynamicTeamNameOptions(sortedTeamNames);
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to retrieve teams');
        }
        setIsLoading(false);
    }, [authToken, currentUser.role]);


    useEffect(() => {
        getConfiguration();
        getTeams();
    }, [getConfiguration, getTeams]);


    return (
        <div>
            <div className="message-container">
                {loadingMessage && <p>{loadingMessage}</p>}
                {successMessage && <p>{successMessage}</p>}
                {error && <p>{error}</p>}
            </div>
            <div className="page-container">
                <div className="component-container">
                    <h3>Teams</h3>
                    <form onSubmit={createTeams}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Creating Teams..." ? "Loading..." : "Create Teams"}
                        </button>
                    </form>
                </div>
                <div className="component-container">
                    <h3>Players</h3>
                    <form onSubmit={createPlayers}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Creating Players..." ? "Loading..." : "Create Players"}
                        </button>
                    </form>
                    <form onSubmit={upsertPlayers}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Upserting Players..." ? "Loading..." : "Upsert Players"}
                        </button>
                    </form>
                </div>
                <div className="component-container">
                    <h3>Player Stats</h3>
                    <form onSubmit={createPlayerStats}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Creating Player Stats..." ? "Loading..." : "Create Player Stats"}
                        </button>
                    </form>
                    <form onSubmit={upsertPlayerStatsByWeek}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Upserting Player Stats By Week..." ? "Loading..." : "Upsert Player Stats By Week"}
                        </button>
                    </form>
                </div>
                <div className="component-container">
                    <h3>Player Projections</h3>
                    <form onSubmit={createPlayerProjections}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Creating Player Projections..." ? "Loading..." : "Create Player Projections"}
                        </button>
                    </form>
                    <form onSubmit={upsertPlayerProjectionsByWeek}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Upserting Player Projections By Week..." ? "Loading..." : "Upsert Player Projections By Week"}
                        </button>
                    </form>
                </div>
                <div className="component-container">
                    <h3>Scores</h3>
                    <form onSubmit={updateLineupScores}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Updating Lineup Scores..." ? "Loading..." : "Update Lineup Scores"}
                        </button>
                    </form>
                    <form onSubmit={updateRosterScores}>
                        <button className="database-button" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Updating Roster Scores..." ? "Loading..." : "Update Roster Scores"}
                        </button>
                    </form>
                </div>
                <div className="component-container">
                    <h3>Configuration</h3>
                    <form onSubmit={updateConfiguration}>
                        <select 
                            value={selectedConfigKey} 
                            className="database-key-select"
                            onChange={(e) => {
                                setSelectedConfigKey(e.target.value);
                                setSelectedConfigValue("");
                            }}
                        >
                            <option value="" disabled hidden>Select Config Key</option>
                            {dynamicConfigKeyOptions.map(key => (
                                <option key={key} value={key}>{configKeyDisplayNames[key] || key}</option>
                            ))}
                        </select>
                        <select 
                            value={selectedConfigValue} 
                            className="database-value-select"
                            onChange={(e) => setSelectedConfigValue(e.target.value)} 
                            disabled={!selectedConfigKey}
                        >
                            <option value="" disabled hidden>Select Config Value</option>
                            {(configValueOptions[selectedConfigKey] || []).map(value => (
                                <option key={value} value={value}>{value}</option>
                            ))}
                        </select>
                        <button className="database-button" type="submit" disabled={isLoading || !selectedConfigKey || !selectedConfigValue}>
                            {isLoading && loadingMessage === "Updating Configuration..." ? "Loading..." : "Update Configuration"}
                        </button>
                    </form>
                    <button className="database-button" onClick={toggleConfigTableVisibility}>
                        {isConfigTableVisible ? "Hide Configuration" : "Show Configuration"}
                    </button>
                    {isConfigTableVisible && (
                        <div className="table-container">
                            <table>
                                <tbody>
                                    {configurations.map((config, index) => (
                                        <tr key={index}>
                                            <td>{configKeyDisplayNames[config.configKey] || config.configKey}</td>
                                            <td>{config.configValue}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>
                <div className="component-container">
                    <h3>Team Status</h3>
                    <form onSubmit={ToggleTeamStatus}>
                        <select 
                            value={selectedTeamName} 
                            className="database-team-select" 
                            onChange={(e) => setSelectedTeamName(e.target.value)}
                        >
                            <option value="" disabled hidden>Select a Team</option>
                            {dynamicTeamNameOptions.map(teamName => (
                                <option key={teamName} value={teamName}>{teamNameDisplayNames[teamName] || teamName}</option>
                            ))}
                        </select>
                        <button className="database-button" type="submit" disabled={isLoading || selectedTeamName === ""}>
                            {isLoading && loadingMessage === `Updating ${selectedTeamName} Status...` ? "Loading..." : `Update ${selectedTeamName} Status`}
                        </button>
                    </form>
                    <button className="database-button" onClick={toggleTeamsTableVisibility}>
                        {isTeamsTableVisible ? "Hide Teams" : "Show Teams"}
                    </button>
                    {isTeamsTableVisible && (
                        <div className="table-container">
                            <table>
                                <tbody>
                                    {teams.map((team, index) => (
                                        <tr key={index}>
                                            <td>{teamNameDisplayNames[team.team] || team.team}</td>
                                            <td className={
                                                team.status === 'Active' ? 'green-highlight' :
                                                team.status === 'Inactive' ? 'red-highlight' : ''
                                            }>
                                                {team.status}
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )

}

export default DatabaseComponent;
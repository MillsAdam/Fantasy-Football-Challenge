import React, { useState, useContext, useEffect, useCallback } from "react";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import { useConfig } from "../context/ConfigContext";
import "../styles/AdminComponent.css";
import { orderedConfigKeys, configDisplayOrder, configKeyDisplayNames, configValueOptions, teamNameDisplayNames } from "../constants/AdminConstants";


function AdminComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [selectedConfigKey, setSelectedConfigKey] = useState("");
    const [selectedConfigValue, setSelectedConfigValue] = useState("");
    const { configurations, updateConfigurations } = useConfig();
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
        }, 3000); // Clear the message after 3 seconds
    }

    const toggleTeamsTableVisibility = () => {
        setIsTeamsTableVisible(!isTeamsTableVisible);
    }

    const toggleConfigTableVisibility = () => {
        setIsConfigTableVisible(!isConfigTableVisible);
    }

    async function createTeamsAndPlayers(e) {
        e.preventDefault();
        setLoadingMessage("Creating Teams and Players...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newTeams = await DatabaseService.createTeams();
                if (newTeams) {
                    displaySuccessMessage("Teams created successfully")

                    const newPlayers = await DatabaseService.createPlayers();
                    if (newPlayers) {
                        displaySuccessMessage("Players created successfully")
                    }
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create teams and players');
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

    async function createPlayerStatsAndProjections(e) {
        e.preventDefault();
        setLoadingMessage("Creating Player Stats and Projections...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerStats = await DatabaseService.createPlayerStats();
                const newPlayerStatsExt = await DatabaseService.createPlayerStatsExt();
                const newPlayerProjections = await DatabaseService.createPlayerProjections();
                const newPlayerProjectionsExt = await DatabaseService.createPlayerProjectionsExt();
                if (newPlayerStats && newPlayerStatsExt && newPlayerProjections && newPlayerProjectionsExt) {
                    displaySuccessMessage("Player stats and projections created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player stats');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function upsertPlayerStatsAndProjectionsByWeek(e) {
        e.preventDefault();
        setLoadingMessage("Upserting Player Stats and Projections By Week...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerStatsByWeek = await DatabaseService.upsertPlayerStatsByWeek();
                const newPlayerStatsByWeekExt = await DatabaseService.upsertPlayerStatsExtByWeek();
                const newPlayerProjectionsByWeek = await DatabaseService.upsertPlayerProjectionsByWeek();
                const newPlayerProjectionsByWeekExt = await DatabaseService.upsertPlayerProjectionsExtByWeek();
                if (newPlayerStatsByWeek && newPlayerStatsByWeekExt && newPlayerProjectionsByWeek && newPlayerProjectionsByWeekExt) {
                    displaySuccessMessage("Player stats and projections by week created successfully")
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player stats by week');
        }
        setIsLoading(false);
        setLoadingMessage("");
    }

    async function updateLineupAndRosterScores(e) {
        e.preventDefault();
        setLoadingMessage("Updating Lineup and Roster Scores...");
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedLineupScores = await DatabaseService.updateLineupScores();
                if (updatedLineupScores) {
                    displaySuccessMessage("Lineup scores updated successfully")

                    const updatedRosterScores = await DatabaseService.updateRosterScores();
                    if (updatedRosterScores) {
                        displaySuccessMessage("Roster scores updated successfully")
                    }
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update lineup scores');
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

                if (selectedConfigKey === 'startingLineupWeek') {
                    const lineupWeek1Config = {
                        ConfigKey: 'lineupWeek1',
                        ConfigValue: parseInt(selectedConfigValue, 10)
                    };
                    const lineupWeek2Config = {
                        ConfigKey: 'lineupWeek2',
                        ConfigValue: parseInt(selectedConfigValue, 10) + 1
                    };
                    const lineupWeek3Config = {
                        ConfigKey: 'lineupWeek3',
                        ConfigValue: parseInt(selectedConfigValue, 10) + 2
                    };
                    const lineupWeek4Config = {
                        ConfigKey: 'lineupWeek4',
                        ConfigValue: parseInt(selectedConfigValue, 10) + 3
                    };
                    await DatabaseService.updateConfiguration(lineupWeek1Config);
                    await DatabaseService.updateConfiguration(lineupWeek2Config);
                    await DatabaseService.updateConfiguration(lineupWeek3Config);
                    await DatabaseService.updateConfiguration(lineupWeek4Config);
                }

                if (updatedConfiguration) {
                    const fetchedConfigurations = await DatabaseService.getConfiguration();
                    updateConfigurations(fetchedConfigurations);
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
                const sortedConfigurations = configurations.sort((a, b) => a.configKey.localeCompare(b.configKey));
                updateConfigurations(sortedConfigurations);
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to retrieve configuration');
        }
        setIsLoading(false);
    }, [authToken, currentUser.role, configurations, updateConfigurations]);

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
                    <h3>Teams / Players / Scores</h3>
                    <form onSubmit={createTeamsAndPlayers}>
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Creating Teams and Players..." ? "Loading..." : "Create Teams / Players"}
                        </button>
                    </form>
                    <form onSubmit={upsertPlayers}>
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Upserting Players..." ? "Loading..." : "Upsert Players"}
                        </button>
                    </form>
                    <h3>Player Stats / Projections</h3>
                    <form onSubmit={createPlayerStatsAndProjections}>
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading}>
                            {isLoading && loadingMessage === "Creating Player Stats and Projections..." ? "Loading..." : "Create Player Stats / Projections"}
                        </button>
                    </form>
                    <form onSubmit={upsertPlayerStatsAndProjectionsByWeek}>
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading}>
                            {isLoading && 
                                loadingMessage === "Upserting Player Stats and Projections By Week..." ? 
                                "Loading..." : 
                                `Upsert Player Stats / Projections for Current Week ${configurations.find(config => config.configKey === "currentWeek")?.configValue}`}
                        </button>
                    </form>
                    <h3>Lineup / Roster Scores</h3>
                    <form onSubmit={updateLineupAndRosterScores}>
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading}>
                            {isLoading && 
                                loadingMessage === "Updating Lineup and Roster Scores..." ? 
                                    "Loading..." : 
                                    `Update Lineup / Roster Scores for Lineup Week ${configurations.find(config => config.configKey === "currentLineupWeek")?.configValue}`}
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
                            {[...orderedConfigKeys.keys()].map(key => (
                                <option key={key} value={key}>{orderedConfigKeys.get(key)}</option>
                            ))}
                        </select>
                        <select 
                            value={selectedConfigValue} 
                            className="database-value-select"
                            onChange={(e) => setSelectedConfigValue(e.target.value)} 
                            disabled={!selectedConfigKey}
                        >
                            <option value="" disabled hidden>Select Config Value</option>
                            {
                                configValueOptions[selectedConfigKey] && 
                                (typeof configValueOptions[selectedConfigKey] === 'object' && !Array.isArray(configValueOptions[selectedConfigKey]) 
                                    ? Object.entries(configValueOptions[selectedConfigKey]).map(([value, displayText]) => (
                                        <option key={value} value={value}>{displayText}</option>
                                    ))
                                    : Array.isArray(configValueOptions[selectedConfigKey]) && 
                                    configValueOptions[selectedConfigKey].map(value => (
                                        <option key={value} value={value}>{value}</option>
                                    ))
                                )
                            }
                        </select>
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading || !selectedConfigKey || !selectedConfigValue}>
                            {isLoading && loadingMessage === "Updating Configuration..." ? "Loading..." : "Update Configuration"}
                        </button>
                    </form>
                    <button className="btn btn-neutral sm: btn-sm" onClick={toggleConfigTableVisibility}>
                        {isConfigTableVisible ? "Hide Configuration" : "Show Configuration"}
                    </button>
                    {isConfigTableVisible && (
                        <div className="table-container">
                            <table>
                                <tbody>
                                    {configurations
                                        .sort((a,b) => configDisplayOrder.indexOf(a.configKey) - configDisplayOrder.indexOf(b.configKey))
                                        .map((config, index) => (
                                        <tr key={index}>
                                            <td>{configKeyDisplayNames[config.configKey] || config.configKey}</td>
                                            <td>
                                                {config.configKey === 'lockRosters' || config.configKey === 'lockLineups' || config.configKey === 'currentWeek' || config.configKey === 'startingLineupWeek' 
                                                    ? configValueOptions[config.configKey][config.configValue] 
                                                    : config.configValue}
                                            </td>
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
                        <button className="btn btn-neutral sm: btn-sm" type="submit" disabled={isLoading || selectedTeamName === ""}>
                            {isLoading && loadingMessage === `Updating ${selectedTeamName} Status...` ? "Loading..." : `Update ${selectedTeamName} Status`}
                        </button>
                    </form>
                    <button className="btn btn-neutral sm: btn-sm" onClick={toggleTeamsTableVisibility}>
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

export default AdminComponent;
import React, { useState, useContext, useEffect, useCallback } from "react";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import "../styles/DatabaseComponent.css";

const configKeyOptions = ['current_week', 'current_season_type', 'current_lineup_week'];
const configValueOptions = {
    'current_week': [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18],
    'current_season_type': [1, 3],
    'current_lineup_week': [1, 2, 3, 4]
};
const configKeyDisplayNames = {
    'current_week': 'Current Week',
    'current_season_type': 'Current Season Type',
    'current_lineup_week': 'Current Lineup Week'
};

function DatabaseComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [selectedConfigKey, setSelectedConfigKey] = useState(configKeyOptions[0]);
    const [selectedConfigValue, setSelectedConfigValue] = useState(configValueOptions[configKeyOptions[0]][0]);
    const [configurations, setConfigurations] = useState([]);

    useEffect(() => {
        getConfiguration();
    }, []);

    async function createTeams(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newTeams = await DatabaseService.createTeams();
                if (newTeams) {
                    console.log("Teams created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create teams');
        }
        setIsLoading(false);
    }

    async function createPlayers(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayers = await DatabaseService.createPlayers();
                if (newPlayers) {
                    console.log("Players created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create players');
        }
        setIsLoading(false);
    }

    async function updatePlayers(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedPlayers = await DatabaseService.updatePlayers();
                if (updatedPlayers) {
                    console.log("Players updated");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update players');
        }
        setIsLoading(false);
    }

    async function createPlayerStats(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerStats = await DatabaseService.createPlayerStats();
                if (newPlayerStats) {
                    console.log("Player stats created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player stats');
        }
        setIsLoading(false);
    }

    async function createPlayerProjections(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const newPlayerProjections = await DatabaseService.createPlayerProjections();
                if (newPlayerProjections) {
                    console.log("Player projections created");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to create player projections');
        }
        setIsLoading(false);
    }

    async function updateLineupScores(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedLineupScores = await DatabaseService.updateLineupScores();
                if (updatedLineupScores) {
                    console.log("Lineup scores updated");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update lineup scores');
        }
        setIsLoading(false);
    }

    async function updateRosterScores(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const updatedRosterScores = await DatabaseService.updateRosterScores();
                if (updatedRosterScores) {
                    console.log("Roster scores updated");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update roster scores');
        }
        setIsLoading(false);
    }

    async function updateConfiguration(e) {
        e.preventDefault();
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
                    console.log("Configuration updated");
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to update configuration');
        }
        setIsLoading(false);
    }

    const getConfiguration = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        try {
            if (authToken && currentUser.role === 'admin') {
                const fetchedConfiguration = await DatabaseService.getConfiguration();
                if (fetchedConfiguration) {
                    const sortedConfigurations = configKeyOptions.map(key => fetchedConfiguration.find(config => config.configKey === key));
                    setConfigurations(sortedConfigurations);
                }
            }
        } catch (error) {
            console.error('An error occurred: ', error);
            setError('Failed to retrieve configuration');
        }
        setIsLoading(false);
    }, [authToken, currentUser.role]);

    useEffect(() => {
        getConfiguration();
    }, [getConfiguration]);

    return (
        <div>
            <div className="database-container">
                <div className="form-container">
                    <h3>Teams</h3>
                    <form onSubmit={createTeams}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Teams"}</button>
                    </form>
                </div>
                <div className="form-container">
                    <h3>Players</h3>
                    <form onSubmit={createPlayers}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Players"}</button>
                    </form>
                    <form onSubmit={updatePlayers}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Update Players"}</button>
                    </form>
                </div>
                <div className="form-container">
                    <h3>Player Stats</h3>
                    <form onSubmit={createPlayerStats}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Player Stats"}</button>
                    </form>
                </div>
                <div className="form-container">
                    <h3>Player Projections</h3>
                    <form onSubmit={createPlayerProjections}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Create Player Projections"}</button>
                    </form>
                </div>
                <div className="form-container">
                    <h3>Scores</h3>
                    <form onSubmit={updateLineupScores}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Update Lineup Scores"}</button>
                    </form>
                    <form onSubmit={updateRosterScores}>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Update Roster Scores"}</button>
                    </form>
                </div>
                <div className="form-container">
                    <h3>Configuration</h3>
                    <form onSubmit={updateConfiguration}>
                        <select 
                            value={selectedConfigKey} 
                            className="database-select"
                            onChange={(e) => {
                                setSelectedConfigKey(e.target.value);
                                setSelectedConfigValue(configValueOptions[e.target.value][0]);
                            }}
                        >
                            {configKeyOptions.map(key => (
                                <option key={key} value={key}>{configKeyDisplayNames[key]}</option>
                            ))}
                        </select>
                        <select 
                            value={selectedConfigValue} 
                            className="database-select"
                            onChange={(e) => setSelectedConfigValue(e.target.value)} 
                        >
                            {configValueOptions[selectedConfigKey].map(value => (
                                <option key={value} value={value}>{value}</option>
                            ))}
                        </select>
                        <button className="database-button" type="submit" disabled={isLoading}>{isLoading ? "Loading..." : "Update Configuration"}</button>
                    </form>
                    <div className="configuration-table">
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
                </div>
            </div>
            <div className="error-container">
                {error && <p>{error}</p>}
            </div>
        </div>
    )

}

export default DatabaseComponent;
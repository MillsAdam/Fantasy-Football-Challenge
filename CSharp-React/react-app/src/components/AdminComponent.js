import React, { useState, useContext, useEffect, useCallback } from "react";
import { AuthContext } from "../context/AuthContext";
import DatabaseService from "../services/DatabaseService";
import { useConfig } from "../context/ConfigContext";
import { orderedConfigKeys, configDisplayOrder, configKeyDisplayNames, configValueOptions, teamNameDisplayNames } from "../constants/AdminConstants";
import NavigationBar from "./NavigationBar";

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
	const [selectedAction, setSelectedAction] = useState("");

	const displaySuccessMessage = (message) => {
		setSuccessMessage(message);
		setTimeout(() => {
			setSuccessMessage("");
		}, 3000); // Clear the message after 3 seconds
	};

	const toggleTeamsTableVisibility = () => {
		setIsTeamsTableVisible(!isTeamsTableVisible);
	};

	const toggleConfigTableVisibility = () => {
		setIsConfigTableVisible(!isConfigTableVisible);
	};

	async function createTeamsAndPlayers(e) {
		e.preventDefault();
		setLoadingMessage("Creating Teams and Players...");
		setIsLoading(true);
		setError(null);
		try {
			if (authToken && currentUser.role === "admin") {
				const newTeams = await DatabaseService.createTeams();
				if (newTeams) {
					displaySuccessMessage("Teams created successfully");

					const newPlayers = await DatabaseService.createPlayers();
					if (newPlayers) {
						displaySuccessMessage("Players created successfully");
					}
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to create teams and players");
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
			if (authToken && currentUser.role === "admin") {
				const updatedPlayers = await DatabaseService.upsertPlayers();
				if (updatedPlayers) {
					displaySuccessMessage("Players updated successfully");
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to update players");
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
			if (authToken && currentUser.role === "admin") {
				const newPlayerStats = await DatabaseService.createPlayerStats();
				const newPlayerStatsExt = await DatabaseService.createPlayerStatsExt();
				const newPlayerProjections = await DatabaseService.createPlayerProjections();
				const newPlayerProjectionsExt = await DatabaseService.createPlayerProjectionsExt();
				if (newPlayerStats && newPlayerStatsExt && newPlayerProjections && newPlayerProjectionsExt) {
					displaySuccessMessage("Player stats and projections created successfully");
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to create player stats");
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
			if (authToken && currentUser.role === "admin") {
				const newPlayerStatsByWeek = await DatabaseService.upsertPlayerStatsByWeek();
				const newPlayerStatsByWeekExt = await DatabaseService.upsertPlayerStatsExtByWeek();
				const newPlayerProjectionsByWeek = await DatabaseService.upsertPlayerProjectionsByWeek();
				const newPlayerProjectionsByWeekExt = await DatabaseService.upsertPlayerProjectionsExtByWeek();
				if (newPlayerStatsByWeek && newPlayerStatsByWeekExt && newPlayerProjectionsByWeek && newPlayerProjectionsByWeekExt) {
					displaySuccessMessage("Player stats and projections by week created successfully");
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to create player stats by week");
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
			if (authToken && currentUser.role === "admin") {
				const updatedLineupScores = await DatabaseService.updateLineupScores();
				if (updatedLineupScores) {
					displaySuccessMessage("Lineup scores updated successfully");

					const updatedRosterScores = await DatabaseService.updateRosterScores();
					if (updatedRosterScores) {
						displaySuccessMessage("Roster scores updated successfully");
					}
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to update lineup scores");
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
			if (authToken && currentUser.role === "admin") {
				const configuration = {
					ConfigKey: selectedConfigKey,
					ConfigValue: parseInt(selectedConfigValue, 10),
				};
				const updatedConfiguration = await DatabaseService.updateConfiguration(configuration);

				if (selectedConfigKey === "startingLineupWeek") {
					const lineupWeek1Config = {
						ConfigKey: "lineupWeek1",
						ConfigValue: parseInt(selectedConfigValue, 10),
					};
					const lineupWeek2Config = {
						ConfigKey: "lineupWeek2",
						ConfigValue: parseInt(selectedConfigValue, 10) + 1,
					};
					const lineupWeek3Config = {
						ConfigKey: "lineupWeek3",
						ConfigValue: parseInt(selectedConfigValue, 10) + 2,
					};
					const lineupWeek4Config = {
						ConfigKey: "lineupWeek4",
						ConfigValue: parseInt(selectedConfigValue, 10) + 3,
					};
					await DatabaseService.updateConfiguration(lineupWeek1Config);
					await DatabaseService.updateConfiguration(lineupWeek2Config);
					await DatabaseService.updateConfiguration(lineupWeek3Config);
					await DatabaseService.updateConfiguration(lineupWeek4Config);
				}

				if (updatedConfiguration) {
					const fetchedConfigurations = await DatabaseService.getConfiguration();
					updateConfigurations(fetchedConfigurations);
					displaySuccessMessage("Configuration updated successfully");
					setSelectedConfigKey("");
					setSelectedConfigValue("");
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to update configuration");
		}
		setIsLoading(false);
		setLoadingMessage("");
	}

	const getConfiguration = useCallback(async () => {
		setIsLoading(true);
		setError(null);
		try {
			if (authToken && currentUser.role === "admin") {
				const sortedConfigurations = configurations.sort((a, b) => a.configKey.localeCompare(b.configKey));
				updateConfigurations(sortedConfigurations);
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to retrieve configuration");
		}
		setIsLoading(false);
	}, [authToken, currentUser.role, configurations, updateConfigurations]);

	async function ToggleTeamStatus(e) {
		e.preventDefault();
		setLoadingMessage(`Updating ${selectedTeamName} Status...`);
		setIsLoading(true);
		setError(null);
		try {
			if (authToken && currentUser.role === "admin") {
				const updatedTeam = await DatabaseService.ToggleTeamStatus(selectedTeamName);
				if (updatedTeam) {
					await getTeams();
					displaySuccessMessage(`${selectedTeamName} status updated successfully`);
					setSelectedTeamName("");
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError(`Failed to toggle ${selectedTeamName} status`);
		}
		setIsLoading(false);
		setLoadingMessage("");
	}

	const getTeams = useCallback(async () => {
		setIsLoading(true);
		setError(null);
		try {
			if (authToken && currentUser.role === "admin") {
				const fetchedTeams = await DatabaseService.getTeams();
				if (fetchedTeams) {
					const sortedTeams = fetchedTeams.sort((a, b) => {
						return a.team.localeCompare(b.team);
					});
					const sortedTeamNames = fetchedTeams.map((team) => team.team).sort();
					setTeams(sortedTeams);
					setDynamicTeamNameOptions(sortedTeamNames);
				}
			}
		} catch (error) {
			console.error("An error occurred: ", error);
			setError("Failed to retrieve teams");
		}
		setIsLoading(false);
	}, [authToken, currentUser.role]);

	useEffect(() => {
		getConfiguration();
		getTeams();
	}, [getConfiguration, getTeams]);

	const handleApply = (e) => {
		e.preventDefault();
		switch (selectedAction) {
			case "createTeamsAndPlayers":
				createTeamsAndPlayers(e);
				break;
			case "upsertPlayers":
				upsertPlayers(e);
				setSelectedAction("");
				break;
			case "createPlayerStatsAndProjections":
				createPlayerStatsAndProjections(e);
				break;
			case "upsertPlayerStatsAndProjectionsByWeek":
				upsertPlayerStatsAndProjectionsByWeek(e);
				break;
			case "updateLineupAndRosterScores":
				updateLineupAndRosterScores(e);
				break;
			default:
		}
	};

	return (
		<div className="flex flex-col min-h-screen">
			<NavigationBar />
			<div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto ">
				<div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
					<div className="mb-4 text-primary text-xl">Action</div>
					<select
						className="select select-accent w-full select-sm md:select-md mb-4"
						value={selectedAction}
						onChange={(e) => {
							setSelectedAction(e.target.value);
						}}
					>
						<option value="" disabled hidden>
							Select an Action
						</option>
						<option value="createTeamsAndPlayers">Create Teams / Players</option>
						<option value="upsertPlayers">Upsert Players</option>
						<option value="createPlayerStatsAndProjections">Create Player Stats / Projections</option>
						<option value="upsertPlayerStatsAndProjectionsByWeek">Upsert Player Stats / Projections (Current Week {configurations.find((config) => config.configKey === "currentWeek")?.configValue})</option>
						<option value="updateLineupAndRosterScores">Update Lineup / Roster Scores (Lineup Week {configurations.find((config) => config.configKey === "currentLineupWeek")?.configValue})</option>
					</select>
					<button className="btn btn-primary btn-sm md:btn-md w-full mb-4" type="submit" disabled={isLoading || !selectedAction} onClick={handleApply}>
						{isLoading ? "Loading..." : "Apply"}
					</button>
				</div>

				<div className="divider lg:divider-horizontal divider-vertical"></div>

				<div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
					<div className="mb-4 text-primary text-xl">Configuration</div>
					<form onSubmit={updateConfiguration}>
						<select
							className="select select-accent w-full select-sm md:select-md mb-4"
							value={selectedConfigKey}
							onChange={(e) => {
								setSelectedConfigKey(e.target.value);
								setSelectedConfigValue("");
							}}
						>
							<option value="" disabled hidden>
								Select Config Key
							</option>
							{[...orderedConfigKeys.keys()].map((key) => (
								<option key={key} value={key}>
									{orderedConfigKeys.get(key)}
								</option>
							))}
						</select>
						<select className="select select-accent w-full select-sm md:select-md mb-4" value={selectedConfigValue} onChange={(e) => setSelectedConfigValue(e.target.value)} disabled={!selectedConfigKey}>
							<option value="" disabled hidden>
								Select Config Value
							</option>
							{configValueOptions[selectedConfigKey] &&
								(typeof configValueOptions[selectedConfigKey] === "object" && !Array.isArray(configValueOptions[selectedConfigKey])
									? Object.entries(configValueOptions[selectedConfigKey]).map(([value, displayText]) => (
											<option key={value} value={value}>
												{displayText}
											</option>
									  ))
									: Array.isArray(configValueOptions[selectedConfigKey]) &&
									  configValueOptions[selectedConfigKey].map((value) => (
											<option key={value} value={value}>
												{value}
											</option>
									  )))}
						</select>
						<button className="btn btn-primary btn-sm md:btn-md w-full mb-4" type="submit" disabled={isLoading || !selectedConfigKey || !selectedConfigValue}>
							{isLoading && loadingMessage === "Updating Configuration..." ? "Loading..." : "Update Configuration"}
						</button>
					</form>

					<div className="flex flex-col items-center justify-center">
						<div className="form-control">
							<label className="cursor-pointer label">
								<span className="label-text mr-2 md:mr-4">Toggle Configuration</span>
								<input type="checkbox" className="toggle toggle-info ml-2 md:ml-4" onClick={toggleConfigTableVisibility} />
							</label>
						</div>
					</div>
					{isConfigTableVisible && (
						<div className="overflow-auto">
							<table className="table table-xs table-pin-rows">
								<tbody>
									{configurations
										.sort((a, b) => configDisplayOrder.indexOf(a.configKey) - configDisplayOrder.indexOf(b.configKey))
										.map((config, index) => (
											<tr key={index} className="bg-neutral hover:bg-info-content">
												<td>{configKeyDisplayNames[config.configKey] || config.configKey}</td>
												<td className={ 
													(config.configKey === 'lockLeagues' || config.configKey === 'lockRosters' || config.configKey === 'lockLineups') && 
													configValueOptions[config.configKey][config.configValue] === 'Unlocked' ? 'text-green-500' :
													(config.configKey === 'lockLeagues' || config.configKey === 'lockRosters' || config.configKey === 'lockLineups') && 
													configValueOptions[config.configKey][config.configValue] === 'Locked' ? 'text-red-500' : '' 
												}>{config.configKey === 'lockLeagues' || config.configKey === "lockRosters" || config.configKey === "lockLineups" || config.configKey === "currentWeek" || config.configKey === "startingLineupWeek" ? configValueOptions[config.configKey][config.configValue] : config.configValue}</td>
											</tr>
										))}
								</tbody>
							</table>
						</div>
					)}
				</div>

				<div className="divider lg:divider-horizontal divider-vertical"></div>

				<div className="flex-1 w-full  mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
					<div className="mb-4 text-primary text-xl">Team Status</div>
					<form onSubmit={ToggleTeamStatus}>
						<select className="select select-accent w-full select-sm md:select-md mb-4" value={selectedTeamName} onChange={(e) => setSelectedTeamName(e.target.value)}>
							<option value="" disabled hidden>
								Select a Team
							</option>
							{dynamicTeamNameOptions.map((teamName) => (
								<option key={teamName} value={teamName}>
									{teamNameDisplayNames[teamName] || teamName}
								</option>
							))}
						</select>
						<button className="btn btn-primary btn-sm md:btn-md w-full mb-4" type="submit" disabled={isLoading || selectedTeamName === ""}>
							{isLoading && loadingMessage === `Updating ${selectedTeamName} Status...` ? "Loading..." : `Update ${selectedTeamName} Status`}
						</button>
					</form>

					<div className="flex flex-col items-center justify-center">
						<div className="form-control">
							<label className="cursor-pointer label">
								<span className="label-text mr-2 md:mr-4">Toggle Teams</span>
								<input type="checkbox" className="toggle toggle-info ml-2 md:ml-4" onClick={toggleTeamsTableVisibility} />
							</label>
						</div>
					</div>
					{isTeamsTableVisible && (
						<div className="overflow-auto">
							<table className="table table-xs table-pin-rows">
								<tbody>
									{teams.map((team, index) => (
										<tr key={index} className="bg-neutral hover:bg-info-content">
											<td>{teamNameDisplayNames[team.team] || team.team}</td>
											<td className={team.status === "Active" ? 'text-green-500' : team.status === "Inactive" ? 'text-red-500' : ""}>{team.status}</td>
										</tr>
									))}
								</tbody>
							</table>
						</div>
					)}
				</div>
			</div>

			{/* <div className="flex-1 w-full p-4">
							<div className="mb-4">EXAMPLES</div>
				<button className="btn btn-primary">Primary</button>
				<button className="btn btn-secondary">Secondary</button>
				<button className="btn btn-accent">Accent</button>
				<button className="btn btn-neutral">Neutral</button>
				<button className="btn btn-outline">Outline</button>
				<button className="btn btn-ghost">Ghost</button>
				<button className="btn btn-link">Link</button>
				<button className="btn btn-disabled">Disabled</button>
				<button className="btn bg-base-100">Base 100</button>
				<button className="btn bg-base-200">Base 200</button>
				<button className="btn bg-base-300">Base 300</button>
				<button className="btn btn-info">Info</button>
				<button className="btn btn-success">Success</button>
				<button className="btn btn-warning">Warning</button>
				<button className="btn btn-error">Error</button>
			</div> */}
			<div className="message-container">
				{loadingMessage && <p>{loadingMessage}</p>}
				{successMessage && <p>{successMessage}</p>}
				{error && <p>{error}</p>}
			</div>
		</div>
	);
}

export default AdminComponent;

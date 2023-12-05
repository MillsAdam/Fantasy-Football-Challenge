import React, { useState, useEffect } from "react";
// import { AuthContext } from "../context/AuthContext";
import StatsService from "../services/StatsService";
import DatabaseService from "../services/DatabaseService";
import { useConfig } from "../context/ConfigContext";
import "../styles/StatsComponent.css";
import { 
    positionOptions, 
    positionDisplayOptions, 
    intervalOptions, 
    intervalDisplayOptions, 
    categoryOptions, 
    categoryDisplayOptions, 
    weekOptions, 
    weekDisplayOptions, 
    conferenceOptions, 
    conferenceDisplayOptions, 
    teamNameDisplayOptions, 
    positionColumns, 
    headerColumns 
} from "../constants/StatsConstants";

function StatsComponent() {
    // const { authToken, currentUser } = useContext(AuthContext);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [searchResults, setSearchResults] = useState([]);
    const [selectedPosition, setSelectedPosition] = useState("");
    const [selectedInterval, setSelectedInterval] = useState("");
    const [selectedCategory, setSelectedCategory] = useState("");
    const [selectedFilter, setSelectedFilter] = useState("");
    const [selectedWeek, setSelectedWeek] = useState("");
    const [activeTeamNameOptions, setActiveTeamNameOptions] = useState([]);
    const [currentWeek, setCurrentWeek] = useState('');
    const { configurations } = useConfig();

    useEffect(() => {
        async function getActiveTeamNameOptions() {
            setIsLoading(true);
            try {
                const activeTeamNameOptionsData = await DatabaseService.getActiveTeams();
                const sortedTeamNames = activeTeamNameOptionsData.map(team => team.team).sort();
                setActiveTeamNameOptions(sortedTeamNames);
            } catch (error) {
                console.error('An error occurred: ', error);
                setError('Failed to get active team name options');
            }
            setIsLoading(false);
        }

        getActiveTeamNameOptions();
    }, []);



    useEffect(() => {
        const currentWeekConfig = configurations.find(config => config.configKey === 'current_week');
        if (currentWeekConfig) {
            const currentWeek = currentWeekConfig.configValue;
            setCurrentWeek(currentWeek);
        }
    }, [configurations]);

    useEffect(() => {
        if (selectedPosition === 'qb' || 
            selectedPosition === 'rb' || 
            selectedPosition === 'wr' || 
            selectedPosition === 'te' || 
            selectedPosition === 'flex' || 
            selectedPosition === 'k' || 
            selectedPosition === 'def') {
            setSelectedInterval('');
            setSelectedCategory('');
            setSelectedFilter('');
            setSelectedWeek('');
        }
    }, [selectedPosition]);

    useEffect(() => {
        if (selectedCategory === 'conf' || selectedCategory === 'team' || selectedCategory === 'name') {
            setSelectedFilter('');
        }
    }, [selectedCategory]);

    // Might delete this useEffect (usually want to search by the same week)
    useEffect(() => {
        if (selectedInterval === 'weekly total' || selectedInterval === 'weekly projected') {
            setSelectedWeek('');
        }
    }, [selectedInterval]);

    async function searchPlayerStats(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);
        try {
            const searchData = await StatsService.searchPlayerStats({
                position: selectedPosition,
                interval: selectedInterval,
                category: selectedCategory,
                filter: selectedFilter,
                week: selectedWeek
            });
            const filteredSearchData = searchData.filter(player => 
                player.teamStatus === 'Active'
            );
            setSearchResults(filteredSearchData);
        } catch (error) {
            console.error('An error occurred: ', error);
            setError(error);
        }
        setIsLoading(false);
    }

    async function  clearSearch(e) {
        e.preventDefault();
        setSelectedPosition("");
        setSelectedInterval("");
        setSelectedCategory("");
        setSelectedFilter("");
        setSelectedWeek("");
        setSearchResults([]);
    }

    return (
        <div>
            <div className="page-container">
                <div className="search-container">
                    <form onSubmit={searchPlayerStats}>
                        <div>
                            <select 
                                className="stats-select" 
                                value={selectedPosition} 
                                onChange={(e) => setSelectedPosition(e.target.value)}
                            >
                                <option value="" disabled hidden>Select a position</option>
                                {positionOptions.map((position) => (
                                    <option key={position} value={position}>{positionDisplayOptions[position]}</option>
                                ))}
                            </select>
                        </div>
                        <div>

                            <select 
                                className="stats-select" 
                                value={selectedInterval} 
                                onChange={(e) => setSelectedInterval(e.target.value)} 
                                disabled={selectedPosition === ''}
                            >
                                <option value="" disabled hidden>Select an interval</option>
                                {intervalOptions.map((interval) => (
                                    <option key={interval} value={interval}>{intervalDisplayOptions[interval]}</option>
                                ))}
                            </select>
                        </div>
                        {(selectedInterval === 'weekly total' || selectedInterval === 'weekly projected') && (
                            <div>
                                <select 
                                    className="stats-filter-select"
                                    value={selectedWeek} 
                                    onChange={(e) => setSelectedWeek(e.target.value)}
                                >
                                    <option value="" disabled hidden>Select a week</option>
                                    {weekOptions
                                        .filter(week => week <= currentWeek)
                                        .map((week) => (
                                        <option key={week} value={week}>{weekDisplayOptions[week]}</option>
                                    ))}
                                </select>
                            </div>
                        )}
                        <div>
                            <select 
                                className="stats-select" 
                                value={selectedCategory} 
                                onChange={(e) => setSelectedCategory(e.target.value)} 
                                disabled={selectedPosition === '' || selectedInterval === ''}
                            >
                                <option value="" disabled hidden>Select a category</option>
                                {categoryOptions.map((category) => (
                                    <option key={category} value={category}>{categoryDisplayOptions[category]}</option>
                                ))}
                            </select>
                        </div>
                        {selectedCategory !== 'all' && selectedCategory !== '' && (
                            <div>
                                {(selectedCategory === 'conf' || selectedCategory === 'team') ? (
                                    <select 
                                        className="stats-filter-select" 
                                        value={selectedFilter}
                                        onChange={(e) => setSelectedFilter(e.target.value)}
                                    >
                                        <option value="" disabled hidden>Select a filter</option>
                                        {selectedCategory === 'conf' && conferenceOptions.map((conference) => (
                                            <option key={conference} value={conference}>{conferenceDisplayOptions[conference]}</option>
                                        ))}
                                        {selectedCategory === 'team' && activeTeamNameOptions.map((team) => (
                                            <option key={team} value={team}>{teamNameDisplayOptions[team] || team}</option>
                                        ))}
                                    </select>
                                ) : selectedCategory === 'name' ? (
                                    <input 
                                        className="stats-filter-input" 
                                        type="text" 
                                        value={selectedFilter} 
                                        placeholder="Enter Name" 
                                        onChange={(e) => setSelectedFilter(e.target.value)} 
                                    />
                                ) : null}
                            </div>
                        )}
                        <div>
                            <button 
                                className="stats-button" 
                                type="submit" 
                                disabled=
                                    {isLoading || 
                                    (selectedPosition === '' && selectedInterval === '' && selectedCategory === '') || 
                                    (selectedInterval === 'weekly total' && selectedWeek === '') ||
                                    (selectedInterval === 'weekly projected' && selectedWeek === '') ||
                                    (selectedCategory !== 'all' && selectedCategory !== '' && selectedFilter === '')
                                }
                            >
                                Search
                            </button>
                            <button className="stats-button" onClick={clearSearch} disabled={isLoading}>Clear</button>
                        </div>
                    </form>
                    {isLoading ? (<p>Loading...</p>) : (
                        searchResults.length > 0 && (
                            <div className="table-container">
                                <table>
                                    <thead>
                                        <tr>
                                            <th colSpan="6">Player Info</th>
                                            {headerColumns[selectedPosition].map((column) => (
                                                <th key={column.label} colSpan={column.colSpan}>{column.label}</th>
                                            ))}
                                            <th colSpan="2">Points</th>
                                        </tr>
                                        <tr>
                                            <th>Conf</th>
                                            <th>Team</th>
                                            <th>Pos</th>
                                            <th>Week</th>
                                            <th>Inj</th>
                                            <th>Name</th>
                                            {positionColumns[selectedPosition].map((column) => (
                                                <th key={column.key}>{column.label}</th>
                                            ))}
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {searchResults.map((player,index) => (
                                            <tr key={index}>
                                                <td>{player.conference}</td>
                                                <td>{player.team}</td>
                                                <td>{player.position}</td>
                                                <td>{player.week}</td>
                                                <td className={
                                                    player.injuryStatus === 'P' || player.injuryStatus === null ? 'green-highlight' :
                                                    ["Q"].includes(player.injuryStatus?.charAt(0)) ? 'yellow-highlight' :
                                                    ["D", "O"].includes(player.injuryStatus?.charAt(0)) ? 'red-highlight' : ''
                                                }>
                                                    {player.injuryStatus ? player.injuryStatus.charAt(0) : 'A'}
                                                </td>
                                                <td>{player.name}</td>
                                                {positionColumns[selectedPosition].map((column) => (
                                                    <td key={column.key}>{player[column.key]}</td>
                                                ))}
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        )
                    )}
                    <div className="message-container">
                        {error && <p>{error.message}</p>}
                    </div>
                </div>
                
            </div>
        </div>
    );
}

export default StatsComponent;

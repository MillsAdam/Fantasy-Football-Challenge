import React, { useState, useEffect } from "react";
// import { AuthContext } from "../context/AuthContext";
import StatsService from "../services/StatsService";
import DatabaseService from "../services/DatabaseService";
import { useConfig } from "../context/ConfigContext";
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
        const currentWeekConfig = configurations.find(config => config.configKey === 'currentWeek');
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
                <div className="component-container">
                    <div className="search-container">
                        <form onSubmit={searchPlayerStats}>
                            <div>
                                <select className="btn btn-neutral btn-outline btn-sm md:btn-md" 
                                    style={{ width: '100%', marginBottom: '1rem' }} 
                                    value={selectedPosition} 
                                    onChange={(e) => setSelectedPosition(e.target.value)}
                                >
                                    <option value="" disabled hidden>Select a Position</option>
                                    {positionOptions.map((position) => (
                                        <option style={{ textAlign: 'left' }} key={position} value={position}>{positionDisplayOptions[position]}</option>
                                    ))}
                                </select>
                            </div>
                            <div>

                                <select className="btn btn-neutral btn-outline btn-sm md:btn-md" 
                                    style={{ width: '100%', marginBottom: '1rem' }} 
                                    value={selectedInterval} 
                                    onChange={(e) => setSelectedInterval(e.target.value)} 
                                    disabled={selectedPosition === ''}
                                >
                                    <option value="" disabled hidden>Select an Interval</option>
                                    {intervalOptions.map((interval) => (
                                        <option style={{ textAlign: 'left' }} key={interval} value={interval}>{intervalDisplayOptions[interval]}</option>
                                    ))}
                                </select>
                            </div>
                            {(selectedInterval === 'weekly total' || selectedInterval === 'weekly projected') && (
                                <div>
                                    <select className="btn btn-neutral btn-outline btn-sm md:btn-md" 
                                        style={{ width: '100%', marginBottom: '1rem' }}
                                        value={selectedWeek} 
                                        onChange={(e) => setSelectedWeek(e.target.value)}
                                    >
                                        <option value="" disabled hidden>Select a Week</option>
                                        {weekOptions
                                            .filter(week => week <= currentWeek)
                                            .map((week) => (
                                            <option style={{ textAlign: 'left' }} key={week} value={week}>{weekDisplayOptions[week]}</option>
                                        ))}
                                    </select>
                                </div>
                            )}
                            <div>
                                <select className="btn btn-neutral btn-outline btn-sm md:btn-md" 
                                    style={{ width: '100%', marginBottom: '1rem' }} 
                                    value={selectedCategory} 
                                    onChange={(e) => setSelectedCategory(e.target.value)} 
                                    disabled={selectedPosition === '' || selectedInterval === ''}
                                >
                                    <option value="" disabled hidden>Select a Category</option>
                                    {categoryOptions.map((category) => (
                                        <option style={{ textAlign: 'left' }} key={category} value={category}>{categoryDisplayOptions[category]}</option>
                                    ))}
                                </select>
                            </div>
                            {selectedCategory !== 'all' && selectedCategory !== '' && (
                                <div>
                                    {(selectedCategory === 'conf' || selectedCategory === 'team') ? (
                                        <select className="btn btn-neutral btn-outline btn-sm md:btn-md" 
                                            style={{ width: '100%', marginBottom: '1rem' }} 
                                            value={selectedFilter}
                                            onChange={(e) => setSelectedFilter(e.target.value)}
                                        >
                                            <option value="" disabled hidden>Select a Filter</option>
                                            {selectedCategory === 'conf' && conferenceOptions.map((conference) => (
                                                <option style={{ textAlign: 'left' }} key={conference} value={conference}>{conferenceDisplayOptions[conference]}</option>
                                            ))}
                                            {selectedCategory === 'team' && activeTeamNameOptions.map((team) => (
                                                <option style={{ textAlign: 'left' }} key={team} value={team}>{teamNameDisplayOptions[team] || team}</option>
                                            ))}
                                        </select>
                                    ) : selectedCategory === 'name' ? (
                                        <input className="btn btn-neutral btn-outline btn-sm md:btn-md" 
                                            style={{ textAlign: 'left', width: '100%', marginBottom: '1rem' }} 
                                            type="text" 
                                            value={selectedFilter} 
                                            placeholder="Enter Name" 
                                            onChange={(e) => setSelectedFilter(e.target.value)} 
                                        />
                                    ) : null}
                                </div>
                            )}
                            <div className="horizontal-container" style={{ marginBottom: '1rem' }}>
                                <button 
                                    className="btn btn-success btn-outline btn-sm md:btn-md" 
                                    style={{ width: '45%' }}
                                    type="submit" 
                                    disabled=
                                        {isLoading || 
                                        (selectedPosition !== '' && selectedInterval === '') || 
                                        (selectedPosition !== '' && selectedInterval !== '' && selectedCategory === '') ||
                                        (selectedPosition === '' && selectedInterval === '' && selectedCategory === '') || 
                                        (selectedInterval === 'weekly total' && selectedWeek === '') ||
                                        (selectedInterval === 'weekly projected' && selectedWeek === '') ||
                                        (selectedCategory !== 'all' && selectedCategory !== '' && selectedFilter === '')
                                    }
                                >
                                    Search
                                </button>
                                <button 
                                    className="btn btn-warning btn-outline btn-sm md:btn-md" 
                                    style={{ width: '45%' }}
                                    onClick={clearSearch} 
                                    disabled={isLoading}
                                >
                                        Clear
                                </button>
                            </div>
                        </form>
                    </div>
                    
                    {isLoading ? (<p>Loading...</p>) : (
                        searchResults.length > 0 && (
                            <div className="overflow-x auto" style={{ overflow: 'auto' }}>
                                <table className="table table-xs table-pin-rows">
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
                                            <tr key={index} className="hover">
                                                <td>{player.conference}</td>
                                                <td>{player.team}</td>
                                                <td>{player.position}</td>
                                                <td>{player.week}</td>
                                                <td className={
                                                    ["P"].includes(player.injuryStatus?.charAt(0)) ? 'green-highlight' : 
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

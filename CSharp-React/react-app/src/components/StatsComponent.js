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
import NavigationBar from "./NavigationBar";

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
            selectedPosition === 'def' || 
            selectedPosition === '') {
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
        <div className="flex flex-col min-h-screen">
            <NavigationBar />
            <div className="flex lg:flex-row lg:justify-between lg:items-start flex-wrap w-90 gap-4 flex-col justify-center align-center my-4 mx-auto">
                <div className="flex-1 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                    <div className="search-container">
                        <form onSubmit={searchPlayerStats}>
                            <div>
                                <select className="select select-accent w-full select-sm md:select-md mb-4" 
                                    value={selectedPosition} 
                                    onChange={(e) => setSelectedPosition(e.target.value)}
                                >
                                    <option value="" disabled hidden>Select a Position</option>
                                    {positionOptions.map((position) => (
                                        <option key={position} value={position}>{positionDisplayOptions[position]}</option>
                                    ))}
                                </select>
                            </div>
                            <div>
                                <select className="select select-accent w-full select-sm md:select-md mb-4" 
                                    value={selectedInterval} 
                                    onChange={(e) => setSelectedInterval(e.target.value)} 
                                    disabled={selectedPosition === ''}
                                >
                                    <option value="" disabled hidden>Select an Interval</option>
                                    {intervalOptions.map((interval) => (
                                        <option key={interval} value={interval}>{intervalDisplayOptions[interval]}</option>
                                    ))}
                                </select>
                            </div>
                            {(selectedInterval === 'weekly total' || selectedInterval === 'weekly projected') && (
                                <div>
                                    <select className="select select-accent w-full select-sm md:select-md mb-4" 
                                        value={selectedWeek} 
                                        onChange={(e) => setSelectedWeek(e.target.value)}
                                    >
                                        <option value="" disabled hidden>Select a Week</option>
                                        {weekOptions
                                            .filter(week => week <= currentWeek)
                                            .map((week) => (
                                            <option key={week} value={week}>{weekDisplayOptions[week]}</option>
                                        ))}
                                    </select>
                                </div>
                            )}
                            <div>
                                <select className="select select-accent w-full select-sm md:select-md mb-4" 
                                    value={selectedCategory} 
                                    onChange={(e) => setSelectedCategory(e.target.value)} 
                                    disabled={selectedPosition === '' || selectedInterval === ''}
                                >
                                    <option value="" disabled hidden>Select a Category</option>
                                    {categoryOptions.map((category) => (
                                        <option key={category} value={category}>{categoryDisplayOptions[category]}</option>
                                    ))}
                                </select>
                            </div>
                            {selectedCategory !== 'all' && selectedCategory !== '' && (
                                <div>
                                    {(selectedCategory === 'conf' || selectedCategory === 'team') ? (
                                        <select className="select select-accent w-full select-sm md:select-md mb-4" 
                                            value={selectedFilter}
                                            onChange={(e) => setSelectedFilter(e.target.value)}
                                        >
                                            <option value="" disabled hidden>Select a Filter</option>
                                            {selectedCategory === 'conf' && conferenceOptions.map((conference) => (
                                                <option key={conference} value={conference}>{conferenceDisplayOptions[conference]}</option>
                                            ))}
                                            {selectedCategory === 'team' && activeTeamNameOptions.map((team) => (
                                                <option key={team} value={team}>{teamNameDisplayOptions[team] || team}</option>
                                            ))}
                                        </select>
                                    ) : selectedCategory === 'name' ? (
                                        <input className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                                            type="text" 
                                            value={selectedFilter} 
                                            placeholder="Enter Name" 
                                            onChange={(e) => setSelectedFilter(e.target.value)} 
                                        />
                                    ) : null}
                                </div>
                            )}
                            <div className="flex flex-row justify-between align-center flex-nowrap mb-4">
                                <button 
                                    className="btn btn-primary btn-sm md:btn-md w-45" 
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
                                    className="btn btn-secondary btn-sm md:btn-md w-45" 
                                    onClick={clearSearch} 
                                    disabled=
                                        {isLoading || 
                                        (selectedPosition === '' && selectedInterval === '' && selectedCategory === '' && selectedFilter === '' && selectedWeek === '')
                                    }
                                >
                                        Clear
                                </button>
                            </div>
                        </form>
                    </div>
                    
                    {isLoading ? (<p>Loading...</p>) : (
                        searchResults.length > 0 && (
                            <div>
                                <div className="mb-4 text-success">
                                    Search Results
                                </div>
                                <div className="overflow-auto">
                                    <table className="table table-xs table-pin-rows">
                                        <thead>
                                            <tr className="bg-base-300">
                                                <th colSpan="6">Player Info</th>
                                                {headerColumns[selectedPosition].map((column) => (
                                                    <th key={column.label} colSpan={column.colSpan}>{column.label}</th>
                                                ))}
                                                <th colSpan="2">Points</th>
                                            </tr>
                                            <tr className="bg-base-100">
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
                                                <tr key={index} className="bg-neutral hover:bg-info-content">
                                                    <td>{player.conference}</td>
                                                    <td>{player.team}</td>
                                                    <td>{player.position}</td>
                                                    <td>{player.week}</td>
                                                    <td className={
                                                        ["P"].includes(player.injuryStatus?.charAt(0)) ? 'text-green-500' : 
                                                        ["Q"].includes(player.injuryStatus?.charAt(0)) ? 'text-yellow-500' : 
                                                        ["D", "O"].includes(player.injuryStatus?.charAt(0)) ? 'text-red-500' : ''
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

import React, { useState, useContext, useEffect, useCallback } from "react";
import { AuthContext } from "../context/AuthContext";
import StatsService from "../services/StatsService";
import DatabaseService from "../services/DatabaseService";
import "../styles/StatsComponent.css";

const positionOptions = ['qb', 'rb', 'wr', 'te', 'flex', 'k', 'def'];
const positionDisplayOptions = {
    'qb': 'QB',
    'rb': 'RB',
    'wr': 'WR',
    'te': 'TE',
    'flex': 'FLEX',
    'k': 'K',
    'def': 'DEF'
};
const intervalOptions = ['season total', 'season average', 'last 4 total', 'last 4 average', 'weekly total', 'weekly projected'];
const intervalDisplayOptions = {
    'season total': 'Season Total',
    'season average': 'Season Average',
    'last 4 total': 'Last 4 Total',
    'last 4 average': 'Last 4 Average',
    'weekly total': 'Weekly Total',
    'weekly projected': 'Weekly Projected'
};
const categoryOptions = ['all', 'conf', 'team', 'name'];
const categoryDisplayOptions = {
    'all': 'All',
    'conf': 'Conference',
    'team': 'Team',
    'name': 'Name'
};
const weekOptions = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22];
const weekDisplayOptions = {
    1: 'Week 1',
    2: 'Week 2',
    3: 'Week 3',
    4: 'Week 4',
    5: 'Week 5',
    6: 'Week 6',
    7: 'Week 7',
    8: 'Week 8',
    9: 'Week 9',
    10: 'Week 10',
    11: 'Week 11',
    12: 'Week 12',
    13: 'Week 13',
    14: 'Week 14',
    15: 'Week 15',
    16: 'Week 16',
    17: 'Week 17',
    18: 'Week 18',
    19: 'Wild Card',
    20: 'Divisional',
    21: 'Conf Championship',
    22: 'Super Bowl'
};
const conferenceOptions = ['afc', 'nfc'];
const conferenceDisplayOptions = {
    'afc': 'AFC',
    'nfc': 'NFC'
};
const teamNameDisplayOptions = {
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
    'LAC': 'Los Angeles Chargers',
    'LAR': 'Los Angeles Rams',
    'LV': 'Las Vegas Raiders',
    'MIA': 'Miami Dolphins',
    'MIN': 'Minnesota Vikings',
    'NE': 'New England Patriots',
    'NO': 'New Orleans Saints',
    'NYG': 'New York Giants',
    'NYJ': 'New York Jets',
    'PHI': 'Philadelphia Eagles',
    'PIT': 'Pittsburgh Steelers',
    'SEA': 'Seattle Seahawks',
    'SF': 'San Francisco 49ers',
    'TB': 'Tampa Bay Buccaneers',
    'TEN': 'Tennessee Titans',
    'WAS': 'Washington Football Team'
};
// weeks (from configuration)


function StatsComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
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
        async function fetchConfigurations() {
            try {
                const fetchedConfigurations = await DatabaseService.getConfiguration();
                const currentWeekConfig = fetchedConfigurations.find(config => config.configKey === 'current_week');
                const currentWeek = currentWeekConfig.configValue;
                setCurrentWeek(currentWeek);
            } catch (error) {
                console.error('An error occurred: ', error);
                setError('Failed to get configurations');
            }
        }
        fetchConfigurations();
    }, []);

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
                                            <th colSpan="6">Description</th>
                                            <th colSpan="7">Passing</th>
                                            <th colSpan="3">Rushing</th>
                                            <th colSpan="2">Extra</th>
                                            <th colSpan="2">Points</th>
                                        </tr>
                                        <tr>
                                            <th>Conf</th>
                                            <th>Team</th>
                                            <th>Pos</th>
                                            <th>Week</th>
                                            <th>Inj</th>
                                            <th>Name</th>
                                            <th>Comp</th>
                                            <th>Att</th>
                                            <th>Pct</th>
                                            <th>Yds</th>
                                            <th>TD</th>
                                            <th>Int</th>
                                            <th>Rtng</th>
                                            <th>Att</th>
                                            <th>Yds</th>
                                            <th>TD</th>
                                            <th>2PC</th>
                                            <th>FL</th>
                                            <th>Tot</th>
                                            <th>Avg</th>
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
                                                <td>{player.passingCompletions}</td>
                                                <td>{player.passingAttempts}</td>
                                                <td>{player.passingCompletionPercentage}</td>
                                                <td>{player.passingYards}</td>
                                                <td>{player.passingTouchdowns}</td>
                                                <td>{player.passingInterceptions}</td>
                                                <td>{player.passingRating}</td>
                                                <td>{player.rushingAttempts}</td>
                                                <td>{player.rushingYards}</td>
                                                <td>{player.rushingTouchdowns}</td>
                                                <td>{player.twoPointConversions}</td>
                                                <td>{player.fumblesLost}</td>
                                                <td>{player.fantasyPointsTotal}</td>
                                                <td>{player.fantasyPointsAverage}</td>
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

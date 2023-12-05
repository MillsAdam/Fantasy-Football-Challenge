import React, { useState, useEffect } from "react";
// import { AuthContext } from "../context/AuthContext";
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

const sharedColumns = [
    { key: 'rushingAttempts', label: 'Att' },
    { key: 'rushingYards', label: 'Yds' },
    { key: 'rushingYardsPerAttempt', label: 'Y/A' },
    { key: 'rushingTouchdowns', label: 'TD' },
    { key: 'receivingTargets', label: 'Tgt' },
    { key: 'receptions', label: 'Rec' },
    { key: 'receivingYards', label: 'Yds' },
    { key: 'receivingYardsPerReception', label: 'Y/R' },
    { key: 'receivingTouchdowns', label: 'TD' },
    { key: 'returnTouchdowns', label: 'retTD' },
    { key: 'twoPointConversions', label: '2PC' },
    { key: 'usage', label: 'Usg' },
    { key: 'fumblesLost', label: 'FL' },
    { key: 'fantasyPointsTotal', label: 'Tot' },
    { key: 'fantasyPointsAverage', label: 'Avg' }
];
const positionColumns = {
    qb: [
        { key: 'passingCompletions', label: 'Comp' },
        { key: 'passingAttempts', label: 'Att' },
        { key: 'passingCompletionPercentage', label: 'Pct' },
        { key: 'passingYards', label: 'Yds' },
        { key: 'passingTouchdowns', label: 'TD' },
        { key: 'passingInterceptions', label: 'Int' },
        { key: 'passingRating', label: 'Rtng' },
        { key: 'rushingAttempts', label: 'Att' },
        { key: 'rushingYards', label: 'Yds' },
        { key: 'rushingTouchdowns', label: 'TD' },
        { key: 'twoPointConversions', label: '2PC' },
        { key: 'fumblesLost', label: 'FL' },
        { key: 'fantasyPointsTotal', label: 'Tot' },
        { key: 'fantasyPointsAverage', label: 'Avg' }
    ], 
    rb: sharedColumns,
    wr: sharedColumns,
    te: sharedColumns,
    flex: sharedColumns,
    k: [
        { key: 'fieldGoalsMade', label: 'FGM' },
        { key: 'fieldGoalsAttempted', label: 'FGA' },
        { key: 'fieldGoalPercentage', label: 'Pct' },
        { key: 'fieldGoalsMade0to19', label: '0-19' },
        { key: 'fieldGoalsMade20to29', label: '20-29' },
        { key: 'fieldGoalsMade30to39', label: '30-39' },
        { key: 'fieldGoalsMade40to49', label: '40-49' },
        { key: 'fieldGoalsMade50Plus', label: '50+' },
        { key: 'extraPointsMade', label: 'XPM' },
        { key: 'extraPointsAttempted', label: 'XPA' },
        { key: 'extraPointPercentage', label: 'Pct'},
        { key: 'fantasyPointsTotal', label: 'Tot' },
        { key: 'fantasyPointsAverage', label: 'Avg' }
    ],
    def: [
        { key: 'defensiveTouchdowns', label: 'Def' },
        { key: 'specialTeamsTouchdowns', label: 'ST' },
        { key: 'touchdownsScored', label: 'TD' },
        { key: 'fumblesForced', label: 'FF' },
        { key: 'fumblesRecovered', label: 'FR' },
        { key: 'interceptions', label: 'Int' },
        { key: 'tacklesForLoss', label: 'TFL' },
        { key: 'quarterbackHits', label: 'QH' },
        { key: 'sacks', label: 'Sck' },
        { key: 'safeties', label: 'Sfty' },
        { key: 'blockedKicks', label: 'Blk' },
        { key: 'pointsAllowed', label: 'PtsA' },
        { key: 'fantasyPointsTotal', label: 'Tot' },
        { key: 'fantasyPointsAverage', label: 'Avg' }
    ]
};
const sharedHeaders = [
    { label: 'Rushing', colSpan: 4 },
    { label: 'Receiving', colSpan: 5 },
    { label: 'Extra', colSpan: 4 },
];
const headerColumns = {
    qb: [
        { label: 'Passing', colSpan: 7 },
        { label: 'Rushing', colSpan: 3 },
        { label: 'Extra', colSpan: 2 },
    ],
    rb: sharedHeaders,
    wr: sharedHeaders,
    te: sharedHeaders,
    flex: sharedHeaders,
    k: [
        { label: 'Field Goals', colSpan: 8 },
        { label: 'Extra Points', colSpan: 3 },
    ],
    def: [
        { label: 'Touchdown', colSpan: 3 },
        { label: 'Defensive', colSpan: 9 },
    ]
};

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

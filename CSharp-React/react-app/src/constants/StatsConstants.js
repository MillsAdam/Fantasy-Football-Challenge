export const positionOptions = ['qb', 'rb', 'wr', 'te', 'flex', 'k', 'def'];
export const positionDisplayOptions = {
    'qb': 'QB',
    'rb': 'RB',
    'wr': 'WR',
    'te': 'TE',
    'flex': 'FLEX',
    'k': 'K',
    'def': 'DEF'
};
export const intervalOptions = ['season total', 'season average', 'last 4 total', 'last 4 average', 'weekly total', 'weekly projected'];
export const intervalDisplayOptions = {
    'season total': 'Season Total',
    'season average': 'Season Average',
    'last 4 total': 'Last 4 Total',
    'last 4 average': 'Last 4 Average',
    'weekly total': 'Weekly Total',
    'weekly projected': 'Weekly Projected'
};
export const categoryOptions = ['all', 'conf', 'team', 'name'];
export const categoryDisplayOptions = {
    'all': 'All',
    'conf': 'Conference',
    'team': 'Team',
    'name': 'Name'
};
export const weekOptions = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22];
export const weekDisplayOptions = {
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
export const conferenceOptions = ['afc', 'nfc'];
export const conferenceDisplayOptions = {
    'afc': 'AFC',
    'nfc': 'NFC'
};
export const teamNameDisplayOptions = {
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

export const sharedColumns = [
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
export const positionColumns = {
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
export const sharedHeaders = [
    { label: 'Rushing', colSpan: 4 },
    { label: 'Receiving', colSpan: 5 },
    { label: 'Extra', colSpan: 4 },
];
export const headerColumns = {
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
<template>
    <div>
        <form @submit.prevent="searchPlayerStats">
            <div class="form-row">

                <div class="form-group">
                    <label for="searchPosition">Position</label>
                    <select v-model="searchPosition" class="form-control" id="searchPosition">
                        <option v-for="position in positions" :value="position" :key="position" :class="{ 'select-option': searchPosition === position }">
                            {{ position.toUpperCase() }}</option>
                    </select>
                </div>

                <div class="form-group">
                    <label for="searchIntervalPoints">Stats</label>
                    <select v-model="searchIntervalPoints" class="form-control" id="searchIntervalPoints" @change="updateIntervalAndPoints">
                        <option v-for="interval in intervals" :value="interval" :key="interval" :class="{ 'select-option': searchInterval === interval }">
                            {{ interval }}
                        </option>
                    </select>
                </div>

                <div class="form-group" v-if="searchInterval !== 'season' && searchInterval !== ''">
                    <label for="searchWeek">Week</label>
                    <select v-model="searchWeek" class="form-control" id="searchWeek">
                        <option v-for="week in weeks" :value="week" :key="week" :class="{ 'select-option': searchWeek === week }">
                            {{ week }}
                        </option>
                    </select>
                </div>

                <div class="form-group">
                    <label for="searchCategory">Category</label>
                    <select v-model="searchCategory" class="form-control" id="searchCategory">
                        <option v-for="category in categories" :value="category.toLowerCase()" :key="category" :class="{ 'select-option': searchCategory === category.toLowerCase() }">
                            {{ category }}
                        </option>
                    </select>
                </div>

                <div class="form-group" v-if="searchCategory !== 'all' && searchCategory !== ''">
                    <label for="searchTerm" v-if="searchCategory === 'conference'">Conference</label>
                    <select v-model="searchTerm" class="form-control" id="searchTerm" v-if="searchCategory === 'conference'">
                        <option v-for="conference in conferences" :value="conference" :key="conference" :class="{ 'select-option': searchCategory === conference }">
                            {{ conference.toUpperCase() }}
                        </option>
                    </select>
                    <label for="searchTerm" v-if="searchCategory === 'team'">Team</label>
                    <select v-model="searchTerm" class="form-control" id="searchTerm" v-if="searchCategory === 'team'">
                        <option v-for="(teamName, teamAbbreviation) in teams" :value="teamAbbreviation" :key="teamAbbreviation" :class="{ 'select-option': searchCategory === teamAbbreviation }">
                            {{ teamName }}
                        </option>
                    </select>
                    <label for="searchTerm" v-if="searchCategory === 'name'" autocomplete="name">Name</label>
                    <input v-model="searchTerm" type="text" class="form-control custom-input" id="searchTerm" v-if="searchCategory === 'name'" placeholder="Player Name">
                </div>
            </div>

            <div class="form-group">
                <button type="submit">Apply Filters</button>
            </div>
            
        </form>
    </div>
</template>

<script>
import StatsService from '../services/StatsService';

export default {
    data() {
        return {
            searchPosition: "",
            positions: ["qb", "flex", "rb", "wr", "te", "k", "def"],
            searchInterval: "",
            intervals: ["Season (Total)", "Season (Average)", "Season (Projected)", "Last 4 (Total)", "Last 4 (Average)", "Next 4 (Projected)", "Remaining (Projected)", "Weekly (Total)", "Weekly (Projected)"],
            searchPoints: "",
            points: ["total", "average", "projected"],
            searchCategory: "",
            categories: ["All", "Conference", "Team", "Name"],
            searchWeek: null,
            weeks: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18],
            searchTerm: "",
            conferences: ["afc", "nfc"],
            teams: {
                ari: "Arizona Cardinals",
                atl: "Atlanta Falcons",
                bal: "Baltimore Ravens",
                buf: "Buffalo Bills",
                car: "Carolina Panthers",
                chi: "Chicago Bears",
                cin: "Cincinnati Bengals",
                cle: "Cleveland Browns",
                dal: "Dallas Cowboys",
                den: "Denver Broncos",
                det: "Detroit Lions",
                gb: "Green Bay Packers",
                hou: "Houston Texans",
                ind: "Indianapolis Colts",
                jax: "Jacksonville Jaguars",
                kc: "Kansas City Chiefs",
                lac: "Los Angeles Chargers",
                lar: "Los Angeles Rams",
                lv: "Las Vegas Raiders",
                mia: "Miami Dolphins",
                min: "Minnesota Vikings",
                ne: "New England Patriots",
                no: "New Orleans Saints",
                nyg: "New York Giants",
                nyj: "New York Jets",
                phi: "Philadelphia Eagles",
                pit: "Pittsburgh Steelers",
                sea: "Seattle Seahawks",
                sf: "San Francisco 49ers",
                tb: "Tampa Bay Buccaneers",
                ten: "Tennessee Titans",
                wsh: "Washington Commanders",
            },
            searchIntervalPoints: null,
        }
    }, 

    computed: {
        filteredPoints() {
            if (this.searchInterval === 'last4') {
                return this.points.filter(point => point === 'total' || point === 'average');
            } else if (this.searchInterval === 'next4' || this.searchInterval === 'remaining') {
                return this.points.filter(point => point === 'projected');
            } else if (this.searchInterval === 'weekly') {
                return this.points.filter(point => point === 'total' || point === 'projected');
            }
            return this.points;
        },
    }, 

    methods: {
        updateIntervalAndPoints() {
            const mapping = {
                'season (total)': { interval: 'season', points: 'total' },
                'season (average)': { interval: 'season', points: 'average' },
                'season (projected)': { interval: 'season', points: 'projected' },
                'last 4 (total)': { interval: 'last4', points: 'total' },
                'last 4 (average)': { interval: 'last4', points: 'average' },
                'next 4 (projected)': { interval: 'next4', points: 'projected' },
                'remaining (projected)': { interval: 'remaining', points: 'projected' },
                'weekly (total)': { interval: 'weekly', points: 'total' },
                'weekly (projected)': { interval: 'weekly', points: 'projected' },
            };

            const intervalPoints = this.searchIntervalPoints.toLowerCase();

            if (mapping[intervalPoints]) {
                const { interval, points } = mapping[intervalPoints];
                this.searchInterval = interval;
                this.searchPoints = points;
            } else {
                this.searchInterval = '';
                this.searchPoints = '';
            }
        },

        async searchPlayerStats() {
            this.sortingColumn = this.defaultSortingColumn;
            let columnsToShow = {};

            if (this.searchPosition === "qb") {
                columnsToShow = {
                    playerID: true,
                    week: true,
                    team: true,
                    position: true,
                    name: true,
                    passingCompletions: true,
                    passingAttempts: true,
                    passingCompletionPercentage: true,
                    passingYards: true,
                    passingTouchdowns: true,
                    passingInterceptions: true,
                    passingRating: true,
                    rushingAttempts: true,
                    rushingYards: true,
                    rushingTouchdowns: true,
                    twoPointConversions: true,
                    fumblesLost: true,
                    fantasyPointsTotal: true,
                    fantasyPointsAverage: true
                };
            } else if (this.searchPosition === "flex" || this.searchPosition === "rb" || this.searchPosition === "wr" || this.searchPosition === "te") {
                columnsToShow = {
                    playerID: true,
                    week: true,
                    team: true,
                    position: true,
                    name: true,
                    rushingAttempts: true,
                    rushingYards: true,
                    rushingYardsPerAttempt: true,
                    rushingTouchdowns: true,
                    receivingTargets: true,
                    receptions: true,
                    receivingYards: true,
                    receivingYardsPerReception: true,
                    receivingTouchdowns: true,
                    returnTouchdowns: true,
                    twoPointConversions: true,
                    usage: true,
                    fumblesLost: true,
                    fantasyPointsTotal: true,
                    fantasyPointsAverage: true
                };
            } else if (this.searchPosition === "k") {
                columnsToShow = {
                    playerID: true,
                    week: true,
                    team: true,
                    position: true,
                    name: true,
                    fieldGoalsMade: true,
                    fieldGoalsAttempted: true,
                    fieldGoalPercentage: true,
                    fieldGoalsMade0to19: true,
                    fieldGoalsMade20to29: true,
                    fieldGoalsMade30to39: true,
                    fieldGoalsMade40to49: true,
                    fieldGoalsMade50Plus: true,
                    extraPointsMade: true,
                    extraPointsAttempted: true,
                    extraPointPercentage: true,
                    fantasyPointsTotal: true,
                    fantasyPointsAverage: true
                };
            } else if (this.searchPosition === "def") {
                columnsToShow = {
                    playerID: true,
                    week: true,
                    team: true,
                    position: true,
                    name: true,
                    defensiveTouchdowns: true,
                    specialTeamsTouchdowns: true,
                    touchdownsScored: true,
                    fumblesForced: true,
                    fumblesRecovered: true,
                    interceptions: true,
                    tacklesForLoss: true,
                    quarterbackHits: true,
                    sacks: true,
                    safeties: true,
                    blockedKicks: true,
                    pointsAllowed: true,
                    fantasyPointsTotal: true,
                    fantasyPointsAverage: true
                };
            } 

            this.columnsToShow = columnsToShow;

            this.shouldShowQuarterbackHeaders = this.searchPosition === "qb";
            this.shouldShowFlexHeaders = this.searchPosition === "flex" || this.searchPosition === "rb" || this.searchPosition === "wr" || this.searchPosition === "te";
            this.shouldShowKickersHeaders = this.searchPosition === "k";
            this.shouldShowDefenseHeaders = this.searchPosition === "def";

            StatsService.searchPlayerStats({ 
                searchPosition: this.searchPosition,
                searchInterval: this.searchInterval,
                searchPoints: this.searchPoints,
                searchCategory: this.searchCategory,
                searchTerm: this.searchTerm,
                searchWeek: this.searchWeek
            }).then(response => {
                console.log('Response:', response.data)
                this.playerStats = response.data;
                this.$emit('player-stats-updated', response.data);  
            }).catch(error => {
                console.log('Error:', error);
            });
              
        },

        search() {
            const searchParams = {
                searchPosition: this.searchPosition,
                searchInterval: this.searchInterval,
                searchPoints: this.searchPoints,
                searchCategory: this.searchCategory,
                searchWeek: this.searchWeek,
                searchTerm: this.searchTerm,
            };
            this.$emit('search', searchParams);
        },
    }, 

    watch: {
        searchCategory(newCategory) {
            if (newCategory === "name") {
                this.searchTerm = "";
            }
        }
    },
}
</script>

<style scoped>
.form-row {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding-top: 10px;
    gap: 10px;
}
.form-group {
    flex: 1;
    margin-bottom: 10px;
}

label {
    font-weight: bold;
    display: block;
    margin-bottom: 5px;
}

select, input {
    width: 100%;
    padding: 8px 0px;
    border: 1px solid #ccc;
    border-radius: 5px;
}

select.form-control option:hover {
    background-color: #407F7F;
    color: #ccc;
}

.custom-input {
    padding-left: 10px;
}

button {
  width: 100%;
  margin-top: 10px;
}

</style>
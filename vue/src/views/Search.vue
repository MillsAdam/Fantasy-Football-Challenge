<template>
    <div>
        <form @submit.prevent="searchPlayerStats">
            <div class="form-group">
                <label for="searchPosition">Position</label>
                <select v-model="searchPosition" class="form-control" id="searchPosition">
                    <option v-for="position in positions" :value="position" :key="position" :class="{ 'select-option': searchPosition === position }">
                        {{ position.toUpperCase() }}</option>
                </select>
            </div>

            <div class="form-group">
                <label for="searchInterval">Interval</label>
                <select v-model="searchInterval" class="form-control" id="searchInterval">
                    <option v-for="interval in intervals" :value="interval" :key="interval" :class="{ 'select-option': searchInterval === interval }">
                        {{ interval.toUpperCase() }}
                    </option>
                </select>
            </div>

            <div class="form-group">
                <label for="searchPoints">Points</label>
                <select v-model="searchPoints" class="form-control" id="searchPoints">
                    <option v-for="point in filteredPoints" :value="point" :key="point" :class="{ 'select-option': searchPoints === point }">
                        {{ point.toUpperCase() }}
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
                    <option v-for="category in categories" :value="category" :key="category" :class="{ 'select-option': searchCategory === category }">
                        {{ category.toUpperCase() }}
                    </option>
                </select>
            </div>

            <div class="form-group" v-if="searchCategory !== 'all' && searchCategory !== ''">
                <label for="searchTerm">Term</label>
                <select v-model="searchTerm" class="form-control" id="searchTerm" v-if="searchCategory === 'conference'">
                    <option v-for="conference in conferences" :value="conference" :key="conference" :class="{ 'select-option': searchCategory === conference }">
                        {{ conference.toUpperCase() }}
                    </option>
                </select>
                <select v-model="searchTerm" class="form-control" id="searchTerm" v-if="searchCategory === 'team'">
                    <option v-for="team in teams" :value="team" :key="team" :class="{ 'select-option': searchCategory === team }">
                        {{ team.toUpperCase() }}
                    </option>
                </select>
                <input v-model="searchTerm" type="text" class="form-control" id="searchTerm" v-if="searchCategory === 'name'">
            </div>

            <button type="submit">Apply Filters</button>
        </form>

        <div v-if="playerStats.length > 0">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <!-- @click="sortBy('playerID')" :class="{ 'sorted-asc': sortingColumn === 'playerID' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'playerID' && sortingOrder === 'desc' }" -->
                        <th scope="col" @click="sortBy('playerID')" :class="{ 'sorted-asc': sortingColumn === 'playerID' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'playerID' && sortingOrder === 'desc' }" v-if="columnsToShow.playerID">ID</th>
                        <th scope="col" v-if="columnsToShow.week">WK</th>
                        <th scope="col" v-if="columnsToShow.team">TM</th>
                        <th scope="col" v-if="columnsToShow.position">POS</th>
                        <th scope="col" v-if="columnsToShow.name">NAME</th>
                        <th scope="col" v-if="columnsToShow.passingCompletions">COMP</th>
                        <th scope="col" v-if="columnsToShow.passingAttempts">ATT</th>
                        <th scope="col" v-if="columnsToShow.passingCompletionPercentage">COMP %</th>
                        <th scope="col" v-if="columnsToShow.passingYards">YDS</th>
                        <th scope="col" v-if="columnsToShow.passingTouchdowns">TD</th>
                        <th scope="col" v-if="columnsToShow.passingInterceptions">INT</th>
                        <th scope="col" v-if="columnsToShow.passingRating">RTNG</th>
                        <th scope="col" v-if="columnsToShow.rushingAttempts">ATT</th>
                        <th scope="col" v-if="columnsToShow.rushingYards">YDS</th>
                        <th scope="col" v-if="columnsToShow.rushingYardsPerAttempt">YDS/ATT</th>
                        <th scope="col" v-if="columnsToShow.rushingTouchdowns">TD</th>
                        <th scope="col" v-if="columnsToShow.receivingTargets">TGT</th>
                        <th scope="col" v-if="columnsToShow.receptions">REC</th>
                        <th scope="col" v-if="columnsToShow.receivingYards">YDS</th>
                        <th scope="col" v-if="columnsToShow.receivingYardsPerReception">YDS/REC</th>
                        <th scope="col" v-if="columnsToShow.receivingTouchdowns">TD</th>
                        <th scope="col" v-if="columnsToShow.returnTouchdowns">RT TD</th>
                        <th scope="col" v-if="columnsToShow.twoPointConversions">2P</th>
                        <th scope="col" v-if="columnsToShow.usage">TEAM %</th>
                        <th scope="col" v-if="columnsToShow.fumblesLost">FL</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade">FGM</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsAttempted">FGA</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalPercentage">FG %</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade0to19">0-19</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade20to29">20-29</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade30to39">30-39</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade40to49">40-49</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade50Plus">50+</th>
                        <th scope="col" v-if="columnsToShow.extraPointsMade">XPM</th>
                        <th scope="col" v-if="columnsToShow.extraPointsAttempted">XPA</th>
                        <th scope="col" v-if="columnsToShow.extraPointPercentage">XP %</th>
                        <th scope="col" v-if="columnsToShow.defensiveTouchdowns">DEF TD</th>
                        <th scope="col" v-if="columnsToShow.specialTeamsTouchdowns">SPEC TD</th>
                        <th scope="col" v-if="columnsToShow.touchdownsScored">TD</th>
                        <th scope="col" v-if="columnsToShow.fumblesForced">FF</th>
                        <th scope="col" v-if="columnsToShow.fumblesRecovered">FR</th>
                        <th scope="col" v-if="columnsToShow.interceptions">INT</th>
                        <th scope="col" v-if="columnsToShow.tacklesForLoss">TFL</th>
                        <th scope="col" v-if="columnsToShow.quarterbackHits">QBH</th>
                        <th scope="col" v-if="columnsToShow.sacks">SACK</th>
                        <th scope="col" v-if="columnsToShow.safeties">SAF</th>
                        <th scope="col" v-if="columnsToShow.blockedKicks">BK</th>
                        <th scope="col" v-if="columnsToShow.pointsAllowed">PA</th>
                        <th scope="col" v-if="columnsToShow.fantasyPointsTotal">FP TOT</th>
                        <th scope="col" v-if="columnsToShow.fantasyPointsAverage">FP AVG</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- sortedPlayerStats -->
                    <tr v-for="(playerStat, index) in sortedPlayerStats" :key="index">
                        <td v-if="columnsToShow.playerID">{{ playerStat.playerID }}</td>
                        <td v-if="columnsToShow.week">{{ playerStat.week }}</td>
                        <td v-if="columnsToShow.team">{{ playerStat.team }}</td>
                        <td v-if="columnsToShow.position">{{ playerStat.position }}</td>
                        <td v-if="columnsToShow.name">{{ playerStat.name }}</td>
                        <td v-if="columnsToShow.passingCompletions">{{ playerStat.passingCompletions }}</td>
                        <td v-if="columnsToShow.passingAttempts">{{ playerStat.passingAttempts }}</td>
                        <td v-if="columnsToShow.passingCompletionPercentage">{{ playerStat.passingCompletionPercentage }}</td>
                        <td v-if="columnsToShow.passingYards">{{ playerStat.passingYards }}</td>
                        <td v-if="columnsToShow.passingTouchdowns">{{ playerStat.passingTouchdowns }}</td>
                        <td v-if="columnsToShow.passingInterceptions">{{ playerStat.passingInterceptions }}</td>
                        <td v-if="columnsToShow.passingRating">{{ playerStat.passingRating }}</td>
                        <td v-if="columnsToShow.rushingAttempts">{{ playerStat.rushingAttempts }}</td>
                        <td v-if="columnsToShow.rushingYards">{{ playerStat.rushingYards }}</td>
                        <td v-if="columnsToShow.rushingYardsPerAttempt">{{ playerStat.rushingYardsPerAttempt }}</td>
                        <td v-if="columnsToShow.rushingTouchdowns">{{ playerStat.rushingTouchdowns }}</td>
                        <td v-if="columnsToShow.receivingTargets">{{ playerStat.receivingTargets }}</td>
                        <td v-if="columnsToShow.receptions">{{ playerStat.receptions }}</td>
                        <td v-if="columnsToShow.receivingYards">{{ playerStat.receivingYards }}</td>
                        <td v-if="columnsToShow.receivingYardsPerReception">{{ playerStat.receivingYardsPerReception }}</td>
                        <td v-if="columnsToShow.receivingTouchdowns">{{ playerStat.receivingTouchdowns }}</td>
                        <td v-if="columnsToShow.returnTouchdowns">{{ playerStat.returnTouchdowns }}</td>
                        <td v-if="columnsToShow.twoPointConversions">{{ playerStat.twoPointConversions }}</td>
                        <td v-if="columnsToShow.usage">{{ playerStat.usage }}</td>
                        <td v-if="columnsToShow.fumblesLost">{{ playerStat.fumblesLost }}</td>
                        <td v-if="columnsToShow.fieldGoalsMade">{{ playerStat.fieldGoalsMade }}</td>
                        <td v-if="columnsToShow.fieldGoalsAttempted">{{ playerStat.fieldGoalsAttempted }}</td>
                        <td v-if="columnsToShow.fieldGoalPercentage">{{ playerStat.fieldGoalPercentage }}</td>
                        <td v-if="columnsToShow.fieldGoalsMade0to19">{{ playerStat.fieldGoalsMade0to19 }}</td>
                        <td v-if="columnsToShow.fieldGoalsMade20to29">{{ playerStat.fieldGoalsMade20to29 }}</td>
                        <td v-if="columnsToShow.fieldGoalsMade30to39">{{ playerStat.fieldGoalsMade30to39 }}</td>
                        <td v-if="columnsToShow.fieldGoalsMade40to49">{{ playerStat.fieldGoalsMade40to49 }}</td>
                        <td v-if="columnsToShow.fieldGoalsMade50Plus">{{ playerStat.fieldGoalsMade50Plus }}</td>
                        <td v-if="columnsToShow.extraPointsMade">{{ playerStat.extraPointsMade }}</td>
                        <td v-if="columnsToShow.extraPointsAttempted">{{ playerStat.extraPointsAttempted }}</td>
                        <td v-if="columnsToShow.extraPointPercentage">{{ playerStat.extraPointPercentage }}</td>
                        <td v-if="columnsToShow.defensiveTouchdowns">{{ playerStat.defensiveTouchdowns }}</td>
                        <td v-if="columnsToShow.specialTeamsTouchdowns">{{ playerStat.specialTeamsTouchdowns }}</td>
                        <td v-if="columnsToShow.touchdownsScored">{{ playerStat.touchdownsScored }}</td>
                        <td v-if="columnsToShow.fumblesForced">{{ playerStat.fumblesForced }}</td>
                        <td v-if="columnsToShow.fumblesRecovered">{{ playerStat.fumblesRecovered }}</td>
                        <td v-if="columnsToShow.interceptions">{{ playerStat.interceptions }}</td>
                        <td v-if="columnsToShow.tacklesForLoss">{{ playerStat.tacklesForLoss }}</td>
                        <td v-if="columnsToShow.quarterbackHits">{{ playerStat.quarterbackHits }}</td>
                        <td v-if="columnsToShow.sacks">{{ playerStat.sacks }}</td>
                        <td v-if="columnsToShow.safeties">{{ playerStat.safeties }}</td>
                        <td v-if="columnsToShow.blockedKicks">{{ playerStat.blockedKicks }}</td>
                        <td v-if="columnsToShow.pointsAllowed">{{ playerStat.pointsAllowed }}</td>
                        <td v-if="columnsToShow.fantasyPointsTotal">{{ playerStat.fantasyPointsTotal }}</td>
                        <td v-if="columnsToShow.fantasyPointsAverage">{{ playerStat.fantasyPointsAverage }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
            
        <div v-else>
            <p>No player stats found for the selected filters.</p>        
        </div>
    </div>
</template>

<script>
import StatsService from "../services/StatsService.js";

export default {
    data() {
        return {
            playerStats: [],
            searchPosition: "",
            positions: ["qb", "flex", "rb", "wr", "te", "k", "def"],
            searchInterval: "",
            intervals: ["season", "last4", "next4", "remaining", "weekly"],
            searchPoints: "",
            points: ["total", "average", "projected"],
            searchCategory: "",
            searchWeek: null,
            weeks: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18],
            categories: ["all", "conference", "team", "name"],
            searchTerm: "",
            conferences: ["afc", "nfc"],
            teams: ["ari", "atl", "bal", "buf", "car", "chi", "cin", "cle", "dal", "den", "det", "gb", "hou", "ind", "jax", "kc", "lac", "lar", "lv", "mia", "min", "ne", "no", "nyg", "nyj", "phi", "pit", "sea", "sf", "tb", "ten", "wsh"],
            sortingColumn: null,
            sortingOrder: 'desc',
            defaultSortingColumn: 'fantasyPointsTotal',
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
        sortedPlayerStats() {
            if (this.sortingColumn) {
                return this.playerStats.slice().sort((a, b) => {
                    const aValue = a[this.sortingColumn];
                    const bValue = b[this.sortingColumn];

                    if (this.sortingOrder === 'asc') {
                        return aValue > bValue ? 1 : -1;
                    } else {
                        return bValue > aValue ? 1 : -1;
                    }
                });
            }
            return this.playerStats.slice;
        },
    },
    
    methods: {
        searchPlayerStats() {
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
            }).catch(error => {
                console.log('Error:', error);
            });    
        },
        sortBy(columnName) {
            if (this.sortingColumn === columnName) {
                this.sortingOrder = this.sortingOrder === 'asc' ? 'desc' : 'desc';
            } else {
                this.sortingColumn = columnName;
                this.sortingOrder = 'desc';
            }

            this.playerStats.sort((a, b) => {
                const aValue = a[this.sortingColumn];
                const bValue = b[this.sortingColumn];

                if (this.sortingOrder === 'asc') {
                    return aValue > bValue ? 1 : -1;
                } else {
                    return bValue > aValue ? 1 : -1;
                }
            });
        },
    },

    created() {
        this.searchPlayerStats();
        this.sortBy(this.defaultSortingColumn);
    },

}

</script>

<style scoped>
.form-group {
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

.select-option:hover {
    background-color: #407F7F;
    color: #ccc;
}

.custom-select:hover {
    background-color:#407F7F;
    color: #ccc;
}

button {
  width: 100%;
  margin-top: 10px;
}


table {
    margin-top: 10px;
    width: 100%;
    overflow: auto;
}

th {
    background-color: #343a40;
    color: white;
    font-weight: bold;
    font-size: 13px;
    padding: 10px 0px;
}

tr:nth-child(even) {
    background-color: #f2f2f2;
}

tr:nth-child(odd) {
    background-color: #ffffff;
}

td {
    padding-top: 10px;
    border: 1px solid #ddd;
}

.sorted-asc::after {
    content: " ▲";
}

.sorted-desc::after {
    content: " ▼";
}

</style>
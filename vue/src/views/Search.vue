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
                    <option v-for="point in points" :value="point" :key="point" :class="{ 'select-option': searchPoints === point }">
                        {{ point.toUpperCase() }}
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
            <div class="form-group">
                <label for="searchTerm">Term</label>
                <input v-model="searchTerm" type="text" class="form-control" id="searchTerm">
            </div>
            <div class="form-group">
                <label for="searchWeek">Week</label>
                <input v-model="searchWeek" type="number" class="form-control" id="searchWeek">
            </div>
            <button type="submit">Apply Filters</button>
        </form>

        <div v-if="playerStats.length > 0">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th scope="col" @click="sortBy('playerID')" v-if="columnsToShow.playerID">ID</th>
                        <th scope="col" @click="sortBy('week')" v-if="columnsToShow.week">WK</th>
                        <th scope="col" @click="sortBy('team')" v-if="columnsToShow.team">TM</th>
                        <th scope="col" @click="sortBy('position')" v-if="columnsToShow.position">POS</th>
                        <th scope="col" @click="sortBy('name')" v-if="columnsToShow.name">NAME</th>
                        <th scope="col" @click="sortBy('passingCompletions')" v-if="columnsToShow.passingCompletions">COMP</th>
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
                    <tr v-for="playerStat in sortedPlayerStats" :key="playerStat.playerID">
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
            categories: ["all", "conference", "team", "name"],
            searchTerm: null,
            searchWeek: null,
            sortingColumn: null,
            sortingOrder: 'asc'
        }
    },
    computed: {
        sortedPlayerStats() {
            if (!this.sortingColumn) {
                return this.playerStats;
            }

            const sortedData = [...this.playerStats];

            sortedData.sort((a, b) => {
                if (this.sortingOrder === 'asc') {
                    return a[this.sortingColumn] > b[this.sortingColumn];
                } else {
                    return a[this.sortingColumn] < b[this.sortingColumn];
                }
            });

            return sortedData;
        }
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
        }

    },
    sortBy(columnName) {
        if (this.sortingColumn === columnName) {
            this.sortingOrder = this.sortingOrder === 'asc' ? 'desc' : 'asc';
        } else {
            this.sortingColumn = columnName;
            this.sortingOrder = 'asc';
        }

        this.sortingOrder = this.sortingOrder === 'asc' ? 1 : -1;
        this.playerStats.sort((a, b) => {
            return this.sortingOrder * (a[this.sortingColumn] - b[this.sortingColumn] ? 1 : -1);
        })
    }
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

</style>
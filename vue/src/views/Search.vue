<template>
    <div>
        <form @submit.prevent="searchPlayerStats">
            <div class="form-group">
                <label for="searchPosition">Position</label>
                <select v-model="searchPosition" class="form-control" id="searchPosition">
                    <option value="qb">QB</option>
                    <option value="flex">FLEX</option>
                    <option value="rb">RB</option>
                    <option value="wr">WR</option>
                    <option value="te">TE</option>
                    <option value="k">K</option>
                    <option value="def">DEF</option>
                </select>
            </div>
            <div class="form-group">
                <label for="searchInterval">Interval</label>
                <select v-model="searchInterval" class="form-control" id="searchInterval">
                    <option value="season">Season</option>
                    <option value="last4">Last 4</option>
                    <option value="next4">Next 4</option>
                    <option value="remaining">Remaining</option>
                    <option value="weekly">Weekly</option>
                </select>
            </div>
            <div class="form-group">
                <label for="searchPoints">Points</label>
                <select v-model="searchPoints" class="form-control" id="searchPoints">
                    <option value="total">Total</option>
                    <option value="average">Average</option>
                    <option value="projected">Projected</option>
                </select>
            </div>
            <div class="form-group">
                <label for="searchCategory">Category</label>
                <select v-model="searchCategory" class="form-control" id="searchCategory">
                    <option value="all">All</option>
                    <option value="conference">Conference</option>
                    <option value="team">Team</option>
                    <option value="name">Name</option>
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
                        <th scope="col" v-if="columnsToShow.playerID">ID</th>
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
                        <th scope="col" v-if="columnsToShow.returnTouchdowns">Return Touchdowns</th>
                        <th scope="col" v-if="columnsToShow.twoPointConversions">Two Point Conversions</th>
                        <th scope="col" v-if="columnsToShow.usage">Usage</th>
                        <th scope="col" v-if="columnsToShow.fumblesLost">Fumbles Lost</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade">Field Goals Made</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsAttempted">Field Goals Attempted</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalPercentage">Field Goal Percentage</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade0to19">Field Goals Made 0 to 19</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade20to29">Field Goals Made 20 to 29</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade30to39">Field Goals Made 30 to 39</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade40to49">Field Goals Made 40 to 49</th>
                        <th scope="col" v-if="columnsToShow.fieldGoalsMade50Plus">Field Goals Made 50 Plus</th>
                        <th scope="col" v-if="columnsToShow.extraPointsMade">Extra Points Made</th>
                        <th scope="col" v-if="columnsToShow.extraPointsAttempted">Extra Points Attempted</th>
                        <th scope="col" v-if="columnsToShow.extraPointPercentage">Extra Point Percentage</th>
                        <th scope="col" v-if="columnsToShow.defensiveTouchdowns">Defensive Touchdowns</th>
                        <th scope="col" v-if="columnsToShow.specialTeamsTouchdowns">Special Teams Touchdowns</th>
                        <th scope="col" v-if="columnsToShow.touchdownsScored">Touchdowns Scored</th>
                        <th scope="col" v-if="columnsToShow.fumblesForced">Fumbles Forced</th>
                        <th scope="col" v-if="columnsToShow.fumblesRecovered">Fumbles Recovered</th>
                        <th scope="col" v-if="columnsToShow.interceptions">Interceptions</th>
                        <th scope="col" v-if="columnsToShow.tacklesForLoss">Tackles For Loss</th>
                        <th scope="col" v-if="columnsToShow.quarterbackHits">Quarterback Hits</th>
                        <th scope="col" v-if="columnsToShow.sacks">Sacks</th>
                        <th scope="col" v-if="columnsToShow.safeties">Safeties</th>
                        <th scope="col" v-if="columnsToShow.blockedKicks">Blocked Kicks</th>
                        <th scope="col" v-if="columnsToShow.pointsAllowed">Points Allowed</th>
                        <th scope="col" v-if="columnsToShow.fantasyPointsTotal">Fantasy Points Total</th>
                        <th scope="col" v-if="columnsToShow.fantasyPointsAverage">Fantasy Points Average</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="playerStat in playerStats" :key="playerStat.playerID">
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
            searchInterval: "",
            searchPoints: "",
            searchCategory: "",
            searchTerm: null,
            searchWeek: null,
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

button {
  width: 100%;
  margin-top: 10px;
}

</style>
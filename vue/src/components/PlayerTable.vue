<template>
    <div>
        <div v-if="playerStats.length > 0">
            <table class="table table-striped">
                <thead>
                    <tr class="table-headers">
                        <th colspan="5">Description</th>
                        <th v-if="shouldShowQuarterbackHeaders" colspan="7">Passing</th>
                        <th v-if="shouldShowQuarterbackHeaders" colspan="3">Rushing</th>
                        <th v-if="shouldShowQuarterbackHeaders" colspan="4">Miscellaneous</th>
                        <th v-if="shouldShowFlexHeaders" colspan="4">Rushing</th>
                        <th v-if="shouldShowFlexHeaders" colspan="5">Receiving</th>
                        <th v-if="shouldShowFlexHeaders" colspan="6">Miscellaneous</th>
                        <th v-if="shouldShowKickersHeaders" colspan="8">Field Goal</th>
                        <th v-if="shouldShowKickersHeaders" colspan="3">Extra Point</th>
                        <th v-if="shouldShowKickersHeaders" colspan="2">Miscellaneous</th>
                        <th v-if="shouldShowDefenseHeaders" colspan="3">Touchdown</th>
                        <th v-if="shouldShowDefenseHeaders" colspan="8">Defense</th>
                        <th v-if="shouldShowDefenseHeaders" colspan="3">Miscellaneous</th>
                    </tr>
                    <tr class="table-columns">
                        <th scope="col" @click="sortBy('playerID')" :class="{ 'sorted-asc': sortingColumn === 'playerID' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'playerID' && sortingOrder === 'desc' }" v-if="columnsToShow.playerID">ID</th>
                        <th scope="col" @click="sortBy('week')" :class="{ 'sorted-asc': sortingColumn === 'week' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'week' && sortingOrder === 'desc' }" v-if="columnsToShow.week">WK</th>
                        <th scope="col" @click="sortBy('team')" :class="{ 'sorted-asc': sortingColumn === 'team' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'team' && sortingOrder === 'desc' }" v-if="columnsToShow.team">TM</th>
                        <th scope="col" @click="sortBy('position')" :class="{ 'sorted-asc': sortingColumn === 'position' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'position' && sortingOrder === 'desc' }" v-if="columnsToShow.position">POS</th>
                        <th scope="col" @click="sortBy('name')" :class="{ 'sorted-asc': sortingColumn === 'name' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'name' && sortingOrder === 'desc' }" v-if="columnsToShow.name">NAME</th>
                        <th scope="col" @click="sortBy('passingCompletions')" :class="{ 'sorted-asc': sortingColumn === 'passingCompletions' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingCompletions' && sortingOrder === 'desc' }" v-if="columnsToShow.passingCompletions">COMP</th>
                        <th scope="col" @click="sortBy('passingAttempts')" :class="{ 'sorted-asc': sortingColumn === 'passingAttempts' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingAttempts' && sortingOrder === 'desc' }" v-if="columnsToShow.passingAttempts">ATT</th>
                        <th scope="col" @click="sortBy('passingCompletionPercentage')" :class="{ 'sorted-asc': sortingColumn === 'passingCompletionPercentage' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingCompletionPercentage' && sortingOrder === 'desc' }" v-if="columnsToShow.passingCompletionPercentage">COMP %</th>
                        <th scope="col" @click="sortBy('passingYards')" :class="{ 'sorted-asc': sortingColumn === 'passingYards' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingYards' && sortingOrder === 'desc' }" v-if="columnsToShow.passingYards">YDS</th>
                        <th scope="col" @click="sortBy('passingTouchdowns')" :class="{ 'sorted-asc': sortingColumn === 'passingTouchdowns' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingTouchdowns' && sortingOrder === 'desc' }" v-if="columnsToShow.passingTouchdowns">TD</th>
                        <th scope="col" @click="sortBy('passingInterceptions')" :class="{ 'sorted-asc': sortingColumn === 'passingInterceptions' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingInterceptions' && sortingOrder === 'desc' }" v-if="columnsToShow.passingInterceptions">INT</th>
                        <th scope="col" @click="sortBy('passingRating')" :class="{ 'sorted-asc': sortingColumn === 'passingRating' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'passingRating' && sortingOrder === 'desc' }" v-if="columnsToShow.passingRating">RTNG</th>
                        <th scope="col" @click="sortBy('rushingAttempts')" :class="{ 'sorted-asc': sortingColumn === 'rushingAttempts' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'rushingAttempts' && sortingOrder === 'desc' }" v-if="columnsToShow.rushingAttempts">ATT</th>
                        <th scope="col" @click="sortBy('rushingYards')" :class="{ 'sorted-asc': sortingColumn === 'rushingYards' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'rushingYards' && sortingOrder === 'desc' }" v-if="columnsToShow.rushingYards">YDS</th>
                        <th scope="col" @click="sortBy('rushingYardsPerAttempt')" :class="{ 'sorted-asc': sortingColumn === 'rushingYardsPerAttempt' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'rushingYardsPerAttempt' && sortingOrder === 'desc' }" v-if="columnsToShow.rushingYardsPerAttempt">YDS/ATT</th>
                        <th scope="col" @click="sortBy('rushingTouchdowns')" :class="{ 'sorted-asc': sortingColumn === 'rushingTouchdowns' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'rushingTouchdowns' && sortingOrder === 'desc' }" v-if="columnsToShow.rushingTouchdowns">TD</th>
                        <th scope="col" @click="sortBy('receivingTargets')" :class="{ 'sorted-asc': sortingColumn === 'receivingTargets' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'receivingTargets' && sortingOrder === 'desc' }" v-if="columnsToShow.receivingTargets">TGT</th>
                        <th scope="col" @click="sortBy('receptions')" :class="{ 'sorted-asc': sortingColumn === 'receptions' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'receptions' && sortingOrder === 'desc' }" v-if="columnsToShow.receptions">REC</th>
                        <th scope="col" @click="sortBy('receivingYards')" :class="{ 'sorted-asc': sortingColumn === 'receivingYards' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'receivingYards' && sortingOrder === 'desc' }" v-if="columnsToShow.receivingYards">YDS</th>
                        <th scope="col" @click="sortBy('receivingYardsPerReception')" :class="{ 'sorted-asc': sortingColumn === 'receivingYardsPerReception' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'receivingYardsPerReception' && sortingOrder === 'desc' }" v-if="columnsToShow.receivingYardsPerReception">YDS/REC</th>
                        <th scope="col" @click="sortBy('receivingTouchdowns')" :class="{ 'sorted-asc': sortingColumn === 'receivingTouchdowns' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'receivingTouchdowns' && sortingOrder === 'desc' }" v-if="columnsToShow.receivingTouchdowns">TD</th>
                        <th scope="col" @click="sortBy('usage')" :class="{ 'sorted-asc': sortingColumn === 'usage' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'usage' && sortingOrder === 'desc' }" v-if="columnsToShow.usage">TEAM %</th>
                        <th scope="col" @click="sortBy('returnTouchdowns')" :class="{ 'sorted-asc': sortingColumn === 'returnTouchdowns' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'returnTouchdowns' && sortingOrder === 'desc' }" v-if="columnsToShow.returnTouchdowns">RT TD</th>
                        <th scope="col" @click="sortBy('twoPointConversions')" :class="{ 'sorted-asc': sortingColumn === 'twoPointConversions' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'twoPointConversions' && sortingOrder === 'desc' }" v-if="columnsToShow.twoPointConversions">2P</th>
                        <th scope="col" @click="sortBy('fumblesLost')" :class="{ 'sorted-asc': sortingColumn === 'fumblesLost' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fumblesLost' && sortingOrder === 'desc' }" v-if="columnsToShow.fumblesLost">FL</th>
                        <th scope="col" @click="sortBy('fieldGoalsMade')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsMade' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsMade' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsMade">FGM</th>
                        <th scope="col" @click="sortBy('fieldGoalsAttempted')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsAttempted' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsAttempted' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsAttempted">FGA</th>
                        <th scope="col" @click="sortBy('fieldGoalPercentage')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalPercentage' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalPercentage' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalPercentage">FG %</th>
                        <th scope="col" @click="sortBy('fieldGoalsMade0to19')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsMade0to19' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsMade0to19' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsMade0to19">0-19</th>
                        <th scope="col" @click="sortBy('fieldGoalsMade20to29')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsMade20to29' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsMade20to29' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsMade20to29">20-29</th>
                        <th scope="col" @click="sortBy('fieldGoalsMade30to39')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsMade30to39' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsMade30to39' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsMade30to39">30-39</th>
                        <th scope="col" @click="sortBy('fieldGoalsMade40to49')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsMade40to49' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsMade40to49' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsMade40to49">40-49</th>
                        <th scope="col" @click="sortBy('fieldGoalsMade50Plus')" :class="{ 'sorted-asc': sortingColumn === 'fieldGoalsMade50Plus' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fieldGoalsMade50Plus' && sortingOrder === 'desc' }" v-if="columnsToShow.fieldGoalsMade50Plus">50+</th>
                        <th scope="col" @click="sortBy('extraPointsMade')" :class="{ 'sorted-asc': sortingColumn === 'extraPointsMade' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'extraPointsMade' && sortingOrder === 'desc' }" v-if="columnsToShow.extraPointsMade">XPM</th>
                        <th scope="col" @click="sortBy('extraPointsAttempted')" :class="{ 'sorted-asc': sortingColumn === 'extraPointsAttempted' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'extraPointsAttempted' && sortingOrder === 'desc' }" v-if="columnsToShow.extraPointsAttempted">XPA</th>
                        <th scope="col" @click="sortBy('extraPointPercentage')" :class="{ 'sorted-asc': sortingColumn === 'extraPointPercentage' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'extraPointPercentage' && sortingOrder === 'desc' }" v-if="columnsToShow.extraPointPercentage">XP %</th>
                        <th scope="col" @click="sortBy('defensiveTouchdowns')" :class="{ 'sorted-asc': sortingColumn === 'defensiveTouchdowns' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'defensiveTouchdowns' && sortingOrder === 'desc' }" v-if="columnsToShow.defensiveTouchdowns">DEF TD</th>
                        <th scope="col" @click="sortBy('specialTeamsTouchdowns')" :class="{ 'sorted-asc': sortingColumn === 'specialTeamsTouchdowns' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'specialTeamsTouchdowns' && sortingOrder === 'desc' }" v-if="columnsToShow.specialTeamsTouchdowns">SPEC TD</th>
                        <th scope="col" @click="sortBy('touchdownsScored')" :class="{ 'sorted-asc': sortingColumn === 'touchdownsScored' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'touchdownsScored' && sortingOrder === 'desc' }" v-if="columnsToShow.touchdownsScored">TD</th>
                        <th scope="col" @click="sortBy('fumblesForced')" :class="{ 'sorted-asc': sortingColumn === 'fumblesForced' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fumblesForced' && sortingOrder === 'desc' }" v-if="columnsToShow.fumblesForced">FF</th>
                        <th scope="col" @click="sortBy('fumblesRecovered')" :class="{ 'sorted-asc': sortingColumn === 'fumblesRecovered' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fumblesRecovered' && sortingOrder === 'desc' }" v-if="columnsToShow.fumblesRecovered">FR</th>
                        <th scope="col" @click="sortBy('interceptions')" :class="{ 'sorted-asc': sortingColumn === 'interceptions' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'interceptions' && sortingOrder === 'desc' }" v-if="columnsToShow.interceptions">INT</th>
                        <th scope="col" @click="sortBy('tacklesForLoss')" :class="{ 'sorted-asc': sortingColumn === 'tacklesForLoss' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'tacklesForLoss' && sortingOrder === 'desc' }" v-if="columnsToShow.tacklesForLoss">TFL</th>
                        <th scope="col" @click="sortBy('quarterbackHits')" :class="{ 'sorted-asc': sortingColumn === 'quarterbackHits' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'quarterbackHits' && sortingOrder === 'desc' }" v-if="columnsToShow.quarterbackHits">QBH</th>
                        <th scope="col" @click="sortBy('sacks')" :class="{ 'sorted-asc': sortingColumn === 'sacks' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'sacks' && sortingOrder === 'desc' }" v-if="columnsToShow.sacks">SACK</th>
                        <th scope="col" @click="sortBy('safeties')" :class="{ 'sorted-asc': sortingColumn === 'safeties' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'safeties' && sortingOrder === 'desc' }" v-if="columnsToShow.safeties">SAF</th>
                        <th scope="col" @click="sortBy('blockedKicks')" :class="{ 'sorted-asc': sortingColumn === 'blockedKicks' && sortingOrder === 'asc', 'blockedKicks-desc': sortingColumn === 'blockedKicks' && sortingOrder === 'desc' }" v-if="columnsToShow.blockedKicks">BK</th>
                        <th scope="col" @click="sortBy('pointsAllowed')" :class="{ 'sorted-asc': sortingColumn === 'pointsAllowed' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'pointsAllowed' && sortingOrder === 'desc' }" v-if="columnsToShow.pointsAllowed">PA</th>
                        <th scope="col" @click="sortBy('fantasyPointsTotal')" :class="{ 'sorted-asc': sortingColumn === 'fantasyPointsTotal' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fantasyPointsTotal' && sortingOrder === 'desc' }" v-if="columnsToShow.fantasyPointsTotal">FP TOT</th>
                        <th scope="col" @click="sortBy('fantasyPointsAverage')" :class="{ 'sorted-asc': sortingColumn === 'fantasyPointsAverage' && sortingOrder === 'asc', 'sorted-desc': sortingColumn === 'fantasyPointsAverage' && sortingOrder === 'desc' }" v-if="columnsToShow.fantasyPointsAverage">FP AVG</th>
                    </tr>
                </thead>
                <tbody>
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
                        <td v-if="columnsToShow.usage">{{ playerStat.usage }}</td>
                        <td v-if="columnsToShow.returnTouchdowns">{{ playerStat.returnTouchdowns }}</td>
                        <td v-if="columnsToShow.twoPointConversions">{{ playerStat.twoPointConversions }}</td>
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
// import StatsService from "../services/StatsService.js";

export default {
    props: {
            playerStatsProp: Array,
    },

    data() {
        return {
            playerStats: [...this.playerStatsProp],
            sortingColumn: null,
            sortingOrder: 'asc',
            defaultSortingColumn: 'fantasyPointsTotal',
        }
    },

    

    computed: {
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

        columnsToShowCategories() {
            if (this.searchPosition === "qb") {
                return {
                    passing: true,
                    rushing: true,
                    miscellaneous: true,
                };
            } else if (this.searchPosition === "flex" || this.searchPosition === "rb" || this.searchPosition === "wr" || this.searchPosition === "te") {
                return {
                    rushing: true,
                    receiving: true,
                    miscellaneous: true,
                };
            } else if (this.searchPosition === "k") {
                return {
                    fieldGoal: true,
                    extraPoint: true,
                    miscellaneous: true,
                };
            } else if (this.searchPosition === "def") {
                return {
                    touchdown: true,
                    defense: true,
                    miscellaneous: true,
                };
            } else {
                return {
                    passing: false,
                    rushing: false,
                    receiving: false,
                    fieldGoal: false,
                    extraPoint: false,
                    touchdown: false,
                    defense: false,
                    miscellaneous: false,
                };
            }
        },
    }, 

    methods: {
        searchPlayerStats() {
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
        },

        sortBy(columnName) {
            if (this.sortingColumn === columnName) {
                this.sortingOrder = this.sortingOrder === 'asc' ? 'desc' : 'asc';
            } else {
                this.sortingColumn = columnName;
                this.sortingOrder = 'asc';
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
        this.$root.$on('player-stats-updated', (data) => {
            this.playerStats = data;
        })
        this.searchPlayerStats();
        this.sortBy(this.defaultSortingColumn);
    },
}
</script>

<style scoped>
table {
    margin-top: 10px;
    width: 100%;
    overflow: auto;
}

th {
    background-color: #343a40;
    color: white;
    font-weight: bold;
    padding: 10px 0px;
}

.table-headers{
    font-size: 15px;
}
.table-columns {
    cursor: pointer;
    font-size: 13px;
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
    font-size: 15px;
}

.sorted-asc::after {
    content: " ▲";
}

.sorted-desc::after {
    content: " ▼";
}

</style>
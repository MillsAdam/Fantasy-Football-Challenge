<template>
    <div>
        <SearchForm @search="searchPlayerStats" />
        <PlayerTable :playerStats="playerStats" />
    </div>
</template>

<script> 
import SearchForm from '@/components/SearchForm.vue';
import PlayerTable from '@/components/PlayerTable.vue';
import StatsService from '@/services/StatsService.js';

export default {
    data() {
        return {
            playerStats: []
        }
    },

    methods: {
        searchPlayerStats(searchParams) {
            console.log(searchParams);
            StatsService.searchPlayerStats(searchParams).then(response => {
                console.log('Response:', response.data)
                this.playerStats = response.data;
                this.$emit('player-stats-updated', response.data);  
            }).catch(error => {
                console.log('Error:', error);
            }); 
        }
    },

    components: {
        SearchForm, 
        PlayerTable
    }
}
</script>
import axios from 'axios';

const DatabaseService = {
    async searchPlayersName(playerName) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/name?playerName=${playerName}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async searchPlayersTeam(teamName) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/team?teamName=${teamName}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async searchPlayersPosition(position) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/position?position=${position}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createTeams() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/teams`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayers() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async upsertPlayers() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/upsert`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerStats() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/stats`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async upsertPlayerStatsByWeek() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/stats/week`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerProjections() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/projections`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async upsertPlayerProjectionsByWeek() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/projections/week`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerStatsExt() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/stats/ext`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async upsertPlayerStatsExtByWeek() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/stats/ext/week`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerProjectionsExt() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/projections/ext`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async upsertPlayerProjectionsExtByWeek() {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/players/projections/ext/week`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updateLineupScores() {
        try {
            const response = await axios.put(`https://fantasyplayoffchallenge.azurewebsites.net/api/scores/lineups`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updateRosterScores() {
        try {
            const response = await axios.put(`https://fantasyplayoffchallenge.azurewebsites.net/api/scores/rosters`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updateConfiguration(configuration) {
        try {
            const response = await axios.put(`https://fantasyplayoffchallenge.azurewebsites.net/api/configuration`, configuration);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getConfiguration() {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/configuration`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async ToggleTeamStatus(teamName) {
        try {
            const response = await axios.put(`https://fantasyplayoffchallenge.azurewebsites.net/api/teams?teamName=${teamName}`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getTeams() {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/teams`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getActiveTeams() {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/teams/active`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    }
}

export default DatabaseService;
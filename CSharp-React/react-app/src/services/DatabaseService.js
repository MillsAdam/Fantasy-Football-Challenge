import axios from 'axios';

const DatabaseService = {
    async searchPlayersName(playerName) {
        try {
            const response = await axios.get(`http://localhost:5000/api/players/name?playerName=${playerName}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async searchPlayersTeam(teamName) {
        try {
            const response = await axios.get(`http://localhost:5000/api/players/team?teamName=${teamName}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async searchPlayersPosition(position) {
        try {
            const response = await axios.get(`http://localhost:5000/api/players/position?position=${position}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createTeams() {
        try {
            const response = await axios.post(`http://localhost:5000/api/teams`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayers() {
        try {
            const response = await axios.post(`http://localhost:5000/api/players`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updatePlayers() {
        try {
            const response = await axios.put(`http://localhost:5000/api/players`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerStats() {
        try {
            const response = await axios.post(`http://localhost:5000/api/players/stats`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updatePlayerStats() {
        try {
            const response = await axios.put(`http://localhost:5000/api/players/stats`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerProjections() {
        try {
            const response = await axios.post(`http://localhost:5000/api/players/projections`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updatePlayerProjections() {
        try {
            const response = await axios.put(`http://localhost:5000/api/players/projections`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerStatsExt() {
        try {
            const response = await axios.post(`http://localhost:5000/api/players/stats/ext`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updatePlayerStatsExt() {
        try {
            const response = await axios.put(`http://localhost:5000/api/players/stats/ext`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createPlayerProjectionsExt() {
        try {
            const response = await axios.post(`http://localhost:5000/api/players/projections/ext`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updatePlayerProjectionsExt() {
        try {
            const response = await axios.put(`http://localhost:5000/api/players/projections/ext`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updateLineupScores() {
        try {
            const response = await axios.put(`http://localhost:5000/api/scores/lineups`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updateRosterScores() {
        try {
            const response = await axios.put(`http://localhost:5000/api/scores/rosters`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async updateConfiguration(configuration) {
        try {
            const response = await axios.put(`http://localhost:5000/api/configuration`, configuration);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getConfiguration() {
        try {
            const response = await axios.get(`http://localhost:5000/api/configuration`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async ToggleTeamStatus(teamName) {
        try {
            const response = await axios.put(`http://localhost:5000/api/teams?teamName=${teamName}`, {});
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getTeams() {
        try {
            const response = await axios.get(`http://localhost:5000/api/teams`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getActiveTeams() {
        try {
            const response = await axios.get(`http://localhost:5000/api/teams/active`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    }
}

export default DatabaseService;
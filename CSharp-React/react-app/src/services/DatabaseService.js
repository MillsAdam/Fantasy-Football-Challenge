import axios from 'axios';

const DatabaseService = {
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

    async searchPlayers(playerName) {
        try {
            const response = await axios.get(`http://localhost:5000/api/players?playerName=${playerName}`);
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

    async createPlayerProjections() {
        try {
            const response = await axios.post(`http://localhost:5000/api/players/projections`, {});
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
    }
}

export default DatabaseService;
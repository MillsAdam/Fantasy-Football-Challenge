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
}

export default DatabaseService;
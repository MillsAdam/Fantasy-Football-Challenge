import axios from 'axios';

const LineupService = {
    async getLineupPlayersByUser(authToken) {
        try {
            const response = await axios.get('http://localhost:5000/api/lineupplayers', {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createLineupPlayer(playerId, lineupPosition, authToken) {
        try {
            const response = await axios.post(`http://localhost:5000/api/lineupplayers?playerId=${playerId}&lineupPosition=${lineupPosition}`, {}, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async deleteLineupPlayer(playerId, authToken) {
        try {
            const response = await axios.delete(`http://localhost:5000/api/lineupplayers?playerId=${playerId}`, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    }
}

export default LineupService;
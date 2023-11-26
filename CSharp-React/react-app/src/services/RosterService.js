import axios from 'axios';

const RosterService = {
    async getRosterPlayersByUser(authToken) {
        try {
            const response = await axios.get('http://localhost:5000/api/rosterplayers', {
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

    async createRosterPlayer(playerId, authToken) {
        try {
            const response = await axios.post(`http://localhost:5000/api/rosterplayers?playerId=${playerId}`, {}, {
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

    async deleteRosterPlayer(playerId, authToken) {
        try {
            const response = await axios.delete(`http://localhost:5000/api/rosterplayers?playerId=${playerId}`, {
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

    async updateRosterPlayer(oldPlayerId, newPlayerId, authToken) {
        try {
            const response = await axios.put(`http://localhost:5000/api/rosterplayers?oldPlayerId=${oldPlayerId}&newPlayerId=${newPlayerId}`, {}, {
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
};

export default RosterService;
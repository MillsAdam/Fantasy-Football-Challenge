import axios from 'axios';

const RosterService = {
    async getRosterPlayersByUser(authToken) {
        try {
            const response = await axios.get('https://fantasyplayoffchallenge.azurewebsites.net/api/rosterplayers', {
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

    async getRosterPlayersByUserId(userId) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/rosterplayers/league?userId=${userId}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createRosterPlayer(playerId, authToken) {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/rosterplayers?playerId=${playerId}`, {}, {
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
            const response = await axios.delete(`https://fantasyplayoffchallenge.azurewebsites.net/api/rosterplayers?playerId=${playerId}`, {
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
            const response = await axios.put(`https://fantasyplayoffchallenge.azurewebsites.net/api/rosterplayers?oldPlayerId=${oldPlayerId}&newPlayerId=${newPlayerId}`, {}, {
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
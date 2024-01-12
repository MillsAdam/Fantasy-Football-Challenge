import axios from 'axios';

const LineupService = {
    async getLineupPlayersByUser(authToken) {
        try {
            const response = await axios.get('https://fantasyplayoffchallenge.azurewebsites.net/api/lineupplayers', {
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

    async getLineupPlayersByWeek(authToken, week) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/lineupplayers/week?gameWeek=${week}`, {
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

    async getLineupPlayersByUserIdAndWeek(userId, week) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/lineupplayers/league?userId=${userId}&gameWeek=${week}`);
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async getWeeklyScoreByWeek(authToken, week) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/fantasylineups/score?gameWeek=${week}`, {
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
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/lineupplayers?playerId=${playerId}&lineupPosition=${lineupPosition}`, {}, {
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
            const response = await axios.delete(`https://fantasyplayoffchallenge.azurewebsites.net/api/lineupplayers?playerId=${playerId}`, {
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
import axios from 'axios';

const LeagueService = {
    async getFantasyRosters() {
        try {
            const response = await axios.get('http://localhost:5000/api/fantasyrosters');
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    },

    async createRoster(teamName, authToken) {
        try {
            const response = await axios.post(`http://localhost:5000/api/fantasyrosters?teamName=${teamName}`, {}, {
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

export default LeagueService;
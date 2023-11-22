import axios from 'axios';

const LeagueService = {
    getFantasyRosters() {
        return axios.get('http://localhost:5000/api/fantasyrosters')
            .then(response => response.data)
            .catch(error => {
                console.error('An error occurred: ', error);
                throw error;
            });
    },

    createRoster(teamName, authToken) {
        return axios.post(`http://localhost:5000/api/fantasyrosters?teamName=${teamName}`, {}, {
            headers: {
                Authorization: `Bearer ${authToken}`
            },
        })
            .then(response => response.data)
            .catch(error => {
                console.error('An error occurred: ', error);
                throw error;
            });
    },
};

export default LeagueService;
import axios from 'axios';

const LeagueService = {
    async getFantasyRosters(authToken) {
        try {
            const response = await axios.get('https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyrosters', {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error getting fantasy rosters: ', error);
            throw error;
        }
    },

    async createRoster(teamName, authToken) {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyrosters?teamName=${teamName}`, {}, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error creating roster: ', error);
            throw error;
        }
    },

    async registerFantasyLeague(leagueName, leaguePassword, authToken) {
        try {
            const response = await axios.post('https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyleagues/register', {
                LeagueName: leagueName,
                LeaguePassword: leaguePassword,
            }, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            throw error;
        }
    },

    async searchFantasyLeagues(leagueName, authToken) {
        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyleagues/search?leagueName=${leagueName}`, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error searching leagues: ', error);
            throw error;
        }
    },

    async joinFantasyLeague(leagueId, leaguePassword, authToken) {
        try {
            const response = await axios.post(`https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyleagues/join`, {
                FantasyLeagueId: leagueId,
                LeaguePassword: leaguePassword,
            }, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error joining league: ', error);
            throw error;
        }
    },

    async getFantasyLeagues(authToken) {
        try {
            const response = await axios.get('https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyleagues/myleagues', {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error fetching leagues: ', error);
            throw error;
        }
    },

    async setCurrentLeague(leagueId, authToken) {
        try {
            const response = await axios.put(`https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyleagues/setcurrentleague?fantasyLeagueId=${leagueId}`, {}, {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error setting current league: ', error);
            throw error;
        }
    },

    async getCurrentLeagueId(authToken) {
        try {
            const response = await axios.get('https://fantasyplayoffchallenge.azurewebsites.net/api/fantasyleagues/currentleagueid', {
                headers: {
                    Authorization: `Bearer ${authToken}`
                },
            });
            return response.data;
        } catch (error) {
            console.error('Error getting current league: ', error);
            throw error;
        }
    },
};

export default LeagueService;
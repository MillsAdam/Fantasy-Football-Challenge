import axios from 'axios';

const StatsService = {
    async searchPlayerStats({ position, interval, category, filter, week}) {
        const params = {
            Position: position,
            Interval: interval,
            Category: category,
            Filter: filter,
            Week: week
        };

        try {
            const response = await axios.get(`https://fantasyplayoffchallenge.azurewebsites.net/api/playerstats`, { params });
            return response.data;
        } catch (error) {
            console.error('An error occurred: ', error);
            throw error;
        }
    }
};

export default StatsService;
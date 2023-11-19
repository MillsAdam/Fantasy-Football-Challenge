import axios from "axios";

const http = axios.create({
    baseURL: 'http://localhost:9000'
})

export default {
    searchPlayerStats({ searchPosition, searchInterval, searchPoints, searchCategory, searchTerm, searchWeek }) {
        const params = { 
            Position: searchPosition, 
            Interval: searchInterval, 
            Points: searchPoints, 
            Category: searchCategory, 
            Term: searchTerm, 
            Week: searchWeek
        };

        return http.get('/search', { params })
    }
}
import axios from 'axios';

const AuthService = {
    login(user) {
        return axios.post('http://localhost:5000/login', user);
    },

    register(user) {
        return axios.post('http://localhost:5000/register', user);
    },
}

export default AuthService;
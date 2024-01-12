import axios from 'axios';

const AuthService = {
    login(user) {
        return axios.post('https://fantasyplayoffchallenge.azurewebsites.net/login', user);
    },

    register(user) {
        return axios.post('https://fantasyplayoffchallenge.azurewebsites.net/register', user);
    },
}

export default AuthService;
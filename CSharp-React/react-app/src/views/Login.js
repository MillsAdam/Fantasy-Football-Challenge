import React, { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthService from '../services/AuthService';
import { AuthContext } from '../context/AuthContext';

function Login() {
    const [user, setUser] = useState({ username: '', password: '' });
    const [invalidCredentials, setInvalidCredentials] = useState(false);
    const { setAuthToken, setCurrentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        setUser({ ...user, [e.target.name]: e.target.value });
    };

    const login = async (e) => {
        e.preventDefault();
        try {
            const response = await AuthService.login(user);
            if (response.status === 200) {
                localStorage.setItem('authToken', response.data.token);
                localStorage.setItem('currentUser', JSON.stringify(response.data.user));
                setAuthToken(response.data.token);
                setCurrentUser(response.data.user);
                navigate('/');
            }
        } catch (error) {
            if (error.response && error.response.status === 401) {
                setInvalidCredentials(true);
            }
        }
    };

    return (
        <div className="login">
            <h1>Login</h1>
            <form onSubmit={login}>
                <div className="form-input-group">
                    <label htmlFor="username">Username</label>
                    <input className="form" type="text" id="username" name="username"  value={user.username} onChange={handleInputChange} autoComplete="username" />
                </div>
                <div className="form-input-group">
                    <label htmlFor="password">Password</label>
                    <input className="form" type="password" id="password" name="password" value={user.password} onChange={handleInputChange} autoComplete="current-password" />
                </div>
                <button className="form" type="submit" value="Login">Login</button>
            </form>
            {invalidCredentials && <p className="invalid-credentials">Invalid credentials.</p>}
            <p>Don't have an account? <Link to="/register">Register</Link></p>
        </div>
    );
}

export default Login;
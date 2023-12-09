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
        <div className="page-container" style={{ alignItems: 'center', width: '30%' }}>
            <div className="component-container">
                <h1>Login</h1>
                <form onSubmit={login}>
                    <div style={{ marginBottom: '1rem' }}>
                        <label htmlFor="username">Username</label>
                        <input 
                            className="btn btn-neutral btn-outline" 
                            style={{ textAlign: 'left', width: '100%' }} 
                            type="text" 
                            id="username" 
                            name="username"  
                            value={user.username} 
                            onChange={handleInputChange} 
                            autoComplete="username" />
                    </div>
                    <div style={{ marginBottom: '1rem' }}>
                        <label htmlFor="password">Password</label>
                        <input 
                            className="btn btn-neutral btn-outline" 
                            style={{ textAlign: 'left', width: '100%' }} 
                            type="password" 
                            id="password" 
                            name="password" 
                            value={user.password} 
                            onChange={handleInputChange} 
                            autoComplete="current-password" />
                    </div>
                    <button className="btn btn-primary btn-outline" style={{ width: '100%' }} type="submit" value="Login">Login</button>
                </form>
                {invalidCredentials && <p className="text-error" style={{ marginTop: '1rem' }}>Invalid credentials.</p>}
                <div style={{ marginTop: '1rem', fontSize: '0.9rem' }}>
                    Don't have an account? <Link className="btn btn-primary btn-outline btn-sm" style={{ marginLeft: '0.5rem' }} to="/register">Register</Link>
                </div>
            </div>
        </div>
        
    );
}

export default Login;
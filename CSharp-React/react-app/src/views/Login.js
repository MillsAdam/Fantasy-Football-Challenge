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
        <div className="flex flex-col justify-between items-center flex-wrap w-90 md:w-30 my-4 mx-auto">
            <div className="flex-1 p-4 w-full">
                <div className="mb-4">
                    Login
                </div>
                <form onSubmit={login}>
                    <div className="mb-4">
                        <div>
                            Username:
                        </div>
                        <input 
                            className="input input-primary input-bordered w-full input-sm md:input-md" 
                            type="text" 
                            name="username" 
                            id="username" 
                            value={user.username} 
                            onChange={handleInputChange} 
                            autoComplete="username" />
                    </div>
                    <div className="mb-4">
                        <div>
                            Password:
                        </div>
                        <input 
                            className="input input-primary input-bordered w-full input-sm md:input-md" 
                            type="password" 
                            name="password" 
                            id="password" 
                            value={user.password} 
                            onChange={handleInputChange} 
                            autoComplete="current-password" />
                    </div>
                    <button className="btn btn-primary btn-outline btn-sm md:btn-md w-full" type="submit" value="Login">Login</button>
                </form>
                {invalidCredentials && <p className="text-error mt-4">Invalid credentials.</p>}
                <div className="mt-4" style={{ fontSize: '0.9rem' }}>
                    Don't have an account? <Link className="btn btn-secondary btn-outline btn-xs sm:btn-sm ml-2" to="/register">Register</Link>
                </div>
            </div>
        </div>
        
    );
}

export default Login;
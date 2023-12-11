import React, { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthService from '../services/AuthService';
import { AuthContext } from '../context/AuthContext';
// import LogoInLight from '../assets/Fantasy Playoff Main Logo.png';
import LogoInDark from '../assets/Fantasy Playoff Inverted Color.png';

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
        <div className="flex flex-col justify-center items-center min-h-screen">
            <div className="w-full md:max-w-md max-w-xs">
                <div className="flex-1 w-full">
                    <div className="flex flex-col justify-center items-center mb-20">
                        <img src={LogoInDark} alt="logo-main" className="md:w-72 w-48"/>
                    </div>
                    <div className="text-xl text-primary mb-4">
                        Login
                    </div>
                    <form onSubmit={login}>
                        <div className="mb-4">
                            <div>
                                Username
                            </div>
                            <input 
                                className="input input-accent input-bordered w-full input-sm md:input-md" 
                                type="text" 
                                name="username" 
                                id="username" 
                                value={user.username} 
                                onChange={handleInputChange} 
                                autoComplete="username" />
                        </div>
                        <div className="mb-4">
                            <div>
                                Password
                            </div>
                            <input 
                                className="input input-accent input-bordered w-full input-sm md:input-md" 
                                type="password" 
                                name="password" 
                                id="password" 
                                value={user.password} 
                                onChange={handleInputChange} 
                                autoComplete="current-password" />
                        </div>
                        <button className="btn btn-primary btn-sm md:btn-md w-full my-4" type="submit" value="Login">Login</button>
                    </form>
                    {invalidCredentials && <p className="text-error my-4">Invalid credentials.</p>}
                    <div className="flex flex-row items-center justify-center mt-4">
                        <div className="text-xs sm:text-sm mr-1">
                            Don't have an account?
                        </div>
                        <div className="btn btn-link btn-xs sm:btn-sm ml-1">
                            <Link to="/register">Register</Link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Login;
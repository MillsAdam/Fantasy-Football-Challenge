import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import logo from '../logo.svg';

const Home = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div className="home-login-register-container">
            <div className="component-container" style={{ alignItems: 'center', width: '100%' }}>
                <div style={{ marginBottom: '1rem' }}>
                    Home Page
                </div>
                <p>You must be authenticated to see this.</p>
                <img src={logo} className="App-logo" alt="logo" />
            </div>
            
        </div>
    );
};

export default Home;
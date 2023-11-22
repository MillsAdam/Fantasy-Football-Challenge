import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import logo from '../logo.svg';

const Home = () => {
    const { authToken } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, navigate]);

    return (
        <div className="home">
            <h1>Home Page</h1>
            <p>You must be authenticated to see this.</p>
            <img src={logo} className="App-logo" alt="logo" />
        </div>
    );
};

export default Home;
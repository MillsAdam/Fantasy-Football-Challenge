import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import logo from '../logo.svg';
import '../styles/Home.css';

const Home = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div className="home">
            <h1>Home Page</h1>
            <p>You must be authenticated to see this.</p>
            <img src={logo} className="App-logo" alt="logo" />
        </div>
    );
};

export default Home;
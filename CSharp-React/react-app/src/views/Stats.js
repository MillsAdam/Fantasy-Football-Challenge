import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import StatsComponent from '../components/StatsComponent';

const Stats = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div>
            <h1>Stats Page</h1>
            <StatsComponent />
        </div>
    );
};

export default Stats;
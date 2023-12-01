import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import LeagueComponent from '../components/LeagueComponent';

const League = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div className="league">
            <h1>League Page</h1>
            <LeagueComponent />
        </div>
    );
};

export default League;
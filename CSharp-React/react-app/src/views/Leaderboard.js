import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import LeaderboardComponent from '../components/LeaderboardComponent';

const Leaderboard = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div className="leaderboard">
            <LeaderboardComponent />
        </div>
    );
};

export default Leaderboard;
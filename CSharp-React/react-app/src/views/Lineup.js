import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import LineupComponent from '../components/LineupComponent';

const Lineup = () => {
    const { authToken } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, navigate]);

    return (
        <div className="lineup">
            <h1>Lineup Page</h1>
            <LineupComponent />
        </div>
    );
}

export default Lineup;
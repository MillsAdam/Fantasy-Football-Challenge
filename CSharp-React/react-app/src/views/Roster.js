import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import RosterComponent from '../components/RosterComponent';

const Roster = () => {
    const { authToken } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, navigate]);

    return (
        <div className="roster">
            <h1>Roster Page</h1>
            <RosterComponent />
        </div>
    );
};

export default Roster;
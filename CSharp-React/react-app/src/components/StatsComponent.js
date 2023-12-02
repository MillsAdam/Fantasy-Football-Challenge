import React, { useContext, useEffect } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from 'react-router-dom';
import "../styles/StatsComponent.css";

function StatsComponent() {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div>
            <h1>Stats</h1>
            <p>Coming soon...</p>
        </div>
    );
}

export default StatsComponent;

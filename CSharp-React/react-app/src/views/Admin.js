import React, { useContext, useEffect } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from 'react-router-dom';
import DatabaseComponent from "../components/DatabaseComponent";

function Admin() {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        } else if (currentUser.role !== 'admin') {
            navigate('/');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div>
            <h1>Admin Page</h1>
            <DatabaseComponent />
        </div>
    )
}

export default Admin;
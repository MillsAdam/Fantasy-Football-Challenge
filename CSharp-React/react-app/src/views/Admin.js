import React, { useContext, useEffect } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from 'react-router-dom';
import AdminComponent from "../components/AdminComponent";

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
            <AdminComponent />
        </div>
    )
}

export default Admin;
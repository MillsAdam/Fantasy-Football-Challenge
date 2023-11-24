import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { Navigate } from 'react-router-dom';

const AdminRoute = ({ children }) => {
    const { authToken, currentUser } = useContext(AuthContext);

    if (!authToken) {
        return <Navigate to="/login" />;
    } else if (currentUser.role !== 'admin') {
        return <Navigate to="/" />;
    }

    return children;
}

export default AdminRoute;
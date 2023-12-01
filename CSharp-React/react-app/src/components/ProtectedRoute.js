import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { Navigate } from 'react-router-dom';

const ProtectedRoute = ({ children }) => {
    const { authToken, currentUser } = useContext(AuthContext);

    if (!authToken && !currentUser) {
        return <Navigate to="/login" />;
    }

    return children;
}

export default ProtectedRoute;
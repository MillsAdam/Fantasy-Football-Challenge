import React, { createContext, useContext, useState } from 'react';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [authToken, setAuthToken] = useState(localStorage.getItem('authToken'));
    const [currentUser, setCurrentUser] = useState(null);

    return (
        <AuthContext.Provider value={{ authToken, setAuthToken, currentUser, setCurrentUser }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
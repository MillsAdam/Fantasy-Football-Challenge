import React, { createContext, useContext, useState, useEffect } from 'react';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [authToken, setAuthToken] = useState(localStorage.getItem('authToken') || null);
    
    const [currentUser, setCurrentUser] = useState(() => {
        const storedUserJSON = localStorage.getItem('currentUser');
        // Make sure there's a value before parsing
        return storedUserJSON ? JSON.parse(storedUserJSON) : null;
    });

    useEffect(() => {
        // Only set the authToken in localStorage if it's not null
        if (authToken) {
            localStorage.setItem('authToken', authToken);
        } else {
            // If authToken is null, remove it from localStorage
            localStorage.removeItem('authToken');
        }
    }, [authToken]);

    useEffect(() => {
        // Only set the currentUser in localStorage if it's not null
        if (currentUser) {
            localStorage.setItem('currentUser', JSON.stringify(currentUser));
        } else {
            // If currentUser is null, remove it from localStorage
            localStorage.removeItem('currentUser');
        }
    }, [currentUser]);

    return (
        <AuthContext.Provider value={{ authToken, setAuthToken, currentUser, setCurrentUser }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);

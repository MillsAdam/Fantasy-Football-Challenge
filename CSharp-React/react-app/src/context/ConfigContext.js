import React, { createContext, useState, useEffect, useContext } from 'react';
import DatabaseService from '../services/DatabaseService';

const ConfigContext = createContext();

export const ConfigProvider = ({ children }) => {
    const [configurations, setConfigurations] = useState([]);

    useEffect(() => {
        const fetchConfigurations = async() => {
            try {
                const fetchedConfigurations = await DatabaseService.getConfiguration();
                setConfigurations(fetchedConfigurations);
            } catch (error) {
                console.error('Error fetching configurations: ', error);
            }
        };

        fetchConfigurations();
    }, []);

    const updateConfigurations = (newConfigurations) => {
        setConfigurations(newConfigurations);
    };

    return (
        <ConfigContext.Provider value={{ configurations, updateConfigurations }}>
            {children}
        </ConfigContext.Provider>
    );
};

export const useConfig = () => useContext(ConfigContext);
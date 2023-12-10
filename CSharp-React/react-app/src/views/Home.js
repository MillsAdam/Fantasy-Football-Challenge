import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import logo from '../logo.svg';

const Home = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div className="flex flex-col justify-between items-center flex-wrap w-90 md:w-30 my-4 mx-auto">
            <div className="flex-1 p-4 w-full">
                <div className="mb-4">
                    Home Page
                </div>
                <p>You must be authenticated to see this.</p>
                <img src={logo} className="App-logo" alt="logo" />
            </div>
            
        </div>
    );
};

export default Home;
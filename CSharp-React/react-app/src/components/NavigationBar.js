import React from 'react';
import { useAuth } from '../context/AuthContext';
import { Link, useNavigate } from 'react-router-dom';

function NavigationBar() {
    const { authToken, currentUser } = useAuth();
    const navigate = useNavigate();

    const handleLogoutClick = () => {
        navigate('/logout');
    }

    return (
        <nav>
            {authToken && (
                <>
                    <Link to="/" className="App-link">Home</Link>{" | "}
                    <Link to="/league" className="App-link">League</Link>{" | "}
                    <Link to="/roster" className="App-link">Roster</Link>{" | "}
                    <Link to="/lineup" className="App-link">Lineup</Link>{" | "}
                    <Link to="/stats" className="App-link">Stats</Link>{" | "}
                    <span to="/logout" className="App-link" onClick={handleLogoutClick} style={{ cursor: 'pointer' }}>Logout</span>
                    {currentUser && currentUser.role === 'admin' && (
                        <>
                            {" | "}
                            <Link to="/admin" className="App-link">Admin</Link>
                        </>
                    )}
                </>
            )}
            {!authToken && (
                <>
                    <Link to="/login" className="App-link">Login</Link>{" | "}
                    <Link to="/register" className="App-link">Register</Link>
                </>
            )}
        </nav>
    );
}

export default NavigationBar;
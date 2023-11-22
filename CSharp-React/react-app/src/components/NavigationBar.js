import React, { useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { Link, useNavigate } from 'react-router-dom';

function NavigationBar() {
    const { authToken } = useContext(AuthContext);
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
                    <span to="/logout" className="App-link" onClick={handleLogoutClick} style={{ cursor: 'pointer' }}>Logout</span>
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
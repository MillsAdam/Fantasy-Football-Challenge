import { useEffect, useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';

function Logout() {
    const { setAuthToken, setCurrentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        localStorage.removeItem('authToken');
        localStorage.removeItem('currentUser');
        setAuthToken(null);
        setCurrentUser(null);
        navigate('/login');
    }, [setAuthToken, setCurrentUser, navigate]);

    return null;
}

export default Logout;
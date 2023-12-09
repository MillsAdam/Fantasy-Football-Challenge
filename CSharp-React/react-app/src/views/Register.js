import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthService from '../services/AuthService';

function Register() {
    const [user, setUser] = useState({
        username: '',
        password: '',
        confirmPassword: '',
        role: 'user',
    });

    const [registrationErrors, setRegistrationErrors] = useState(false);
    const [registrationErrorMsg, setRegistrationErrorMsg] = useState('There were problems registering this user.');

    const navigate = useNavigate();

    const register = async (e) => {
        e.preventDefault();
        if (user.password !== user.confirmPassword) {
            setRegistrationErrors(true);
            setRegistrationErrorMsg('Password & Confirm Password do not match.');
        } else {
            try {
                const response = await AuthService.register(user);
                if (response.status === 201) {
                    navigate('/login', { state: { registration: 'success' } });
                }
            } catch (error) {
                setRegistrationErrors(true);
                if (error.response && error.response.status === 400) {
                    setRegistrationErrorMsg('Bad Request: Validation Errors');
                } else {
                    setRegistrationErrorMsg('An unexpected error occurred.');
                }
            }
        }
    };

    const clearErrors = () => {
        setRegistrationErrors(false);
        setRegistrationErrorMsg('There were problems registering this user.');
    };

    return (
        <div className="page-container" style={{ alignItems: 'center', width: '30%' }}>
            <div className="component-container">
                <h1>Register</h1>
                {registrationErrors && (
                    <div role="alert">
                        {registrationErrorMsg}
                    </div>
                )}
                <form onSubmit={register}>
                    <div style={{ marginBottom: '1rem' }}>
                        <label htmlFor="username">Username: </label>
                        <input 
                            className="btn btn-neutral btn-outline" 
                            style={{ textAlign: 'left', width: '100%' }} 
                            type="text" 
                            id="username" 
                            name="username" 
                            value={user.username} 
                            onChange={e => setUser({ ...user, username: e.target.value })} 
                            onFocus={clearErrors} 
                            autoComplete="username" />
                    </div>
                    <div style={{ marginBottom: '1rem' }}>
                        <label htmlFor="password">Password: </label>
                        <input 
                            className="btn btn-neutral btn-outline" 
                            style={{ textAlign: 'left', width: '100%' }} 
                            type="password" 
                            id="password" 
                            name="password" 
                            value={user.password} 
                            onChange={e => setUser({ ...user, password: e.target.value })} 
                            onFocus={clearErrors} 
                            autoComplete="new-password" />
                    </div>
                    <div style={{ marginBottom: '1rem' }}>
                        <label htmlFor="confirmPassword">Confirm Password: </label>
                        <input 
                            className="btn btn-neutral btn-outline" 
                            style={{ textAlign: 'left', width: '100%' }} 
                            type="password" 
                            id="confirmPassword" 
                            name="confirmPassword" 
                            value={user.confirmPassword} 
                            onChange={e => setUser({ ...user, confirmPassword: e.target.value })} 
                            onFocus={clearErrors} 
                            autoComplete="new-password" />
                    </div>
                    <button className="btn btn-primary btn-outline" style={{ width: '100%' }} type="submit" value="Register">Register</button>
                </form>
                {registrationErrors && <div className="text-error" style={{ marginTop: '1rem' }}>{registrationErrorMsg}</div>}
                <div style={{ marginTop: '1rem', fontSize: '0.9rem' }}>
                    Already have an account? <Link className="btn btn-primary btn-outline btn-sm" style={{ marginLeft: '0.5rem' }} to="/login">Login</Link>
                </div>
            </div>
        </div>
        
    );



}

export default Register;
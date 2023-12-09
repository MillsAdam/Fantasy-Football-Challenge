import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthService from '../services/AuthService';
import '../styles/LoginRegister.css';

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
        <div className="page-container">
            <div className="component-container">
                <h1>Register</h1>
                {registrationErrors && (
                    <div role="alert">
                        {registrationErrorMsg}
                    </div>
                )}
                <form onSubmit={register}>
                    <div className="form-input-group">
                        <label htmlFor="username">Username: </label>
                        <input type="text" id="username" name="username" value={user.username} onChange={e => setUser({ ...user, username: e.target.value })} onFocus={clearErrors} autoComplete="username" />
                    </div>
                    <div className="form-input-group">
                        <label htmlFor="password">Password: </label>
                        <input type="password" id="password" name="password" value={user.password} onChange={e => setUser({ ...user, password: e.target.value })} onFocus={clearErrors} autoComplete="new-password" />
                    </div>
                    <div className="form-input-group">
                        <label htmlFor="confirmPassword">Confirm Password: </label>
                        <input type="password" id="confirmPassword" name="confirmPassword" value={user.confirmPassword} onChange={e => setUser({ ...user, confirmPassword: e.target.value })} onFocus={clearErrors} autoComplete="new-password" />
                    </div>
                    <button className="btn btn-neutral sm: btn-sm" style={{ width: '100%' }} type="submit" value="Register">Register</button>
                </form>
                {registrationErrors && <div className="registration-errors">{registrationErrorMsg}</div>}
                <div className="account-query">
                    Already have an account? <Link className="login-link" to="/login">Login</Link>
                </div>
            </div>
        </div>
        
    );



}

export default Register;
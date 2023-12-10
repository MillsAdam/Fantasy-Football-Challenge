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
        <div className="flex flex-col justify-between items-center flex-wrap w-90 md:w-30 my-4 mx-auto">
            <div className="flex-1 p-4 w-full">
                <div className="mb-4">
                    Register
                </div>
                {registrationErrors && (
                    <div role="alert">
                        {registrationErrorMsg}
                    </div>
                )}
                <form onSubmit={register}>
                    <div className="mb-4">
                        <div>
                            Username:
                        </div>
                        <input 
                            className="input input-primary input-bordered w-full input-sm md:input-md" 
                            type="text" 
                            name="username" 
                            id="username" 
                            value={user.username} 
                            onChange={e => setUser({ ...user, username: e.target.value })} 
                            onFocus={clearErrors} 
                            autoComplete="username" />
                    </div>
                    <div className="mb-4">
                        <div>
                            Password:
                        </div>
                        <input 
                            className="input input-primary input-bordered w-full input-sm md:input-md" 
                            type="password" 
                            name="password" 
                            id="password" 
                            value={user.password} 
                            onChange={e => setUser({ ...user, password: e.target.value })} 
                            onFocus={clearErrors} 
                            autoComplete="new-password" />
                    </div>
                    <div className="mb-4">
                        <div>
                            Confirm Password:
                        </div>
                        <input 
                            className="input input-primary input-bordered w-full input-sm md:input-md" 
                            type="password" 
                            name="confirmPassword" 
                            id="confirmPassword" 
                            value={user.confirmPassword} 
                            onChange={e => setUser({ ...user, confirmPassword: e.target.value })} 
                            onFocus={clearErrors} 
                            autoComplete="new-password" />
                    </div>
                    <button className="btn btn-primary btn-outline btn-sm md:btn-md w-full" type="submit" value="Register">Register</button>
                </form>
                {registrationErrors && <div className="text-error mt-4">{registrationErrorMsg}</div>}
                <div className="mt-4" style={{ fontSize: '0.9rem' }}>
                    Already have an account? <Link className="btn btn-secondary btn-outline btn-xs sm:btn-sm ml-2" to="/login">Login</Link>
                </div>
            </div>
        </div>
        
    );



}

export default Register;
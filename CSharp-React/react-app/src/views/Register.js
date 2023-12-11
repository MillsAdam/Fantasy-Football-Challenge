import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import AuthService from '../services/AuthService';
// import LogoInLight from '../assets/Fantasy Playoff Main Logo.png';
import LogoInDark from '../assets/Fantasy Playoff Inverted Color.png';

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
        <div className="flex flex-col justify-center items-center min-h-screen">
            <div className="w-full md:max-w-md max-w-xs">
                <div className="flex-1 w-full">
                    <div className="flex flex-col justify-center items-center mb-20">
                        <img src={LogoInDark} alt="logo-main" className="md:w-72 w-48"/>
                    </div>
                    <div className="mb-4 text-xl text-primary">
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
                                Username
                            </div>
                            <input 
                                className="input input-accent input-bordered w-full input-sm md:input-md" 
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
                                Password
                            </div>
                            <input 
                                className="input input-accent input-bordered w-full input-sm md:input-md" 
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
                                Confirm Password
                            </div>
                            <input 
                                className="input input-accent input-bordered w-full input-sm md:input-md mb-4" 
                                type="password" 
                                name="confirmPassword" 
                                id="confirmPassword" 
                                value={user.confirmPassword} 
                                onChange={e => setUser({ ...user, confirmPassword: e.target.value })} 
                                onFocus={clearErrors} 
                                autoComplete="new-password" />
                        </div>
                        <button className="btn btn-primary btn-sm md:btn-md w-full mb-4" type="submit" value="Register">Register</button>
                    </form>
                    {registrationErrors && <div className="text-error my-4">{registrationErrorMsg}</div>}
                    <div className="flex flex-row items-center justify-center mt-4">
                        <div className="text-xs sm:text-sm mr-1">
                            Already have an account?
                        </div>
                        <div className="btn btn-link btn-xs sm:btn-sm ml-1">
                            <Link  to="/login">Login</Link>
                        </div>
                    </div>
                </div>
            </div>
        </div>  
    );



}

export default Register;
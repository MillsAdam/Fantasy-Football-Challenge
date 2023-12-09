import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { Link, useNavigate } from 'react-router-dom';

function NavigationBar() {
    const { authToken, currentUser } = useAuth();
    const navigate = useNavigate();
    const [isMenuOpen, setIsMenuOpen] = useState(false);

    const handleLogoutClick = () => {
        setIsMenuOpen(false);
        navigate('/logout');
    }

    const toggleMenu = () => {
        setIsMenuOpen(!isMenuOpen);
    }

    const handleLinkClick = () => {
        setIsMenuOpen(false);
    }
    

    return (
        <div className="navbar bg-base-300 rounded-box">
            <div className="flex-1 px-2 lg:flex-none">
                <Link to="/" className="text-lg font-bold">FPC</Link>
            </div>
            <div className=" flex justify-end flex-1 px-2">
                <div className="flex items-stretch hidden lg:flex">
                    {authToken && (
                        <>
                            <Link to="/" className="btn btn-ghost rounded-btn">Home</Link>
                            <Link to="/league" className="btn btn-ghost rounded-btn">League</Link>
                            <Link to="/roster" className="btn btn-ghost rounded-btn">Roster</Link>
                            <Link to="/lineup" className="btn btn-ghost rounded-btn">Lineup</Link>
                            <Link to="/stats" className="btn btn-ghost rounded-btn">Stats</Link>
                            <span to="/logout" className="btn btn-ghost rounded-btn" onClick={handleLogoutClick} style={{ cursor: 'pointer' }}>Logout</span>
                            {currentUser && currentUser.role === 'admin' && (
                                <>
                                    <Link to="/admin" className="btn btn-ghost rounded-btn">Admin</Link>
                                </>
                            )}
                        </>
                    )}
                    {!authToken && (
                        <>
                            <Link to="/login" className="btn btn-ghost rounded-btn">Login</Link>
                            <Link to="/register" className="btn btn-ghost rounded-btn">Register</Link>
                        </>
                    )}
                </div>
            </div>
            <div className="flex-none">
                <div className="dropdown dropdown-end">
                    <button onClick={toggleMenu} tabIndex={0} className="btn btn-square btn-ghost lg:hidden">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" className="inline-block w-6 h-6 stroke-current"> 
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16m-7 6h7"></path> 
                        </svg>
                    </button>
                    {authToken && (
                        <div className={`${isMenuOpen ? 'block' : 'hidden'} absolute w-full z-50 lg:w-auto`}>
                            <ul tabIndex={0} className="menu menu-compact dropdown-content mt-3 p-2 shadow bg-base-100 rounded-box w-52" >
                                <li><Link to="/" onClick={handleLinkClick}>Home</Link></li>
                                <li><Link to="/league" onClick={handleLinkClick}>League</Link></li>
                                <li><Link to="/roster" onClick={handleLinkClick}>Roster</Link></li>
                                <li><Link to="/lineup" onClick={handleLinkClick}>Lineup</Link></li>
                                <li><Link to="/stats" onClick={handleLinkClick}>Stats</Link></li>
                                <li><span onClick={handleLogoutClick}>Logout</span></li>
                                {currentUser && currentUser.role === 'admin' && (
                                    <li><Link to="/admin" onClick={handleLinkClick}>Admin</Link></li>
                                )}
                            </ul>
                        </div>
                    )}
                    {!authToken && (
                        <div className={`${isMenuOpen ? 'block' : 'hidden'} absolute w-full z-50 lg:w-auto`}>
                            <ul tabIndex={0} className="menu menu-compact dropdown-content mt-3 p-2 shadow bg-base-100 rounded-box w-52">
                                <li><Link to="/login" onClick={handleLinkClick}>Login</Link></li>
                                <li><Link to="/register" onClick={handleLinkClick}>Register</Link></li>
                            </ul>
                        </div>
                    )}
                </div>
            </div>
            
        </div>
    );
}

export default NavigationBar;
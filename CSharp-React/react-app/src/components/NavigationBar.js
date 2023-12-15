import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { Link, useNavigate } from 'react-router-dom';
import LogoSymbol from '../assets/Fantasy Playoff Logo Symbol.png';

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
        <div className="navbar bg-base-300">
            <div className="navbar-start">
                <div className="btn btn-ghost text-xl">
                    <Link to="/">
                        <img src={LogoSymbol} alt="logo-symbol" className="w-6 inline-block"/>
                    </Link>
                </div>
            </div>
            <div className="navbar-center hidden lg:flex grow">
                {authToken ? (
                    <ul className="menu menu-horizontal px-1 justify-center flex-1">
                        <li><Link to="/" className="btn btn-ghost rounded-btn text-lg">Home</Link></li>
                        <li><Link to="/league" className="btn btn-ghost rounded-btn text-lg">League</Link></li>
                        <li><Link to="/leaderboard" className="btn btn-ghost rounded-btn text-lg">Leaderboard</Link></li>
                        <li><Link to="/roster" className="btn btn-ghost rounded-btn text-lg">Roster</Link></li>
                        <li><Link to="/lineup" className="btn btn-ghost rounded-btn text-lg">Lineup</Link></li>
                        <li><Link to="/stats" className="btn btn-ghost rounded-btn text-lg">Stats</Link></li>
                    </ul>
                ) : (
                    <ul className="menu menu-horizontal px-1 justify-center flex-1">
                        <li><Link to="/login" className="btn btn-ghost rounded-btn text-lg">Login</Link></li>
                        <li><Link to="/register" className="btn btn-ghost rounded-btn text-lg">Register</Link></li>
                    </ul>
                )}
            </div>
            <div className="navbar-end flex">
                <div className="hidden lg:flex">
                    {currentUser && currentUser.role === 'admin' && (
                        <Link to="/admin" className="btn btn-ghost rounded-btn text-lg">Admin</Link>
                    )}
                    <span to="/logout" className="btn btn-ghost rounded-btn text-lg" onClick={handleLogoutClick} style={{ cursor: 'pointer' }}>Logout</span>
                </div>
                <div className="lg:hidden">
                    <div className="dropdown dropdown-end">
                        <button onClick={toggleMenu} tabIndex={0} className="btn btn-square btn-ghost">
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" className="inline-block w-6 h-6 stroke-current"> 
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16m-7 6h7"></path> 
                            </svg>
                        </button>
                        {authToken ? (
                            <div className={`${isMenuOpen ? 'block' : 'hidden'} absolute w-full z-50 lg:w-auto`}>
                                <ul tabIndex={0} className="menu menu-compact dropdown-content mt-3 p-2 shadow bg-base-200 rounded-box w-52" >
                                    <li><Link to="/" onClick={handleLinkClick}>Home</Link></li>
                                    <li><Link to="/league" onClick={handleLinkClick}>League</Link></li>
                                    <li><Link to="/leaderboard" onClick={handleLinkClick}>Leaderboard</Link></li>
                                    <li><Link to="/roster" onClick={handleLinkClick}>Roster</Link></li>
                                    <li><Link to="/lineup" onClick={handleLinkClick}>Lineup</Link></li>
                                    <li><Link to="/stats" onClick={handleLinkClick}>Stats</Link></li>
                                    <li><span onClick={handleLogoutClick}>Logout</span></li>
                                    {currentUser && currentUser.role === 'admin' && (
                                        <li><Link to="/admin" onClick={handleLinkClick}>Admin</Link></li>
                                    )}
                                </ul>
                            </div>
                        ) : (
                            <div className={`${isMenuOpen ? 'block' : 'hidden'} absolute w-full z-50 lg:w-auto`}>
                                <ul tabIndex={0} className="menu menu-compact dropdown-content mt-3 p-2 shadow bg-base-200 rounded-box w-52">
                                    <li><Link to="/login" onClick={handleLinkClick}>Login</Link></li>
                                    <li><Link to="/register" onClick={handleLinkClick}>Register</Link></li>
                                </ul>
                            </div>
                        
                        )}
                    </div>
                </div>
                
            </div>
        </div>
    );
}

export default NavigationBar;
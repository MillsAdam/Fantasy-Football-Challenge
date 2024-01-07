import React, { useContext, useEffect } from 'react';
import { AuthContext } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import NavigationBar from '../components/NavigationBar';
// import LogoInLight from '../assets/Fantasy Playoff Main Logo.png';
import LogoInDark from '../assets/Fantasy Playoff Inverted Color.png';

const Home = () => {
    const { authToken, currentUser } = useContext(AuthContext);
    const navigate = useNavigate();

    useEffect(() => {
        if (!authToken) {
            navigate('/login');
        }
    }, [authToken, currentUser, navigate]);

    return (
        <div className="flex flex-col min-h-screen">
            <NavigationBar />
            <div className="flex flex-col justify-center items-center w-90 my-8 mx-auto px-4">
                <div className="flex flex-col md:flex-row items-center justify-center w-full mb-10 gap-8">
                    <img src={LogoInDark} alt="logo-main" className="w-72 mb-6 md:mb-0"/>

                    <div className="flex flex-col items-center md:items-start w-full max-w-2xl">
                        <h1 className="text-5xl font-bold text-center md:text-left mb-4 text-primary">Welcome to the Fantasy Playoff Challenge!</h1>
                        <p className="text-xl mb-4 text-accent">
                            Embrace the thrill of NFL playoffs like never before!
                        </p>
                        <p className="text-lg mb-6 text-center md:text-left">
                            Join the ultimate fantasy football experience where strategy meets excitement in the postseason frenzy. The Fantasy Playoff Challenge invites you to create and manage a dream team of 27 players from the remaining NFL playoff teams.
                        </p>
                    </div>
                </div>
                
                <div className="flex flex-col justify-center lg:w-60 w-full mx-auto px-4 py-8 bg-base-200 shadow-md rounded-lg">
                    <h2 className="text-3xl font-semibold mb-4 text-primary">How It Works:</h2>
                    <ul className="list-disc pl-8 mb-8 text-lg flex flex-col items-start custom-bullet"> 
                        <li className="mb-2 text-left">Build Your Roster: Select any 27 players from the teams in the NFL playoffs.</li>
                        <li className="mb-2 text-left">Weekly Lineups: Set your lineup with up to 2 QBs, 2 RBs, 3 WRs, 1 TE, 1 Flex, 1 Kicker, and 1 Defense.</li>
                        <li className="mb-2 text-left">Point-Per-Reception (PPR) Scoring: Every catch counts, adding more excitement to your gameplay.</li>
                        <li className="mb-2 text-left">Cumulative Scoring: Scores accumulate through the Super Bowl.</li>
                        <li className="mb-2 text-left">League creating and league joining locks at the start of playoffs.</li>
                        <li className="mb-2 text-left">Rosters Lock at Playoff Start.</li>
                        <li className="mb-2 text-left">Weekly Lineup Lock: Set your lineup before the first playoff game each week.</li>
                        <li className="mb-2 text-left">Reset and Refresh: Lineups and playoff weeks reset every Tuesday.</li>
                        <li className="mb-2 text-left">Stay Informed: Player projections update 72 hours before each game.</li>
                        <li className="mb-2 text-left">Updated Scores: Player scores are posted the following day, keeping you in the loop with the latest results.</li>
                    </ul>
                </div>
            </div>
        </div>
    );
};

export default Home;
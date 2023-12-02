import './App.css';
import React from 'react';
import ErrorBoundary from './ErrorBoundary';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavigationBar from './components/NavigationBar';
import Home from './views/Home';
import Login from './views/Login';
import Logout from './views/Logout';
import Register from './views/Register';
import ProtectedRoute from './components/ProtectedRoute';
import League from './views/League';
import Roster from './views/Roster';
import Admin from './views/Admin';
import AdminRoute from './components/AdminRoute';
import Lineup from './views/Lineup';
import Stats from './views/Stats';


function App() {
  return (
    <Router>
      <ErrorBoundary>
        <div className="App">
          <header className="App-header">
            <NavigationBar />
          </header>
          <Routes>
            <Route path="/" element={<ProtectedRoute><Home /></ProtectedRoute>} />
            <Route path="/login" element={<Login />} />
            <Route path="/logout" element={<ProtectedRoute><Logout /></ProtectedRoute>} />
            <Route path="/register" element={<Register />} />
            <Route path="/league" element={<ProtectedRoute><League /></ProtectedRoute>} />
            <Route path="/roster" element={<ProtectedRoute><Roster /></ProtectedRoute>} />
            <Route path="/lineup" element={<ProtectedRoute><Lineup /></ProtectedRoute>} />
            <Route path="/stats" element={<ProtectedRoute><Stats /></ProtectedRoute>} />
            <Route path="/admin" element={<AdminRoute><Admin /></AdminRoute>} />
          </Routes>
        </div>
      </ErrorBoundary>     
    </Router>
  );
}

export default App;

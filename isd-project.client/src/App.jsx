/*import { useEffect, useState } from 'react';*/
import './App.css'

import HomePage from './containers/HomePage/HomePage';
import LoginForm from './containers/Auth/LoginForm';
import RegisterForm from './containers/Auth/RegisterForm';
import UserProfile from './containers/User/UserProfile';

import { BrowserRouter as Router } from 'react-router-dom';
import { Route, Routes } from 'react-router-dom';


function App() {

    return (
        <Router>
            <Routes>
                <Route path='/' element={<HomePage />} />
                <Route path='/login' element={<LoginForm />} />
                <Route path='/signup' element={<RegisterForm />} />
                <Route path='/me' element={<UserProfile />} />
            </Routes>
        </Router>
    );
}

export default App;
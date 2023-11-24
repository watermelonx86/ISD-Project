import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'

import LoginForm from './components/LoginForm.jsx';
import RegisterForm from './components/RegisterForm.jsx';

import './index.css';

import { BrowserRouter as Router } from 'react-router-dom'
import { Route, Routes } from 'react-router-dom';

/*import App from './App'*/


ReactDOM.createRoot(document.getElementById('root')).render(
    <Router>
        <Routes>
            <Route path='/' element={<App />} />
            <Route path='/login' element={<LoginForm />} />
            <Route path='/signup' element={<RegisterForm />} />
        </Routes>
        {/*{<LoginForm />
        <RegisterForm />}*/}
    </Router>
)
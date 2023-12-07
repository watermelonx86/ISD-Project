/*import { useEffect, useState } from 'react';*/
import './App.css'

import HomePage from './containers/HomePage/HomePage';
import LoginForm from './containers/Auth/LoginForm';
import RegisterForm from './containers/Auth/RegisterForm';
import UserProfile from './containers/User/UserProfile';
import UserProfileEdit from './containers/User/UserProfileEdit';
import Product from './containers/Product/Product';
import ProductDetail from './containers/Product/ProductDetail';
import FillForm from './containers/Product/FillForm';
import Censor from './containers/Employee/Censor';

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
                <Route path='/san-pham-bao-hiem' element={<Product />} />
                <Route path='/san-pham-bao-hiem/:id' element={<ProductDetail />} />
                <Route path='/san-pham-bao-hiem/:id/dang-ky-bao-hiem' element={<FillForm />} />
                <Route path='/edit' element={<UserProfileEdit />} />
                <Route path='/duyet-don-dang-ky' element={<Censor />} />
            </Routes>
        </Router>
    );
}

export default App;
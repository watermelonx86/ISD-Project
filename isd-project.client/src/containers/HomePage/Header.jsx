import React, { useEffect, useState } from 'react';
import { useLocation, NavLink } from 'react-router-dom';

import { useAuth } from '../../services/auth.jsx';

import img from '../../assets/baohiem.png';

import Avatar from '../../assets/avatar.png';
import { motion } from 'framer-motion';
import { useNavigate } from 'react-router-dom';

import './Header.css';

const Header = () => {

   /* const location = useLocation();*/

    const { isLoggedIn, login, logout } = useAuth();

    const [isMobileMenuOpen, setMobileMenuOpen] = useState(false);

    const navigate = useNavigate();

    const toggleMobileMenu = () => {
        setMobileMenuOpen(!isMobileMenuOpen);
    };

    const profile = () => {
        navigate('/me');
    }

    return (
        <header>
            <nav className="bg-gray-200 border-gray-200 px-4 lg:px-6 py-2.5">
                <div className="flex flex-wrap justify-between items-center mx-auto max-w-screen-xl">
                    <NavLink to="/" className="flex items-center" id = "home">
                        <img src={img} className="mr-3 h-6 sm:h-9" alt="Flowbite Logo" />
                        <span className="self-center text-xl font-semibold whitespace-nowrap text-black">
                            PGR2
                        </span>
                    </NavLink>
                  
                    <div className="flex items-center lg:order-2">
                        {isLoggedIn ? (
                            <div className='relative'>
                                <motion.img whileTap={{ scale: 0.6 }}
                                    src={Avatar}
                                    className='w-10 min-w-[40px] h-10 min-h-[40px]  drop-shadow-xl cursor-pointer'
                                    alt='userprofile'
                                    onClick={profile}
                                />
                            </div>) : (
                        <div>
                            <NavLink to="/login" className="text-gray-700 hover:bg-gray-50 focus:ring-4 focus:ring-gray-300 font-medium rounded-lg text-sm px-4 lg:px-5 py-2 lg:py-2.5 mr-2 hover:bg-gray-400 focus:outline-none focus:ring-gray-800">
                                Log in
                            </NavLink>
                            <NavLink to="/signup" className="text-white bg-primary-700 hover:bg-primary-800 focus:ring-4 focus:ring-primary-300 font-medium rounded-lg text-sm px-4 lg:px-5 py-2 lg:py-2.5 mr-2 dark:bg-primary-600 dark:hover:bg-primary-700 focus:outline-none dark:focus:ring-primary-800">
                                Sign up
                            </NavLink>
                        </div>
                        )}
                        <button data-collapse-toggle="mobile-menu-2"
                            type="button"
                            className="inline-flex items-center p-2 ml-1 text-sm text-gray-500 rounded-lg lg:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
                            onClick={toggleMobileMenu}
                            aria-controls="mobile-menu-2"
                            aria-expanded={isMobileMenuOpen}>
                            <span className="sr-only">Open main menu</span>
                            <svg className="w-6 h-6" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                <path fillRule="evenodd" d="M3 5a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 10a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 15a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1z" clipRule="evenodd">
                                </path>
                            </svg>
                            <svg className="hidden w-6 h-6" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                                <path fillRule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clipRule="evenodd">
                                </path>
                            </svg>
                        </button>
                    </div>
                    
                    <div id="mobile-menu-2" className={`${isMobileMenuOpen ? '' : 'hidden'
                        } justify-between items-center w-full lg:flex lg:w-auto lg:order-1`}>
                        <ul className="flex flex-col mt-4 font-medium lg:flex-row lg:space-x-8 lg:mt-0">
                            <li>
                                <NavLink to="/" className="block py-2 pr-4 pl-3 text-gray-700 rounded bg-primary-700 lg:bg-transparent lg:p-0" aria-current="page">
                                    Trang chủ
                                </NavLink>
                            </li>
                            <li>
                                <NavLink to="/san-pham-bao-hiem" className="block py-2 pr-4 pl-3 text-gray-700 border-b border-gray-100 hover:bg-gray-600 lg:hover:bg-transparent lg:border-0 lg:hover:text-primary-700 lg:p-0 text-gray-400 lg:hover:text-gray-500 hover:bg-gray-700 lg:hover:bg-transparent border-gray-700">
                                    Sản phẩm bảo hiểm
                                </NavLink>
                            </li>
                            <li>
                                <a href="#" className="block py-2 pr-4 pl-3 text-gray-700 border-b border-gray-100 hover:bg-gray-600 lg:hover:bg-transparent lg:border-0 lg:hover:text-primary-700 lg:p-0 text-gray-400 lg:hover:text-gray-500 hover:bg-gray-700 lg:hover:bg-transparent border-gray-700">
                                    Chăm sóc khách hàng
                                </a>
                            </li>
                            <li>
                                <a href="#" className="block py-2 pr-4 pl-3 text-gray-700 border-b border-gray-100 hover:bg-gray-600 lg:hover:bg-transparent lg:border-0 lg:hover:text-primary-700 lg:p-0 text-gray-400 lg:hover:text-gray-500 hover:bg-gray-700 lg:hover:bg-transparent border-gray-700">
                                    Liên hệ
                                </a>
                            </li>
                            {/*<li>
                                <a href="#" className="block py-2 pr-4 pl-3 text-gray-200 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-primary-700 lg:p-0 text-gray-400 lg:hover:text-white hover:bg-gray-700 hover:text-white lg:hover:bg-transparent border-gray-700">
                                    Team
                                </a>
                            </li>
                            <li>
                                <a href="#" className="block py-2 pr-4 pl-3 text-gray-200 border-b border-gray-100 hover:bg-gray-50 lg:hover:bg-transparent lg:border-0 lg:hover:text-primary-700 lg:p-0 text-gray-400 lg:hover:text-white hover:bg-gray-700 hover:text-white lg:hover:bg-transparent border-gray-700">
                                    Contact
                                </a>
                            </li>*/}
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;
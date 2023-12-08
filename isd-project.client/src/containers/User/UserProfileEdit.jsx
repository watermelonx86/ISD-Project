import React, { useEffect, useState } from 'react';
import { useLocation, NavLink } from 'react-router-dom';
import axios from 'axios';
import Header from '../HomePage/Header.jsx';
import Footer from '../HomePage/Footer.jsx';

const UserProfileEdit = () => {

    const UserInfo = () => {

        // thực hiện các hành động khác với id, token, role ở đây
        //console.log('User Info:', { userAccountId, token, role, userId });
    }

    //này để lúc bấm vào avatar là nó tự chạy cái hàm UserInfo
    useEffect(() => {
        UserInfo();
    }, []);

    const [userData, setUserData] = useState('');

    const userId = localStorage.getItem('userId');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`https://localhost:7267/api/Customer/get-customer/${userId}`);
                if (response.status === 200) {
                    setUserData(response.data);
                    console.log(response.data);
                } else {
                    console.error("Error fetching user data");
                }
            } catch (error) {
                console.error("Error during API request:", error);
            }
        };
        fetchData();
    }, []);

    return (
        <section className="bg-gray-100  bg-opacity-50 h-screen h-auto">
            <Header />
            
            <div className="mx-auto container max-w-full md:w-3/4 shadow-md mt-5 mb-5 shadow rounded-lg border">
                <div className="bg-gray-100 p-4 border-t-2 bg-opacity-5 border-indigo-400 rounded-t">
                    <div className="max-w-sm mx-auto md:w-full md:mx-0">
                        <div className="inline-flex items-center space-x-4">
                            {/*<img
                                className="w-10 h-10 object-cover rounded-full"
                                alt="User avatar"
                                src="https://avatars3.githubusercontent.com/u/72724639?s=400&u=964a4803693899ad66a9229db55953a3dbaad5c6&v=4"
                            />*/}

                            <h1 className="text-black-600">Thông tin người dùng</h1>
                        </div>
                    </div>
                </div>
                <div className="bg-white space-y-6">
                    <div className="md:inline-flex space-y-4 md:space-y-0 w-full p-4 text-black-500 items-center">
                        <h2 className="md:w-1/4 mx-auto text-lg text-center">Account</h2>

                        <div className="md:w-3/4 mx-auto">
                            <div className="p-1">                
                                <label className="text-sm text-black-400">Email</label>
                                <div className="w-full inline-flex border bg-slate-100 ">

                                    <input
                                        type="email"
                                        className="focus:outline-none focus:text-black-600 p-2"
                                        value={userData.email}
                                        disabled
                                    />
                                </div>
                            </div>

                            <div className="p-1">                
                                <label className="text-sm text-black-400">CMND/CCCD</label>
                                <div className="w-full inline-flex border bg-slate-100 ">

                                    <input
                                        type="email"
                                        className="focus:outline-none focus:text-black-600 p-2"
                                        // value={userData.email}
                                        disabled
                                    />
                                </div>
                            </div>

                            <div className="p-1">
                                <label className="text-sm text-black-400">Phone number</label>
                                <div className="w-full inline-flex border bg-slate-100">

                                    <input
                                        type="text"
                                        className="focus:outline-none focus:text-black-600 p-2"
                                        value={userData.phoneNumber}
                                        disabled
                                    />
                                </div>
                            </div>

                        </div>
                    </div>

                    <hr />
                    <div className="md:inline-flex  space-y-4 md:space-y-0  w-full p-4 text-black-500 items-center">
                        <h2 className="md:w-1/4 mx-auto text-center text-lg">Personal info</h2>
                        <div className="md:w-3/4 mx-auto">
                            <div>
                                <label className="text-sm text-black-400">Full name</label>
                                <div className="w-full inline-flex border bg-slate-100">
                                    
                                    <input
                                        type="text"
                                        className="focus:outline-none focus:text-black-600 p-2"
                                        value={userData.name}
                                        disabled
                                    />
                                </div>
                            </div>
                            <div>
                                <label className="text-sm text-black-400">Gender</label>
                                <div className="w-full inline-flex border bg-slate-100">
                                    <input
                                        type="text"
                                        className="focus:outline-none focus:text-black-900 p-2"
                                        value="Male"
                                        disabled
                                    />
                                </div>
                            </div>
                            <div>
                                <label className="text-sm text-black-400">Job</label>
                                <div className="w-full inline-flex border">

                                    <input
                                        type="text"
                                        className="w-11/12 focus:outline-none focus:text-black-600 p-2"
                                        defaultValue="Student"
                                        
                                    />
                                </div>
                            </div>
                            <div>
                                <label className="text-sm text-black-400">Address</label>
                                <div className="w-full inline-flex border">
                                    <input
                                        type="text" 
                                        className="w-11/12 focus:outline-none focus:text-black-600 p-2"
                                        placeholder={userData.email}
                                    />
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr />
                    <div className="md:inline-flex  space-y-4 md:space-y-0  w-full p-4 text-black-500 items-center">
                        <h2 className="md:w-1/4 mx-auto text-center text-lg">Password</h2>
                        <div className="md:w-3/4 mx-auto">
                            <div>
                                <label className="text-sm text-black-400">Old Password</label>
                                <div className="w-full inline-flex border">
                                    
                                    <input
                                        type="password"
                                        className="w-11/12 focus:outline-none focus:text-black-600 p-2"
                                    />
                                </div>
                            </div>

                            <div>
                                <label className="text-sm text-black-400">New Password</label>
                                <div className="w-full inline-flex border">
                                    <input
                                        type="password"
                                        className="w-11/12 focus:outline-none focus:text-black-600 p-2"
                                    />
                                </div>
                            </div>
                            
                            <div>
                                <label className="text-sm text-black-400">Confirm New Password</label>
                                <div className="w-full inline-flex border">
                                    <input
                                        type="password"
                                        className="w-11/12 focus:outline-none focus:text-black-600 p-2"
                                    />
                                </div>
                            </div>
                            <div className="w-full" align="right">
                                <button className="bg-blue-600 px-4 py-2 m-5 mb-0 text-white hover:bg-blue-700">
                                    Change
                                </button>
                            </div>
                            
                        </div>
                    </div>

                    <hr />
                    <div className="w-full p-4 text-right text-black-500">
                        <button className="inline-flex items-center focus:outline-none mr-4">
                            <svg
                                fill="none" 
                                className="w-4 mr-2"
                                viewBox="0 0 24 24"
                                stroke="currentColor"
                            >
                                <path
                                    strokeLinecap="round"
                                    strokeLinejoin="round"
                                    strokeWidth="2"
                                    d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
                                />
                            </svg>
                            Delete account
                        </button>
                    </div>
                </div>
            </div>

            <Footer />
        </section>
    )
}

export default UserProfileEdit;
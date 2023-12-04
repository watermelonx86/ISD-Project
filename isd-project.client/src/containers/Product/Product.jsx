import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import Header from "../HomePage/Header";
import Footer from "../HomePage/Footer";

const Product = () => {

    const [menuTab, setMenuTab] = useState('p1')
    const [loading, setLoading] = useState(false)
    const navigate = useNavigate();

    const insuranceData = Array.from({ length: 4 }, (_, index) => index + 1); //test

    //loading 
    useEffect(() => {
        setLoading(true)
        setTimeout(() => {
            setLoading(false)
        }, 1000)
    }, [])

    const handleMenuTabs = (type) => {
        setMenuTab(type)
    }

    //item thay bằng mã sản phẩm bảo hiểm
    const handleDetail = (item) => { 
        console.log(item);
        navigate(`/san-pham-bao-hiem/${item}`);
    }

    return (
        <React.Fragment>
            <Header />
            <section className="h-auto my-12 max-w-screen-xl mx-auto px-6">
                <div className="flex items-center justify-center space-x-6">
                    <p className={menuTab === 'p1' ? "active_menu_tab poppins bg-gray-500" : "menu_tab poppins"}
                        onClick={() => handleMenuTabs('p1')}>
                        Bảo hiểm loại 1
                    </p>
                    <p className={menuTab === 'p2' ? "active_menu_tab poppins bg-gray-500" : "menu_tab poppins"}
                        onClick={() => handleMenuTabs('p2')}>
                        Bảo hiểm loại 2
                    </p>
                    <p className={menuTab === 'p3' ? "active_menu_tab poppins bg-gray-500" : "menu_tab poppins"}
                        onClick={() => handleMenuTabs('p3')}>
                        Bảo hiểm loại 3
                    </p>
                    <p className={menuTab === 'p4' ? "active_menu_tab poppins bg-gray-500" : "menu_tab poppins"}
                        onClick={() => handleMenuTabs('p4')}>
                        Bảo hiểm loại 4
                    </p>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10 my-12">
                    {insuranceData.map((item) => (
                        <div key={item} className="bg-white border border-gray-250 transition transform duration-700 hover:shadow-xl hover:scale-105 p-4 rounded-lg relative">
                            <span className="bg-red-100 border border-red-500 rounded-full text-primary text-sm poppins px-4 py-1 inline-block mb-4 ">
                                Loại bảo hiểm
                            </span>
                            <img className="w-64 mx-auto transform transition duration-300 hover:scale-105" src="https://www.prudential.com.vn/export/sites/prudential-vn/vi/.thu-vien/hinh-anh/san-pham-bao-hiem-nhan-tho/ke-hoach-bao-ve/sp-phu-bao-an-thumb-366x206.jpg" alt="" />
                            <div className="flex flex-col items-center my-3 space-y-2">
                            <h1 className="py-3 text-gray-900 poppins text-lg">Tên bảo hiểm</h1>
                                <div className= "w-11/12 border-b-2"></div>
                                <p className="py-3 text-gray-500 poppins text-sm text-center">Mô tả</p>
                                <button type="button"
                                    className="focus:outline-none text-white bg-buttonProduct hover:bg-red-700 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-20 py-3 mb-2"
                                    onClick={() => handleDetail(item)}
                                >
                                    Tìm hiểu thêm
                                </button>
                                
                            </div>
                    </div>
                    ))}
                </div>
            </section>
            <Footer />
        </React.Fragment>
    )
}

export default Product
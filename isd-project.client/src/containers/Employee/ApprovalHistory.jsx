import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import Header from "../HomePage/Header";
import Footer from "../HomePage/Footer";

const ApprovalHistory = () => {
    return (
        <div>
            <Header />

            <section className="mx-auto w-4/5  h-auto my-10">
               
                <div>
                    <h3 className="md:w-3/4 text-2xl my-5 leading-6 font-medium pt-2 font-bold text-green-600">
                        Đơn đã duyệt
                    </h3>
                    <table className="w-full select-none">
                        <thead>
                            <tr className="bg-gray-400 border-2">
                                <th className="p-2">Họ và tên</th>
                                <th className="p-2">Email</th>
                                <th className="p-2">Số điện thoại</th>
                                <th className="p-2">Ngày đăng ký</th>
                                <th className="p-2">Người duyệt</th>
                            </tr>
                        </thead>
                        <tbody >
                            <tr className="border hover:bg-slate-100">
                                <td className="p-3 text-center ">Nguyen Van A</td>
                                <td className="p-3 text-center">nguyenvana@gmail.com</td>
                                <td className="p-3 text-center">0123456789</td>
                                <td className="p-3 text-center">1/1/2024</td>
                                <td className="p-3 text-center font-bold">Le Van B</td>
                            </tr>

                            <tr className="border hover:bg-slate-100">
                                <td className="p-3 text-center ">Nguyen Van A</td>
                                <td className="p-3 text-center">nguyenvana@gmail.com</td>
                                <td className="p-3 text-center">0123456789</td>
                                <td className="p-3 text-center">1/1/2024</td>
                                <td className="p-3 text-center font-bold">Le Van B</td>
                            </tr>

                            <tr className="border hover:bg-slate-100">
                                <td className="p-3 text-center ">Nguyen Van A</td>
                                <td className="p-3 text-center">nguyenvana@gmail.com</td>
                                <td className="p-3 text-center">0123456789</td>
                                <td className="p-3 text-center">1/1/2024</td>
                                <td className="p-3 text-center font-bold">Le Van B</td>
                            </tr>

                        </tbody>
                    </table>
                </div>

                <div>
                    <h3 className="md:w-3/4 text-2xl my-5 leading-6 font-medium pt-2 font-bold text-red-600">
                        Đơn từ chối duyệt
                    </h3>
                    <table className="w-full select-none">
                        <thead>
                            <tr className="bg-gray-400 border-2">
                                <th className="p-2">Họ và tên</th>
                                <th className="p-2">Email</th>
                                <th className="p-2">Số điện thoại</th>
                                <th className="p-2">Ngày đăng ký</th>
                                <th className="p-2">Người duyệt</th>
                                <th className="p-2">Lý do</th>
                            </tr>
                        </thead>
                        <tbody >
                            <tr className="border hover:bg-slate-100">
                                <td className="p-3 text-center ">Nguyen Van A</td>
                                <td className="p-3 text-center">nguyenvana@gmail.com</td>
                                <td className="p-3 text-center">0123456789</td>
                                <td className="p-3 text-center">1/1/2024</td>
                                <td className="p-3 text-center font-bold">Le Van B</td>
                                <td className="p-3 text-center text-red-500">Ly do ABCXYZ</td>
                            </tr>

                            <tr className="border hover:bg-slate-100">
                                <td className="p-3 text-center ">Nguyen Van A</td>
                                <td className="p-3 text-center">nguyenvana@gmail.com</td>
                                <td className="p-3 text-center">0123456789</td>
                                <td className="p-3 text-center">1/1/2024</td>
                                <td className="p-3 text-center font-bold">Le Van B</td>
                                <td className="p-3 text-center text-red-500">Ly do ABCXYZ</td>
                            </tr>

                            <tr className="border hover:bg-slate-100">
                                <td className="p-3 text-center ">Nguyen Van A</td>
                                <td className="p-3 text-center">nguyenvana@gmail.com</td>
                                <td className="p-3 text-center">0123456789</td>
                                <td className="p-3 text-center">1/1/2024</td>
                                <td className="p-3 text-center font-bold">Le Van B</td>
                                <td className="p-3 text-center text-red-500">Ly do ABCXYZ</td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </section>

            <Footer />
        </div>
    )
}

export default ApprovalHistory
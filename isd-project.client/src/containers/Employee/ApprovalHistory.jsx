import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

import Header from "../HomePage/Header";
import Footer from "../HomePage/Footer";

const ApprovalHistory = () => {
    const [data, setData] = useState([]);
    useEffect(() => {
        const fetchData = async () => {
          try {
            const response = await axios.get('https://localhost:7267/api/ApprovalStatus/get-approval-status/2');
            // Update the component state with the fetched data
            setData(response.data);
          } catch (error) {
            console.error('Error fetching data:', error);
          }
        };
    
       
        fetchData();
      }, []); 
    
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
                                <th className="p-2">Tên khách hàng</th>
                                <th className="p-2">Email khách hàng</th>
                                <th className="p-2">Bảo hiểm đăng ký</th>
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
                                <th className="p-2">Tên khách hàng</th>
                                <th className="p-2">Email khách hàng</th>
                                <th className="p-2">Bảo hiểm đăng ký</th>
                                <th className="p-2">Ngày đăng ký</th>
                                <th className="p-2">Người duyệt</th>
                                <th className="p-2">Lý do từ chối</th>
                            </tr>
                        </thead>
                        <tbody >
                        {data.map((data,index) => (
                            <tr key={index} className="border hover:bg-slate-100">
                                <td className="p-3 text-center">{data.customerName}</td>
                                <td className="p-3 text-center">{data.customerEmail}</td>
                                <td className="p-3 text-center">{data.insuranceName}</td>
                                <td className="p-3 text-center">{data.approvalDate}</td>
                                <td className="p-3 text-center font-bold">{data.validationDepartmentName}</td>
                                <td className="p-3 text-center text-red-500">{data.approvalComment}</td>
                            </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </section>

            <Footer />
        </div>
    )
}

export default ApprovalHistory
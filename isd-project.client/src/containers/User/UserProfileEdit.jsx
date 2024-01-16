﻿import React, { useEffect, useState } from 'react';
import { useNavigate, useParams, useLocation } from 'react-router-dom';
import axios from 'axios';
import Header from "../HomePage/Header";
import Footer from "../HomePage/Footer";
import { NavLink } from 'react-router-dom';

const UserProfileEdit = () => {
    const location = useLocation();
    const item = location.pathname.split('/')[2];
    const [flag, setFlag] = useState();
    const [success, setSuccess] = useState(false);
    const [openModal, setOpenModal] = useState(false);
    const [openToast, setOpenToast] = useState(false);
    const navigate = useNavigate();
    
    //biến thông tin lỗi
    const [errorMessage, setErrorMessage] = useState('');

    //biến thông tin cá nhân
    const [cccd, setCccd] = useState('');
    const [day_start, setDayStart] = useState('');
    const [day_end, setDayEnd] = useState('');
    const [fullname, setFullName] = useState('');
    const [birthday, setBirthDay] = useState('');
    const [gender, setGender] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [email, setEmail] = useState('');
    const [country, setCountry] = useState('');
    const [job, setJob] = useState('');

    useEffect(() => {
        setGender('Nam');
        const fetchData = async () => {
            try {
                const response = await axios.get(`https://localhost:7267/api/User/get-user/${userId}`);
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


    //biến thông tin địa chỉ
    const [city, setCity] = useState('');
    const [district, setDistrict] = useState('');
    const [ward, setWard] = useState('');
    const [street, setStreet] = useState('');


    //object thông tin cá nhân
    const information = {
        "Bảo hiểm": item,
        "CCCD": cccd,
        "Ngày cấp": day_start,
        "Có giá trị đến": day_end,
        "Họ tên": fullname,
        "Ngày sinh": birthday,
        "Giới tính": gender,
        "Số điện thoại": phoneNumber,
        "Email": email,
        "Quốc tịch": country,
        "Nghề nghiệp": job,
    };


    //object thông tin địa chỉ
    const address = {
        "Thành phố / Tỉnh": city,
        "Quận / Huyện": district,
        "Phường / Xã": ward,
        "Đường / Ấp": street,
    };


    const [errorCCCD, setErrorCCCD] = useState('');
    const [errorDate, setErrorDate] = useState('');
    const [errorFullName, setErrorFullname] = useState('');
    const [errorPhoneNumber, setErrorPhoneNumber] = useState('');
    const [errorEmail, setErrorEmail] = useState('');
    const [errorCountry, setErrorCountry] = useState('');
    const [errorJob, setErrorJob] = useState('');


    // Hàm chuyển đổi dữ liệu để match với backend
    const convertGenderToInt = (genderString) => {
        if (genderString === 'Nam') return 0;
        else if (genderString === 'Nữ') return 1;
    }

    const convertPhoneNumber = (phoneNumber) => {
        return phoneNumber.replace('0', '+84');
    }

    const convertToBoolean = (value) => {
        if (value === 'Yes') return true;
        else if (value === 'No') return false;
    }

    const handleSendForm = async () => {
        try {
            const genderInt = convertGenderToInt(gender);
            const formattedPhoneNumber = convertPhoneNumber(phoneNumber);
            // InsuranceId
            const insuranceId = item;

            // Customer information
            const customerInfo = {
                identityDocumentId: cccd,
                dateIssued: day_start,
                validUntil: day_end,
                email: email,
                name: fullname,
                gender: genderInt,
                dateOfBirth: birthday,
                address: street + ", " + ward + ", " + district + ", " + city,
                phoneNumber: formattedPhoneNumber,
                nationality: country,
                job: job
            };

            // Gửi POST API request để tạo InsuranceContract
            const requestData = {
                insuranceId: insuranceId,
                customerRegisterRequest: customerInfo,
                healthInformationDto: healthInfo
            };
            console.log('Request:', requestData);
            const apiUrl = 'https://localhost:7267/api/Validate/validate-insurance-contract';
            const response = await axios.post(apiUrl, requestData);
            console.log('Response:', response.data);
            if(response.status === 200) {
                //TODO: Hiện thông báo thành công
                alert('Đã gửi đơn đăng ký bảo hiểm của bạn. Xin hãy đợi kết quả duyệt hồ sơ của chúng tôi');
            }
        } catch (error) {
            if (axios.isAxiosError(error)) {
                // TODO: Nếu có lỗi, hiên thông báo lỗi
                alert('Có lỗi xảy ra. Vui lòng thử lại sau.');
                setErrorMessage(error.response.data);
                console.error('Axios error:', error.response );
            } 
        }
    };

    const handleSubmit = () => {
        setFlag(1);
        setErrorCCCD('');
        setErrorDate('');
        setErrorFullname('');
        setErrorPhoneNumber('');
        setErrorEmail('');
        setErrorCountry('');
        setErrorJob('');

        // Kiểm tra nếu độ dài là 9 hoặc 12 và toàn bộ là số
        if ((cccd.length === 9 || cccd.length === 12) && /^\d+$/.test(cccd)) {
            // Điều kiện thỏa mãn, thực hiện các hành động cần thiết ở đây
            setErrorCCCD('');
            //console.log('Đã nhập đúng định dạng CCCD');
        } else {
            // Hiển thị thông báo lỗi và không thực hiện hành động
            setErrorCCCD('Định dạng không đúng. Vui lòng nhập 9 hoặc 12 ký tự số.');
            setFlag(0);
        }
        if (day_end > day_start) {
            setErrorDate('');
            //console.log('Đã nhập đúng định dạng ngày cấp và ngày hết hạn CCCD');
        } else {
            setErrorDate('Ngày "Có giá trị đến" phải lớn hơn ngày "Ngày cấp".');
            setFlag(0);
        }
        // Kiểm tra nếu tên chỉ chứa kí tự chữ
        if (/^[a-zA-ZÀ-ỹ\s]+$/.test(fullname)) {
            setErrorFullname('');
            //console.log('Đã nhập đúng định dạng họ tên');
        } else {
            setErrorFullname('Tên chỉ được chứa kí tự chữ.');
            setFlag(0);
        }
        // kiểm tra sđt chỉ toàn số
        if (/^\d+$/.test(phoneNumber)) {
            // Điều kiện thỏa mãn, thực hiện các hành động cần thiết ở đây
            setErrorPhoneNumber('');
            //console.log('Đã nhập đúng định dạng SĐT');
        } else {
            // Hiển thị thông báo lỗi và không thực hiện hành động
            setErrorPhoneNumber('Định dạng không đúng. Vui lòng chỉ điền ký tự số.');
            setFlag(0);
        }
        // Kiểm tra xem địa chỉ email có chứa ký tự @ hay không
        if (email.includes('@')) {
            setErrorEmail('');
            //console.log('Đã nhập đúng định dạng email');
        } else {
            setErrorEmail('Vui lòng điền đúng định dạng email.');
            setFlag(0);
        }
        // Kiểm tra nếu quốc tịch chỉ chứa kí tự chữ
        if (/^[a-zA-ZÀ-ỹ\s]+$/.test(country)) {
            setErrorCountry('');
            //console.log('Đã nhập đúng định dạng quốc tịch');
        } else {
            setErrorCountry('Quốc tịch chỉ được chứa kí tự chữ.');
            setFlag(0);
        }
        // Kiểm tra nếu nghề nghiệp chỉ chứa kí tự chữ
        if (/^[a-zA-ZÀ-ỹ\s]+$/.test(job)) {
            setErrorJob('');
            //console.log('Đã nhập đúng định dạng nghề nghiệp');
        } else {
            setErrorJob('Nghề nghiệp chỉ được chứa kí tự chữ.');
            setFlag(0);
        }

    };


    // biến kiểm tra yc đổi mật khẩu
    const [changePass, setchangePass] = useState(false);

    // biến đổi mật khẩu
    const [oldPass, setoldPass] = useState('');
    const [newPass, setnewPass] = useState('');
    const [confirmNewPass, setconfirmNewPass] = useState('');

    const handleChangePass = async () => {
        try {

            // InsuranceId
            const insuranceId = item;

            // Gửi POST API request để tạo InsuranceContract
            const requestData = {
                token: localStorage.getItem('token'),
                password: newPass,
                ConfirmPassword: confirmNewPass
            };
            console.log('Request:', requestData);
            const apiUrl = 'https://localhost:7267/api/UserAccount/reset-password';
            const response = await axios.post(apiUrl, requestData);
            console.log('Response:', response.data);
            if(response.status === 200) {
                //TODO: Hiện thông báo thành công
                alert('Đổi password thành công');
                setchangePass(false);
            }
        } catch (error) {
            if (axios.isAxiosError(error)) {
                // TODO: Nếu có lỗi, hiên thông báo lỗi
                alert('Có lỗi xảy ra. Vui lòng thử lại sau.');
                setErrorMessage(error.response.data);
                console.error('Axios error:', error.response );
            } 
        }
    }

    // useEffect(() => {
    //     if (flag === 1) {
    //         console.log("flag: ", flag);
    //         //handleSendForm();
    //         //setSuccess(true);
    //     }
    // }, [flag]);

    // useEffect(() => {
    //     if (flag === 0) {
    //         console.log("flag: ", flag);
            // setOpenToast(true);
    //     }
    // }, [flag]);


    useEffect(() => {
        // Kiểm tra thay đổi của success và thực hiện hành động nếu success là true
        if (success) {
            setOpenModal(true);
        }
    }, [success]);

    // mở modal thông báo gửi form thành công
    /*const handleOpenModal = () => {
        setOpenModal(true);
    }*/

    const handleCloseModal = () => {
        setOpenModal(false);
        //navigate('/san-pham-bao-hiem');
    }

    useEffect(() => {
        if (openToast) {
          const timeoutId = setTimeout(() => {
            setOpenToast(false);
          }, 4000);
          return () => clearTimeout(timeoutId);
        }
      }, [openToast]);


    return (
        <React.Fragment>
            <Header />
            <section className="h-auto px-6 py-2 rounded-xl mx-auto max-w-screen-xl bg-white border-2 border-gray-150 my-6">
                <div align="right">
                    <div>
                        <button data-modal-target="default-modal" data-modal-toggle="default-modal" type="button" 
                                onClick={() => setchangePass(true)}
                                className="bg-red-600 hover:bg-red-700 text-white py-2 px-4 rounded inline-block mr-1">
                            Đổi mật khẩu
                        </button>

                        <NavLink to="/me">
                            <h5 className="bg-gray-400 hover:bg-gray-500 text-white py-2 px-4 rounded inline-block" >
                                Trở về
                            </h5>
                        </NavLink>
                    </div>  
                </div>
                {changePass ? (
                    <>
                    <div id="default-modal" tabindex="-1" aria-hidden="true" className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
                        <div className="relative p-4 w-full max-w-2xl max-h-full">
                            <div className="relative bg-white rounded-lg shadow dark:bg-gray-700">
                                <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
                                    <h3 className="text-xl font-semibold text-gray-900 dark:text-white">
                                        Đổi mật khẩu
                                    </h3>
                                    <button type="button" onClick={() => setchangePass(false)}
                                    className="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="default-modal">
                                        <svg className="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                                        </svg>
                                        <span className="sr-only">Close modal</span>
                                    </button>
                                </div>

                                <div className="p-4 md:p-5 space-y-4">
                                    <form class="max-w-sm mx-auto">
                                        <div>
                                            <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Mật khẩu cũ</label>
                                            <input 
                                                type="password" 
                                                className="focus:outline-none focus:text-black-600 p-1 w-full border rounded"
                                                onChange={(e) => setoldPass(e.target.value)}/>
                                        </div>

                                        <div>
                                            <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Mật khẩu mới</label>
                                            <input 
                                                type="password" 
                                                className="focus:outline-none focus:text-black-600 p-1 w-full border rounded"
                                                onChange={(e) => setnewPass(e.target.value)}/>
                                        </div>

                                        <div>
                                            <label class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Nhập lại mật khẩu mới</label>
                                            <input 
                                            type="password" 
                                            className="focus:outline-none focus:text-black-600 p-1 w-full border rounded"
                                            onChange={(e) => setconfirmNewPass(e.target.value)}/>
                                        </div>
                                    </form>
                                </div>

                                <div className="p-4 md:p-5 border-t border-gray-200 rounded-b dark:border-gray-600" align="right">
                                    <button data-modal-hide="default-modal" 
                                        type="button" 
                                        onClick={() => handleChangePass()}
                                            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
                                        Đổi 
                                        </button>
                                    <button data-modal-hide="default-modal" type="button" onClick={() => setchangePass(false)}
                                            className="ms-3 text-gray-500 bg-white hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-blue-300 rounded-lg border border-gray-200 text-sm font-medium px-5 py-2.5 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
                                        Hủy
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="opacity-75 fixed inset-0 z-40 bg-black"></div>
                    </>
                ): null }

                <form className="mx-auto m-auto max-w-[790px] mt-5">

                    {/* Câu hỏi thông tin cá nhân */}
                    <p className="text-md font-bold text-[#1C1D1F] md:my-5 xs:my-4">Thông tin cá nhân</p>
                    <div className= "flex flex-wrap justify-between border-box">
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="CCCD" id="CCCD"
                                    value={cccd}
                                    className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer 
                                    ${errorCCCD && 'border-red-500' // Thêm lớp CSS đỏ khi có lỗi
                                    }`}
                                    placeholder=" "
                                    onChange={(e) => setCccd(e.target.value)}
                                    required />
                                <label htmlFor="CCCD"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Số CMND/CCCD
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                                {errorCCCD && <div className="text-red-500 text-sm mt-1">{errorCCCD}</div>}
                            </div>
                            
                        </div>
                        <div className="w-[48%] flex flex-row justify-between z-0 pt-6 pl-8 mb-5 group">
                            <div className="w-[48%] mr-2">
                                <div className="inline-flex flex-col relative w-full">
                                    <input type="date" name="dayStart" id="dayStart"
                                        value={day_start}
                                        className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                        placeholder=" "
                                        onChange={(e) => setDayStart(e.target.value)}
                                        required />
                                    <label htmlFor="dayStart"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Ngày cấp
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                </div>
                            </div>
                            <div className="w-[48%]">
                                <div className="inline-flex flex-col relative w-full">
                                    <input type="date" name="dayEnd" id="dayEnd"
                                        value={day_end}
                                        className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer
                                        ${errorDate && 'border-red-500'
                                        }`}
                                        placeholder=" "
                                        onChange={(e) => setDayEnd(e.target.value)}
                                        required />
                                    <label htmlFor="dayEnd"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Có giá trị đến
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                    {errorDate && <div className="text-red-500 text-sm mt-1">{errorDate}</div>}
                                </div>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="name" id="name"
                                    value={fullname}
                                    className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer
                                    ${errorFullName && 'border-red-500'
                                        }`}
                                    placeholder=" "
                                    onChange={(e) => setFullName(e.target.value)}
                                    required />
                                <label htmlFor="name"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Họ tên
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                                {errorFullName && <div className="text-red-500 text-sm mt-1">{errorFullName}</div>}
                            </div>
                        </div>
                        <div className="w-[48%] flex flex-row justify-between z-0 pt-6 pl-8 mb-5 group">
                            <div className="w-[48%] mr-2">
                                <div className="inline-flex flex-col relative w-full">
                                    <input type="date" name="birthDay" id="birthDay"
                                        value={birthday}
                                        className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                        placeholder=" "
                                        onChange={(e) => setBirthDay(e.target.value)}
                                        required />
                                    <label htmlFor="birthDay"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Ngày sinh
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                </div>
                            </div>
                            <div className="w-[48%]">
                                <div className="inline-flex flex-col relative w-full">
                                    <label htmlFor="gender"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Giới tính
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                    <select id="gender"
                                        className="block py-2.5 px-0 w-full text-sm text-gray-500 bg-transparent border-0 border-b-2 border-gray-200 appearance-none focus:outline-none focus:ring-0 focus:border-gray-200 peer"
                                        value={gender}
                                        onChange={(e) => setGender(e.target.value)}
                                        
                                    >
                                        <option value="Nam">Nam</option>
                                        <option value="Nữ">Nữ</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="phone" id="phone"
                                    value={phoneNumber}
                                    className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer
                                    ${errorPhoneNumber && 'border-red-500'
                                        }`}
                                    placeholder=" "
                                    onChange={(e) => setPhoneNumber(e.target.value)}
                                    required />
                                <label htmlFor="phone"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Số điện thoại
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                                {errorPhoneNumber && <div className="text-red-500 text-sm mt-1">{errorPhoneNumber}</div>}
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="email" name="email" id="email"
                                    value={email}
                                    className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer
                                    ${errorEmail && 'border-red-500'
                                        }`}
                                    placeholder=" "
                                    onChange={(e) => setEmail(e.target.value)}
                                    required />
                                <label htmlFor="email"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Email
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                                {errorEmail && <div className="text-red-500 text-sm mt-1">{errorEmail}</div>}
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="country" id="country"
                                    value={country}
                                    className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer
                                    ${errorCountry && 'border-red-500'
                                        }`}
                                    placeholder=" "
                                    onChange={(e) => setCountry(e.target.value)}
                                    required />
                                <label htmlFor="country"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Quốc tịch
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                                {errorCountry && <div className="text-red-500 text-sm mt-1">{errorCountry}</div>}
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="job" id="job"
                                    value={job}
                                    className={`block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer
                                    ${errorJob && 'border-red-500'
                                        }`}
                                    placeholder=" "
                                    onChange={(e) => setJob(e.target.value)}
                                    required />
                                <label htmlFor="job"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Nghề nghiệp
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                                {errorJob && <div className="text-red-500 text-sm mt-1">{errorJob}</div>}
                            </div>
                        </div>
                    </div>

                    {/* Câu hỏi địa chỉ */}
                    <p className="text-md font-bold text-[#1C1D1F] md:my-8 xs:my-4">Địa chỉ</p>
                    <div className="flex flex-wrap justify-between border-box">
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="city" id="city"
                                    value={city}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setCity(e.target.value)}
                                    required />
                                <label htmlFor="city"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Thành phố / Tỉnh
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="district" id="district"
                                    value={district}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setDistrict(e.target.value)}
                                    required />
                                <label htmlFor="district"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Quận / Huyện
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="ward" id="ward"
                                    value={ward}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setWard(e.target.value)}
                                    required />
                                <label htmlFor="ward"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Phường / Xã
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="street" id="street"
                                    value={street}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setStreet(e.target.value)}
                                    required />
                                <label htmlFor="street"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Đường / Ấp
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                    </div>

                    <div className="flex justify-center my-8">
                        <button type="button"
                            className="focus:outline-none text-white bg-buttonProduct hover:bg-red-700 focus:ring-4 focus:ring-red-300 font-medium rounded-xl text-sm px-20 py-3 mb-2"
                            onClick={() => {
                                handleSubmit();
                            }}
                        >
                            Xác nhận
                        </button>
                    </div>
                </form>
                {openToast && (
                    <div id="toast-danger" className="flex fixed top-5 right-5 items-center w-full max-w-xs p-4 mb-4 text-gray-500 bg-white rounded-lg shadow" role="alert">
                        <div className="inline-flex items-center justify-center flex-shrink-0 w-8 h-8 text-red-500 bg-red-100 rounded-lg">
                            <svg className="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                                <path d="M10 .5a9.5 9.5 0 1 0 9.5 9.5A9.51 9.51 0 0 0 10 .5Zm3.707 11.793a1 1 0 1 1-1.414 1.414L10 11.414l-2.293 2.293a1 1 0 0 1-1.414-1.414L8.586 10 6.293 7.707a1 1 0 0 1 1.414-1.414L10 8.586l2.293-2.293a1 1 0 0 1 1.414 1.414L11.414 10l2.293 2.293Z" />
                            </svg>
                            <span className="sr-only">Error icon</span>
                        </div>
                        <div className="ms-3 text-sm font-normal">{errorMessage}</div>
                    </div>
                )}
                {openModal && (
                    <div>
                        <div className="fixed top-0 left-0 w-full h-full bg-overLay opacity-70 z-[200]"></div>
                        <div id="successModal" tabIndex="-1" aria-hidden="true"
                            className="flex overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-[300] justify-center items-center w-full md:inset-0 h-modal md:h-full">
                            <div className="relative p-4 w-full max-w-md h-full md:h-auto">
                                <div className="relative p-4 text-center bg-white border-2 border-slate-400 rounded-lg shadow sm:p-5">
                                    <button type="button"
                                        className="text-gray-400 absolute top-2.5 right-2.5 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm p-1.5 ml-auto inline-flex items-center"
                                        data-modal-toggle="successModal"
                                        onClick={handleCloseModal}
                                    >
                                        <svg aria-hidden="true" className="w-5 h-5" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fillRule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clipRule="evenodd"></path></svg>
                                        <span className="sr-only">Close modal</span>
                                    </button>
                                    <div className="w-12 h-12 rounded-full bg-green-200 p-2 flex items-center justify-center mx-auto mb-3.5">
                                        <svg aria-hidden="true" className="w-8 h-8 text-green-500" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd"></path></svg>
                                        <span className="sr-only">Success</span>
                                    </div>
                                    <p className="mb-4 text-lg font-semibold text-gray-900"> Đã gửi thành công!
                                    </p>
                                    <p className="mb-4 text-lg font-semibold text-gray-900"> Vui lòng chờ email phản hồi của chúng tôi.
                                    </p>
                                    <button data-modal-toggle="successModal"
                                        type="button"
                                        className="w-32 py-2 px-3 text-sm font-medium text-center text-white rounded-lg bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300"
                                        onClick={handleCloseModal}
                                    >
                                        Xác nhận
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                )}
            </section>
            <Footer />
        </React.Fragment>
    )
}

export default UserProfileEdit
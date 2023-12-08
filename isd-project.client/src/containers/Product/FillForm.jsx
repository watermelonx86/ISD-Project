import React, { useEffect, useState } from 'react';
import { useNavigate, useParams, useLocation } from 'react-router-dom';
import axios from 'axios';


import Header from "../HomePage/Header";
import Footer from "../HomePage/Footer";

const FillForm = () => {
    const location = useLocation();
    const item = location.pathname.split('/')[2];

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
    }, []);


    //biến thông tin địa chỉ
    const [city, setCity] = useState('');
    const [district, setDistrict] = useState('');
    const [ward, setWard] = useState('');
    const [street, setStreet] = useState('');


    //biến khai báo sức khỏe
    const [height, setHeight] = useState('');
    const [weight, setWeight] = useState('');
    const [smoking, setSmoking] = useState('');
    const [smoking_frequency, setSmokingFrequency] = useState('');
    const [alcohol, setAlcohol] = useState('');
    const [alcohol_frequency, setAlcoholFrequency] = useState('');
    const [sport, setSport] = useState('');
    const [sport_detail, setSportDetail] = useState('');
    const [cancer, setCancer] = useState('');
    const [dengue, setDengue] = useState('');
    const [drug, setDrug] = useState('');
    const [congenital_disease, setCongenitalDisease] = useState('');
    const [congenital_disease_detail, setCongenitalDiseaseDetail] = useState('');
    const [weight_loss, setWeightLoss] = useState('');
    const [weight_loss_detail, setWeightLossDetail] = useState('');
    


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


    //object khai báo sức khỏe
    const healthDeclare = {
        "Chiều cao": height,
        "Cân nặng": weight,
        "Hút thuốc": smoking,
        "Tần suất hút thuốc": smoking_frequency,
        "Rượu bia": alcohol,
        "Tần suất rượu bia": alcohol_frequency,
        "Ma túy": drug,
        "Thể thao nguy hiểm": sport,
        "Chi tiết môn thể thao": sport_detail,
        "Ung thư": cancer,
        "Sốt xuất huyết": dengue,
        "Bệnh bẩm sinh": congenital_disease,
        "Chi tiết bệnh bẩm sinh": congenital_disease_detail,
        "Sụt cân": weight_loss,
        "Chi tiết sụt cân": weight_loss_detail,
    };

    // Hàm chuyển đổi dữ liệu để match với backend
    const convertGenderToInt = (genderString) => {
        if(genderString === 'Nam') return 1;
        else if (genderString === 'Nữ') return 2;
    }

    const convertPhoneNumber = (phoneNumber) => {
        return phoneNumber.replace('0','+84');
    }

    const convertToBoolean = (value) => {
        if (value === 'Yes') return true;
        else if (value === 'No') return false;
    }

    // Hàm gửi form
    const handleSendForm = async () => {
    try {
        const genderInt = convertGenderToInt(gender);
        const formattedPhoneNumber = convertPhoneNumber(phoneNumber);
        const api_add_customer = "https://localhost:7267/api/Customer/add-customer";
        const api_add_health_info = "https://localhost:7267/api/HealthInformation/add-health-information";

        // Customer information
        const customerInfo = {
            identityDocumentId: cccd,
            dateIssued : day_start,
            validUntil : day_end,
            email: email,
            name: fullname,
            gender: genderInt,
            address: street + ", " + ward + ", " + district + ", " + city,
            phoneNumber: formattedPhoneNumber,
            nationality: country,
            job: job
        };

        // Gửi POST API request để tạo customer
        const responseCustomer = await axios.post(api_add_customer, customerInfo);
        const customerData = responseCustomer.data;

        try {
            if (responseCustomer.status === 200) {
                console.log('Customer created successfully:', customerData);

                // Lấy UserId từ dữ liệu trả về
                const userId = customerData.userId;

                // Health information
                const healthInfo = {
                    height: Number(height),
                    weight: Number(weight),
                    smoking: convertToBoolean(smoking),
                    cigarettesPerDay: Number(smoking_frequency),
                    alcoholConsumption: convertToBoolean(alcohol),
                    daysPerWeekAlcohol: Number(alcohol_frequency),
                    drugUse: convertToBoolean(drug),
                    engagesInDangerousSports: convertToBoolean(sport),
                    dangerousSportsDetails: sport_detail,
                    diagnosedWithHealthConditions: convertToBoolean(cancer),
                    hasSpecificHealthConditions: convertToBoolean(congenital_disease),
                    experiencedDiseasesInLast5Years: convertToBoolean(dengue),
                    experiencedDiseasesDetails: congenital_disease_detail,
                    unexplainedWeightLoss: convertToBoolean(weight_loss),
                    unexplainedWeightLossDetails: weight_loss_detail,
                    customerId: Number(userId)
                };

                console.log(healthInfo);

                // Gửi POST API request để thêm thông tin sức khỏe
                const responseHealthInfo = await axios.post(api_add_health_info, healthInfo);
                const healthInfoData = responseHealthInfo.data;

                try {
                    if (responseHealthInfo.status === 200) {
                        console.log('Health information added successfully:', healthInfoData);
                    } else {
                        console.error('Failed to add health information:', healthInfoData);

                        // Nếu thất bại, gọi API để xoá customer
                        const responseDeleteCustomer = await axios.delete(`https://localhost:7267/api/Customer/delete-customer/${userId}`);
                        const deleteCustomerData = responseDeleteCustomer.data;

                        try {
                            if (responseDeleteCustomer.status === 200) {
                                console.log('Customer deleted successfully:', deleteCustomerData);
                            } else {
                                console.error('Failed to delete customer:', deleteCustomerData);
                            }
                        } catch (deleteError) {
                            console.error('Error in deleting customer:', deleteError);
                        }
                    }
                } catch (healthInfoError) {
                    console.error('Error in adding health information:', healthInfoError);
                }
            } else {
                console.error('Failed to create customer:', customerData);
            }
        } catch (customerError) {
            console.error('Error in creating customer:', customerError);
        }
    } catch (error) {
        if (axios.isAxiosError(error)) {
            // Handle Axios-specific errors
            console.error('Axios error:', error.response || error.message);
        } else {
            // Handle other types of errors
            console.error('Unexpected error:', error);
        }
    }
};


    return (
        <React.Fragment>
            <Header />
            <section className="h-auto px-6 py-2 rounded-xl mx-auto max-w-screen-xl bg-white border-2 border-gray-150 my-6">
                <div className="m-auto max-w-[725px] md:mb-0 sm:mt-3 sm:mb-3 md:px-[0px] xs:px-5 xs:mt-2 xs:mb-2 bg-white md:rounded-t-xl sm:rounded-t-xl xs:rounded-none py-[24px] leading-[28px]">
                    <p className="text-[#1C1D1F] text-[15px]">
                        Bên Mua Bảo Hiểm (đồng thời là Người được bảo hiểm)
                    </p>
                    <p className="text-[#F2A916] font-[15px]">
                        Lưu ý: Sản phẩm này chỉ dành cho Khách hàng có Quốc tịch Việt Nam và đang sinh sống tại Việt Nam.
                    </p>
                </div>
                <form className="mx-auto m-auto max-w-[790px] mt-5">

                    {/* Câu hỏi thông tin cá nhân */}
                    <p className="text-md font-bold text-[#1C1D1F] md:my-8 xs:my-4">Thông tin cá nhân</p>
                    <div className= "flex flex-wrap justify-between border-box">
                        <div className="w-[48%] flex flex-row justify-between z-0 pt-6 pl-8 mb-5 group">
                            <div className="w-[48%] mr-2">
                                <div className="inline-flex flex-col relative w-full">
                                    <input type="text" name="product" id="product"
                                        className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                        value={item }
                                        required
                                        disabled
                                    />
                                    <label htmlFor="product"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Bảo hiểm đã chọn
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                </div>
                            </div>
                            <div className="w-[48%]">
                                <div className="inline-flex flex-col relative w-full">
                                    <input type="number" name="CCCD" id="CCCD"
                                        value={cccd}
                                        className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                        placeholder=" "
                                        onChange={(e) => setCccd(e.target.value)}
                                        required />
                                    <label htmlFor="CCCD"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Số CMND/CCCD
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                </div>
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
                                        className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                        placeholder=" "
                                        onChange={(e) => setDayEnd(e.target.value)}
                                        required />
                                    <label htmlFor="dayEnd"
                                        className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                        Có giá trị đến
                                        <span className="text-require font-bold mt-[5px]"> *</span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="name" id="name"
                                    value={fullname}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setFullName(e.target.value)}
                                    required />
                                <label htmlFor="name"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Họ tên
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
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
                                        className="block py-2.5 px-0 w-full text-sm text-gray-500 bg-transparent border-0 border-b-2 border-gray-200 appearance-none dark:text-gray-400 dark:border-gray-700 focus:outline-none focus:ring-0 focus:border-gray-200 peer"
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
                                <input type="number" name="phone" id="phone"
                                    value={phoneNumber}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setPhoneNumber(e.target.value)}
                                    required />
                                <label htmlFor="phone"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Số điện thoại
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="email" name="email" id="email"
                                    value={email}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setEmail(e.target.value)}
                                    required />
                                <label htmlFor="email"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Email
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="country" id="country"
                                    value={country}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setCountry(e.target.value)}
                                    required />
                                <label htmlFor="country"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Quốc tịch
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="job" id="job"
                                    value={job}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setJob(e.target.value)}
                                    required />
                                <label htmlFor="job"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Nghề nghiệp
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
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

                    {/* Khai báo sức khỏe */}
                    <p className="text-md font-bold text-[#1C1D1F] md:my-8 xs:my-4">Khai báo sức khỏe</p>
                    <div className="flex flex-wrap justify-between border-box">
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="number" name="height" id="height"
                                    value={height}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setHeight(e.target.value)}
                                    required />
                                <label htmlFor="height"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Chiều cao (cm)
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="number" name="weight" id="weight"
                                    value={weight}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setWeight(e.target.value)}
                                    required />
                                <label htmlFor="weight"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Cân nặng (kg)
                                    <span className="text-require font-bold mt-[5px]"> *</span>
                                </label>
                            </div>
                        </div>
                     
                    </div>

                    {/* Câu hỏi hút thuốc */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">1. NĐBH có hút thuốc trong vòng 12 tháng gần đây không?</p>
                    <fieldset className= "flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="smoke-option-1" type="radio"
                                name="smoke"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={smoking === 'Yes'}
                                onChange={(e) => setSmoking(e.target.value)}
                                required
                            />

                            <label htmlFor="smoke-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="smoke-option-2" type="radio"
                                name="smoke"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={smoking === 'No'}
                                onChange={(e) => setSmoking(e.target.value)}
                                required
                            />
                            <label htmlFor="smoke-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>
                    <fieldset className="flex flex-wrap justify-between">
                        <p className="pt-6 pl-8 text-[#1C1D1F]">Nếu có, vui lòng cho biết số điếu hút bình quân trong 1 ngày?</p>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="number" name="smoking-frequency" id="smoking-frequency"
                                    value={smoking_frequency}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setSmokingFrequency(e.target.value)}
                                    />
                                <label htmlFor="smoking-frequency"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Số điếu / Ngày
                                </label>
                            </div>
                        </div>
                    </fieldset>

                    {/* Câu hỏi rượu bia */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">2. NĐBH có thường xuyên sử dụng rượu/bia không?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="alcohol-option-1" type="radio"
                                name="alcohol"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={alcohol === 'Yes'}
                                onChange={(e) => setAlcohol(e.target.value)}
                                required />
                            <label htmlFor="alcohol-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="alcohol-option-2" type="radio"
                                name="alcohol"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={alcohol === 'No'}
                                onChange={(e) => setAlcohol(e.target.value)}
                                required />
                            <label htmlFor="alcohol-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>
                    <fieldset className="flex flex-col justify-between">
                        <p className="pt-6 pl-8 text-[#1C1D1F]">Trung bình số ngày uống rượu/bia trong 1 tuần?</p>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="number" name="alcohol-frequency" id="alcohol-frequency"
                                    value={alcohol_frequency}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setAlcoholFrequency(e.target.value)}
                                    />
                                <label htmlFor="alcohol-frequency"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Ngày / Tuần
                                </label>
                            </div>
                        </div>
                    </fieldset>

                    {/* Câu hỏi ma túy */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">3. NĐBH đã từng hoặc đang sử dụng cần sa, ma
                        túy, thuốc phiện, cocain, heroin, morphin, thuốc
                        lắc, hoặc các chất gây nghiện khác mà không có chỉ định của bác sỹ không?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="drug-option-1" type="radio"
                                name="drug"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={drug === 'Yes'}
                                onChange={(e) => setDrug(e.target.value)}
                                required />
                            <label htmlFor="drug-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="drug-option-2" type="radio"
                                name="drug"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={drug === 'No'}
                                onChange={(e) => setDrug(e.target.value)}
                                required />
                            <label htmlFor="drug-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>

                    {/* Câu hỏi thể thao nguy hiểm */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">4. NĐBH có tham gia hoặc dự định tham gia bất kỳ
                        môn thể thao nguy hiểm nào sau đây không:
                        lặn, leo núi, nhảy dù, đua xe, lướt ván hoặc
                        những môn thể thao nguy hiểm khác?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="sport-option-1" type="radio"
                                name="sport"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={sport === 'Yes'}
                                onChange={(e) => setSport(e.target.value)}
                                required />
                            <label htmlFor="sport-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="sport-option-2" type="radio"
                                name="sport"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={sport === 'No'}
                                onChange={(e) => setSport(e.target.value)}
                                required />
                            <label htmlFor="sport-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>
                    <fieldset className="flex flex-col justify-between">
                        <p className="pt-6 pl-8 text-[#1C1D1F]">Nếu CÓ, vui lòng kê khai chi tiết.</p>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="sport-detail" id="sport-detail"
                                    value={sport_detail}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setSportDetail(e.target.value)}
                                    />
                                <label htmlFor="sport-detail"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Chi tiết
                                </label>
                            </div>
                        </div>
                    </fieldset>

                    {/* Câu hỏi chẩn đoán ung thư */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">5. NĐBH đã từng được chẩn đoán: Ung thư, dị sản
                        (ung thư tại chỗ), suy thận, đái tháo đường,
                        HIV/AIDS, đột quỵ, cơn thiếu máu não thoáng
                        qua hoặc bất kỳ bệnh nào liên quan đến tim?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="cancer-option-1" type="radio"
                                name="cancer"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={cancer === 'Yes'}
                                onChange={(e) => setCancer(e.target.value)}
                                required />
                            <label htmlFor="cancer-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="cancer-option-2" type="radio"
                                name="cancer"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={cancer === 'No'}
                                onChange={(e) => setCancer(e.target.value)}
                                required />
                            <label htmlFor="cancer-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>

                    {/* Câu hỏi chẩn đoán sốt xuất huyết */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">6. Trong vòng 5 năm vừa qua, NĐBH có mắc bất kỳ
                        bệnh nào sau đây không: Dengue (sốt xuất
                        huyết), thương hàn, viêm ruột do amip, sốt rét?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="dengue-option-1" type="radio"
                                name="dengue"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={dengue === 'Yes'}
                                onChange={(e) => setDengue(e.target.value)}
                                required />
                            <label htmlFor="dengue-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="dengue-option-2" type="radio"
                                name="dengue"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={dengue === 'No'}
                                onChange={(e) => setDengue(e.target.value)}
                                required />
                            <label htmlFor="dengue-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>

                    {/* Câu hỏi bệnh bẩm sinh */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">7. Ngoài các bệnh kể trên, NĐBH có bất kỳ thương
                        tật hoặc bệnh/thương tật bẩm sinh nào không:
                        bệnh xơ nang, mù, điếc, bại não, bất thường tâm
                        thần (ví dụ bệnh Down)?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="congenital-disease-option-1" type="radio"
                                name="congenital-disease"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={congenital_disease === 'Yes'}
                                onChange={(e) => setCongenitalDisease(e.target.value)}
                                required />
                            <label htmlFor="congenital-disease-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="congenital-disease-option-2" type="radio"
                                name="congenital-disease"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={congenital_disease === 'No'}
                                onChange={(e) => setCongenitalDisease(e.target.value)}
                                required />
                            <label htmlFor="congenital-disease-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>
                    <fieldset className="flex flex-col justify-between">
                        <p className="pt-6 pl-8 text-[#1C1D1F]">Nếu CÓ, vui lòng kê khai chi tiết.</p>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="congenital-disease-detail" id="congenital-disease-detail"
                                    value={congenital_disease_detail}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setCongenitalDiseaseDetail(e.target.value)}
                                    />
                                <label htmlFor="congenital-disease-detail"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Chi tiết
                                </label>
                            </div>
                        </div>
                    </fieldset>

                    {/* Câu hỏi sụt cân */}
                    <p className="pt-6 pl-8 text-[#1C1D1F]">8. Ngoài các bệnh kể trên, NĐBH có bất kỳ dấu
                        hiệu, triệu chứng bất thường nào bao gồm sụt
                        cân không rõ nguyên nhân mà chưa đi khám
                        không?</p>
                    <fieldset className="flex flex-wrap justify-between">
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="weight-loss-option-1" type="radio"
                                name="weight-loss"
                                value="Yes"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={weight_loss === 'Yes'}
                                onChange={(e) => setWeightLoss(e.target.value)}
                                required />
                            <label htmlFor="weight-loss-option-1" className="block ms-2 text-sm font-medium text-gray-900">
                                Có
                            </label>
                        </div>
                        <div className="flex items-center w-[48%] mb-4 pt-6 pl-8">
                            <input id="weight-loss-option-2" type="radio"
                                name="weight-loss"
                                value="No"
                                className="w-4 h-4 border-gray-300 focus:ring-2 focus:ring-blue-300"
                                checked={weight_loss === 'No'}
                                onChange={(e) => setWeightLoss(e.target.value)}
                                required />
                            <label htmlFor="weight-loss-option-2" className="block ms-2 text-sm font-medium text-gray-900">
                                Không
                            </label>
                        </div>
                    </fieldset>
                    <fieldset className="flex flex-col justify-between">
                        <p className="pt-6 pl-8 text-[#1C1D1F]">Nếu CÓ, vui lòng kê khai chi tiết.</p>
                        <div className="w-[48%] z-0 pt-6 pl-8 mb-5 group">
                            <div className="inline-flex flex-col relative w-full">
                                <input type="text" name="weight-loss-detail" id="weight-loss-detail"
                                    value={weight_loss_detail}
                                    className="block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:border-blue-600 peer"
                                    placeholder=" "
                                    onChange={(e) => setWeightLossDetail(e.target.value)}
                                    />
                                <label htmlFor="weight-loss-detail"
                                    className="peer-focus:font-medium absolute text-sm text-gray-500 duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:start-0 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto peer-focus:text-blue-600 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6">
                                    Chi tiết
                                </label>
                            </div>
                        </div>
                    </fieldset>

                    <div className="flex justify-center my-8">
                        <button type="button"
                            className="focus:outline-none text-white bg-buttonProduct hover:bg-red-700 focus:ring-4 focus:ring-red-300 font-medium rounded-3xl text-sm px-20 py-3 mb-2"
                            onClick={handleSendForm}
                        >
                            Gửi phiếu thông tin
                        </button>
                    </div>
                </form>
            </section>
            <Footer />
        </React.Fragment>
    )
}

export default FillForm
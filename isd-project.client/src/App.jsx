import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import React from 'react';
import HomePage from './containers/HomePage/HomePage';
import LoginForm from './containers/Auth/LoginForm';
import RegisterForm from './containers/Auth/RegisterForm';
import UserProfile from './containers/User/UserProfile';
import UserProfileEdit from './containers/User/UserProfileEdit';
import MyProduct from './containers/User/MyProduct';
import Health from './containers/User/Health';
import IncurenceManage from './containers/User/InsurenceManage';
import Product from './containers/Product/Product';
import ProductDetail from './containers/Product/ProductDetail';
import FillForm from './containers/Product/FillForm';
import WaitApproval from './containers/Employee/WaitApproval';
import ApprovalHistory from './containers/Employee/ApprovalHistory';
import ActivateAccount from './containers/User/ActivateAccount';

import { Dashboard, Auth } from "./containers/Admin/layouts";

import InsureceManage from './containers/User/InsurenceManage';
function App() {
  return (
    //<Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/signup" element={<RegisterForm />} />
        <Route path="/me" element={<UserProfile />} />
        <Route path="/edit" element={<UserProfileEdit />} />
        <Route path="/my-product" element={<MyProduct />} />
        <Route path="/health" element={<Health />} />
        <Route path="/insurence" element={<InsureceManage />} />
        <Route path="/san-pham-bao-hiem" element={<Product />} />
        <Route path="/san-pham-bao-hiem/:id" element={<ProductDetail />} />
        <Route path="/san-pham-bao-hiem/:id/dang-ky-bao-hiem" element={<FillForm />} />
        <Route path="/duyet-don-dang-ky" element={<WaitApproval />} />
        <Route path="/lich-su-duyet" element={<ApprovalHistory />} />
        {/* New route for account activation */}
        <Route path="/activate/:userAccountId" element={<ActivateAccount />} />
        <Route path="/dashboard/*" element={<Dashboard />} />
        <Route path="*" element={<Navigate to="/" replace />} />
        {/* <Route path="test" element={<FillForm />} /> */}
      </Routes>
    //</Router>
  );
}

export default App;

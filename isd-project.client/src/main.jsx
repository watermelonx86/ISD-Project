import React from 'react'
import ReactDOM from 'react-dom/client'
// import App from './App.jsx'
import './index.css'
import LoginForm from './components/LoginForm.jsx'
import RegisterForm from './components/RegisterForm.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
        {/* <App /> */}
        <LoginForm />
        <RegisterForm />
  </React.StrictMode>,
)

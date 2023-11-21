import { useState } from "react";
import axios from "axios";
const RegisterForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const handleRegister = () => {
        axios.post('https://localhost:7267/api/User/register', {
            email : email,
            password: password,
            confirmPassword: confirmPassword
        }).then(response => {
            console.log(response.data);
        }).catch(error => {
            if (error.response) {
                console.error('Request failed with status code:', error.response.status);
                console.error('Response data:', error.response.data);
            } else if (error.request) {
                console.error('No response received from server:', error.request);
            } else {
                console.error('Error:', error.message);
            }
        })
    }

    return (
    <div>
        <h1>Register Form</h1>
        <form>
            <label>
                Email
                <input type="text" value={email} onChange={(e) => setEmail(e.target.value)}></input>
            </label>
            <br/>
            <label>
                Password
                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)}></input>
            </label>
            <br/>
            <label>
                Confirm Password
                <input type="password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)}></input>
            </label>
            <br/>
            <button type="button" onClick={handleRegister}>Register</button>
        </form>
    </div>
    );
}

export default RegisterForm;
import { useState } from 'react';
import axios from 'axios'

const LoginForm = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = () => {
        axios.post('https://localhost:7267/api/User/login', {
            email: email,
            password: password
        })
            .then(response => {
                console.log(response.data);
            })
            .catch(error => {
                if (error.response) {
                    console.error('Request failed with status code:', error.response.status);
                    console.error('Response data:', error.response.data);
                } else if (error.request) {
                    console.error('No response received from server:', error.request);
                } else {
                    console.error('Error:', error.message);
                }
            });
    };


    return (
    <div>
        <h1>Login Form</h1>
        <form>
            <label>Email: 
                <input type="text" value={email} onChange={(e) => setEmail(e.target.value)} />
            </label>
            <br />
            <label> Password: 
                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            </label>
            <br />
            <button type="button" onClick={handleLogin}>Login</button>
        </form>
    </div>
    )

}
export default LoginForm;
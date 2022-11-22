import './login.scss';
import React, { useState } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import logo from '../../assets/images/logo.png';
import { useAuthContext } from '../../context/RequiredAuth/authContext';

const Login = () => {
    const [isUserNameError, setIsUserNameError] = useState('');
    const [isPasswordError, setIsPasswordError] = useState('');
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const { setIsAuthenticated, setUser } = useAuthContext();

    const handleLogin = () => {
        let isAdmin = false;

        userName === '' ? setIsUserNameError('User name is required') : setIsUserNameError('');
        password === '' ? setIsPasswordError('Password is required') : setIsPasswordError('');

        if (userName === 'adminHN' && password === 'Admin@123') {
            isAdmin = true;
            localStorage.setItem('accessToken', true);
            setIsAuthenticated(true);
            navigate('/');
        }

        if (userName === 'user' && password === 'user@123') {
            isAdmin = false;
            localStorage.setItem('accessToken', true);
            setIsAuthenticated(true);
            navigate('/');
        }

        setUser({
            name: userName,
            isAdmin,
        });
    };

    return (
        <section className="screen">
            <div className="body">
                <form className="form">
                    <img src={logo} alt="" className="logo"></img>

                    <div className="form_item">
                        <label>User name:</label>
                        <input
                            className={`${isUserNameError ? 'input-error' : ''}`}
                            type="text"
                            placeholder="Enter username"
                            value={userName}
                            onChange={(e) => {
                                e.target.value === '' ? setIsUserNameError('User name is required') : setIsUserNameError('');
                                setUserName(e.target.value);
                            }}
                            onBlur={(e) => {
                                e.target.value === '' ? setIsUserNameError('User name is required') : setIsUserNameError('');
                            }}
                            onFocus={() => {
                                setIsUserNameError('');
                            }}
                        />
                        {isUserNameError && <label className="form_item_error">{isUserNameError}</label>}
                    </div>
                    <div className="form_item">
                        <label>Password: </label>
                        <input
                            type="password"
                            id="input-bar"
                            className={`${isPasswordError ? 'input-error' : ''}`}
                            placeholder="Enter password"
                            value={password}
                            onChange={(e) => {
                                e.target.value === '' ? setIsPasswordError('Password is required') : setIsPasswordError('');

                                setPassword(e.target.value);
                            }}
                            onBlur={(e) => {
                                e.target.value === '' ? setIsPasswordError('Password is required') : setIsPasswordError('');
                            }}
                            onFocus={() => {
                                setIsPasswordError('');
                            }}
                        />
                        {isPasswordError && <label className="form_item_error">{isPasswordError}</label>}
                    </div>

                    <Button variant="danger" onClick={handleLogin}>
                        Login
                    </Button>
                </form>
            </div>
        </section>
    );
};

export default Login;

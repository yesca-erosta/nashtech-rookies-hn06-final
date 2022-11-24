import './login.scss';
import React, { useState, useEffect } from 'react';
import { Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import logo from '../../assets/images/logo.png';
import { useAuthContext } from '../../context/RequiredAuth/authContext';

const Login = () => {
    const [isUserNameError, setIsUserNameError] = useState('');
    const [isPasswordError, setIsPasswordError] = useState('');
    const [isLoginError, setIsLoginError] = useState(false);
    const [isNoResponseError, setIsNoResponseError] = useState(false);
    const [disable, setDisable] = useState(true);

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const { setIsAuthenticated, setToken, setOldPasswordLogin } = useAuthContext();

    useEffect(() => {
        if (!Boolean(userName) || !Boolean(password)) {
            setDisable(true);
        } else {
            setDisable(false);
        }

        return;
    }, [userName, password]);

    const handleLogin = async () => {
        userName === '' ? setIsUserNameError('User name is required') : setIsUserNameError('');
        password === '' ? setIsPasswordError('Password is required') : setIsPasswordError('');

        const result = await fetch(`https://localhost:7060/api/Account`, {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify({
                username: userName,
                password: password,
            }),
        });

        const data = await result.json();

        if (data.token) {
            setIsAuthenticated(true);
            navigate('/');
        }

        if (result.status === 400) {
            setIsLoginError(true);
        }

        if (result.status === 500) {
            setIsNoResponseError(true);
            setIsLoginError(false);
        }

        if (!userName || !password) {
            setIsLoginError(false);
        }

        setToken(data);
        sessionStorage.setItem('localStorage', data.token);
        setOldPasswordLogin(password);
        sessionStorage.setItem('userInformation', JSON.stringify(data));
    };

    return (
        <section className="screen">
            <div className="body">
                <form className="form">
                    <img src={logo} alt="" className="logo"></img>

                    <div className="form_item">
                        <label>Username:</label>
                        <input
                            className={`${isUserNameError ? 'input-error' : ''}`}
                            type="text"
                            placeholder="Enter username"
                            value={userName}
                            onChange={(e) => {
                                e.target.value === '' ? setIsUserNameError('User name is required') : setIsUserNameError('');
                                setUserName(e.target.value);
                                setIsLoginError(false);
                            }}
                            onBlur={(e) => {
                                e.target.value === '' ? setIsUserNameError('User name is required') : setIsUserNameError('');
                            }}
                            onFocus={() => {
                                setIsUserNameError('');
                                setIsLoginError(false);
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
                                setIsLoginError(false);
                                setPassword(e.target.value);
                            }}
                            onBlur={(e) => {
                                e.target.value === '' ? setIsPasswordError('Password is required') : setIsPasswordError('');
                            }}
                            onFocus={() => {
                                setIsPasswordError('');
                                setIsLoginError(false);
                            }}
                        />
                        {isPasswordError && <label className="form_item_error">{isPasswordError}</label>}
                    </div>

                    <Button variant="danger" onClick={handleLogin} disabled={disable}>
                        Login
                    </Button>

                    {isLoginError && (
                        <div className="login_false">
                            <div>Username or password is incorrect.</div>
                            <div>Please try again!</div>
                        </div>
                    )}
                    {isNoResponseError && <div className="login_false">Sorry the request failed!</div>}
                </form>
            </div>
        </section>
    );
};

export default Login;

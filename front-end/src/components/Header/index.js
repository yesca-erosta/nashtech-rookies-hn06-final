import classNames from 'classnames/bind';
import styles from './header.module.scss';
import { Outlet, useLocation } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import routes from '../../config/routes';
import { useEffect, useState } from 'react';
import React from 'react';

import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import Dropdown from 'react-bootstrap/Dropdown';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { AiFillEyeInvisible, AiFillEye } from 'react-icons/ai';
import { useAuthContext } from '../../context/RequiredAuth/authContext';

const cx = classNames.bind(styles);

function Header() {
    const location = useLocation();
    const [name, setName] = useState();
    const [hideOld, setHideOld] = useState(false);
    const [hideNew, setHideNew] = useState(false);
    const [disable, setDisable] = useState(true);

    const [newPassword, setNewPassword] = useState('');
    const [oldPassword, setOldPassword] = useState('');
    const [isOldPasswordError, setIsOldPasswordError] = useState(false);
    const [isEmptyPasswordError, setIsEmptyPasswordError] = useState(false);
    const [isSamePasswordError, setIsSamePasswordError] = useState(false);
    const [isComplexityPasswordError, setIsComplexityPasswordError] = useState(false);

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [showSuccess, setShowSuccess] = useState(false);
    const handleCloseSuccess = () => setShowSuccess(false);

    const [showChangePassword, setShowChangePassword] = useState(false);
    const handleCloseChangePassword = () => setShowChangePassword(false);
    const handleShowChangePassWord = () => setShowChangePassword(true);

    const { token, oldPasswordLogin, setToken } = useAuthContext();

    useEffect(() => {
        const user = localStorage.getItem('userInformation');
        if (user) {
            try {
                setToken(JSON.parse(user));
            } catch (error) {}
        }
    }, [setToken]);

    const handleCloseRemoveAccessToken = () => {
        setShow(false);
        localStorage.removeItem('localStorage');
    };

    const toggleBtnOld = () => {
        setHideOld((pre) => !pre);
    };

    const toggleBtnNew = () => {
        setHideNew((pre) => !pre);
    };

    useEffect(() => {
        if (!Boolean(oldPassword) || !Boolean(newPassword)) {
            setDisable(true);
        } else {
            setDisable(false);
        }

        return;
    }, [oldPassword, newPassword]);

    setTimeout(() => {
        localStorage.removeItem('localStorage');
        window.location.reload();
    }, 6000000);

    useEffect(() => {
        const result = Object.entries(routes).filter(([key, value]) => {
            if (location.pathname === value.path) {
                return value.name;
            }

            return null;
        });

        setName(result[0][1].name);
    }, [location]);

    const handleSave = async () => {
        const result = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Account`, {
            method: 'PUT',
            headers: {
                Accept: 'application/json',
                Authorization: `Bearer ${token.token}`,
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },

            body: JSON.stringify({
                oldPassword: oldPassword,
                newPassword: newPassword,
            }),
        });

        oldPassword !== oldPasswordLogin ? setIsOldPasswordError(true) : setIsOldPasswordError(false);
        !newPassword ? setIsEmptyPasswordError(true) : setIsEmptyPasswordError(false);
        newPassword === oldPassword ? setIsSamePasswordError(true) : setIsSamePasswordError(false);

        oldPassword === oldPasswordLogin && newPassword !== oldPassword && newPassword && result.status === 400
            ? setIsComplexityPasswordError(true)
            : setIsComplexityPasswordError(false);

        if (result.status === 200) {
            setShowChangePassword(false);
            setShowSuccess(true);
        } else {
            setShowSuccess(false);
        }
    };

    return (
        <header className={cx('wrapper')}>
            <div className={cx('inner')}>
                <div className={cx('inner-page')}>{name}</div>

                <div className={cx('inner-name')}>
                    <div>{token.userName}</div>
                    <div>
                        <Dropdown>
                            <Dropdown.Toggle variant="" style={{ color: 'white' }} />

                            <Dropdown.Menu style={{ color: 'white' }}>
                                <Dropdown.Item>Action</Dropdown.Item>
                                <Dropdown.Item onClick={handleShowChangePassWord}>Change password</Dropdown.Item>
                                <NavDropdown.Divider />
                                <Dropdown.Item onClick={handleShow}>Log out</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                        <Outlet />
                    </div>
                </div>
            </div>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header>
                    <h3 className={cx('modal-title')}>Are you sure</h3>
                </Modal.Header>
                <Modal.Body>Do you want to log out?</Modal.Body>
                <Modal.Footer style={{ justifyContent: 'flex-start' }}>
                    <Button variant="danger" onClick={handleCloseRemoveAccessToken} href="/">
                        Log out
                    </Button>
                    <Button variant="outline-primary" onClick={handleClose}>
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={showChangePassword} onHide={handleCloseChangePassword}>
                <Modal.Header>
                    <h3 className={cx('modal-title')}>Change Password</h3>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <h6>Old password:</h6>
                        <div className={cx('input-new-password')}>
                            <Form.Control
                                type={hideOld ? 'text' : 'password'}
                                placeholder="Enter old password"
                                value={oldPassword}
                                onChange={(e) => {
                                    setOldPassword(e.target.value);
                                }}
                                onFocus={() => {
                                    setIsOldPasswordError(false);
                                    setIsEmptyPasswordError(false);
                                    setIsSamePasswordError(false);
                                }}
                            />
                            <div className={cx('icon-new')} onClick={toggleBtnOld}>
                                {hideOld ? <AiFillEye /> : <AiFillEyeInvisible />}
                            </div>
                        </div>
                    </Form>
                    {isOldPasswordError && <div className={cx('oldPassword_false')}>Password is incorrect!</div>}
                    <br></br>
                    <Form>
                        <h6>New password:</h6>
                        <div className={cx('input-new-password')}>
                            <Form.Control
                                type={hideNew ? 'text' : 'password'}
                                placeholder="Enter new password"
                                value={newPassword}
                                onChange={(e) => {
                                    setNewPassword(e.target.value);
                                }}
                                onFocus={() => {
                                    setIsEmptyPasswordError(false);
                                    setIsSamePasswordError(false);
                                }}
                            />
                            <div className={cx('icon-new')} onClick={toggleBtnNew}>
                                {hideNew ? <AiFillEye /> : <AiFillEyeInvisible />}
                            </div>
                        </div>
                    </Form>
                    {isEmptyPasswordError && (
                        <div className={cx('oldPassword_false')}>You should provide the new password!</div>
                    )}

                    {isSamePasswordError && (
                        <div className={cx('oldPassword_false')}>
                            The new password should not be the same with the old password!
                        </div>
                    )}

                    {isComplexityPasswordError && (
                        <div className={cx('oldPassword_false')}>The password should match the complexity!</div>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={handleSave} disabled={disable}>
                        Save
                    </Button>
                    <Button variant="outline-primary" onClick={handleCloseChangePassword} href="">
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={showSuccess} onHide={handleCloseSuccess}>
                <Modal.Header closeButton>
                    <h3 className={cx('modal-title')}>Change password</h3>
                </Modal.Header>
                <Modal.Body>Your password has been changed successfully!</Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-primary" onClick={handleCloseSuccess}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </header>
    );
}

export default Header;

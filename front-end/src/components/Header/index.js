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

const cx = classNames.bind(styles);

function Header() {
    const location = useLocation();
    const [name, setName] = useState();
    const [hideOld, setHideOld] = useState(false);
    const [hideNew, setHideNew] = useState(false);

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [showChangePassword, setShowChangePassword] = useState(false);
    const handleCloseChangePassword = () => setShowChangePassword(false);
    const handleShowChangePassWord = () => setShowChangePassword(true);

    const handleCloseRemoveAccessToken = () => {
        setShow(false);
        localStorage.removeItem('accessToken');
    };

    const toggleBtnOld = () => {
        setHideOld((pre) => !pre);
    };

    const toggleBtnNew = () => {
        setHideNew((pre) => !pre);
    };

    useEffect(() => {
        const result = Object.entries(routes).filter(([key, value]) => {
            if (location.pathname === value.path) {
                return value.name;
            }

            return null;
        });

        setName(result[0][1].name);
    }, [location]);

    return (
        <header className={cx('wrapper')}>
            <div className={cx('inner')}>
                <div className={cx('inner-page')}>{name}</div>

                <div className={cx('inner-name')}>
                    <div>binhnv</div>
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
                <Modal.Header closeButton>
                    <h3 className={cx('modal-title')}>Are you sure</h3>
                </Modal.Header>
                <Modal.Body>Do you want to log out?</Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={handleCloseRemoveAccessToken} href="/">
                        Log out
                    </Button>
                    <Button variant="outline-primary" onClick={handleClose}>
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={showChangePassword} onHide={handleCloseChangePassword}>
                <Modal.Header closeButton>
                    <h3 className={cx('modal-title')}>Change Password</h3>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <h6>Old password:</h6>
                        <div className={cx('input-new-password')}>
                            <Form.Control type={hideOld ? 'text' : 'password'} placeholder="Enter old password" />
                            <div className={cx('icon-new')} onClick={toggleBtnOld}>
                                {hideOld ? <AiFillEyeInvisible /> : <AiFillEye />}
                            </div>
                        </div>
                    </Form>
                    <br></br>
                    <Form>
                        <h6>New password:</h6>
                        <div className={cx('input-new-password')}>
                            <Form.Control type={hideNew ? 'text' : 'password'} placeholder="Enter new password" />
                            <div className={cx('icon-new')} onClick={toggleBtnNew}>
                                {hideNew ? <AiFillEyeInvisible /> : <AiFillEye />}
                            </div>
                        </div>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger">Submit</Button>
                    <Button variant="outline-primary" onClick={handleCloseChangePassword} href="">
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>
        </header>
    );
}

export default Header;

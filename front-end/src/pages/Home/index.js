import { useAuthContext } from '../../context/RequiredAuth/authContext';
import { useEffect, useState } from 'react';
import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import { AiFillEyeInvisible, AiFillEye } from 'react-icons/ai';
import './home.scss';

import classNames from 'classnames/bind';
import styles from '../../components/Header/header.module.scss';

const cx = classNames.bind(styles);

function Home() {
    const [hideNew, setHideNew] = useState(false);
    const { token, oldPasswordLogin } = useAuthContext();
    const [showFirstChangePassword, setShowFirstChangePassWord] = useState(false);
    const [newPassword, setNewPassword] = useState('');

    const [isEmptyPasswordError, setIsEmptyPasswordError] = useState(false);
    const [isSamePasswordError, setIsSamePasswordError] = useState(false);

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);

    const toggleBtnNew = () => {
        setHideNew((pre) => !pre);
    };

    useEffect(() => {
        if (token.needUpdatePwdOnLogin) {
            setShowFirstChangePassWord(true);
        } else {
            setShowFirstChangePassWord(false);
        }
    }, [token]);

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
                oldPassword: oldPasswordLogin,
                newPassword: newPassword,
            }),
        });

        if (result.status === 200) {
            setShowFirstChangePassWord(false);
            setShow(true);
        }

        !newPassword ? setIsEmptyPasswordError(true) : setIsEmptyPasswordError(false);
        newPassword === oldPasswordLogin ? setIsSamePasswordError(true) : setIsSamePasswordError(false);
    };

    return (
        <div>
            <Modal
                show={showFirstChangePassword}
                onHide={() => {
                    if (token.needUpdatePwdOnLogin) {
                        setShowFirstChangePassWord(true);
                    } else {
                        setShowFirstChangePassWord(false);
                    }
                }}
            >
                <Modal.Header closeButton>
                    <h3 className={cx('modal-title')}>Change Password</h3>
                </Modal.Header>
                <Modal.Body>
                    <div>This is the first time you logged in.</div>
                    <div>You have to change your password to continue.</div>
                    <hr></hr>
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
                            />
                            <div className={cx('icon-new')} onClick={toggleBtnNew}>
                                {hideNew ? <AiFillEyeInvisible /> : <AiFillEye />}
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
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={handleSave}>
                        Save
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <h3 className={cx('modal-title')}>Change password</h3>
                </Modal.Header>
                <Modal.Body>Your password has been changed successfilly!</Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-primary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
            <h1>Home </h1>
        </div>
    );
}

export default Home;

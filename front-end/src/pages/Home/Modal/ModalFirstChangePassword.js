import classNames from 'classnames/bind';
import { useState } from 'react';
import { useEffect } from 'react';
import { Button, Form, Modal } from 'react-bootstrap';
import { AiFillEye, AiFillEyeInvisible } from 'react-icons/ai';
import { useNavigate } from 'react-router-dom';
import styles from '../../../components/Header/header.module.scss';
import { Loading } from '../../../components/Loading/Loading';
import { BASE_URL, TOKEN_KEY } from '../../../constants';
import { useAppContext } from '../../../context/RequiredAuth/authContext';
export const ModalFirstChangePassword = () => {
    const cx = classNames.bind(styles);
    const [hideNew, setHideNew] = useState(false);
    const { token, oldPasswordLogin } = useAppContext();
    const [showFirstChangePassword, setShowFirstChangePassWord] = useState(false);
    const [newPassword, setNewPassword] = useState('');
    const [disable, setDisable] = useState(true);

    const [isEmptyPasswordError, setIsEmptyPasswordError] = useState(false);
    const [isSamePasswordError, setIsSamePasswordError] = useState(false);
    const [isComplexityPasswordError, setIsComplexityPasswordError] = useState(false);

    const [show, setShow] = useState(false);
    const navigate = useNavigate();

    const toggleBtnNew = () => {
        setHideNew((pre) => !pre);
    };

    useEffect(() => {
        if (token.needUpdatePwdOnLogin === true) {
            setShowFirstChangePassWord(true);
        } else {
            setShowFirstChangePassWord(false);
        }
    }, [token]);

    useEffect(() => {
        if (!Boolean(newPassword)) {
            setDisable(true);
        } else {
            setDisable(false);
        }

        return;
    }, [newPassword]);

    const [loading, setLoading] = useState(false);

    const handleSave = async () => {
        setLoading(true);
        const result = await fetch(`${BASE_URL}/Account`, {
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

        if (result.status === 400) {
            setIsComplexityPasswordError(true);
        }

        if (newPassword === oldPasswordLogin) {
            setIsSamePasswordError(true);
            setIsComplexityPasswordError(false);
        } else {
            setIsSamePasswordError(false);
        }

        setLoading(false);
    };

    const handleOnChangeEnter = (e) => {
        if (e.key === 'Enter') {
            handleSave();
        }
    };

    const handleClose = () => {
        localStorage.removeItem(TOKEN_KEY);
        setShow(false);
        navigate('/login');
    };
    return (
        <>
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
                <Modal.Header>
                    <h3 className={cx('modal-title')}>Change Password</h3>
                </Modal.Header>
                <Modal.Body>
                    <div>This is the first time you logged in.</div>
                    <div>You have to change your password to continue.</div>
                    <hr></hr>
                    <Form onSubmit={(e) => e.preventDefault()}>
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
                                    setIsComplexityPasswordError(false);
                                }}
                                onKeyUp={handleOnChangeEnter}
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
                        <div className={cx('oldPassword_false')}>
                            The password must more than 8 character long and has at least 1 uppercase letter and 1 number
                        </div>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={handleSave} disabled={disable}>
                        Save
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header>
                    <h3 className={cx('modal-title')}>Change password</h3>
                </Modal.Header>
                <Modal.Body>Your password has been changed successfully!</Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>

            {loading && <Loading />}
        </>
    );
};

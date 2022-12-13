import classNames from 'classnames/bind';
import { React, useMemo, useState } from 'react';
import { Toast, ToastContainer } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useLocation, useNavigate } from 'react-router-dom';
import { updateData } from '../../../apiServices';
import { USER } from '../../../constants';
import styles from './EditUser.module.scss';
import DatePicker from 'react-datepicker';

const EditUser = () => {
    const cx = classNames.bind(styles);
    const location = useLocation();
    const { user } = location?.state;

    const initUser = {
        dateOfBirth: new Date(user.dateOfBirth),
        gender: user.gender,
        joinedDate: new Date(user.joinedDate),
        type: user.type === 'Staff' ? 0 : 1,
        password: '',
        userName: user.userName,
    };

    const [data, setData] = useState(initUser);

    const [isShowToast, setIsShowToast] = useState(false);

    const [arrMsg, setArrMsg] = useState({
        UserName: '',
        Password: '',
        FirstName: '',
        LastName: '',
        DateOfBirth: '',
        JoinedDate: '',
    });
    const navigate = useNavigate();

    const onSubmit = async () => {
        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object dataAdd
        Object.keys(data).map((k) => (data[k] = typeof data[k] == 'string' ? data[k].trim() : data[k]));

        const res = await updateData(`${USER}/${user.id}`, data);

        if (res.code === 'ERR_BAD_REQUEST') {
            setArrMsg(res?.response?.data?.errors);
            if (res?.response?.status === 409) {
                setArrMsg({ UserName: [res?.response?.data] });
            }
            if (res?.response?.data?.errors?.requestModel) {
                alert('Please input all fields');
            }
        } else {
            setData(initUser);

            setIsShowToast(true);
            setTimeout(() => {
                setIsShowToast(false);
            }, 5000);

            navigate('/manageruser');
        }
    };

    const onChange = (e) => {
        setArrMsg('');
        if (e.target.name === 'gender' || e.target.name === 'type') {
            setData({ ...data, [e.target.name]: parseInt(e.target.value) ?? 0 });
        } else {
            setData({ ...data, [e.target.name]: e.target.value });
        }
    };

    const onChangeDateOfBirth = (date) => {
        setData({ ...data, dateOfBirth: date });
    };

    const onChangeJoinedDate = (date) => {
        setData({ ...data, joinedDate: date });
    };

    const isInputComplete = useMemo(() => {
        const { type, userName, password, gender, ...otherData } = data;
        return Object.values(otherData).every((x) => x !== null && x !== '');
    }, [data]);

    return (
        <>
            <div className={cx('container')}>
                <h3 className={cx('title')}>Edit User</h3>

                <Form>
                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>First Name</Form.Label>
                        <Form.Control
                            type="text"
                            className={cx('input')}
                            name="firstName"
                            placeholder={user.firstName}
                            readOnly
                            disabled
                        />
                    </Form.Group>
                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Last Name</Form.Label>
                        <Form.Control
                            type="text"
                            className={cx('input')}
                            name="lastName"
                            placeholder={user.lastName}
                            readOnly
                            disabled
                        />
                    </Form.Group>

                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
                        {/* <Form.Control
                            isInvalid={arrMsg.DateOfBirth}
                            type="date"
                            className={cx('input')}
                            name="dateOfBirth"
                            value={dateStrToDate(data.dateOfBirth)}
                            onChange={onChange}
                        /> */}

                        <DatePicker
                            name="dateOfBirth"
                            selected={data.dateOfBirth}
                            className={`${arrMsg?.DateOfBirth ? 'border-danger' : ''} form-control w-full `}
                            onChange={(date) => onChangeDateOfBirth(date)}
                            placeholderText="dd/MM/yyyy"
                            dateFormat="dd/MM/yyyy"
                        />
                    </Form.Group>
                    {arrMsg.DateOfBirth && <p className={cx('msgError')}>{arrMsg.DateOfBirth[0]}</p>}
                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Gender</Form.Label>
                        <div key={`gender-radio`} onChange={onChange} className={cx('input-radio-gender')}>
                            <Form.Check
                                inline
                                label="Male"
                                name="gender"
                                type="radio"
                                value={parseInt(1)}
                                id={`gender-radio-1`}
                                defaultChecked={user.gender === 1}
                            />
                            <Form.Check
                                inline
                                label="Female"
                                name="gender"
                                type="radio"
                                value={parseInt(2)}
                                id={`gender-radio-2`}
                                defaultChecked={user.gender === 2}
                            />
                        </div>
                    </Form.Group>

                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Joined Date</Form.Label>
                        {/* <Form.Control
                            isInvalid={arrMsg.JoinedDate}
                            type="date"
                            className={cx('input')}
                            name="joinedDate"
                            value={dateStrToDate(data.joinedDate)}
                            onChange={onChange}
                        /> */}
                        <DatePicker
                            name="joinedDate"
                            selected={data.joinedDate}
                            className={`${arrMsg?.JoinedDate ? 'border-danger' : ''} form-control w-full `}
                            onChange={(date) => onChangeJoinedDate(date)}
                            placeholderText="dd/MM/yyyy"
                            dateFormat="dd/MM/yyyy"
                        />
                    </Form.Group>
                    {arrMsg.JoinedDate && <p className={cx('msgError')}>{arrMsg.JoinedDate[0]}</p>}
                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Type</Form.Label>
                        <Form.Select onChange={onChange} name="type" value={data.type}>
                            <option value={0} defaultValue={data.type === 0}>
                                Staff
                            </option>
                            <option value={1} defaultValue={data.type === 1}>
                                Admin
                            </option>
                        </Form.Select>
                    </Form.Group>
                    <div className={cx('button')}>
                        <Button variant="danger" onClick={onSubmit} disabled={!isInputComplete}>
                            Save
                        </Button>
                        <Button
                            variant="outline-secondary"
                            onClick={() => {
                                navigate('/manageruser');
                            }}
                            className={cx('cancel-button')}
                        >
                            Cancel
                        </Button>
                    </div>
                </Form>
            </div>
            <ToastContainer className="p-4 toastContainer" position={'top-end'}>
                <Toast show={isShowToast}>
                    <Toast.Header closeButton={true}>
                        <strong className="me-auto">Notify</strong>
                    </Toast.Header>
                    <Toast.Body>User is edited successfully.</Toast.Body>
                </Toast>
            </ToastContainer>
        </>
    );
};

export default EditUser;

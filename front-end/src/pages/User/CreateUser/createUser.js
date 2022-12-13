import classNames from 'classnames/bind';
import { React, useMemo, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { AiFillEye, AiFillEyeInvisible } from 'react-icons/ai';
import { useNavigate } from 'react-router-dom';
import { createData } from '../../../apiServices';
import { USER } from '../../../constants';
import DatePicker from 'react-datepicker';
import styles from './createUser.module.scss';

const cx = classNames.bind(styles);

function CreateUser() {
    let navigate = useNavigate();
    const [hidePass, setHidePass] = useState(true);
    const [dataAdd, setDataAdd] = useState({
        userName: '',
        password: '',
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        gender: '',
        joinedDate: '',
        type: 0,
    });

    const toggleBtnOld = () => {
        setHidePass((pre) => !pre);
    };

    const [arrMsg, setArrMsg] = useState({
        UserName: '',
        Password: '',
        FirstName: '',
        LastName: '',
        DateOfBirth: '',
        JoinedDate: '',
    });

    const isInputComplete = useMemo(() => {
        const { type, ...otherData } = dataAdd;
        return Object.values(otherData).every((x) => x !== null && x !== '');
    }, [dataAdd]);

    const onSaveAdd = async () => {
        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object dataAdd
        Object.keys(dataAdd).map((k) => (dataAdd[k] = typeof dataAdd[k] == 'string' ? dataAdd[k].trim() : dataAdd[k]));

        // Handle datepicker date off by one day
        const newDateOfBirth = new Date(dataAdd.dateOfBirth);
        const newJoinedDate = new Date(dataAdd.joinedDate);

        newDateOfBirth.setDate(newDateOfBirth.getDate() + 1);
        newJoinedDate.setDate(newJoinedDate.getDate() + 1);

        const res = await createData(USER, { ...dataAdd, dateOfBirth: newDateOfBirth, joinedDate: newJoinedDate });

        if (res.code === 'ERR_BAD_REQUEST') {
            setArrMsg(res?.response?.data?.errors);
            if (res?.response?.status === 409) {
                setArrMsg({ UserName: [res?.response?.data] });
            }
            if (res?.response?.data?.errors?.requestModel) {
                alert('Please input all fields');
            }
        } else {
            navigate('/manageruser');
        }
    };

    const handleChangeAdd = (e) => {
        if (e.target.name === 'gender' || e.target.name === 'type') {
            setDataAdd({ ...dataAdd, [e.target.name]: parseInt(e.target.value) });
        } else {
            setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
        }
    };

    const onCancelAdd = () => {
        navigate('/manageruser');
    };

    const onChangeDateOfBirth = (date) => {
        setDataAdd({ ...dataAdd, dateOfBirth: date });
    };

    const onChangeJoinedDate = (date) => {
        setDataAdd({ ...dataAdd, joinedDate: date });
    };

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New User</h3>

            <Form>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Username</Form.Label>
                    <Form.Control
                        isInvalid={arrMsg.UserName}
                        type="text"
                        className={cx('input')}
                        name="userName"
                        onChange={handleChangeAdd}
                    />
                </Form.Group>
                {arrMsg.UserName && <p className={cx('msgError')}>{arrMsg.UserName[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Password</Form.Label>
                    <div className={cx('input-new-password')}>
                        <Form.Control
                            isInvalid={arrMsg.Password}
                            type={hidePass ? 'password' : 'text'}
                            className={cx('input')}
                            name="password"
                            onChange={handleChangeAdd}
                        />
                        <div className={cx('icon-new')} onClick={toggleBtnOld}>
                            {!hidePass ? <AiFillEye /> : <AiFillEyeInvisible />}
                        </div>
                    </div>
                </Form.Group>
                {arrMsg.Password && <p className={cx('msgError')}>{arrMsg.Password[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>First Name</Form.Label>
                    <Form.Control
                        isInvalid={arrMsg.FirstName}
                        type="text"
                        className={cx('input')}
                        name="firstName"
                        onChange={handleChangeAdd}
                    />
                </Form.Group>
                {arrMsg.FirstName && <p className={cx('msgError')}>{arrMsg.FirstName[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Last Name</Form.Label>
                    <Form.Control
                        isInvalid={arrMsg.LastName}
                        type="text"
                        className={cx('input')}
                        name="lastName"
                        onChange={handleChangeAdd}
                    />
                </Form.Group>
                {arrMsg.LastName && <p className={cx('msgError')}>{arrMsg.LastName[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>

                    <DatePicker
                        name="dateOfBirth"
                        selected={dataAdd.dateOfBirth}
                        className={`${arrMsg?.DateOfBirth ? 'border-danger' : ''} form-control w-full `}
                        onChange={(date) => onChangeDateOfBirth(date)}
                        placeholderText="dd/MM/yyyy"
                        dateFormat="dd/MM/yyyy"
                    />
                </Form.Group>
                {arrMsg.DateOfBirth && <p className={cx('msgError')}>{arrMsg.DateOfBirth[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Gender</Form.Label>

                    <div key={`gender-radio`} className={cx('input-radio-gender')}>
                        <Form.Check
                            inline
                            label="Male"
                            name="gender"
                            type="radio"
                            value={1}
                            id={`gender-radio-1`}
                            onChange={handleChangeAdd}
                        />
                        <Form.Check
                            inline
                            label="Female"
                            name="gender"
                            type="radio"
                            className={cx('form-check-input:checked')}
                            value={2}
                            id={`gender-radio-2`}
                            onChange={handleChangeAdd}
                        />
                    </div>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Joined Date</Form.Label>
                    <DatePicker
                        name="joinedDate"
                        selected={dataAdd.joinedDate}
                        className={`${arrMsg?.JoinedDate ? 'border-danger' : ''} form-control w-full `}
                        onChange={(date) => onChangeJoinedDate(date)}
                        placeholderText="dd/MM/yyyy"
                        dateFormat="dd/MM/yyyy"
                    />
                </Form.Group>
                {arrMsg.JoinedDate && <p className={cx('msgError')}>{arrMsg.JoinedDate[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Type</Form.Label>
                    <Form.Select onChange={handleChangeAdd} name="type">
                        <option value={0}>Staff</option>
                        <option value={1}>Admin</option>
                    </Form.Select>
                </Form.Group>
                <div className={cx('button')}>
                    <Button variant="danger" onClick={onSaveAdd} disabled={!isInputComplete}>
                        Save
                    </Button>
                    <Button variant="outline-secondary" className={cx('cancel-button')} onClick={onCancelAdd}>
                        Cancel
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default CreateUser;

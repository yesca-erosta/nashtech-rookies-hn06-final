import Button from 'react-bootstrap/Button';
import { React, useEffect, useState } from 'react';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from './createUser.module.scss';
import { createData, getAllData } from '../../../apiServices';
import { USER } from '../../../constants';

const cx = classNames.bind(styles);

function CreateUser() {
    const [user, setUser] = useState({});
    const [dataAdd, setDataAdd] = useState({
        userName: '',
        password: '',
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        gender: '',
        joinedDate: '',
        type: '',
    });

    const getData = async () => {
        const dataUser = await getAllData(USER);
        setUser(dataUser);
        console.log(user);
    };
    useEffect(() => {
        getData();
    }, []);

    const onSaveAdd = async () => {
        await createData(USER, dataAdd);
        getData();
        console.log(user);
        setDataAdd({
            userName: '',
            password: '',
            firstName: '',
            lastName: '',
            dateOfBirth: '',
            gender: '',
            joinedDate: '',
            type: '',
        });
    };
    console.log(dataAdd);

    const handleChangeAdd = (e) => {
        setDataAdd({ ...dataAdd, [e.target.name]: e.target.value, "type": +e.target.value, "gender": +e.target.value });
        console.log(e.target.value);
    };

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New User</h3>

            <Form>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>User Name</Form.Label>
                    <Form.Control type="text" className={cx('input')} name="userName" onChange={handleChangeAdd} />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Password</Form.Label>
                    <Form.Control type="password" className={cx('input')} name="password" onChange={handleChangeAdd} />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>First Name</Form.Label>
                    <Form.Control type="text" className={cx('input')} name="firstName" onChange={handleChangeAdd} />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Last Name</Form.Label>
                    <Form.Control type="text" className={cx('input')} name="lastName" onChange={handleChangeAdd} />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
                    <Form.Control type="date" className={cx('input')} name="dateOfBirth" onChange={handleChangeAdd} />
                </Form.Group>

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
                            value={2}
                            id={`gender-radio-2`}
                            onChange={handleChangeAdd}
                        />
                    </div>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Joined Date</Form.Label>
                    <Form.Control type="date" className={cx('input')} name="joinedDate" onChange={handleChangeAdd} />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Type</Form.Label>
                    <Form.Select onChange={handleChangeAdd} name="type">
                        <option value={0}>Staff</option>
                        <option value={1}>Admin</option>
                    </Form.Select>
                </Form.Group>
                <div className={cx('button')}>
                    <Button variant="danger" onClick={onSaveAdd}>
                        Save
                    </Button>
                    <Button variant="outline-success" className={cx('cancel-button')}>
                        Cancel
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default CreateUser;

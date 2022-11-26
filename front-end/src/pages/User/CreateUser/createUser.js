import Button from 'react-bootstrap/Button';
import { React, useEffect, useState } from 'react';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from './createUser.module.scss';
import { createData, getAllData } from '../../../apiServices';
import { USER } from '../../../constants';
import { AiFillEyeInvisible, AiFillEye } from 'react-icons/ai';

const cx = classNames.bind(styles);

function CreateUser() {
  const [user, setUser] = useState({});
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

  const [error, setError] = useState({
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    joinedDate: '',
  });

  const getData = async () => {
    const dataUser = await getAllData(USER);
    setUser(dataUser);
  };

  const toggleBtnOld = () => {
    setHidePass((pre) => !pre);
  };
  useEffect(() => {
    getData();
  }, [user]);

  useEffect(() => {
    console.log(dataAdd)
  }, [dataAdd]);
  const onSaveAdd = async () => {
    await createData(USER, dataAdd);
    getData();
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

  const handleBlurAdd = (e) => {
    if (e.target.value) {
      setError({ userName: '', password: '', firstName: '', lastName: '', dateOfBirth: '', joinedDate: '' });
    } else {
      setError({ ...error, [e.target.name]: 'This field is required' });
    }
  };

  const handleChangeAdd = (e) => {
    setError({ userName: '', password: '', firstName: '', lastName: '', dateOfBirth: '', joinedDate: '' });
    if (e.target.name === 'gender' || e.target.name === 'type') {
      setDataAdd({ ...dataAdd, [e.target.name]: parseInt(e.target.value) });
    } else {
      setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
    }
  };

  return (
    <div className={cx('container')}>
      <h3 className={cx('title')}>Create New User</h3>

      <Form>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>User Name</Form.Label>
          <Form.Control
            type="text"
            className={cx('input')}
            name="userName"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>
        {error.userName && <p className={cx('msgError')}>{error.userName}</p>}
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Password</Form.Label>
          <div className={cx('input-new-password')}>
            <Form.Control
              type={hidePass ? 'password' : 'text'}
              className={cx('input')}
              name="password"
              onChange={handleChangeAdd}
              onBlur={handleBlurAdd}
            />
            <div className={cx('icon-new')} onClick={toggleBtnOld}>
              {!hidePass ? <AiFillEye /> : <AiFillEyeInvisible />}
            </div>
          </div>
        </Form.Group>
        {error.password && <p className={cx('msgError')}>{error.password}</p>}
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>First Name</Form.Label>
          <Form.Control
            type="text"
            className={cx('input')}
            name="firstName"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>
        {error.firstName && <p className={cx('msgError')}>{error.firstName}</p>}
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Last Name</Form.Label>
          <Form.Control
            type="text"
            className={cx('input')}
            name="lastName"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>
        {error.lastName && <p className={cx('msgError')}>{error.lastName}</p>}
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
          <Form.Control
            type="date"
            className={cx('input')}
            name="dateOfBirth"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>
        {error.dateOfBirth && <p className={cx('msgError')}>{error.dateOfBirth}</p>}
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
          <Form.Control
            type="date"
            className={cx('input')}
            name="joinedDate"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>
        {error.joinedDate && <p className={cx('msgError')}>{error.joinedDate}</p>}
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

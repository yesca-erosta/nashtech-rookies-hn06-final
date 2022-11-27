import classNames from 'classnames/bind';
import { React, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useLocation } from 'react-router-dom';
import { dateStrToDate } from '../../../lib/helper';
import styles from './EditUser.module.scss';
const EditUser = () => {
  const cx = classNames.bind(styles);
  const location = useLocation();

  const { user } = location?.state;

  console.log('user', user);
  const [dataAdd, setDataAdd] = useState({
    dateOfBirth: dateStrToDate(user.dateOfBirth),
    gender: '',
    joinedDate: '',
    type: 0,
  });

  const [error, setError] = useState({
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    joinedDate: '',
  });

  const onSaveAdd = () => {
    console.log('submit');
  };

  const handleBlurAdd = (e) => {
    if (e.target.value) {
      setError({ firstName: '', lastName: '', dateOfBirth: '', joinedDate: '' });
    } else {
      setError({ ...error, [e.target.name]: 'This field is required' });
    }
  };

  const handleChangeAdd = (e) => {
    setError({ firstName: '', lastName: '', dateOfBirth: '', joinedDate: '' });
    if (e.target.name === 'gender' || e.target.name === 'type') {
      setDataAdd({ ...dataAdd, [e.target.name]: parseInt(e.target.value) });
    } else {
      setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
    }
  };

  return (
    <div className={cx('container')}>
      <h3 className={cx('title')}>Edit User</h3>

      <Form>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>First Name</Form.Label>
          <Form.Control type="text" className={cx('input')} name="firstName" placeholder={user.firstName} readOnly disabled />
        </Form.Group>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Last Name</Form.Label>
          <Form.Control type="text" className={cx('input')} name="lastName" placeholder={user.lastName} readOnly disabled />
        </Form.Group>
        {error.lastName && <p className={cx('msgError')}>{error.lastName}</p>}
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
          <Form.Control
            type="date"
            className={cx('input')}
            name="dateOfBirth"
            value={dateStrToDate(user.dateOfBirth)}
            // onChange={handleChangeAdd}
            // onBlur={handleBlurAdd}
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
              defaultChecked={user.gender === 1}
              // onChange={handleChangeAdd}
            />
            <Form.Check
              inline
              label="Female"
              name="gender"
              type="radio"
              value={2}
              id={`gender-radio-2`}
              defaultChecked={user.gender === 2}
              // checked
              // onChange={handleChangeAdd}
            />
          </div>
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Joined Date</Form.Label>
          <Form.Control
            type="date"
            className={cx('input')}
            name="joinedDate"
            value={dateStrToDate(user.joinedDate)}
            // onChange={handleChangeAdd}
            // onBlur={handleBlurAdd}
          />
        </Form.Group>
        {error.joinedDate && <p className={cx('msgError')}>{error.joinedDate}</p>}
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Type</Form.Label>
          <Form.Select onChange={handleChangeAdd} name="type" >
            <option value={0} selected={user.type === 'Staff'}>Staff</option>
            <option value={1} selected={user.type === 'Admin'}>Admin</option>
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
};

export default EditUser;

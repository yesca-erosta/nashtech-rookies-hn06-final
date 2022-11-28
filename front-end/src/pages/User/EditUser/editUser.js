import classNames from 'classnames/bind';
import { React, useMemo, useState } from 'react';
import { Toast, ToastContainer } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useLocation, useNavigate } from 'react-router-dom';
import { updateData } from '../../../apiServices';
import { USER } from '../../../constants';
import { dateStrToDate } from '../../../lib/helper';
import styles from './EditUser.module.scss';
const EditUser = () => {
  const cx = classNames.bind(styles);
  const location = useLocation();

  const { user } = location?.state;

  const initUser = {
    dateOfBirth: user.dateOfBirth,
    gender: user.gender,
    joinedDate: user.joinedDate,
    type: user.type === 'Staff' ? 0 : 1,
    password: '',
    userName: user.userName,
  };

  const [data, setData] = useState(initUser);

  const [isShowToast, setIsShowToast] = useState(false);
  const [error, setError] = useState({
    dateOfBirth: '',
    gender: '',
    joinedDate: '',
    type: '',
  });
  const navigate = useNavigate();

  const onSubmit = async () => {
    const res = await updateData(`${USER}/${user.id}`, data);

    console.log('res', res);
    if (res.code === 'ERR_BAD_REQUEST') {
      alert('Somthing wrong. Cant update user');
    } else {
      setData(initUser);

      setIsShowToast(true);
      setTimeout(() => {
        setIsShowToast(false);
      }, 5000);

      navigate('/manageruser');
    }
  };

  const onBlur = (e) => {
    if (e.target.value) {
      setError({ firstName: '', lastName: '', dateOfBirth: '', joinedDate: '' });
    } else {
      setError({ ...error, [e.target.name]: 'This field is required' });
    }
  };

  const onChange = (e) => {
    setError({ dateOfBirth: '', gender: '', joinedDate: '', type: '' });
    if (e.target.name === 'gender' || e.target.name === 'type') {
      setData({ ...data, [e.target.name]: parseInt(e.target.value) ?? 0 });
    } else {
      setData({ ...data, [e.target.name]: e.target.value });
    }
  };

  console.log('dataAdd', data);
  const isInputValidateComplete = useMemo(() => Object.values(error).every((x) => x === null || x === ''), [error]);
  console.log('isShowToast', isShowToast);
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
            <Form.Control
              type="date"
              className={cx('input')}
              name="dateOfBirth"
              value={dateStrToDate(data.dateOfBirth)}
              onChange={onChange}
              onBlur={onBlur}
            />
          </Form.Group>
          {error.dateOfBirth && <p className={cx('msgError')}>{error.dateOfBirth}</p>}
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
            <Form.Control
              type="date"
              className={cx('input')}
              name="joinedDate"
              value={dateStrToDate(data.joinedDate)}
              onChange={onChange}
              onBlur={onBlur}
            />
          </Form.Group>
          {error.joinedDate && <p className={cx('msgError')}>{error.joinedDate}</p>}
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
            <Button variant="danger" onClick={onSubmit} disabled={!isInputValidateComplete}>
              Save
            </Button>
            <Button
              variant="light"
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

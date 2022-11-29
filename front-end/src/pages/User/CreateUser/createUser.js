import classNames from 'classnames/bind';
import { React, useMemo, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from "./createUser.module.scss"

const cx = classNames.bind(styles)

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

  const [error, setError] = useState({
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    joinedDate: '',
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
    const res = await createData(USER, dataAdd);

    if (res.code === 'ERR_BAD_REQUEST') {
      setArrMsg(res?.response?.data?.errors);
      if (res?.response?.status === 409) {
        setArrMsg({ UserName: [res?.response?.data] });
      }
      if (res?.response?.data?.errors?.requestModel) {
        alert('Please input all fields');
      }
    } else {
      setDataAdd({
        userName: '',
        password: '',
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        gender: '',
        joinedDate: '',
        type: 0,
      });

      navigate('/manageruser');
    }
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

  const onCancelAdd = () => {
    navigate('/manageruser');
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
            onBlur={handleBlurAdd}
          />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Password</Form.Label>
          <div className={cx('input-new-password')}>
            <Form.Control
              isInvalid={arrMsg.Password}
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

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>First Name</Form.Label>
          <Form.Control
            isInvalid={arrMsg.FirstName}
            type="text"
            className={cx('input')}
            name="firstName"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Last Name</Form.Label>
          <Form.Control
            isInvalid={arrMsg.LastName}
            type="text"
            className={cx('input')}
            name="lastName"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
          <Form.Control
            isInvalid={arrMsg.DateOfBirth}
            type="date"
            className={cx('input')}
            name="dateOfBirth"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Gender</Form.Label>

          <div key={`gender-radio`} className={cx('input-radio-gender')}>
            <Form.Check
              inline
              label="Male"
              name="gender"
              type="radio"
              id={`gender-radio-1`}
            />
            <Form.Check
              inline
              label="Female"
              name="gender"
              type="radio"
              id={`gender-radio-2`}
            />
          </div>

        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Joined Date</Form.Label>
          <Form.Control
            isInvalid={arrMsg.JoinedDate}
            type="date"
            className={cx('input')}
            name="joinedDate"
            onChange={handleChangeAdd}
            onBlur={handleBlurAdd}
          />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Type</Form.Label>
          <Form.Control type="text" className={cx('input')} />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Location</Form.Label>
          <Form.Control type="text" className={cx('input')} />
        </Form.Group>
        
        <div className={cx('button')}>
          <Button variant="danger" onClick={onSaveAdd} disabled={!isInputComplete}>
            Save
          </Button>
          <Button variant="light" className={cx('cancel-button')} onClick={onCancelAdd}>
            Cancel
          </Button>
        </div>
        
      </Form>
    </div>
  );
}

export default CreateUser;

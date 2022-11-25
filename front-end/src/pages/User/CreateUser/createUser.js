import axios from 'axios';
import Button from 'react-bootstrap/Button';
import { React, useEffect, useState } from 'react';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from './createUser.module.scss';
import { useAuthContext } from '../../../context/RequiredAuth/authContext';

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
    location: '',
    type: '',
  });

  console.log('user', user);

  const { token } = useAuthContext();
  const createData = async (data) => {
    let response = [];
    await axios({
      method: 'post',
      url: 'https://localhost:7060/api/User',
      headers: { Authorization: `Bearer ${token.token}` },
      data: data,
    })
      .then((res) => {
        response = [...res.data];
      })
      .catch((err) => {
        response = err;
      });
    return response;
  };

  const handleOkAdd = async () => {
    await createData(dataAdd);
    //getData();
    setDataAdd({
      name: '',
      author: '',
      summary: '',
      categoryIds: [],
    });
  };
  useEffect(() => {
    axios({
      method: 'get',
      url: 'https://localhost:7060/api/User',
      data: null,
      headers: { Authorization: `Bearer ${token.token}` },
    })
      .then((data) => {
        setUser(data.data);
        console.log(data.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, [token]);

  const handleChangeAdd = (e) => {
    //   setError({ userName: '',
    //   firstName: '',
    //   password: '',
    //   lastName: '',
    //   joinedDate: '',
    //   location : '',
    //   type: '',
    //   dateOfBirth: ''
    // });
    setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
  };

  return (
    <div className={cx('container')}>
      <h3 className={cx('title')}>Create New User</h3>

      <Form onSubmit={handleOkAdd}>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>User Name</Form.Label>
          <Form.Control type="text" className={cx('input')} onChange={handleChangeAdd} />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Password</Form.Label>
          <Form.Control type="password" className={cx('input')} onChange={handleChangeAdd} />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>First Name</Form.Label>
          <Form.Control type="text" className={cx('input')} onChange={handleChangeAdd} />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Last Name</Form.Label>
          <Form.Control type="text" className={cx('input')} onChange={handleChangeAdd} />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
          <Form.Control type="date" className={cx('input')} onChange={handleChangeAdd} />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Gender</Form.Label>

          <div key={`gender-radio`} className={cx('input-radio-gender')}>
            <Form.Check inline label="Male" name="gender" type="radio" id={`gender-radio-1`} />
            <Form.Check inline label="Female" name="gender" type="radio" id={`gender-radio-2`} />
          </div>
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Joined Date</Form.Label>
          <Form.Control type="date" className={cx('input')} onChange={handleChangeAdd} />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Type</Form.Label>
          <Form.Select>
            <option value="0">Staff</option>
            <option value="1">Admin</option>
          </Form.Select>
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Location</Form.Label>
          <Form.Control type="text" className={cx('input')} />
        </Form.Group>

        <div className={cx('button')}>
          <Button variant="danger" type="submit">
            Save
          </Button>

          <Button variant="outline-success" type="submit" className={cx('cancel-button')}>
            Cancel
          </Button>
        </div>
      </Form>
    </div>
  );
}

export default CreateUser;

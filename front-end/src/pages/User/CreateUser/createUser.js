import axios from "axios";
import Button from 'react-bootstrap/Button';
import { React, useEffect, useState } from "react";
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from "./createUser.module.scss"
import { useAuthContext } from '../../../context/RequiredAuth/authContext';
import Dropdown from 'react-bootstrap/Dropdown';

const cx = classNames.bind(styles)

function CreateUser() {
  const [user, setUser] = useState({});
  const { token } = useAuthContext();


  useEffect(() => {
    axios({
      method: "GET",
      url: "https://localhost:7060/api/User",
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

  return (
    <div className={cx('container')} >
      <h3 className={cx('title')}>Create New User</h3>

      <Form >
        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>User Name</Form.Label>
          <Form.Control type="text" className={cx('input')} />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Password</Form.Label>
          <Form.Control type="password" className={cx('input')} />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>First Name</Form.Label>
          <Form.Control type="text" className={cx('input')} />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Last Name</Form.Label>
          <Form.Control type="text" className={cx('input')} />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Date of Birth</Form.Label>
          <Form.Control type="date" className={cx('input')} />
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
          <Form.Control type="date" className={cx('input')} />
        </Form.Group>

        <Form.Group className={cx('common-form')} >
          <Form.Label className={cx('title_input')}>Type</Form.Label>
          <Form.Select >
            <option value="1">Staff</option>
            <option value="2">Admin</option>
          </Form.Select>
        </Form.Group>

        <Form.Group className={cx('common-form')} >
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

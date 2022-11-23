import { faPen, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useState } from 'react';
import { Button, Col, Form, Modal, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { Link } from 'react-router-dom';
import { TypeFilter } from './TypeFilter/TypeFilter';
import { ButtonCreate } from './ButtonCreate/ButtonCreate';
import { SearchUser } from './SearchUser/SearchUser';
import styles from './User.module.scss';

function User() {
  const [show, setShow] = useState(false);

  const [userDetails, setUserDetails] = useState('');

  const handleClose = () => {
    setShow(false);
    setUserDetails('');
  };
  const handleShow = (staffCode) => {
    setShow(true);
    setUserDetails(data.find((c) => c.staffCode === staffCode));
  };

  console.log('userDetails', userDetails);

  const columns = [
    {
      name: 'Staff Code',
      selector: (row) => row.staffCode,
      sortable: true,
    },
    {
      name: 'Full Name',
      selector: (row) => row.fullName,
      sortable: true,
      cell: (row) => {
        return <Link onClick={() => handleShow(row.staffCode)}>{row.fullName}</Link>;
      },
    },
    {
      name: 'Username',
      selector: (row) => row.username,
    },
    {
      name: 'Joined Date',
      selector: (row) => row.joinedDate,
      sortable: true,
      cell: (row) => {
        return <div>{row.joinedDate}</div>;
      },
    },
    {
      name: 'Type',
      selector: (row) => row.type,
      sortable: true,
    },
    {
      name: 'Action',
      selector: (row) => row.null,
      cell: (row, index) => [
        <Link key={index} to={`#`} className={styles.customPen}>
          <FontAwesomeIcon icon={faPen} />
        </Link>,
        <Link to={'#'} style={{ cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }}>
          <FontAwesomeIcon icon={faRemove} />
        </Link>,
      ],
    },
  ];

  const data = [
    {
      staffCode: 'SD1901',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Staff',
    },
    {
      staffCode: 'SD1902',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Staff',
    },
    {
      staffCode: 'SD1903',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Staff',
    },
    {
      staffCode: 'SD1904',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Admin',
    },
    {
      staffCode: 'SD1905',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Staff',
    },
    {
      staffCode: 'SD1906',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Staff',
    },
    {
      staffCode: 'SD1907',
      fullName: 'Hoan Nguyen Van',
      username: 'hoannv',
      joinedDate: '09/11/2000',
      type: 'Staff',
    },
  ];

  return (
    <div className="main tableMain">
      <h1 className="tableTitle">User List</h1>
      <div className="tableExtension">
        <div className="tableExtensionLeft">
          <TypeFilter />
        </div>

        <div className="tableExtensionRight">
          <SearchUser />
          <ButtonCreate />
        </div>
      </div>

      <DataTable
        columns={columns}
        data={data}
        noHeader
        defaultSortField="id"
        defaultSortAsc={true}
        pagination
        highlightOnHover
        dense
      />

      <Modal show={show} onHide={handleClose}>
        <Modal.Header>
          <Modal.Title className={styles.modalTitle}>User Information</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Row>
            <Form.Label column="sm" lg={3}>
              Staff Code
            </Form.Label>
            <Col>
              <Form.Control size="sm" type="text" placeholder={userDetails.staffCode} readOnly disabled />
            </Col>
          </Row>
          <Row className="mt-3">
            <Form.Label column="sm" lg={3}>
              Full Name
            </Form.Label>
            <Col>
              <Form.Control size="sm" type="text" placeholder={userDetails.fullName} readOnly disabled />
            </Col>
          </Row>
          <Row className="mt-3">
            <Form.Label column="sm" lg={3}>
              Date of Birth
            </Form.Label>
            <Col>
              <Form.Control size="sm" type="text" placeholder={userDetails.joinedDate} readOnly disabled />
            </Col>
          </Row>
          <Row className="mt-3">
            <Form.Label column="sm" lg={3}>
              Gender
            </Form.Label>
            <Col>
              <Form.Check inline disabled label="Female" type={'radio'} id={`inline-radio-1`} />
              <Form.Check inline disabled label="Male" type={'radio'} id={`inline-radio-2`} checked />
            </Col>
          </Row>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="light" size="sm" className={styles.btnCancel} onClick={handleClose}>
            Cancel
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default User;

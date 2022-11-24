import { Button, Col, Form, Modal, Row } from 'react-bootstrap';
import { dateStrToStr } from '../../../lib/helper';
import styles from '../User.module.scss';

export const ModalDetails = ({ userDetails, handleClose, show }) => {
  return (
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
            First Name
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={userDetails.firstName} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Last Name
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={userDetails.lastName} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Date of Birth
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={dateStrToStr(userDetails.dateOfBirth)} readOnly disabled />
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
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Joined Date
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={dateStrToStr(userDetails.joinedDate)} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Type
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={userDetails.type} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Location
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={userDetails.location} readOnly disabled />
          </Col>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="light" size="sm" className={styles.btnCancel} onClick={handleClose}>
          Cancel
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

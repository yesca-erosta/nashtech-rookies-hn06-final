import { Button, Col, Form, Modal, Row } from 'react-bootstrap';
import { convertStatetoStr } from '..';
import { dateStrToStr } from '../../../lib/helper';
import styles from '../asset.module.scss';

export const DetailAsset = ({ showDetail, assetDetail, handleCloseDetail }) => {
  return (
    <Modal show={showDetail} onHide={handleCloseDetail}>
      <Modal.Header>
        <Modal.Title className={styles.modalTitle}>Asset Information</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row>
          <Form.Label column="sm" lg={3}>
            Asset Code
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={assetDetail.assetCode} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Asset Name
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={assetDetail.assetName} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Category
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={assetDetail.category?.name} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Installed Date
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={dateStrToStr(assetDetail.installedDate)} readOnly disabled />
          </Col>
        </Row>
        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            Specification
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={assetDetail.specification} readOnly disabled />
          </Col>
        </Row>

        <Row className="mt-3">
          <Form.Label column="sm" lg={3}>
            State
          </Form.Label>
          <Col>
            <Form.Control size="sm" type="text" placeholder={convertStatetoStr(assetDetail.state)} readOnly disabled />
          </Col>
        </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="light" size="sm" className={styles.btnCancel} onClick={handleCloseDetail}>
          Cancel
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

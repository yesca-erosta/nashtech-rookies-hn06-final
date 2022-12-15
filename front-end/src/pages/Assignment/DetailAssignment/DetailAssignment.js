import { Button, Col, Form, Modal, Row } from 'react-bootstrap';
import { convertStatetoStrAsm } from '..';
import { dateStrToStr } from '../../../lib/helper';
import styles from '../../Asset/asset.module.scss';

export const DetailAssignment = ({ showDetail, assignmentDetail, handleCloseDetail }) => {
    return (
        <Modal show={showDetail} onHide={handleCloseDetail}>
            <Modal.Header>
                <Modal.Title className={styles.modalTitle}>Assignment Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Row>
                    <Form.Label column="sm" lg={3}>
                        Asset Code
                    </Form.Label>
                    <Col>
                        <Form.Control size="sm" type="text" placeholder={assignmentDetail.assetCode} readOnly disabled />
                    </Col>
                </Row>
                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        Asset Name
                    </Form.Label>
                    <Col>
                        <Form.Control size="sm" type="text" placeholder={assignmentDetail.assetName} readOnly disabled />
                    </Col>
                </Row>
                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        Specification
                    </Form.Label>
                    <Col>
                        <Form.Control
                            type="text"
                            placeholder={assignmentDetail.specification}
                            name="specification"
                            as="textarea"
                            rows={3}
                            cols={40}
                            readOnly
                            disabled
                        />
                    </Col>
                </Row>
                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        Assigned to
                    </Form.Label>
                    <Col>
                        <Form.Control size="sm" type="text" placeholder={assignmentDetail.assignedTo} readOnly disabled />
                    </Col>
                </Row>
                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        Assigned by
                    </Form.Label>
                    <Col>
                        <Form.Control size="sm" type="text" placeholder={assignmentDetail.assignedBy} readOnly disabled />
                    </Col>
                </Row>

                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        Assigned Date
                    </Form.Label>
                    <Col>
                        <Form.Control
                            size="sm"
                            type="text"
                            placeholder={dateStrToStr(assignmentDetail.assignedDate)}
                            readOnly
                            disabled
                        />
                    </Col>
                </Row>

                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        State
                    </Form.Label>
                    <Col>
                        <Form.Control
                            size="sm"
                            type="text"
                            placeholder={convertStatetoStrAsm(assignmentDetail.state)}
                            readOnly
                            disabled
                        />
                    </Col>
                </Row>

                <Row className="mt-3">
                    <Form.Label column="sm" lg={3}>
                        Note
                    </Form.Label>
                    <Col>
                        <Form.Control
                            placeholder={assignmentDetail.note}
                            type="text"
                            name="note"
                            as="textarea"
                            rows={3}
                            cols={40}
                            readOnly
                            disabled
                        />
                    </Col>
                </Row>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="outline-secondary" size="sm" className={styles.btnCancel} onClick={handleCloseDetail}>
                    Cancel
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

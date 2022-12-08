import { Button, Form, Modal } from 'react-bootstrap';

export const ShowModalDecline = ({ isShowDecline, setIsShowDecline, handleDecline }) => {
    return (
        <Modal show={isShowDecline} onHide={() => setIsShowDecline(false)}>
            <Modal.Header>
                <Modal.Title>Are you sure?</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Label>Do you want to decline this assignment?</Form.Label>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={handleDecline}>
                    Decline
                </Button>
                <Button variant="outline-secondary" onClick={() => setIsShowDecline(false)}>
                    Cancel
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

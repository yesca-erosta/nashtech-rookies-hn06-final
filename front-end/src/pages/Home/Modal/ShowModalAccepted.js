import { Button, Form, Modal } from 'react-bootstrap';

export const ShowModalAccepted = ({ isShowAccepted, setIsShowAccepted, handleAccepted }) => {
    return (
        <Modal show={isShowAccepted} onHide={() => setIsShowAccepted(false)}>
            <Modal.Header>
                <Modal.Title>Are you sure?</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Label>Do you want to accepted this assignment?</Form.Label>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={handleAccepted}>
                    Accept
                </Button>
                <Button variant="outline-secondary" onClick={() => setIsShowAccepted(false)}>
                    Cancel
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

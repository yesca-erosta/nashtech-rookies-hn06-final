import { Button, Form, Modal } from 'react-bootstrap';

export const ShowModalComplete = ({ showComplete, setShowComplete, handleComplete }) => {
    return (
        <Modal show={showComplete} onHide={() => setShowComplete(false)}>
            <Modal.Header>
                <Modal.Title>Are you sure?</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Label>Do you want to mark this returning request as 'Completed'?</Form.Label>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={handleComplete}>
                    Yes
                </Button>
                <Button variant="outline-secondary" onClick={() => setShowComplete(false)}>
                    No
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

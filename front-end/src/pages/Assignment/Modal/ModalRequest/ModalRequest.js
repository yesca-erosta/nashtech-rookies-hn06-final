import { Button, Form, Modal } from 'react-bootstrap';

export const ModalRequest = ({ showRequest, setShowRequest, handleRequest }) => {
    return (
        <Modal show={showRequest} onHide={() => setShowRequest(false)}>
            <Modal.Header>
                <Modal.Title>Are you sure?</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Label>Do you want to create a returning request for this asset?</Form.Label>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={handleRequest}>
                    Yes
                </Button>
                <Button variant="outline-secondary" onClick={() => setShowRequest(false)}>
                    No
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

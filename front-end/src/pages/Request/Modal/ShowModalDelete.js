import { Button, Form, Modal } from 'react-bootstrap';

export const ShowModalDelete = ({ showDelete, setShowDelete, handleDelete }) => {
    return (
        <Modal show={showDelete} onHide={() => setShowDelete(false)}>
            <Modal.Header>
                <Modal.Title>Are you sure?</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Label>Do you want to cancel this returning request?</Form.Label>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="danger" onClick={handleDelete}>
                    Yes
                </Button>
                <Button variant="outline-secondary" onClick={() => setShowDelete(false)}>
                    No
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

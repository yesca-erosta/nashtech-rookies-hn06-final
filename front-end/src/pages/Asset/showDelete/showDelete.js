import { Button, Modal } from 'react-bootstrap';
import { Form } from 'react-router-dom';

export const ModalDelete = ({ showDelete, setShowDelete, handleDelete }) => {
    <Modal show={showDelete} onHide={() => setShowDelete(false)}>
        <Modal.Header>
            <Modal.Title>Are you sure?</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <Form>
                <Form.Label>Do you want to delete this asset?</Form.Label>
            </Form>
        </Modal.Body>
        <Modal.Footer>
            <Button variant="danger" onClick={handleDelete}>
                Delete
            </Button>
            <Button variant="outline-secondary" onClick={() => setShowDelete(false)}>
                Cancel
            </Button>
        </Modal.Footer>
    </Modal>;
};

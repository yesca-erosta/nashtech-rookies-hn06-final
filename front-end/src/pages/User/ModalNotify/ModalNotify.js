import { Form, Modal } from 'react-bootstrap';

export const ModalNotify = ({ isShowModalCantDelete, setIsShowModalCantDelete }) => {
    return (
        <Modal
            show={isShowModalCantDelete}
            onHide={() => {
                setIsShowModalCantDelete(false);
            }}
        >
            <Modal.Header closeButton>
                <Modal.Title>Cannot Delete User</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Label>There are valid assignments belonging to this user.</Form.Label>
                    <Form.Label>Please close all assignments before disable user.</Form.Label>
                </Form>
            </Modal.Body>
        </Modal>
    );
};

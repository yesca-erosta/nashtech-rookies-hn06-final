import classNames from 'classnames/bind';
import { InputGroup } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import styles from './createAssignment.module.scss';
import { BsSearch } from 'react-icons/bs';

const cx = classNames.bind(styles);

function CreateAssignment() {
    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New Assignment</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>User</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter user'} style={{ width: 600 }} readOnly />
                        <InputGroup.Text style={{ cursor: 'pointer' }}>
                            <BsSearch />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Asset</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter asset'} readOnly />
                        <InputGroup.Text style={{ cursor: 'pointer' }}>
                            <BsSearch />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Assigned Date</Form.Label>
                    <Form.Control type="date" />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Note</Form.Label>
                    <Form.Group className="w-100">
                        <Form.Control type="text" as="textarea" rows={5} cols={40} placeholder="Enter note" />
                    </Form.Group>
                </Form.Group>

                <div className={cx('button')}>
                    <Button variant="danger">Save</Button>

                    <Button variant="outline-success" className={cx('cancel-button')}>
                        Cancel
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default CreateAssignment;

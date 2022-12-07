import classNames from 'classnames/bind';
import { useState } from 'react';
import { InputGroup } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { BsSearch } from 'react-icons/bs';
import { useNavigate } from 'react-router-dom';
import { ModalAsset } from '../Modal/ModalAsset/ModalAsset';
import { ModalUser } from '../Modal/ModalUser/ModalUser';
import styles from '../CreateAssignment/createAssignment.module.scss';

const cx = classNames.bind(styles);

function EditAssignment() {
    const navigate = useNavigate();
    const [isShowListUser, setIsShowListUser] = useState(false);
    const [isShowListAsset, setIsShowListAsset] = useState(false);
    const [nameAsset, setNameAsset] = useState();
    const [nameUser, setNameUser] = useState();

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Edit Assignment</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>User</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter user'} style={{ width: 600 }} readOnly value={nameUser} />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListUser(true)}>
                            <BsSearch />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Asset</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter asset'} readOnly value={nameAsset} />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListAsset(true)}>
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

                    <Button
                        variant="outline-secondary"
                        className={cx('cancel-button')}
                        onClick={() => navigate('/manageassignment')}
                    >
                        Cancel
                    </Button>
                </div>
            </Form>

            {isShowListUser && <ModalUser setIsShowListUser={setIsShowListUser} setNameUser={setNameUser} />}

            {isShowListAsset && <ModalAsset setIsShowListAsset={setIsShowListAsset} setAsset={setNameAsset} />}
        </div>
    );
}

export default EditAssignment;

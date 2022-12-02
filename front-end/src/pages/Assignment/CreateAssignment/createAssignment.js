import classNames from 'classnames/bind';
import { InputGroup, Modal, Table } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import styles from './createAssignment.module.scss';
import { BsSearch } from 'react-icons/bs';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';

const cx = classNames.bind(styles);

function CreateAssignment() {
    const navigate = useNavigate();
    const [isShowListUser, setIsShowListUser] = useState(false);
    const [isShowListAsset, setIsShowListAsset] = useState(false);
    const [isStaffCode, setIsStaffCode] = useState(false);
    const [isFullName, setIsFullName] = useState(false);
    const [isType, setIsType] = useState(false);
    const [isAssetCode, setIsAssetCode] = useState(false);
    const [isAssetName, setIsAssetName] = useState(false);
    const [isCategory, setIsCategory] = useState(false);

    const handleIsStaffCode = () => {
        setIsStaffCode((pre) => !pre);
    };
    const handleIsFullName = () => {
        setIsFullName((pre) => !pre);
    };
    const handleIsType = () => {
        setIsType((pre) => !pre);
    };

    const handleIsAssetCode = () => {
        setIsAssetCode((pre) => !pre);
    };
    const handleIsAssetName = () => {
        setIsAssetName((pre) => !pre);
    };
    const handleIsCategory = () => {
        setIsCategory((pre) => !pre);
    };

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New Assignment</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>User</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter user'} style={{ width: 600 }} readOnly />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListUser(true)}>
                            <BsSearch />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Asset</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter asset'} readOnly />
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

            {isShowListUser && (
                <div className={cx('table_container')}>
                    <h4 style={{ color: '#cf2338' }}>Select User</h4>

                    <Table>
                        <thead>
                            <tr>
                                <th></th>
                                <th>
                                    <>
                                        <div className={cx('title_table')}>
                                            <div>Staff Code</div>
                                            <button className={cx('triagle')} onClick={handleIsStaffCode}>
                                                {isStaffCode ? <GoTriangleUp /> : <GoTriangleDown />}
                                            </button>
                                        </div>
                                    </>
                                </th>
                                <th>
                                    <>
                                        <div className={cx('title_table')}>
                                            <div> Full Name</div>
                                            <button className={cx('triagle')} onClick={handleIsFullName}>
                                                {isFullName ? <GoTriangleUp /> : <GoTriangleDown />}
                                            </button>
                                        </div>
                                    </>
                                </th>
                                <th>
                                    <>
                                        <div className={cx('title_table')}>
                                            <div>Type</div>
                                            <button className={cx('triagle')} onClick={handleIsType}>
                                                {isType ? <GoTriangleUp /> : <GoTriangleDown />}
                                            </button>
                                        </div>
                                    </>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <Form.Check type="checkbox" />
                                </td>
                                <td>Mark</td>
                                <td>Otto</td>
                                <td>@mdo</td>
                            </tr>
                        </tbody>
                    </Table>

                    <Modal.Footer>
                        <Button variant="danger">Save</Button>
                        <Button
                            variant="outline-secondary"
                            onClick={() => setIsShowListUser(false)}
                            style={{ marginLeft: 30 }}
                        >
                            Cancel
                        </Button>
                    </Modal.Footer>
                </div>
            )}

            {isShowListAsset && (
                <div className={cx('table_container')}>
                    <h3 style={{ color: '#cf2338' }}>Select Asset</h3>

                    <Table>
                        <thead>
                            <tr>
                                <th></th>
                                <th>
                                    <>
                                        <div className={cx('title_table')}>
                                            <div>Asset Code</div>
                                            <button className={cx('triagle')} onClick={handleIsAssetCode}>
                                                {isAssetCode ? <GoTriangleUp /> : <GoTriangleDown />}
                                            </button>
                                        </div>
                                    </>
                                </th>
                                <th>
                                    <>
                                        <div className={cx('title_table')}>
                                            <div>Asset Name</div>
                                            <button className={cx('triagle')} onClick={handleIsAssetName}>
                                                {isAssetName ? <GoTriangleUp /> : <GoTriangleDown />}
                                            </button>
                                        </div>
                                    </>
                                </th>
                                <th>
                                    <>
                                        <div className={cx('title_table')}>
                                            <div>Category</div>
                                            <button className={cx('triagle')} onClick={handleIsCategory}>
                                                {isCategory ? <GoTriangleUp /> : <GoTriangleDown />}
                                            </button>
                                        </div>
                                    </>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <Form.Check type="checkbox" />
                                </td>
                                <td>Mark</td>
                                <td>Otto</td>
                                <td>@mdo</td>
                            </tr>
                        </tbody>
                    </Table>

                    <Modal.Footer>
                        <Button variant="danger">Save</Button>
                        <Button
                            variant="outline-secondary"
                            onClick={() => setIsShowListAsset(false)}
                            style={{ marginLeft: 30 }}
                        >
                            Cancel
                        </Button>
                    </Modal.Footer>
                </div>
            )}
        </div>
    );
}

export default CreateAssignment;

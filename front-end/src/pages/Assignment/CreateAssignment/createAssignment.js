import classNames from 'classnames/bind';
import { InputGroup, Modal, Table } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import styles from './createAssignment.module.scss';
import { BsSearch } from 'react-icons/bs';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';
import { getAllDataWithFilterBox } from '../../../apiServices';
import { queryToStringForAsset } from '../../../lib/helper';

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

    const [dataUser, setdataUser] = useState([]);
    const [dataAsset, setdataAsset] = useState([]);

    const [searchUser, setSearchUser] = useState();

    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        sort: 'AssetCodeAcsending',
        states: '0,1,4',
    });

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

    // get data user
    const GetDataUser = async () => {
        const data = await getAllDataWithFilterBox(`User/query?page=1&pageSize=10`);
        setdataUser(data.source);
    };

    useEffect(() => {
        GetDataUser();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    // get data asset
    const GetDataAsset = async () => {
        const data = await getAllDataWithFilterBox(`Asset/query?page=1&pageSize=10`);
        setdataAsset(data.source);
    };

    useEffect(() => {
        GetDataAsset();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    //Search User
    const handleSearchUser = async (valueSearch) => {
        setSearchUser(valueSearch);

        let data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        if (valueSearch) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: valueSearch });
            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, valueSearch: valueSearch }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams(queryParams);
            data = await getAllDataWithFilterBox(`Asset/query?page=1&pageSize=10`);
        }
        setdataUser(data.source);
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
                    <div className={cx('header_search')}>
                        <h4 className={cx('title_search')}>Select User</h4>
                        <InputGroup style={{ width: 200 }}>
                            <Form.Control
                                className={cx('input_search')}
                                placeholder="Search..."
                                value={searchUser}
                                onChange={(e) => setSearchUser(e.target.value)}
                            />

                            <InputGroup.Text style={{ cursor: 'pointer' }}>
                                <button className={cx('icon_search')} onClick={() => handleSearchUser(searchUser)}>
                                    <BsSearch style={{ border: 'none' }} />
                                </button>
                            </InputGroup.Text>
                        </InputGroup>
                    </div>

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
                            {dataUser.map((item) => (
                                <tr key={item.assetCode}>
                                    <td>
                                        <Form.Check type="checkbox" />
                                    </td>
                                    <td>{item.staffCode}</td>
                                    <td>{item.fullName}</td>
                                    <td>{item.type}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>

                    <Modal.Footer>
                        <Button variant="danger">Save</Button>
                        <Button
                            variant="outline-secondary"
                            onClick={() => setIsShowListUser(false)}
                            style={{ marginLeft: 20 }}
                        >
                            Cancel
                        </Button>
                    </Modal.Footer>
                </div>
            )}

            {isShowListAsset && (
                <div className={cx('table_container')}>
                    <div className={cx('header_search')}>
                        <h4 className={cx('title_search')}>Select Asset</h4>
                        <InputGroup style={{ width: 200 }}>
                            <Form.Control className={cx('input_search')} placeholder="Search..." />

                            <InputGroup.Text style={{ cursor: 'pointer' }}>
                                <button className={cx('icon_search')}>
                                    <BsSearch style={{ border: 'none' }} />
                                </button>
                            </InputGroup.Text>
                        </InputGroup>
                    </div>
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
                            {dataAsset.map((item) => (
                                <tr key={item.assetCode}>
                                    <td>
                                        <Form.Check type="checkbox" />
                                    </td>
                                    <td>{item.assetCode}</td>
                                    <td>{item.assetName}</td>
                                    <td>{item.category.name}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>

                    <Modal.Footer>
                        <Button variant="danger">Save</Button>
                        <Button
                            variant="outline-secondary"
                            onClick={() => setIsShowListAsset(false)}
                            style={{ marginLeft: 20 }}
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

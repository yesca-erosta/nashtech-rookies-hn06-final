import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import { Button, Form, InputGroup, Modal, Table } from 'react-bootstrap';
import { BsSearch } from 'react-icons/bs';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';
import { getAllDataWithFilterBox } from '../../../../apiServices';
import { queryToStringForAsset } from '../../../../lib/helper';
import styles from '../../CreateAssignment/createAssignment.module.scss';

export const ModalUser = ({ setIsShowListUser, setUser }) => {
    const cx = classNames.bind(styles);

    const [dataUser, setDataUser] = useState([]);

    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        sort: 'StaffCodeAcsending',
        states: '0,1',
    });

    // get data user
    const GetDataUser = async () => {
        const data = await getAllDataWithFilterBox(`User/query` + queryToStringForAsset(queryParams));
        setDataUser(data.source);
    };

    useEffect(() => {
        GetDataUser();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [isStaffCode, setIsStaffCode] = useState(false);
    const [isFullName, setIsFullName] = useState(false);
    const [isType, setIsType] = useState(false);

    const handleIsStaffCode = async () => {
        setIsStaffCode((pre) => !pre);
        setIsFullName(false);
        setIsType(false);
        if (!isStaffCode) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `StaffCodeDescending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `StaffCodeDescending`,
                    }),
            );
            setDataUser(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `StaffCodeAcsending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `StaffCodeAcsending`,
                    }),
            );
            setDataUser(data.source);
        }
    };

    const handleIsFullName = async () => {
        setIsFullName((pre) => !pre);
        setIsStaffCode(false);
        setIsType(false);

        if (!isFullName) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `NameDescending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `NameDescending`,
                    }),
            );
            setDataUser(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `NameAcsending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `NameAcsending`,
                    }),
            );
            setDataUser(data.source);
        }
    };

    const handleIsType = async () => {
        setIsType((pre) => !pre);

        setIsFullName(false);
        setIsStaffCode(false);

        if (!isType) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `TypeDescending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `TypeDescending`,
                    }),
            );
            setDataUser(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `TypeAcsending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `TypeAcsending`,
                    }),
            );
            setDataUser(data.source);
        }
    };

    const [dataSelected, setDataSelected] = useState('');

    const onChangeUser = (e) => {
        setDataSelected(e.target.id);
    };

    const checkNameByStaffCode = (staffCode) => {
        setIsShowListUser(false);
        const userSelected = dataUser?.find((data) => data?.staffCode === staffCode);
        setUser(userSelected);
    };
    const [search, setSearch] = useState();

    const handleSearch = async (value) => {
        setSearch(value);

        let data = await getAllDataWithFilterBox(`User/query` + queryToStringForAsset(queryParams));
        if (value) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
            data = await getAllDataWithFilterBox(
                `User/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams(queryParams);
            data = await getAllDataWithFilterBox(`User/query` + queryToStringForAsset(queryParams));
        }
        setDataUser(data.source);
    };

    const handleOnChangeEnter = (e) => {
        if (e.key === 'Enter') {
            handleSearch(search);
        }
    };

    return (
        <div className={cx('table_container')}>
            <div className={cx('header_search')}>
                <h4 className={cx('title_search')}>Select User</h4>
                <InputGroup style={{ width: 200 }}>
                    <Form.Control
                        className={cx('input_search')}
                        onKeyUp={handleOnChangeEnter}
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />

                    <InputGroup.Text style={{ cursor: 'pointer' }}>
                        <button
                            className={cx('icon_search')}
                            onClick={() => handleSearch(search)}
                            onKeyUp={handleOnChangeEnter}
                        >
                            <BsSearch style={{ border: 'none' }} />
                        </button>
                    </InputGroup.Text>
                </InputGroup>
            </div>
            {dataUser ? (
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
                                        <div>Full Name</div>
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
                        {dataUser?.map((item) => (
                            <tr key={item.staffCode}>
                                <td>
                                    <Form.Check onChange={onChangeUser} type="radio" id={item.staffCode} name="userRadio" />
                                </td>
                                <td>{item.staffCode}</td>
                                <td>{item.fullName}</td>
                                <td>{item.type}</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            ) : (
                <div style={{ marginTop: '30px', textAlign: '-webkit-center' }}>There are no result to display</div>
            )}

            <Modal.Footer>
                <Button variant="danger" onClick={() => checkNameByStaffCode(dataSelected)}>
                    Save
                </Button>
                <Button variant="outline-secondary" onClick={() => setIsShowListUser(false)} style={{ marginLeft: 20 }}>
                    Cancel
                </Button>
            </Modal.Footer>
        </div>
    );
};

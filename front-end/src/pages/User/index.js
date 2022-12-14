import { faPen, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
import DataTable from 'react-data-table-component';
import ReactPaginate from 'react-paginate';
import { Link } from 'react-router-dom';
import { deleteData, getAllDataWithFilterBox } from '../../apiServices';
import { Loading } from '../../components/Loading/Loading';
import { USER } from '../../constants';
import { dateStrToStr, queryToString } from '../../lib/helper';
import { ButtonCreate } from './ButtonCreate/ButtonCreate';
import { ModalDetails } from './ModalDetails/ModalDetails';
import { ModalNotify } from './ModalNotify/ModalNotify';
import { SearchUser } from './SearchUser/SearchUser';
import { TypeFilter } from './TypeFilter/TypeFilter';
import styles from './User.module.scss';

function User() {
    const [users, setUsers] = useState([]);
    const [totalRows, setTotalRows] = useState(0);
    const [perPage, setPerPage] = useState(10);
    const [loading, setLoading] = useState(false);
    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        types: '0,1',
        sort: 'NameAcsending',
    });
    const [show, setShow] = useState(false);
    const [showRemove, setShowRemove] = useState(false);
    const [userDetails, setUserDetails] = useState('');
    const [searchValue, setSearchValue] = useState('');
    const [userId, setUserId] = useState('');

    const handleClose = () => {
        setShow(false);
        setUserDetails('');
    };

    const handleShow = (staffCode) => {
        setShow(true);
        setUserDetails(users.find((c) => c.staffCode === staffCode));
    };

    const handleShowRemove = (row) => {
        setUserId(row.id);
        setShowRemove(true);
    };

    const handleCloseRemove = () => setShowRemove(false);

    const [isShowModalCantDelete, setIsShowModalCantDelete] = useState(false);

    const handleDisable = async (id) => {
        setLoading(true);
        const deleteRecord = await deleteData(USER, id);

        if (deleteRecord.code === 'ERR_BAD_REQUEST') {
            setIsShowModalCantDelete(true);
        }

        await getData();
        setUserId('');
        setShowRemove(false);
        setLoading(false);
    };

    const columns = [
        {
            name: 'Staff Code',
            selector: (row) => row.staffCode,
            sortable: true,
        },
        {
            name: 'Full Name',
            selector: (row) => row.fullName,
            sortable: true,
            cell: (row) => {
                return <Link onClick={() => handleShow(row.staffCode)}>{row.fullName}</Link>;
            },
        },
        {
            name: 'Username',
            selector: (row) => row.userName,
        },
        {
            name: 'Joined Date',
            selector: (row) => row.joinedDate,
            sortable: true,
            cell: (row) => {
                return <div>{dateStrToStr(row.joinedDate)}</div>;
            },
        },
        {
            name: 'Type',
            selector: (row) => row.type,
            sortable: true,
        },
        {
            name: 'Action',
            selector: (row) => row.null,
            cell: (row) => [
                <Link key={row.staffCode} to={`./edituser`} state={{ user: row }} className={styles.customPen}>
                    <FontAwesomeIcon icon={faPen} />
                </Link>,
                <Link
                    key={`keyDelete_${row.staffCode}`}
                    to={'#'}
                    style={{ cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }}
                >
                    <FontAwesomeIcon icon={faRemove} onClick={() => handleShowRemove(row)} />
                </Link>,
            ],
        },
    ];

    const fetchUsers = async (page) => {
        setLoading(true);
        setQueryParams({ ...queryParams, page: page, pageSize: perPage });

        const data = await getAllDataWithFilterBox(
            `User/query` + queryToString({ ...queryParams, page: page, pageSize: perPage }),
        );

        setUsers(data.source);
        setTotalRows(data.totalRecord);
        setLoading(false);
    };

    useEffect(() => {
        fetchUsers(1); // fetch page 1 of users
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    // Get Data
    const getData = async () => {
        setQueryParams({ ...queryParams, page: 1 });
        const data = await getAllDataWithFilterBox(`User/query` + queryToString({ ...queryParams, page: 1 }));
        setUsers(data.source);
        setTotalRows(data.totalRecord);
        setSelectedPage(1);
    };

    useEffect(() => {
        getData();
        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const onSearch = async (value) => {
        setLoading(true);

        setSearchValue(value);

        let data = await getAllDataWithFilterBox(`User/query` + queryToString({ ...queryParams, page: 1 }));
        if (value) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
            data = await getAllDataWithFilterBox(
                `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams({ ...queryParams, page: 1 });
            data = await getAllDataWithFilterBox(`User/query` + queryToString({ ...queryParams, page: 1 }));
        }
        setTotalRows(data.totalRecord);
        setUsers(data.source);
        setLoading(false);
    };

    const [selectedPage, setSelectedPage] = useState(1);

    const handlePageClick = async (event) => {
        setSelectedPage(event.selected + 1);
        setLoading(true);
        setQueryParams({ ...queryParams, page: event.selected + 1, pageSize: 10 });

        const data = await getAllDataWithFilterBox(
            `User/query` + queryToString({ ...queryParams, page: event.selected + 1, pageSize: 10 }),
        );

        setTotalRows(data.totalRecord);
        setUsers(data.source);
        setPerPage(10);
        setLoading(false);
    };

    const CustomPagination = (e) => {
        const count = Math.ceil(totalRows / perPage);
        return (
            <Row className="mx-0">
                <Col className="d-flex justify-content-end" sm="12">
                    <ReactPaginate
                        previousLabel={'Previous'}
                        nextLabel={'Next'}
                        forcePage={selectedPage !== 0 ? selectedPage - 1 : 0}
                        onPageChange={handlePageClick}
                        pageCount={count || 1}
                        breakLabel={'...'}
                        pageRangeDisplayed={2}
                        marginPagesDisplayed={2}
                        activeClassName={'active '}
                        pageClassName={'page-item text-color'}
                        nextLinkClassName={'page-link text-color'}
                        nextClassName={'page-item next text-color'}
                        previousClassName={'page-item prev text-color'}
                        previousLinkClassName={'page-link text-color'}
                        pageLinkClassName={'page-link text-color'}
                        breakClassName="page-item text-color"
                        breakLinkClassName="page-link text-color"
                        containerClassName={'pagination react-paginate pagination-sm justify-content-end pr-1 mt-3'}
                    />
                </Col>
            </Row>
        );
    };

    const getNameSort = (column) => {
        if (column.id === 1) {
            return 'StaffCode';
        }
        if (column.id === 2) {
            return 'Name';
        }
        if (column.id === 4) {
            return 'JoinedDate';
        }
        if (column.id === 5) {
            return 'Type';
        }
        return 'StaffCode';
    };

    const getDataSort = async (column, sortDirection) => {
        if (sortDirection === 'asc') {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Acsending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToString({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Acsending` }),
            );
            setUsers(data.source);
            setPerPage(10);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` });

            const data = await getAllDataWithFilterBox(
                `User/query` +
                    queryToString({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` }),
            );
            setUsers(data.source);
            setPerPage(10);
        }
    };

    const handleSort = async (column, sortDirection) => {
        setLoading(true);

        await getDataSort(column, sortDirection);

        setSelectedPage(1);

        setLoading(false);
    };

    const msgNoData = () => {
        if (loading) {
            return (
                <div style={{ fontSize: '24px', textAlign: '-webkit-center', fontWeight: 'bold', padding: '24px' }}>
                    Loading...
                </div>
            );
        } else {
            return <div style={{ marginTop: '30px', textAlign: '-webkit-center' }}>There are no records to display</div>;
        }
    };

    return (
        <div className="main tableMain">
            <h1 style={{ color: 'red' }}>User List</h1>
            <div className="tableExtension">
                <div className="tableExtensionLeft">
                    <TypeFilter
                        setDataState={setUsers}
                        setTotalRows={setTotalRows}
                        setQueryParams={setQueryParams}
                        queryParams={queryParams}
                        setLoading={setLoading}
                    />
                </div>

                <div className="tableExtensionRight">
                    <SearchUser onSearch={onSearch} searchValue={searchValue} />
                    <ButtonCreate />
                </div>
            </div>
            {users ? (
                <DataTable
                    title="Users"
                    columns={columns}
                    data={users}
                    noHeader
                    defaultSortField="id"
                    defaultSortAsc={true}
                    highlightOnHover
                    noDataComponent={'There are no records to display'}
                    dense
                    pagination
                    paginationComponent={CustomPagination}
                    paginationServer
                    sortServer
                    onSort={handleSort}
                />
            ) : (
                msgNoData()
            )}

            <ModalDetails userDetails={userDetails} handleClose={handleClose} show={show} />

            <Modal show={showRemove} onHide={handleCloseRemove}>
                <Modal.Header>
                    <Modal.Title>Are you sure?</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Label>Do you want to disable this user?</Form.Label>
                    </Form>
                </Modal.Body>
                <Modal.Footer style={{ justifyContent: 'flex-end' }}>
                    <Button variant="danger" onClick={() => handleDisable(userId)}>
                        Disable
                    </Button>
                    <Button variant="outline-secondary" onClick={handleCloseRemove}>
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>

            <ModalNotify isShowModalCantDelete={isShowModalCantDelete} setIsShowModalCantDelete={setIsShowModalCantDelete} />

            {loading && <Loading />}
        </div>
    );
}

export default User;

import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import styles from './request.module.scss';

import { faCheck, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Col, Form, InputGroup, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import DatePicker from 'react-datepicker';
import { BsSearch } from 'react-icons/bs';
import ReactPaginate from 'react-paginate';
import { getAllDataWithFilterBox } from '../../apiServices';
import { dateStrToStr, queryToStringForAssignments } from '../../lib/helper';
import { StateFilterRequest } from './StateFilter/StateFilter';
const cx = classNames.bind(styles);

export const convertStatetoStrRFR = (state) => {
    switch (state) {
        case 0:
            return 'Completed';
        case 1:
            return 'Waiting for returning';

        default:
            break;
    }
};

function Request() {
    const [date, setDate] = useState();

    const onChangeDate = async (date) => {
        setLoading(true);
        const d = new Date(date).toLocaleDateString('fr-CA');

        setDate(date);
        setTimeout(async () => {
            if (date) {
                setQueryParams({ ...queryParams, page: 1, pageSize: 10, date: d });
                const data = await getAllDataWithFilterBox(
                    `RequestForReturning/query` +
                        queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, date: d }),
                );
                setDataRequestForReturning(data.source);
                setTotalPage(data.totalRecord);
            } else {
                setQueryParams({ ...queryParams, page: 1, pageSize: 10, date: '' });
                const data = await getAllDataWithFilterBox(
                    `RequestForReturning/query` +
                        queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, date: '' }),
                );
                setDataRequestForReturning(data.source);
                setTotalPage(data.totalRecord);
            }
            setLoading(false);
        }, 1500);
    };
    // search
    const [search, setSearch] = useState();
    const handleSearch = async (value) => {
        setLoading(true);
        setSearch(value);

        let data = await getAllDataWithFilterBox(`RequestForReturning/query` + queryToStringForAssignments(queryParams));
        if (value) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
            data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams(queryParams);
            data = await getAllDataWithFilterBox(`RequestForReturning/query` + queryToStringForAssignments(queryParams));
        }
        setDataRequestForReturning(data.source);
        setTotalPage(data.totalRecord);
        setLoading(false);
    };

    const handleOnChangeEnter = (e) => {
        if (e.key === 'Enter') {
            handleSearch(search);
        }
    };

    const columns = [
        {
            name: 'No.',
            selector: (row, index) => index + 1,
            sortable: true,
            maxWidth: '20px',
        },
        {
            name: 'Asset Code',
            selector: (row) => row.assignment.asset.assetCode,
            sortable: true,
        },
        {
            name: 'Asset Name',
            sortable: true,
            selector: (row) => row.assignment.asset.assetName,
        },
        {
            name: 'Requested by',
            selector: (row) => row.requestedBy.userName,
            sortable: true,
        },
        {
            name: 'Assigned Date',
            selector: (row) => row.assignment.assignedDate,
            sortable: true,
            cell: (row) => {
                return <div>{dateStrToStr(row.assignment.assignedDate)}</div>;
            },
        },
        {
            name: 'Accepted by',
            selector: (row) => row.acceptedBy,
            sortable: true,
        },
        {
            name: 'Returned Date',
            selector: (row) => row.returnedDate,
            sortable: true,
            cell: (row) => {
                return <div>{dateStrToStr(row.returnedDate)}</div>;
            },
        },
        {
            name: 'State',
            selector: (row) => row.state,
            sortable: true,
            cell: (row) => {
                return <div style={{ minWidth: 140 }}>{convertStatetoStrRFR(row.state)}</div>;
            },
        },
        {
            name: 'Action',
            selector: (row) => row.null,
            cell: (row) => [
                <Link
                    to={row.state === 0 || row.state === 2 ? `.` : ``}
                    key={row.id}
                    state={{ dataRequestForReturning: row }}
                    className={styles.customPen}
                    style={
                        row.state === 0 || row.state === 2 ? { cursor: 'default', color: '#b7b7b7', fontSize: '13px' } : {}
                    }
                >
                    <FontAwesomeIcon icon={faCheck} />
                </Link>,
                <Link
                    key={`keyDelete_${row.id}`}
                    to={'#'}
                    style={
                        row.state === 0
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.5em', marginLeft: '10px' }
                            : { cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRemove} />
                </Link>,
            ],
        },
    ];

    const [loading, setLoading] = useState(false);

    const [dataRequestForReturning, setDataRequestForReturning] = useState([]);
    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        sort: 'RequestForReturningIdAcsending',
        states: '0,1',
    });
    const [totalPage, setTotalPage] = useState();

    // Get Data
    const getData = async () => {
        setLoading(true);
        setQueryParams({ ...queryParams, page: 1 });
        const data = await getAllDataWithFilterBox(
            `RequestForReturning/query` + queryToStringForAssignments({ ...queryParams, page: 1 }),
        );

        console.log('data', data);
        setDataRequestForReturning(data.source);
        setTotalPage(data.totalRecord);
        setSelectedPage(1);
        setLoading(false);
    };

    useEffect(() => {
        getData();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const fetchRequestForReturning = async (page) => {
        setLoading(true);

        setQueryParams({ ...queryParams, page: page, pageSize: 10 });

        const data = await getAllDataWithFilterBox(`RequestForReturning/query` + queryToStringForAssignments(queryParams));

        setDataRequestForReturning(data.source);
        setTotalPage(data.totalRecord);
        setLoading(false);
    };

    useEffect(() => {
        fetchRequestForReturning(1);

        // fetch page 1 of Assignments
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [selectedPage, setSelectedPage] = useState(1);

    const handlePageClick = async (event) => {
        setLoading(true);
        setSelectedPage(event.selected + 1);
        setQueryParams({ ...queryParams, page: event.selected + 1, pageSize: 10 });

        const data = await getAllDataWithFilterBox(
            `RequestForReturning/query` +
                queryToStringForAssignments({ ...queryParams, page: event.selected + 1, pageSize: 10 }),
        );

        setTotalPage(data.totalRecord);
        setDataRequestForReturning(data.source);
        setLoading(false);
    };

    const CustomPagination = (e) => {
        const count = Math.ceil(totalPage / 10);
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
        switch (column.id) {
            case 1:
                return 'RequestForReturningId';
            case 2:
                return 'RequestForReturningCode';
            case 3:
                return 'RequestForReturningName';
            case 4:
                return 'RequestForReturningRequestedBy';
            case 5:
                return 'RequestForReturningAssignedDate';
            case 6:
                return 'RequestForReturningAcceptedBy';
            case 7:
                return 'equestForReturningReturnedDate';
            case 8:
                return 'RequestForReturningState';
            default:
                return 'RequestForReturningId';
        }
    };

    const getDataSort = async (column, sortDirection) => {
        if (sortDirection === 'asc') {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Acsending` });

            const data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `${getNameSort(column)}Acsending`,
                    }),
            );
            setDataRequestForReturning(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` });

            const data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `${getNameSort(column)}Descending`,
                    }),
            );
            setDataRequestForReturning(data.source);
        }
    };

    const handleSort = async (column, sortDirection) => {
        setLoading(true);
        await getDataSort(column, sortDirection);

        setSelectedPage(1);
        setLoading(false);
    };

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Request List</h1>
            </div>
            <div className={cx('filterbox')}>
                <StateFilterRequest
                    setLoading={setLoading}
                    setQueryParams={setQueryParams}
                    queryParams={queryParams}
                    setDataAssignments={setDataRequestForReturning}
                    setTotalPage={setTotalPage}
                />

                <div>
                    <InputGroup>
                        <Form.Group className={cx('common-form')}>
                            <DatePicker
                                selected={date}
                                className="form-control w-full"
                                onChange={(date) => onChangeDate(date)}
                                placeholderText="dd/MM/yyyy"
                                dateFormat="dd/MM/yyyy"
                            />
                        </Form.Group>
                    </InputGroup>
                </div>

                {/* search */}
                <div>
                    <InputGroup>
                        <Form.Control
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                            onKeyUp={handleOnChangeEnter}
                        />
                        <InputGroup.Text>
                            <button
                                className={cx('input')}
                                onClick={() => handleSearch(search)}
                                onKeyUp={handleOnChangeEnter}
                            >
                                <BsSearch />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>
            </div>

            <div className={cx('main_table')}>
                <DataTable
                    title="Assignments"
                    columns={columns}
                    data={dataRequestForReturning}
                    noHeader
                    defaultSortAsc={true}
                    highlightOnHover
                    noDataComponent={'There are no records to display'}
                    dense
                    progressPending={loading}
                    pagination
                    paginationComponent={CustomPagination}
                    paginationServer
                    sortServer
                    onSort={handleSort}
                />
            </div>

            {/* <DetailAssignment
                showDetail={showDetail}
                assignmentDetail={assignmentDetail}
                handleCloseDetail={handleCloseDetail}
            /> */}

            {/* <ModalDelete showDelete={showDelete} setShowDelete={setShowDelete} handleDelete={handleDelete} /> */}
        </div>
    );
}

export default Request;

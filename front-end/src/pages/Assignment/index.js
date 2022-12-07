import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import styles from './assignment.module.scss';

import { faPen, faRefresh, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Col, Form, InputGroup, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { BsSearch } from 'react-icons/bs';
import ReactPaginate from 'react-paginate';
import { getAllDataWithFilterBox } from '../../apiServices';
import { dateStrToStr, queryToStringForAssignments } from '../../lib/helper';
import { StateFilter } from './StateFilter/StateFilter';

const cx = classNames.bind(styles);

function Assignment() {
    let navigate = useNavigate();
    const [date, setDate] = useState('');

    // search
    const [search, setSearch] = useState();
    const handleSearch = async (value) => {
        setLoading(true);
        setSearch(value);

        let data = await getAllDataWithFilterBox(`Assignment/query` + queryToStringForAssignments(queryParams));
        if (value) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
            data = await getAllDataWithFilterBox(
                `Assignment/query` +
                    queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams(queryParams);
            data = await getAllDataWithFilterBox(`Assignment/query` + queryToStringForAssignments(queryParams));
        }
        setDataAssignments(data.source);
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
            selector: (row) => row.id,
            sortable: true,
        },
        {
            name: 'Asset Code',
            selector: (row) => row.assetCode,
            sortable: true,
        },
        {
            name: 'Asset Name',
            sortable: true,
            selector: (row) => row.assetName,
        },
        {
            name: 'Assigned to',
            selector: (row) => row.assignedTo,
            sortable: true,
        },
        {
            name: 'Assigned by',
            selector: (row) => row.assignedBy,
            sortable: true,
        },
        {
            name: 'Assigned Date',
            selector: (row) => row.assignedDate,
            sortable: true,
            cell: (row) => {
                return <div>{dateStrToStr(row.assignedDate)}</div>;
            },
        },
        {
            name: 'State',
            selector: (row) => row.state,
            sortable: true,
        },
        {
            name: 'Action',
            selector: (row) => row.null,
            cell: (row) => [
                <Link
                    to={`./editassignment`}
                    key={row.id}
                    state={{ assignment: row }}
                    className={styles.customPen}
                    style={row.state === 4 ? { cursor: 'default', color: '#b7b7b7', fontSize: '13px' } : {}}
                >
                    <FontAwesomeIcon icon={faPen} />
                </Link>,
                <Link
                    key={`keyDelete_${row.id}`}
                    to={'#'}
                    style={
                        row.state === 4
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.5em', marginLeft: '10px' }
                            : { cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRemove} />
                </Link>,
                <Link
                    key={`keyReturn_${row.id}`}
                    to={'#'}
                    style={
                        row.state === 4
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.5em', marginLeft: '10px' }
                            : { cursor: 'pointer', fontSize: '1.2em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRefresh} />
                </Link>,
            ],
        },
    ];

    const navigateToCreateAssignment = () => {
        navigate('createnewassignment');
    };

    const [loading, setLoading] = useState(false);

    const [dataAssignments, setDataAssignments] = useState([]);
    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        sort: 'AssignmentIdAcsending',
        states: '0,1',
    });

    // Get Data
    const getData = async () => {
        setLoading(true);
        const data = await getAllDataWithFilterBox(`Assignment/query` + queryToStringForAssignments(queryParams));
        setDataAssignments(data.source);

        setLoading(false);
    };

    useEffect(() => {
        getData();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [totalPage, setTotalPage] = useState();

    const fetchAssignments = async (page) => {
        setLoading(true);

        setQueryParams({ ...queryParams, page: page, pageSize: 10 });

        const data = await getAllDataWithFilterBox(`Assignment/query` + queryToStringForAssignments(queryParams));

        setDataAssignments(data.source);
        setTotalPage(data.totalRecord);
        setLoading(false);
    };

    useEffect(() => {
        fetchAssignments(1);

        // fetch page 1 of Assignments
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [selectedPage, setSelectedPage] = useState(1);

    const handlePageClick = async (event) => {
        setLoading(true);
        console.log('event.selected', event.selected);
        setSelectedPage(event.selected + 1);
        setQueryParams({ ...queryParams, page: event.selected + 1, pageSize: 10 });

        const data = await getAllDataWithFilterBox(
            `Assignment/query` + queryToStringForAssignments({ ...queryParams, page: event.selected + 1, pageSize: 10 }),
        );

        setTotalPage(data.totalRecord);
        setDataAssignments(data.source);
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
                return 'AssignmentId';
            case 2:
                return 'AssignmentCode';
            case 3:
                return 'AssignmentName';
            case 4:
                return 'AssignmentAssignedTo';
            case 5:
                return 'AssignmentAssignedBy';
            case 6:
                return 'AssignmentAssignedDate';
            case 7:
                return 'AssignmentState';
            default:
                return 'AssignmentId';
        }
    };

    const getDataSort = async (column, sortDirection) => {
        if (sortDirection === 'asc') {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Acsending` });

            const data = await getAllDataWithFilterBox(
                `Assignment/query` +
                    queryToStringForAssignments({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `${getNameSort(column)}Acsending`,
                    }),
            );
            setDataAssignments(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` });

            const data = await getAllDataWithFilterBox(
                `Assignment/query` +
                    queryToStringForAssignments({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `${getNameSort(column)}Descending`,
                    }),
            );
            setDataAssignments(data.source);
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
                <h1>Assignment List</h1>
            </div>
            <div className={cx('filterbox')}>
                <StateFilter
                    setLoading={setLoading}
                    setQueryParams={setQueryParams}
                    queryParams={queryParams}
                    setDataAssignments={setDataAssignments}
                    setTotalPage={setTotalPage}
                />

                <div>
                    <InputGroup>
                        <Form.Group className={cx('common-form')}>
                            <Form.Control type="date" value={date} onChange={(e) => setDate(e.target.value)} />
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

                <Button variant="danger" onClick={navigateToCreateAssignment}>
                    Create new assignment
                </Button>
            </div>

            <div className={cx('main_table')}>
                <DataTable
                    title="Assignments"
                    columns={columns}
                    data={dataAssignments}
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
        </div>
    );
}

export default Assignment;

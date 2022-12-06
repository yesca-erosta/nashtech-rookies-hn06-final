import classNames from 'classnames/bind';
import styles from './assignment.module.scss';
import { Link, useNavigate } from 'react-router-dom';
import { useEffect, useRef, useState } from 'react';

import { BsSearch, BsFillCalendarDateFill } from 'react-icons/bs';
import { FaFilter } from 'react-icons/fa';
import { Button, Col, Form, InputGroup, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import ReactPaginate from 'react-paginate';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPen, faRemove } from '@fortawesome/free-solid-svg-icons';
import { getAllDataWithFilterBox } from '../../apiServices';
import { dateStrToStr, queryToStringForAssignments } from '../../lib/helper';

const cx = classNames.bind(styles);

function Assignment() {
    const ref = useRef();
    const [checkedState, setCheckedState] = useState({ accepted: false, waitingForAccepted: false });
    const [showState, setShowState] = useState(false);
    const [placeholderState, setPlaceholderState] = useState('State');

    let navigate = useNavigate();

    const columns = [
        {
            name: 'No.',
            selector: (row, index) => index,
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
                    to={`./editasset`}
                    key={row.id}
                    state={{ asset: row }}
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
            ],
        },
    ];

    useEffect(() => {
        const checkIfClickedOutside = (e) => {
            if (showState && ref.current && !ref.current.contains(e.target)) {
                setShowState(false);
            }
        };
        document.addEventListener('mousedown', checkIfClickedOutside);

        return () => {
            document.removeEventListener('mousedown', checkIfClickedOutside);
        };
    }, [showState]);

    const navigateToCreateAssignment = () => {
        navigate('createnewassignment');
    };

    const handleState = () => {
        setShowState((pre) => !pre);
    };

    const handleChangeCheckboxState = (e, type) => {
        setCheckedState({ ...checkedState, [type]: e.target.checked });
    };

    const handleOkState = () => {
        setShowState((pre) => !pre);
        if (checkedState.accepted && checkedState.waitingForAccepted) {
            return setPlaceholderState('Accepted, Waiting for acceptance');
        }
        if (checkedState.accepted) {
            return setPlaceholderState('Accepted');
        }
        if (checkedState.waitingForAccepted) {
            return setPlaceholderState('Waiting for acceptance');
        }

        return setPlaceholderState('State');
    };

    const handleCancelState = () => {
        setShowState(false);
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

    const fetchAssets = async (page) => {
        setLoading(true);

        setQueryParams({ ...queryParams, page: page, pageSize: 10 });

        const data = await getAllDataWithFilterBox(`Assignment/query` + queryToStringForAssignments(queryParams));

        setDataAssignments(data.source);
        setTotalPage(data.totalRecord);
        setLoading(false);
    };

    useEffect(() => {
        fetchAssets(1); // fetch page 1 of Assets
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    console.log('dataAssignments', dataAssignments);

    const [selectedPage, setSelectedPage] = useState(1);

    const handlePageClick = async (event) => {
        setLoading(true);
        setSelectedPage(event.selected + 1);
        setQueryParams({ ...queryParams, page: event.selected + 1, pageSize: 10 });

        const data = await getAllDataWithFilterBox(`Assignment/query` + queryToStringForAssignments(queryParams));

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

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Assignment List</h1>
            </div>
            <div className={cx('filterbox')}>
                <div style={{ position: 'relative' }}>
                    <InputGroup>
                        <Form.Control placeholder={placeholderState} />

                        <InputGroup.Text>
                            <button className={cx('input')} onClick={handleState}>
                                <FaFilter />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>

                    {showState && (
                        <div className={cx('dropdown')} ref={ref}>
                            <div className={cx('dropdown_container')}>
                                <div className={cx('dropdown_title')}>Select type(s)</div>
                                <div>
                                    <Form.Check
                                        type={'checkbox'}
                                        label={`Accepted`}
                                        id={`accepted`}
                                        onChange={(e) => handleChangeCheckboxState(e, 'accepted')}
                                        checked={checkedState.accepted}
                                    />
                                    <Form.Check
                                        type={'checkbox'}
                                        label={`Waiting for aceepted`}
                                        id={`waitingForAccepted`}
                                        onChange={(e) => handleChangeCheckboxState(e, 'waitingForAccepted')}
                                        checked={checkedState.waitingForAccepted}
                                    />
                                </div>

                                <div className={cx('button')}>
                                    <Button variant="danger" size="sm" className={cx('button_ok')} onClick={handleOkState}>
                                        OK
                                    </Button>
                                    <Button
                                        variant="outline-secondary"
                                        size="sm"
                                        className={cx('button_cancel')}
                                        onClick={handleCancelState}
                                    >
                                        Cancel
                                    </Button>
                                </div>
                            </div>
                        </div>
                    )}
                </div>

                <div>
                    <InputGroup>
                        <Form.Control placeholder="Assigned Date" />

                        <InputGroup.Text>
                            <button className={cx('input')}>
                                <BsFillCalendarDateFill />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>

                <div>
                    <InputGroup>
                        <Form.Control />
                        <InputGroup.Text>
                            <button className={cx('input')}>
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
                    title="Assets"
                    columns={columns}
                    data={dataAssignments}
                    noHeader
                    defaultSortAsc={true}
                    highlightOnHover
                    // noDataComponent={'There are no records to display'}
                    dense
                    progressPending={loading}
                    pagination
                    paginationComponent={CustomPagination}
                    paginationServer
                    // sortServer
                    // onSort={handleSort}
                />
            </div>
        </div>
    );
}

export default Assignment;

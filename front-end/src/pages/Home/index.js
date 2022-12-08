import { faCheck, faRefresh, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import ReactPaginate from 'react-paginate';
import { Link } from 'react-router-dom';
import { getAllDataWithFilterBox } from '../../apiServices';
import { dateStrToStr } from '../../lib/helper';
import './home.scss';

import { ModalFirstChangePassword } from './Modal/ModalFirstChangePassword';

function Home() {
    const [dataAssignment, setDataAssignment] = useState([]);

    // Get Data
    const getData = async () => {
        const data = await getAllDataWithFilterBox(`Assignment/getlistbyuserid`);
        setDataAssignment(data);
    };

    useEffect(() => {
        getData();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const convertStatetoStr = (state) => {
        switch (state) {
            case 0:
                return 'Accepted';
            case 1:
                return 'Waiting for acceptance';

            default:
                break;
        }
    };

    const columns = [
        {
            name: 'Asset Code',
            selector: (row) => row.assetCode,
            sortable: true,
        },
        {
            name: 'Asset Name',
            selector: (row) => row.assetName,
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
            cell: (row) => {
                return <div>{convertStatetoStr(row.state)}</div>;
            },
        },
        {
            name: 'Action',
            selector: (row) => row.null,
            cell: (row) => [
                <Link
                    to={`./editassignment`}
                    key={row.id}
                    state={{ assignment: row }}
                    style={
                        row.state === 0 ? { cursor: 'default', color: '#b7b7b7', fontSize: '17px' } : { fontSize: '17px' }
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
                <Link
                    key={`keyReturn_${row.id}`}
                    to={'#'}
                    style={
                        row.state === 0
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.3em', marginLeft: '10px' }
                            : { cursor: 'pointer', fontSize: '1.2em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRefresh} />
                </Link>,
            ],
        },
    ];

    const CustomPagination = (e) => {
        return (
            <Row style={{ width: 1200 }} className="mx-0">
                <Col className="d-flex justify-content-end" sm="12">
                    <ReactPaginate
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
        <>
            <ModalFirstChangePassword />

            <h1 style={{ color: 'red', marginBottom: 46 }}>My Assignment</h1>

            <div>
                <DataTable
                    title="Assignments"
                    columns={columns}
                    data={dataAssignment}
                    noHeader
                    defaultSortAsc={true}
                    highlightOnHover
                    noDataComponent={'There are no records to display'}
                    dense
                    pagination
                    paginationComponent={CustomPagination}
                    paginationServer
                />
            </div>
        </>
    );
}

export default Home;

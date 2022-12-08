import { faCheck, faRefresh, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import { Link } from 'react-router-dom';
import { getAllDataWithFilterBox, updateData } from '../../apiServices';
import { ASSIGNMENT } from '../../constants';
import { dateStrToStr } from '../../lib/helper';
import './home.scss';

import { ModalFirstChangePassword } from './Modal/ModalFirstChangePassword';
import { ShowModalAccepted } from './Modal/ShowModalAccepted';
import { ShowModalDecline } from './Modal/ShowModalDecline';

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
            case 2:
                return 'Declined';
            default:
                break;
        }
    };
    const [loading, setLoading] = useState(false);

    const [dataId, setDataId] = useState('');
    const [isShowAccepted, setIsShowAccepted] = useState(false);
    const [isShowDecline, setIsShowDecline] = useState(false);

    const handleShowAccepted = (e, row) => {
        if (row.state === 0 || row.state === 2) {
            e.preventDefault();
        } else {
            setDataId(row.id);
            setIsShowAccepted(true);
        }
    };

    const handleShowDecline = (e, row) => {
        if (row.state === 0 || row.state === 2) {
            e.preventDefault();
        } else {
            setDataId(row.id);
            setIsShowDecline(true);
        }
    };

    const handleAccepted = async () => {
        setLoading(true);
        await updateData(`${ASSIGNMENT}/accepted/${dataId}`, dataId);

        getData();
        setDataId('');
        setIsShowAccepted(false);
        setLoading(false);
    };

    const handleDecline = async () => {
        setLoading(true);
        await updateData(`${ASSIGNMENT}/declined/${dataId}`, dataId);

        getData();
        setDataId('');
        setIsShowDecline(false);
        setLoading(false);
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
                    to={'#'}
                    key={row.id}
                    state={{ assignment: row }}
                    style={
                        row.state === 0 || row.state === 2
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '17px' }
                            : { fontSize: '17px' }
                    }
                >
                    <FontAwesomeIcon icon={faCheck} onClick={(e) => handleShowAccepted(e, row)} />
                </Link>,
                <Link
                    key={`keyDelete_${row.id}`}
                    to={'#'}
                    style={
                        row.state === 0 || row.state === 2
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.5em', marginLeft: '10px' }
                            : { cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRemove} onClick={(e) => handleShowDecline(e, row)} />
                </Link>,
                <Link
                    key={`keyReturn_${row.id}`}
                    to={'#'}
                    style={
                        row.state === 1
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.3em', marginLeft: '10px' }
                            : { cursor: 'pointer', fontSize: '1.2em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRefresh} />
                </Link>,
            ],
        },
    ];

    const customStyles = {
        rows: {
            style: {
                width: '1200px',
            },
        },
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
                    progressPending={loading}
                    customStyles={customStyles}
                />
            </div>

            <ShowModalAccepted
                isShowAccepted={isShowAccepted}
                setIsShowAccepted={setIsShowAccepted}
                handleAccepted={handleAccepted}
            />

            <ShowModalDecline
                isShowDecline={isShowDecline}
                setIsShowDecline={setIsShowDecline}
                handleDecline={handleDecline}
            />
        </>
    );
}

export default Home;

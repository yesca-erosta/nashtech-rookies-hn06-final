import { faCheck, faRefresh, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import { Link, useNavigate } from 'react-router-dom';
import { createData, getAllDataWithFilterBox, updateData } from '../../apiServices';
import { Loading } from '../../components/Loading/Loading';
import { ASSIGNMENT, REQUEST_FOR_RETURNING } from '../../constants';
import { dateStrToStr } from '../../lib/helper';
import { ModalRequest } from '../Assignment/Modal/ModalRequest/ModalRequest';
import { DetailAssignmentForUser } from './DetailAssignmentForUser/DetailAssignmentForUser';
import './home.scss';

import { ModalFirstChangePassword } from './Modal/ModalFirstChangePassword';
import { ShowModalAccepted } from './Modal/ShowModalAccepted';
import { ShowModalDecline } from './Modal/ShowModalDecline';

export const convertStatetoStr = (state) => {
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

function Home() {
    const [dataAssignment, setDataAssignment] = useState([]);

    // Get Data
    const getData = async () => {
        setLoading(true);
        const data = await getAllDataWithFilterBox(`Assignment/getlistbyuserid`);
        setDataAssignment(data);
        setLoading(false);
    };

    useEffect(() => {
        getData();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

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

    const [showDetail, setShowDetail] = useState(false);
    const [assignmentDetail, setAssignmentDetail] = useState('');

    const handleShowDetail = (assetCode) => {
        setShowDetail(true);
        setAssignmentDetail(dataAssignment.find((c) => c.assetCode === assetCode));
    };

    const handleCloseDetail = () => {
        setShowDetail(false);
    };

    const [showRequest, setShowRequest] = useState(false);

    const handleShowReturning = (e, assignment) => {
        if (assignment.state === 1 || assignment.isReturning || assignment.state === 2) {
            e.preventDefault();
        } else {
            setDataId(assignment.id);
            setShowRequest(true);
        }
    };

    const navigate = useNavigate();

    const handleRequest = async () => {
        setLoading(true);
        await createData(REQUEST_FOR_RETURNING, { assignmentId: dataId });

        setShowRequest(false);
        navigate('/requestforreturning');
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
            cell: (row) => {
                return <Link onClick={() => handleShowDetail(row.assetCode)}>{row.assetName}</Link>;
            },
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
                        row.state === 1 || row.isReturning || row.state === 2
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.3em', marginLeft: '10px' }
                            : { cursor: 'pointer', fontSize: '1.2em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRefresh} onClick={(e) => handleShowReturning(e, row)} />
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
                    customStyles={customStyles}
                />
            </div>
            <DetailAssignmentForUser
                showDetail={showDetail}
                assignmentDetail={assignmentDetail}
                handleCloseDetail={handleCloseDetail}
            />

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

            <ModalRequest showRequest={showRequest} setShowRequest={setShowRequest} handleRequest={handleRequest} />

            {loading && <Loading />}
        </>
    );
}

export default Home;

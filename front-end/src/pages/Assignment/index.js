import Table from 'react-bootstrap/Table';
import classNames from 'classnames/bind';
import styles from './assignment.module.scss';
import { useNavigate } from 'react-router-dom';
import { useEffect, useRef, useState } from 'react';

import { BsSearch, BsFillCalendarDateFill } from 'react-icons/bs';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';
import { FaFilter } from 'react-icons/fa';
import { Button, Form, InputGroup } from 'react-bootstrap';
import Pagination from 'react-bootstrap/Pagination';

const cx = classNames.bind(styles);

function Assignment() {
    const ref = useRef();
    const [checkedState, setCheckedState] = useState({ accepted: false, waitingForAccepted: false });
    const [showState, setShowState] = useState(false);
    const [showCategory, setShowCategory] = useState(false);
    const [placeholderState, setPlaceholderState] = useState('State');

    const [isNo, setIsNo] = useState(false);
    const [isAssetCode, setIsAssetCode] = useState(false);
    const [isAssetName, setIsAssetName] = useState(false);
    const [isAssignedTo, setIsAssignedTo] = useState(false);
    const [isAssignedBy, setIsAssignedBy] = useState(false);
    const [isAssignedDate, setIsAssignedDate] = useState(false);
    const [isState, setIsState] = useState(false);

    const handleIsNo = () => {
        setIsNo((pre) => !pre);
    };
    const handleIsAssetCode = () => {
        setIsAssetCode((pre) => !pre);
    };
    const handleIsAssetName = () => {
        setIsAssetName((pre) => !pre);
    };
    const handleIsAssignedTo = () => {
        setIsAssignedTo((pre) => !pre);
    };
    const handleIsAssignedBy = () => {
        setIsAssignedBy((pre) => !pre);
    };
    const handleIsAssignedDate = () => {
        setIsAssignedDate((pre) => !pre);
    };
    const handleIsState = () => {
        setIsState((pre) => !pre);
    };

    let navigate = useNavigate();

    useEffect(() => {
        const checkIfClickedOutside = (e) => {
            if (showState && ref.current && !ref.current.contains(e.target)) {
                setShowState(false);
            }

            if (showCategory && ref.current && !ref.current.contains(e.target)) {
                setShowCategory(false);
            }
        };
        document.addEventListener('mousedown', checkIfClickedOutside);

        return () => {
            document.removeEventListener('mousedown', checkIfClickedOutside);
        };
    }, [showState, showCategory]);

    const navigateToCreateAsset = () => {
        navigate('createnewasset');
    };

    const handleState = () => {
        setShowState((pre) => !pre);
    };
    const handleCategory = () => {
        setShowCategory((pre) => !pre);
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
        setShowState((pre) => !pre);
    };

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Assignment List</h1>
            </div>

            <div className={cx('filterbox')}>
                <div>
                    <InputGroup>
                        <Form.Control placeholder={placeholderState} />

                        <InputGroup.Text>
                            <button className={cx('input')} onClick={handleState}>
                                <FaFilter />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>

                <div>
                    <InputGroup>
                        <Form.Control placeholder="Assigned Date" />

                        <InputGroup.Text>
                            <button className={cx('input')} onClick={handleCategory}>
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

                <Button variant="danger" onClick={navigateToCreateAsset}>
                    Create new assignment
                </Button>
            </div>

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
                            <Button variant="light" size="sm" className={cx('button_cancel')} onClick={handleCancelState}>
                                Cancel
                            </Button>
                        </div>
                    </div>
                </div>
            )}

            <div className={cx('table')}>
                <Table responsive="sm">
                    <thead>
                        <tr>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div>No.</div>
                                        <button className={cx('triagle')} onClick={handleIsNo}>
                                            {isNo ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div>Asset Code</div>
                                        <button className={cx('triagle')} onClick={handleIsAssetCode}>
                                            {isAssetCode ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div>Asset Name</div>
                                        <button className={cx('triagle')} onClick={handleIsAssetName}>
                                            {isAssetName ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div>Assigned to</div>
                                        <button className={cx('triagle')} onClick={handleIsAssignedTo}>
                                            {isAssignedTo ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div> Assigned by</div>
                                        <button className={cx('triagle')} onClick={handleIsAssignedBy}>
                                            {isAssignedBy ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div> Assigned Date</div>
                                        <button className={cx('triagle')} onClick={handleIsAssignedDate}>
                                            {isAssignedDate ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div> State</div>
                                        <button className={cx('triagle')} onClick={handleIsState}>
                                            {isState ? <GoTriangleUp /> : <GoTriangleDown />}
                                        </button>
                                    </div>
                                </>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* {.map((item, index) => (
                            <tr key={index}>
                                <td>{index + 1}</td>
                                <td>{item.asset}</td>
                                <td>{item.name}</td>
                                <td>{item.assignedTo}</td>
                                <td>{item.assignedBy}</td>
                                <td>{item.assignedDate}</td>
                                <td>{item.state}</td>
                                <td>
                                    <div className={cx('actions')}>
                                        <button className={cx('pen')} disabled={false} onClick={navigateToEditAsset}>
                                            <BsFillPencilFill />
                                        </button>
                                        <button className={cx('delete')} disabled={false}>
                                            <TiDeleteOutline />
                                        </button>
                                        <button className={cx('delete')} disabled={false}>
                                            <MdRefresh style={{ color: '#0d6efd' }} />
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))} */}
                    </tbody>
                </Table>
            </div>

            <div className={cx('paging')}>
                <Pagination>
                    <Pagination.Item disabled>Previous</Pagination.Item>
                    <Pagination.Item active={1}>1</Pagination.Item>
                    <Pagination.Item active={''}>2</Pagination.Item>
                    <Pagination.Item active={''}>3</Pagination.Item>
                    <Pagination.Item>Next</Pagination.Item>
                </Pagination>
            </div>
        </div>
    );
}

export default Assignment;

import classNames from 'classnames/bind';
import { useEffect, useRef, useState } from 'react';
import { Button, Form, InputGroup } from 'react-bootstrap';
import { FaFilter } from 'react-icons/fa';
import { getAllDataWithFilterBox } from '../../../apiServices';
import { queryToStringForAssignments } from '../../../lib/helper';
import styles from '../request.module.scss';

export const StateFilterRequest = ({ setLoading, setQueryParams, queryParams, setDataAssignments, setTotalPage }) => {
    const cx = classNames.bind(styles);
    const [placeholderState, setPlaceholderState] = useState('State');
    const [showState, setShowState] = useState(false);
    const [checkedState, setCheckedState] = useState({ accepted: false, waitingForReturning: false });
    const ref = useRef();

    const handleOkState = async () => {
        setLoading(true);

        let data = await getAllDataWithFilterBox(
            `RequestForReturning/query` +
                queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, states: '0,1' }),
        );

        if (!checkedState.completed && !checkedState.waitingForReturning) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1' });
            data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, states: '0,1' }),
            );
            setPlaceholderState('State');
        }

        if (checkedState.completed) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0' });
            data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, states: '0' }),
            );
            setPlaceholderState('Completed');
        }
        if (checkedState.waitingForReturning) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '1' });
            data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, states: '1' }),
            );
            setPlaceholderState('Waiting for returning');
        }
        if (checkedState.completed && checkedState.waitingForReturning) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1' });
            data = await getAllDataWithFilterBox(
                `RequestForReturning/query` +
                    queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, states: '0,1' }),
            );
            setPlaceholderState('All');
        }

        setTotalPage(data.totalRecord);
        setDataAssignments(data.source);
        setShowState(false);
        setLoading(false);
    };

    const handleState = () => {
        setShowState((pre) => !pre);
    };

    const handleCancelState = async () => {
        setLoading(true);
        setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1' });
        const data = await getAllDataWithFilterBox(
            `RequestForReturning/query` +
                queryToStringForAssignments({ ...queryParams, page: 1, pageSize: 10, states: '0,1' }),
        );
        setPlaceholderState('State');
        setTotalPage(data.totalRecord);
        setDataAssignments(data.source);
        setCheckedState({ accepted: false, waitingForAccepted: false });
        setShowState(false);
        setLoading(false);
    };

    const handleChangeCheckboxState = (e) => {
        setCheckedState({ ...checkedState, [e.target.name]: e.target.checked });
    };

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

    return (
        <div style={{ position: 'relative' }}>
            <InputGroup>
                <Form.Control placeholder={placeholderState} readOnly onClick={handleState} />

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
                                label={`Completed`}
                                id={`completed`}
                                value={0}
                                name="completed"
                                onChange={handleChangeCheckboxState}
                                checked={checkedState.completed}
                            />
                            <Form.Check
                                type={'checkbox'}
                                label={`Waiting for returning`}
                                id={`waitingForReturning`}
                                value={1}
                                name="waitingForReturning"
                                onChange={handleChangeCheckboxState}
                                checked={checkedState.waitingForReturning}
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
    );
};

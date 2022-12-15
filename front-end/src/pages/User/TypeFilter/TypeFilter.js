import classNames from 'classnames/bind';
import { useEffect, useRef, useState } from 'react';
import { Button, Form, InputGroup } from 'react-bootstrap';
import { FaFilter } from 'react-icons/fa';
import { getAllDataWithFilterBox } from '../../../apiServices';
import { queryToString } from '../../../lib/helper';
import styles from './TypeFilter.module.scss';

export const TypeFilter = ({ setDataState, setQueryParams, queryParams, setTotalRows, setLoading }) => {
    const cx = classNames.bind(styles);
    const [placeholderState, setPlaceholderState] = useState('Type');
    const [showState, setShowState] = useState(false);

    const handleState = () => {
        setShowState((pre) => !pre);
    };
    const ref = useRef();
    const [checkedStateHoan, setCheckedStateHoan] = useState({ admin: false, staff: false });

    useEffect(() => {
        const initSelectState = { admin: false, staff: false };

        const checkIfClickedOutside = async (e) => {
            if (showState && ref.current && !ref.current.contains(e.target)) {
                setLoading(true);
                setCheckedStateHoan(initSelectState);
                setPlaceholderState('Type');
                setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: '0,1' });
                const data = await getAllDataWithFilterBox(
                    `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: '0,1' }),
                );
                setTotalRows(data.totalRecord);
                setDataState(data.source);
                setShowState(false);
                setLoading(false);
            }
        };
        document.addEventListener('mousedown', checkIfClickedOutside);

        return () => {
            document.removeEventListener('mousedown', checkIfClickedOutside);
        };
        // I dont want render when categories changed
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [showState, queryParams]);

    const handleChangeCheckboxHoan = (e) => {
        setCheckedStateHoan({ ...checkedStateHoan, [e.target.name]: e.target.checked });
    };

    const handleOkState = async () => {
        setLoading(true);
        let data = await getAllDataWithFilterBox(
            `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: '0,1' }),
        );

        if (checkedStateHoan.admin) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: '1' });

            data = await getAllDataWithFilterBox(
                `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: '1' }),
            );
            setPlaceholderState('Admin');
        }
        if (checkedStateHoan.staff) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: '0' });

            data = await getAllDataWithFilterBox(
                `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: '0' }),
            );
            setPlaceholderState('Staff');
        }
        if (checkedStateHoan.admin && checkedStateHoan.staff) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: '0,1' });

            data = await getAllDataWithFilterBox(
                `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: '0,1' }),
            );

            setPlaceholderState('All');
        }

        setTotalRows(data.totalRecord);
        setDataState(data.source);
        setShowState(false);
        setLoading(false);
    };

    const handleCancelState = async () => {
        setLoading(true);
        setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: '0,1' });
        const data = await getAllDataWithFilterBox(
            `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: '0,1' }),
        );
        setPlaceholderState('Type');
        setTotalRows(data.totalRecord);
        setDataState(data.source);
        setCheckedStateHoan({ admin: false, staff: false });
        setShowState(false);
        setLoading(false);
    };

    return (
        <>
            <div>
                <InputGroup>
                    <Form.Control placeholder={placeholderState} readOnly onClick={handleState} />

                    <InputGroup.Text>
                        <button className={cx('input-state')} onClick={handleState}>
                            <FaFilter />
                        </button>
                    </InputGroup.Text>
                </InputGroup>
            </div>

            {showState && (
                <div className={cx('dropdown')} ref={ref}>
                    <div className={cx('dropdown_container')}>
                        <div className={cx('dropdown_title')}>Select type(s)</div>
                        <div>
                            <Form.Check
                                type={'checkbox'}
                                label={`Admin`}
                                id={`admin`}
                                value={1}
                                name="admin"
                                onChange={handleChangeCheckboxHoan}
                                checked={checkedStateHoan.admin}
                            />
                            <Form.Check
                                type={'checkbox'}
                                label={`Staff`}
                                id={`staff`}
                                value={0}
                                name="staff"
                                onChange={handleChangeCheckboxHoan}
                                checked={checkedStateHoan.staff}
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
        </>
    );
};

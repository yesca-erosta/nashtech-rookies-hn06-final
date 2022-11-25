import Table from 'react-bootstrap/Table';
import classNames from 'classnames/bind';
import styles from './asset.module.scss';
import { useNavigate } from 'react-router-dom';
import { useEffect, useRef, useState } from 'react';

import { BsFillPencilFill, BsSearch } from 'react-icons/bs';
import { TiDeleteOutline } from 'react-icons/ti';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';
import { FaFilter } from 'react-icons/fa';
import { Button, Form, InputGroup } from 'react-bootstrap';
import Pagination from 'react-bootstrap/Pagination';

const cx = classNames.bind(styles);

function Asset() {
    const ref = useRef();
    const [checkedState, setCheckedState] = useState({ available: false, notAvailable: false });
    const [checkedCategory, setCheckedCategory] = useState({ laptop: false, monitor: false, personalComputer: false });
    const [showState, setShowState] = useState(false);
    const [showCategory, setShowCategory] = useState(false);
    const [placeholderState, setPlaceholderState] = useState('State');
    const [placeholderCategory, setPlaceholderCategory] = useState('Category');

    const [isAssetCode, setIsAssetCode] = useState(false);
    const [isAssetName, setIsAssetName] = useState(false);
    const [isCategory, setIsCategory] = useState(false);
    const [isState, setIsState] = useState(false);

    const handleIsAssetCode = () => {
        setIsAssetCode((pre) => !pre);
    };
    const handleIsAssetName = () => {
        setIsAssetName((pre) => !pre);
    };
    const handleIsCategory = () => {
        setIsCategory((pre) => !pre);
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

    const navigateToEditAsset = () => {
        navigate('editasset');
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
        if (checkedState.available && checkedState.notAvailable) {
            return setPlaceholderState('Available, Not available');
        }
        if (checkedState.available) {
            return setPlaceholderState('Available');
        }
        if (checkedState.notAvailable) {
            return setPlaceholderState('Not available');
        }

        return setPlaceholderState('State');
    };

    const handleCancelState = () => {
        setShowState((pre) => !pre);
    };

    const handleChangeCheckboxCategory = (e, type) => {
        setCheckedCategory({ ...checkedCategory, [type]: e.target.checked });
    };

    const handleOkCategory = () => {
        setShowCategory(false);
        if (checkedCategory.laptop && checkedCategory.monitor && checkedCategory.personalComputer) {
            return setPlaceholderCategory('Laptop, Monitor, Personal Computer');
        }
        if (checkedCategory.monitor && checkedCategory.personalComputer) {
            return setPlaceholderCategory(' Monitor, Personal Computer');
        }
        if (checkedCategory.laptop && checkedCategory.monitor) {
            return setPlaceholderCategory('Laptop, Monitor');
        }
        if (checkedCategory.laptop && checkedCategory.personalComputer) {
            return setPlaceholderCategory('Laptop, Personal Computer');
        }
        if (checkedCategory.laptop) {
            return setPlaceholderCategory('Laptop');
        }
        if (checkedCategory.monitor) {
            return setPlaceholderCategory(' Monitor');
        }
        if (checkedCategory.personalComputer) {
            return setPlaceholderCategory(' Personal Computer');
        }

        return setPlaceholderCategory('Category');
    };

    const handleCancelCategory = () => {
        setShowCategory((pre) => !pre);
    };

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Asset List</h1>
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
                        <Form.Control placeholder={placeholderCategory} />

                        <InputGroup.Text>
                            <button className={cx('input')} onClick={handleCategory}>
                                <FaFilter />
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
                    Create new asset
                </Button>
            </div>

            {showState && (
                <div className={cx('dropdown')} ref={ref}>
                    <div className={cx('dropdown_container')}>
                        <div className={cx('dropdown_title')}>Select type(s)</div>
                        <div>
                            <Form.Check
                                type={'checkbox'}
                                label={`Available`}
                                id={`available`}
                                onChange={(e) => handleChangeCheckboxState(e, 'available')}
                                checked={checkedState.available}
                            />
                            <Form.Check
                                type={'checkbox'}
                                label={`Not available`}
                                id={`notAvailable`}
                                onChange={(e) => handleChangeCheckboxState(e, 'notAvailable')}
                                checked={checkedState.notAvailable}
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

            {showCategory && (
                <div className={cx('dropdown')} style={{ marginLeft: 410 }} ref={ref}>
                    <div className={cx('dropdown_container')}>
                        <div className={cx('dropdown_title')}>Select type(s)</div>
                        <div>
                            <Form.Check
                                type="checkbox"
                                label={`Laptop`}
                                onChange={(e) => handleChangeCheckboxCategory(e, 'laptop')}
                                checked={checkedCategory.laptop}
                            />
                            <Form.Check
                                type="checkbox"
                                label={`Monitor`}
                                onChange={(e) => handleChangeCheckboxCategory(e, 'monitor')}
                                checked={checkedCategory.monitor}
                            />
                            <Form.Check
                                type="checkbox"
                                label={`Personal Computer`}
                                onChange={(e) => handleChangeCheckboxCategory(e, 'personalComputer')}
                                checked={checkedCategory.personalComputer}
                            />
                        </div>

                        <div className={cx('button')}>
                            <Button variant="danger" size="sm" className={cx('button_ok')} onClick={handleOkCategory}>
                                OK
                            </Button>
                            <Button variant="light" size="sm" className={cx('button_cancel')} onClick={handleCancelCategory}>
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
                                        <div>Asset Code</div>
                                        <button className={cx('triagle')} onClick={handleIsAssetCode}>
                                            {isAssetCode ? <GoTriangleDown /> : <GoTriangleUp />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div>Asset Name</div>
                                        <button className={cx('triagle')} onClick={handleIsAssetName}>
                                            {isAssetName ? <GoTriangleDown /> : <GoTriangleUp />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div> Category</div>
                                        <button className={cx('triagle')} onClick={handleIsCategory}>
                                            {isCategory ? <GoTriangleDown /> : <GoTriangleUp />}
                                        </button>
                                    </div>
                                </>
                            </th>
                            <th>
                                <>
                                    <div className={cx('title')}>
                                        <div> State</div>
                                        <button className={cx('triagle')} onClick={handleIsState}>
                                            {isState ? <GoTriangleDown /> : <GoTriangleUp />}
                                        </button>
                                    </div>
                                </>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>LA100001</td>
                            <td>Laptop HP Probook 450 G1</td>
                            <td>Laptop</td>
                            <td>Available</td>

                            <td>
                                <div className={cx('actions')}>
                                    <button className={cx('pen')} disabled={false} onClick={navigateToEditAsset}>
                                        <BsFillPencilFill />
                                    </button>
                                    <button className={cx('delete')} disabled={false}>
                                        <TiDeleteOutline />
                                    </button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>LA100001</td>
                            <td>Monitor Dell UltraSharp</td>
                            <td>Monitor</td>
                            <td>Not Available</td>

                            <td>
                                <div className={cx('actions')}>
                                    <button className={cx('pen')} disabled={false} onClick={navigateToEditAsset}>
                                        <BsFillPencilFill />
                                    </button>
                                    <button className={cx('delete')} disabled={false}>
                                        <TiDeleteOutline />
                                    </button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>LA100001</td>
                            <td>Personal Computer</td>
                            <td>Personal Computer</td>
                            <td>Available</td>

                            <td>
                                <div className={cx('actions')}>
                                    <button className={cx('pen')} disabled={false} onClick={navigateToEditAsset}>
                                        <BsFillPencilFill />
                                    </button>
                                    <button className={cx('delete')} disabled={false}>
                                        <TiDeleteOutline />
                                    </button>
                                </div>
                            </td>
                        </tr>
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

export default Asset;

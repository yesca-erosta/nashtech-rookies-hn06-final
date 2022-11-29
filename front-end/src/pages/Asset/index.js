import axios from 'axios';
import classNames from 'classnames/bind';
import { useEffect, useRef, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { BASE_URL } from '../../constants';
import { useAuthContext } from '../../context/RequiredAuth/authContext';
import styles from './asset.module.scss';

import { Button, Form, InputGroup } from 'react-bootstrap';
import Modal from 'react-bootstrap/Modal';
import Pagination from 'react-bootstrap/Pagination';
import Table from 'react-bootstrap/Table';
import { BsFillPencilFill, BsSearch } from 'react-icons/bs';
import { FaFilter } from 'react-icons/fa';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';
import { TiDeleteOutline } from 'react-icons/ti';

const cx = classNames.bind(styles);

function Asset() {
    const { setId } = useAuthContext();
    const ref = useRef();
    const [checkedState, setCheckedState] = useState({ available: false, notAvailable: false, assigned: false });
    const [checkedCategory, setCheckedCategory] = useState({ laptop: false, monitor: false, personalComputer: false });
    const [showState, setShowState] = useState(false);
    const [showCategory, setShowCategory] = useState(false);
    const [placeholderState, setPlaceholderState] = useState('State');
    const [placeholderCategory, setPlaceholderCategory] = useState('Category');

    const [isAssetCode, setIsAssetCode] = useState(false);
    const [isAssetName, setIsAssetName] = useState(false);
    const [isCategory, setIsCategory] = useState(false);
    const [isState, setIsState] = useState(false);
    const [showDelete, setShowDelete] = useState(false);
    const [activeDeleteId, setactiveDeleteId] = useState();
    const [deleteSuccess, setDeleteSuccess] = useState(false);
    const [search, setSearch] = useState('');

    const [currentPage, setCurrentPage] = useState(1);
    const [totalPage, setTotalPage] = useState();

    const [dataList, setDataList] = useState([]);

    let location = useLocation();
    let navigate = useNavigate();
    const { token } = useAuthContext();

    const handleIsAssetCode = () => {
        setIsAssetCode((pre) => !pre);

        if (dataList) {
            dataList.reverse();
        }
    };
    const handleIsAssetName = () => {
        setIsAssetName((pre) => !pre);
        if (dataList) {
            dataList.reverse();
        }
    };
    const handleIsCategory = () => {
        setIsCategory((pre) => !pre);
        if (dataList) {
            dataList.reverse();
        }
    };
    const handleIsState = () => {
        setIsState((pre) => !pre);
        if (dataList) {
            dataList.reverse();
        }
    };

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

    const navigateToEditAsset = (id) => {
        setId(id);
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
        if (checkedState.available && checkedState.notAvailable && checkedState.assigned) {
            return setPlaceholderState('Available, Not available, Assigned');
        }
        if (checkedState.available && checkedState.notAvailable) {
            return setPlaceholderState('Available, Not available');
        }
        if (checkedState.available && checkedState.assigned) {
            return setPlaceholderState('Available, Assigned');
        }
        if (checkedState.notAvailable && checkedState.assigned) {
            return setPlaceholderState('Not available, Assigned');
        }
        if (checkedState.available) {
            return setPlaceholderState('Available');
        }
        if (checkedState.notAvailable) {
            return setPlaceholderState('Not available');
        }
        if (checkedState.assigned) {
            return setPlaceholderState('Assigned');
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

    const showModalDelete = (id) => {
        setactiveDeleteId(id);
        setShowDelete(true);
    };
    const handleCloseDelete = () => setShowDelete(false);
    const handleDelete = async (id) => {
        await axios({
            method: 'DELETE',
            url: `${BASE_URL}/Asset/${id}`,
            headers: {
                Accept: 'application/json',
                Authorization: `Bearer ${token.token}`,
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
        })
            .then((response) => {
                if (response.status === 200) {
                    setShowDelete(false);
                    setDeleteSuccess((pre) => !pre);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    };

    useEffect(() => {
        if (!location.search) {
            navigate('?p=1');
        }

        return;
    }, [location, navigate]);

    const handlePage = (item) => {
        setCurrentPage(item);
        navigate(`?p=${item}`);
    };

    useEffect(() => {
        const currentSearchPage = location.search?.slice(-1);
        if (currentSearchPage && Number(currentSearchPage)) {
            setCurrentPage(Number(currentSearchPage));
        }
    }, [currentPage, location.pathname, location.search]);

    useEffect(() => {
        axios({
            method: 'GET',
            url: `${BASE_URL}/Asset/query?page=${currentPage}&pageSize=2`,
            headers: {
                Accept: 'application/json',
                Authorization: `Bearer ${token.token}`,
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
        })
            .then((response) => {
                setDataList(response.data.source);
                setTotalPage(response.data.totalPage);
            })
            .catch((error) => {
                console.log(error);
            });
    }, [currentPage, token.token, deleteSuccess]);

    const handleSearch = async () => {
        try {
            const response = await fetch(
                `https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Asset/query?page=1&pageSize=2&valueSearch=${search}`,
                {
                    method: 'GET',
                    headers: {
                        Accept: 'application/json',
                        Authorization: `Bearer ${token.token}`,
                        'Content-Type': 'application/json',
                        'Access-Control-Allow-Origin': '*',
                    },
                },
            );

            const data = await response.json();

            if (response.status === 200) {
                setDataList(data.source);
                setTotalPage(data.totalPage);
            }
        } catch (error) {
            console.log('error');
        }

        return null;
    };

    const handleOnChangeEnter = (e) => {
        if (e.key === 'Enter') {
            handleSearch(search);
        }
    };

    const onChange = (e) => {
        setSearch(e.target.value);
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
                        <Form.Control onKeyUp={handleOnChangeEnter} />

                        <InputGroup.Text>
                            <button
                                className={cx('input')}
                                onClick={handleSearch}
                                value={search}
                                onChange={onChange}
                                onKeyUp={handleOnChangeEnter}
                            >
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
                            <Form.Check
                                type={'checkbox'}
                                label={`Assigned`}
                                id={`assigned`}
                                onChange={(e) => handleChangeCheckboxState(e, 'assigned')}
                                checked={checkedState.assigned}
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
                <div className={cx('dropdown')} style={{ marginLeft: 398, width: 198 }} ref={ref}>
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
                                        <div> Category</div>
                                        <button className={cx('triagle')} onClick={handleIsCategory}>
                                            {isCategory ? <GoTriangleUp /> : <GoTriangleDown />}
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

                    {dataList.length > 0
                        ? dataList.map((item) => (
                              <tbody key={item.id}>
                                  <tr>
                                      <td>{item.assetCode}</td>
                                      <td>{item.assetName}</td>
                                      <td>{item.category.name}</td>
                                      <td>{item.state}</td>

                                      <td>
                                          <div className={cx('actions')}>
                                              <button
                                                  className={cx('pen')}
                                                  disabled={false}
                                                  onClick={() => navigateToEditAsset(item.id)}
                                              >
                                                  <BsFillPencilFill />
                                              </button>
                                              <button
                                                  className={cx('delete')}
                                                  disabled={false}
                                                  onClick={() => showModalDelete(item.id)}
                                              >
                                                  <TiDeleteOutline />
                                              </button>
                                          </div>
                                      </td>
                                  </tr>
                              </tbody>
                          ))
                        : null}
                </Table>
            </div>

            <div className={cx('paging')}>
                <Pagination>
                    <Pagination.Item disabled={currentPage === 1} onClick={() => handlePage(currentPage - 1)}>
                        Previous
                    </Pagination.Item>
                    {[...Array(totalPage)].map((_, pageNumberIndex) => (
                        <Pagination.Item
                            key={pageNumberIndex}
                            onClick={() => handlePage(pageNumberIndex + 1)}
                            active={pageNumberIndex + 1 === currentPage}
                        >
                            {pageNumberIndex + 1}
                        </Pagination.Item>
                    ))}
                    <Pagination.Item
                        disabled={totalPage && totalPage === currentPage}
                        onClick={() => handlePage(currentPage + 1)}
                    >
                        Next
                    </Pagination.Item>
                </Pagination>
            </div>

            <Modal show={showDelete}>
                <Modal.Header>
                    <Modal.Title>Are you sure?</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Label>Do you want to delete asset?</Form.Label>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="outline-secondary" onClick={handleCloseDelete}>
                        Close
                    </Button>
                    <Button variant="danger" onClick={() => handleDelete(activeDeleteId)}>
                        Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

export default Asset;

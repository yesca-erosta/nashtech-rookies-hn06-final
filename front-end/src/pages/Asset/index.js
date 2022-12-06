import classNames from 'classnames/bind';
import { useEffect, useRef, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import styles from './asset.module.scss';

import { faPen, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Col, Form, InputGroup, Modal, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { BsSearch } from 'react-icons/bs';
import { FaFilter } from 'react-icons/fa';
import ReactPaginate from 'react-paginate';
import { deleteData, getAllData, getAllDataWithFilterBox, getOneData } from '../../apiServices';
import { ASSET } from '../../constants';
import { queryToStringForAsset } from '../../lib/helper';
import { DetailAsset } from './DetailAsset/DetailAsset';

const cx = classNames.bind(styles);

export const convertStatetoStr = (state) => {
    switch (state) {
        case 0:
            return 'Not Available';
        case 1:
            return 'Available';
        case 2:
            return 'Waiting For Recycling';
        case 3:
            return 'Recycled';
        case 4:
            return 'Assigned';
        default:
            break;
    }
};

function Asset() {
    const ref = useRef();
    const [loading, setLoading] = useState(false);
    const [showState, setShowState] = useState(false);
    const [showCategory, setShowCategory] = useState(false);
    const [placeholderState, setPlaceholderState] = useState('State');

    const [showDelete, setShowDelete] = useState(false);

    const [assetId, setAssetId] = useState('');

    const handleShowDelete = (e, asset) => {
        if (asset.state === 4) {
            e.preventDefault();
        } else {
            setAssetId(asset.id);
            setShowDelete(true);
        }
    };

    const [search, setSearch] = useState();
    const [assetsHoan, setAssetsHoan] = useState([]);
    const [totalPageHoan, setTotalPageHoan] = useState();
    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        sort: 'AssetCodeAcsending',
        states: '0,1,4',
    });

    let navigate = useNavigate();

    useEffect(() => {
        const initSelectState = { available: false, notAvailable: false, assigned: false };

        const checkIfClickedOutside = async (e) => {
            if (showState && ref.current && !ref.current.contains(e.target)) {
                setLoading(true);
                setCheckedStateHoan(initSelectState);
                setPlaceholderState('State');
                setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' });
                const data = await getAllDataWithFilterBox(
                    `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' }),
                );
                setTotalPageHoan(data.totalRecord);
                setAssetsHoan(data.source);
                setShowState(false);
                setLoading(false);
            }
            if (showCategory && ref.current && !ref.current.contains(e.target)) {
                setLoading(true);
                setCheckedCategoryHoan(categories?.map((v) => ({ ...v, isChecked: false })));
                setQueryParams({ ...queryParams, page: 1, pageSize: 10, category: '' });
                const data = await getAllDataWithFilterBox(
                    `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, category: '' }),
                );
                setTotalPageHoan(data.totalRecord);
                setAssetsHoan(data.source);
                setShowCategory(false);
                setPlacehoderCategory([]);
                setLoading(false);
            }
        };
        document.addEventListener('mousedown', checkIfClickedOutside);

        return () => {
            document.removeEventListener('mousedown', checkIfClickedOutside);
        };
        // I dont want render when categories changed
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [showState, showCategory, queryParams]);

    const navigateToCreateAsset = () => {
        navigate('createnewasset');
    };

    const handleState = () => {
        setShowState((pre) => !pre);
    };
    const handleCategory = () => {
        setShowCategory(true);
    };

    const [checkedStateHoan, setCheckedStateHoan] = useState({ available: false, notAvailable: false, assigned: false });

    const handleChangeCheckboxHoan = (e) => {
        setCheckedStateHoan({ ...checkedStateHoan, [e.target.name]: e.target.checked });
    };

    const handleOkState = async () => {
        setLoading(true);
        let data = await getAllDataWithFilterBox(
            `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' }),
        );

        if (checkedStateHoan.available) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '1' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '1' }),
            );
            setPlaceholderState('Available');
        }
        if (checkedStateHoan.notAvailable) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0' }),
            );
            setPlaceholderState('Not available');
        }
        if (checkedStateHoan.assigned) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '4' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '4' }),
            );

            setPlaceholderState('Assigned');
        }

        if (checkedStateHoan.available && checkedStateHoan.notAvailable) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0,1' }),
            );

            setPlaceholderState('Available, Not available');
        }
        if (checkedStateHoan.available && checkedStateHoan.assigned) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '1, 4' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '1, 4' }),
            );

            setPlaceholderState('Available, Assigned');
        }
        if (checkedStateHoan.notAvailable && checkedStateHoan.assigned) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0, 4' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0, 4' }),
            );
            setPlaceholderState('Not available, Assigned');
        }

        if (checkedStateHoan.available && checkedStateHoan.notAvailable && checkedStateHoan.assigned) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' });

            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' }),
            );

            setPlaceholderState('All');
        }

        setTotalPageHoan(data.totalRecord);
        setAssetsHoan(data.source);
        setShowState(false);
        setLoading(false);
    };

    const handleCancelState = async () => {
        setLoading(true);
        setQueryParams({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' });
        const data = await getAllDataWithFilterBox(
            `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, states: '0,1,4' }),
        );
        setPlaceholderState('State');
        setTotalPageHoan(data.totalRecord);
        setAssetsHoan(data.source);
        setCheckedStateHoan({ available: false, notAvailable: false, assigned: false });
        setShowState(false);
        setLoading(false);
    };

    // Category
    const [categories, setCategories] = useState([]);

    const getDataCategory = async () => {
        const data = await getAllData('Category');
        setCategories(data);
    };

    useEffect(() => {
        getDataCategory();
    }, []);

    const [listCategories, setListCategories] = useState([]);

    const [checkedCategoryHoan, setCheckedCategoryHoan] = useState();

    const [placehoderCategory, setPlacehoderCategory] = useState([]);

    const handleOkCategory = async () => {
        setLoading(true);
        let string = '';
        for (let index = 0; index < listCategories.length; index++) {
            string += listCategories[index] + ',';
        }

        setQueryParams({ ...queryParams, page: 1, pageSize: 10, category: string });

        const data = await getAllDataWithFilterBox(
            `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, category: string }),
        );

        setTotalPageHoan(data.totalRecord);
        setAssetsHoan(data.source);

        // Handle placehoderCategory
        // eslint-disable-next-line array-callback-return
        checkedCategoryHoan?.map((item) => {
            if (item.isChecked === true) {
                setPlacehoderCategory((pre) => (pre.includes(item.name) ? [...pre] : [...pre, item.name]));
            } else {
                setPlacehoderCategory((pre) => (pre.includes(item.name) ? pre.filter((x) => x !== item.name) : [...pre]));
            }
        });

        setShowCategory(false);
        setLoading(false);
    };

    useEffect(() => {
        if (categories) {
            setCheckedCategoryHoan(categories?.map((v) => ({ ...v, isChecked: false })));
        }
    }, [categories]);

    const handleChangeCheckboxCategory = (e) => {
        setCheckedCategoryHoan(
            checkedCategoryHoan.map((x) => {
                if (x.id === e.target.id)
                    return {
                        ...x,
                        isChecked: e.target.checked,
                    };
                return x;
            }),
        );

        setListCategories(
            listCategories.includes(e.target?.value)
                ? listCategories.filter((x) => x !== e.target?.value)
                : [...listCategories, e.target?.value],
        );
    };

    const handleCancelCategory = async () => {
        setLoading(true);
        setCheckedCategoryHoan(categories?.map((v) => ({ ...v, isChecked: false })));
        setQueryParams({ ...queryParams, page: 1, pageSize: 10, category: '' });
        const data = await getAllDataWithFilterBox(
            `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, category: '' }),
        );
        setTotalPageHoan(data.totalRecord);
        setAssetsHoan(data.source);
        setShowCategory(false);
        setPlacehoderCategory([]);
        setLoading(false);
    };

    const handleSearch = async (value) => {
        setLoading(true);
        setSearch(value);

        let data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        if (value) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams(queryParams);
            data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        }
        setTotalPageHoan(data.totalRecord);
        setAssetsHoan(data.source);
        setLoading(false);
    };

    const handleOnChangeEnter = (e) => {
        if (e.key === 'Enter') {
            handleSearch(search);
        }
    };

    /// Hoan

    const [showDetail, setShowDetail] = useState(false);
    const [assetDetail, setAssetDetails] = useState('');

    const handleCloseDetail = () => {
        setShowDetail(false);
    };

    // Get Data
    const getData = async () => {
        setLoading(true);
        const data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        setAssetsHoan(data.source);
        setLoading(false);
    };

    useEffect(() => {
        getData();
        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [isShowModalCantDelete, setIsShowModalCantDelete] = useState(false);

    const handleDelete = async () => {
        setLoading(true);
        const res = await deleteData(ASSET, assetId);

        if (res.code === 'ERR_BAD_REQUEST') {
            setIsShowModalCantDelete(true);
        }

        getData();
        setAssetId('');
        setShowDelete(false);
        setLoading(false);
    };

    const handleShowDetail = (assetCode) => {
        setShowDetail(true);
        setAssetDetails(assetsHoan.find((c) => c.assetCode === assetCode));
    };

    const handleClickEdit = async (e, asset) => {
        const res = await getOneData(ASSET, asset.id);

        if (res.state === 4) {
            e.preventDefault();
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
            cell: (row) => {
                return <Link onClick={() => handleShowDetail(row.assetCode)}>{row.assetName}</Link>;
            },
        },
        {
            name: 'Category',
            sortable: true,
            selector: (row) => row.category?.name,
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
                    onClick={(e) => handleClickEdit(e, row)}
                    to={row.state === 4 ? `.` : `./editasset`}
                    key={row.assetCode}
                    state={{ asset: row }}
                    className={styles.customPen}
                    style={
                        row.state === 4
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '13px' }
                            : { color: 'rgb(102, 101, 101)' }
                    }
                >
                    <FontAwesomeIcon icon={faPen} />
                </Link>,
                <Link
                    key={`keyDelete_${row.assetCode}`}
                    to={'#'}
                    style={
                        row.state === 4
                            ? { cursor: 'default', color: '#b7b7b7', fontSize: '1.5em', marginLeft: '10px' }
                            : { cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }
                    }
                >
                    <FontAwesomeIcon icon={faRemove} onClick={(e) => handleShowDelete(e, row)} />
                </Link>,
            ],
        },
    ];

    const [selectedPage, setSelectedPage] = useState(1);

    const fetchAssets = async (page) => {
        setLoading(true);

        setQueryParams({ ...queryParams, page: page, pageSize: 10 });

        const data = await getAllDataWithFilterBox(
            `Asset/query` + queryToStringForAsset({ ...queryParams, page: page, pageSize: 10 }),
        );

        setAssetsHoan(data.source);
        setTotalPageHoan(data.totalRecord);
        setLoading(false);
    };

    useEffect(() => {
        fetchAssets(1); // fetch page 1 of Assets
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [perPage, setPerPage] = useState(10);

    const handlePageClick = async (event) => {
        setLoading(true);
        setSelectedPage(event.selected + 1);
        setQueryParams({ ...queryParams, page: event.selected + 1, pageSize: 10 });

        const data = await getAllDataWithFilterBox(
            `Asset/query` + queryToStringForAsset({ ...queryParams, page: event.selected + 1, pageSize: 10 }),
        );

        setTotalPageHoan(data.totalRecord);
        setAssetsHoan(data.source);
        setPerPage(10);
        setLoading(false);
    };

    const msgNoData = () => {
        if (loading) {
            return (
                <div style={{ fontSize: '24px', textAlign: '-webkit-center', fontWeight: 'bold', padding: '24px' }}>
                    Loading...
                </div>
            );
        } else {
            return <div style={{ marginTop: '30px', textAlign: '-webkit-center' }}>There are no records to display</div>;
        }
    };

    const CustomPagination = (e) => {
        const count = Math.ceil(totalPageHoan / perPage);
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
        if (column.id === 1) {
            return 'AssetCode';
        }
        if (column.id === 2) {
            return 'AssetName';
        }
        if (column.id === 3) {
            return 'AssetCategoryName';
        }
        if (column.id === 4) {
            return 'AssetState';
        }
        return 'AssetCode';
    };

    const getDataSort = async (column, sortDirection) => {
        if (sortDirection === 'asc') {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Acsending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `${getNameSort(column)}Acsending`,
                    }),
            );
            setAssetsHoan(data.source);
            setPerPage(10);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `${getNameSort(column)}Descending`,
                    }),
            );
            setAssetsHoan(data.source);
            setPerPage(10);
        }
    };

    const handleSort = async (column, sortDirection) => {
        await getDataSort(column, sortDirection);

        setSelectedPage(1);
    };

    return (
        <div className={cx('main tableMain')}>
            <h4 className={cx('tableTitle')}>Asset List</h4>
            <div className={cx('tableExtension')}>
                <div className={cx('filterbox')}>
                    <div>
                        <InputGroup>
                            <Form.Control placeholder={placeholderState} readOnly />

                            <InputGroup.Text>
                                <button className={cx('input-state')} onClick={handleState}>
                                    <FaFilter />
                                </button>
                            </InputGroup.Text>
                        </InputGroup>
                    </div>
                    <div className={cx('filter_category')}>
                        <InputGroup>
                            <Form.Control
                                placeholder={
                                    placehoderCategory?.length === 0
                                        ? 'Category'
                                        : placehoderCategory?.length === checkedCategoryHoan?.length
                                        ? 'All'
                                        : placehoderCategory
                                }
                                readOnly
                            />

                            <InputGroup.Text>
                                <button className={cx('input-category')} onClick={handleCategory}>
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
                                        label={`Available`}
                                        id={`available`}
                                        value={1}
                                        name="available"
                                        onChange={handleChangeCheckboxHoan}
                                        checked={checkedStateHoan.available}
                                    />
                                    <Form.Check
                                        type={'checkbox'}
                                        label={`Not available`}
                                        id={`notAvailable`}
                                        value={0}
                                        name="notAvailable"
                                        onChange={handleChangeCheckboxHoan}
                                        checked={checkedStateHoan.notAvailable}
                                    />
                                    <Form.Check
                                        type={'checkbox'}
                                        label={`Assigned`}
                                        id={`assigned`}
                                        value={4}
                                        name="assigned"
                                        onChange={handleChangeCheckboxHoan}
                                        checked={checkedStateHoan.assigned}
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

                    {showCategory && (
                        <div className={cx('dropdown')} style={{ marginLeft: 374, width: 198 }} ref={ref}>
                            <div className={cx('dropdown_container')}>
                                <div className={cx('dropdown_title')}>Select type(s)</div>
                                {checkedCategoryHoan?.map((category) => (
                                    <div key={category.id}>
                                        <Form.Check
                                            type="checkbox"
                                            name={category.name}
                                            label={category.name}
                                            value={category.id}
                                            id={category.id}
                                            onChange={handleChangeCheckboxCategory}
                                            checked={category.isChecked}
                                        />
                                    </div>
                                ))}

                                <div className={cx('button')}>
                                    <Button
                                        variant="danger"
                                        size="sm"
                                        className={cx('button_ok')}
                                        onClick={handleOkCategory}
                                    >
                                        OK
                                    </Button>
                                    <Button
                                        variant="outline-secondary"
                                        size="sm"
                                        className={cx('button_cancel')}
                                        onClick={handleCancelCategory}
                                    >
                                        Cancel
                                    </Button>
                                </div>
                            </div>
                        </div>
                    )}
                </div>

                <div className={cx('tableExtensionRight')}>
                    <InputGroup>
                        <Form.Control
                            className={cx('value_search')}
                            onKeyUp={handleOnChangeEnter}
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                        />

                        <InputGroup.Text style={{ cursor: 'pointer' }}>
                            <button
                                className={cx('input_search')}
                                onClick={() => handleSearch(search)}
                                onKeyUp={handleOnChangeEnter}
                            >
                                <BsSearch />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>
                <Button variant="danger" onClick={navigateToCreateAsset} style={{ height: 38 }}>
                    Create new asset
                </Button>
            </div>
            {assetsHoan ? (
                <DataTable
                    title="Assets"
                    columns={columns}
                    data={assetsHoan}
                    noHeader
                    defaultSortField="id"
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
            ) : (
                msgNoData()
            )}
            <DetailAsset showDetail={showDetail} assetDetail={assetDetail} handleCloseDetail={handleCloseDetail} />

            {/* TODO: handle modal delete */}
            {/* <ModalDelete showDetail={showDetail} assetDetail={assetDetail} handleCloseDetail={handleCloseDetail} /> */}

            <Modal show={showDelete} onHide={() => setShowDelete(false)}>
                <Modal.Header>
                    <Modal.Title>Are you sure?</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Label>Do you want to delete this asset?</Form.Label>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="danger" onClick={handleDelete}>
                        Delete
                    </Button>
                    <Button variant="outline-secondary" onClick={() => setShowDelete(false)}>
                        Cancel
                    </Button>
                </Modal.Footer>
            </Modal>

            <Modal
                show={isShowModalCantDelete}
                onHide={() => {
                    setIsShowModalCantDelete(false);
                }}
            >
                <Modal.Header closeButton>
                    {isShowModalCantDelete && <Modal.Title>Cannot Delete Asset</Modal.Title>}
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        {isShowModalCantDelete && (
                            <Form.Label>
                                Cannot delete the asset because it belongs to one or more historical assignments
                            </Form.Label>
                        )}

                        {/* TODO: */}
                        <Form.Label>If the asset is not able to be used anymore, please update ...</Form.Label>
                    </Form>
                </Modal.Body>
            </Modal>
        </div>
    );
}

export default Asset;

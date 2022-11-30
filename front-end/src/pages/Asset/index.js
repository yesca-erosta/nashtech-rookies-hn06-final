import classNames from 'classnames/bind';
import { useEffect, useRef, useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useAppContext } from '../../context/RequiredAuth/authContext';
import styles from './asset.module.scss';

import { faPen, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Col, Form, InputGroup, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { BsSearch } from 'react-icons/bs';
import { FaFilter } from 'react-icons/fa';
import ReactPaginate from 'react-paginate';
import { getAllDataWithFilterBox } from '../../apiServices';
import { queryToStringForAsset } from '../../lib/helper';

const cx = classNames.bind(styles);

function Asset() {
  const { newAsset, setNewAsset } = useAppContext();
  const ref = useRef();
  const [checkedState, setCheckedState] = useState({ available: false, notAvailable: false, assigned: false });
  const [checkedCategory, setCheckedCategory] = useState({ laptop: false, monitor: false, personalComputer: false });
  const [showState, setShowState] = useState(false);
  const [showCategory, setShowCategory] = useState(false);
  const [placeholderState, setPlaceholderState] = useState('State');
  const [placeholderCategory, setPlaceholderCategory] = useState('Category');

  const [search, setSearch] = useState();
  const [currentPage, setCurrentPage] = useState(1);
  const [assets, setAssets] = useState([]);
  const [assetsHoan, setAssetsHoan] = useState([]);
  const [totalPageHoan, setTotalPageHoan] = useState();
  const [queryParams, setQueryParams] = useState({
    page: 1,
    pageSize: 10,
    sort: 'AssetCodeAcsending',
  });

  let location = useLocation();
  let navigate = useNavigate();

  useEffect(() => {
    if (newAsset) {
      setAssets((prevAsset) => [newAsset, ...prevAsset]);
      setNewAsset(null);
    }
  }, [newAsset, setNewAsset]);

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

  useEffect(() => {
    setAssetsHoan(assets);
  }, [assets, currentPage]);

  useEffect(() => {
    const currentSearchPage = location.search?.slice(-1);
    if (currentSearchPage && Number(currentSearchPage)) {
      setCurrentPage(Number(currentSearchPage));
    }
  }, [currentPage, location.pathname, location.search]);

  const handleSearch = async (value) => {
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
  };

  const handleOnChangeEnter = (e) => {
    if (e.key === 'Enter') {
      handleSearch(search);
    }
  };

  const convertStatetoStr = (state) => {
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

  /// Hoan

  const [showDetail, setShowDetail] = useState(false);
  const [assetDetails, setAssetDetails] = useState('');

  const handleShowDetail = (assetCode) => {
    setShowDetail(true);
    setAssetDetails(assetsHoan.find((c) => c.assetCode === assetCode));
  };

  const handleClickEdit = (e, state) => {
    if (state === 4) {
      e.preventDefault();
    } else {
      console.log('abc');
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
      selector: (row) => row.category.name,
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
          onClick={(e) => handleClickEdit(e, row.state)}
          key={row.assetCode}
          state={{ user: row }}
          className={styles.customPen}
        >
          <FontAwesomeIcon icon={faPen} />
        </Link>,
        <Link
          key={`keyDelete_${row.assetCode}`}
          to={'#'}
          style={{ cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }}
        >
          <FontAwesomeIcon icon={faRemove} onClick={() => console.log('delete')} />
        </Link>,
      ],
    },
  ];

  const [selectedPage, setSelectedPage] = useState(1);

  const fetchUsers = async (page) => {
    setQueryParams({ ...queryParams, page: page, pageSize: 10 });

    const data = await getAllDataWithFilterBox(
      `Asset/query` + queryToStringForAsset({ ...queryParams, page: page, pageSize: 10 }),
    );

    console.log('data', data);

    setAssetsHoan(data.source);
    setTotalPageHoan(data.totalRecord);
  };

  useEffect(() => {
    fetchUsers(1); // fetch page 1 of users
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const [perPage, setPerPage] = useState(10);

  const handlePageClick = async (event) => {
    setSelectedPage(event.selected + 1);
    setQueryParams({ ...queryParams, page: event.selected + 1, pageSize: 10 });

    const data = await getAllDataWithFilterBox(
      `Asset/query` + queryToStringForAsset({ ...queryParams, page: event.selected + 1, pageSize: 10 }),
    );

    setTotalPageHoan(data.totalRecord);
    setAssetsHoan(data.source);
    setPerPage(10);
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
          queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Acsending` }),
      );
      setAssetsHoan(data.source);
      setPerPage(10);
    } else {
      setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` });

      const data = await getAllDataWithFilterBox(
        `Asset/query` +
          queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, sort: `${getNameSort(column)}Descending` }),
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
    <div className="main tableMain">
      <h1 className="tableTitle">User List</h1>
      <div className="tableExtension">
        <div className="tableExtensionLeft">
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
          </div>
        </div>

        <div className="tableExtensionRight">
          <div>
            <InputGroup>
              <Form.Control onKeyUp={handleOnChangeEnter} value={search} onChange={(e) => setSearch(e.target.value)} />
              <InputGroup.Text>
                <button className={cx('input')} onClick={() => handleSearch(search)} onKeyUp={handleOnChangeEnter}>
                  <BsSearch />
                </button>
              </InputGroup.Text>
            </InputGroup>
          </div>
          <Button variant="danger" onClick={navigateToCreateAsset}>
            Create new asset
          </Button>
        </div>
      </div>

      {assetsHoan ? (
        <DataTable
          title="Users"
          columns={columns}
          data={assetsHoan}
          noHeader
          defaultSortField="id"
          defaultSortAsc={true}
          highlightOnHover
          noDataComponent={'There are no records to display'}
          dense
          //   progressPending={loading}
          pagination
          paginationComponent={CustomPagination}
          paginationServer
          sortServer
          onSort={handleSort}
        />
      ) : (
        <div>Deo co du lieu</div>
      )}
    </div>
  );
}

export default Asset;

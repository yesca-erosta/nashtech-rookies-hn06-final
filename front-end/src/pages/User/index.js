import { faPen, faRemove } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useEffect, useState } from 'react';
import DataTable from 'react-data-table-component';
import { Link } from 'react-router-dom';
import { getAllData, getAllDataWithFilterBox } from '../../apiServices';
import { dateStrToStr, queryToString } from '../../lib/helper';
import { ButtonCreate } from './ButtonCreate/ButtonCreate';
import { ModalDetails } from './ModalDetails/ModalDetails';
import { SearchUser } from './SearchUser/SearchUser';
import { TypeFilter } from './TypeFilter/TypeFilter';
import styles from './User.module.scss';

function User() {
  const [show, setShow] = useState(false);

  const [userDetails, setUserDetails] = useState('');
  const [dataState, setDataState] = useState([]);
  const [searchValue, setSearchValue] = useState('');
  const [queryParams, setQueryParams] = useState({
    page: 1,
    pageSize: 10,
    types: [0, 1],
    sort: 'StaffCodeAcsending',
  });
  const [loading, setLoading] = useState(false);
  const [totalRows, setTotalRows] = useState(0);
  const [perPage, setPerPage] = useState(10);

  const handleClose = () => {
    setShow(false);
    setUserDetails('');
  };

  const handleShow = (staffCode) => {
    setShow(true);
    setUserDetails(dataState.find((c) => c.staffCode === staffCode));
  };

  // Get Data
  const getData = async () => {
    const data = await getAllData('User');
    setDataState(data);
  };

  useEffect(() => {
    getData();
  }, []);

  const columns = [
    {
      name: 'Staff Code',
      selector: (row) => row.staffCode,
      sortable: true,
    },
    {
      name: 'Full Name',
      selector: (row) => row.fullName,
      sortable: true,
      cell: (row) => {
        return <Link onClick={() => handleShow(row.staffCode)}>{row.fullName}</Link>;
      },
    },
    {
      name: 'Username',
      selector: (row) => row.userName,
    },
    {
      name: 'Joined Date',
      selector: (row) => row.joinedDate,
      sortable: true,
      cell: (row) => {
        return <div>{dateStrToStr(row.joinedDate)}</div>;
      },
    },
    {
      name: 'Type',
      selector: (row) => row.type,
      sortable: true,
    },
    {
      name: 'Action',
      selector: (row) => row.null,
      cell: (row) => [
        <Link key={row.staffCode} to={`#`} className={styles.customPen}>
          <FontAwesomeIcon icon={faPen} />
        </Link>,
        <Link
          key={`keyDelete_${row.staffCode}`}
          to={'#'}
          style={{ cursor: 'pointer', color: 'red', fontSize: '1.5em', marginLeft: '10px' }}
        >
          <FontAwesomeIcon icon={faRemove} />
        </Link>,
      ],
    },
  ];

  // Search
  const onSearch = async (value) => {
    setSearchValue(value);

    let data = await getAllDataWithFilterBox(`User/query` + queryToString(queryParams));
    if (value) {
      setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
      data = await getAllDataWithFilterBox(
        `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
      );
    } else {
      delete queryParams.valueSearch;
      setQueryParams(queryParams);
      data = await getAllDataWithFilterBox(`User/query` + queryToString(queryParams));
    }

    setDataState(data.source);
  };

  // Paging
  const fetchUsers = async (page) => {
    setLoading(true);
    setQueryParams({ ...queryParams, page: page, pageSize: perPage });

    const data = await getAllDataWithFilterBox(
      `User/query` + queryToString({ ...queryParams, page: page, pageSize: perPage }),
    );

    setDataState(data.source);

    setTotalRows(data.totalRecord);
    setLoading(false);
  };

  const handlePageChange = (page) => {
    fetchUsers(page);
  };

  const handlePerRowsChange = async (newPerPage, page) => {
    setLoading(true);
    setQueryParams({ ...queryParams, page: page, pageSize: newPerPage });

    const data = await getAllDataWithFilterBox(
      `User/query` + queryToString({ ...queryParams, page: page, pageSize: newPerPage }),
    );

    setDataState(data.source);
    setPerPage(newPerPage);
    setLoading(false);
  };

  useEffect(() => {
    fetchUsers(1); // fetch page 1 of users
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div className="main tableMain">
      <h1 className="tableTitle">User List</h1>
      <div className="tableExtension">
        <div className="tableExtensionLeft">
          <TypeFilter
            setDataState={setDataState}
            setTotalRows={setTotalRows}
            setQueryParams={setQueryParams}
            queryParams={queryParams}
          />
        </div>

        <div className="tableExtensionRight">
          <SearchUser onSearch={onSearch} searchValue={searchValue} />
          <ButtonCreate />
        </div>
      </div>

      {dataState ? (
        <DataTable
          title="Users"
          columns={columns}
          data={dataState}
          noHeader
          defaultSortField="id"
          defaultSortAsc={true}
          highlightOnHover
          dense
          progressPending={loading}
          pagination
          paginationServer
          paginationTotalRows={totalRows ?? totalRows}
          onChangeRowsPerPage={handlePerRowsChange}
          onChangePage={handlePageChange}
        />
      ) : (
        <div style={{ marginTop: '30px', textAlign: '-webkit-center' }}>There are no records to display</div>
      )}

      <ModalDetails userDetails={userDetails} handleClose={handleClose} show={show} />
    </div>
  );
}

export default User;

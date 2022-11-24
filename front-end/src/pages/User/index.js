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

  const handleClose = () => {
    setShow(false);
    setUserDetails('');
  };
  const handleShow = (staffCode) => {
    setShow(true);
    setUserDetails(dataState.find((c) => c.staffCode === staffCode));
  };

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

  const [searchValue, setSearchValue] = useState('');

  const onSearch = async (value) => {
    setSearchValue(value);
    let data = await getAllData(`User`);
    if (value) {
      data = await getAllDataWithFilterBox(`User/query` + queryToString({ valueSearch: value }));
    } else {
      data = await getAllData(`User`);
    }
    setDataState(data);
  };

  console.log('data', dataState);

  return (
    <div className="main tableMain">
      <h1 className="tableTitle">User List</h1>
      <div className="tableExtension">
        <div className="tableExtensionLeft">
          <TypeFilter setDataState={setDataState} />
        </div>

        <div className="tableExtensionRight">
          <SearchUser onSearch={onSearch} searchValue={searchValue} />
          <ButtonCreate />
        </div>
      </div>

      <DataTable
        columns={columns}
        data={dataState}
        noHeader
        defaultSortField="id"
        defaultSortAsc={true}
        pagination
        highlightOnHover
        dense
      />

      <ModalDetails userDetails={userDetails} handleClose={handleClose} show={show} />
    </div>
  );
}

export default User;

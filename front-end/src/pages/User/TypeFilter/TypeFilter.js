import { useState } from 'react';
import { Button, Dropdown, DropdownButton, Form, InputGroup } from 'react-bootstrap';
import { getAllDataWithFilterBox } from '../../../apiServices';
import { queryToString } from '../../../lib/helper';
import styles from './TypeFilter.module.scss';

export const TypeFilter = ({ setDataState, setQueryParams, queryParams, setTotalRows, setLoading }) => {
  const [arrChecked, setArrChecked] = useState({ admin: false, staff: false });

  const [showDropdown, setShowDropdown] = useState(false);

  const toggleDropdown = () => {
    setShowDropdown(!showDropdown);
  };

  // TODO: still can't handle cancel
  const onCancelType = async () => {
    setLoading(true);

    setArrChecked({ admin: false, staff: false });
    const data = await getAllDataWithFilterBox(`User/query` + queryToString({ ...queryParams, types: [0, 1] }));
    setTotalRows(data.totalRecord);
    setDataState(data.source);
    setShowDropdown(false);

    setLoading(false);
  };

  const handleChangeCheckbox = (e, type) => {
    setArrChecked({ ...arrChecked, [type]: e.target.checked });
  };

  const onSubmitType = async () => {
    setLoading(true);

    let data = await getAllDataWithFilterBox(
      `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: [0, 1] }),
    );

    if (arrChecked.admin) {
      setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: 1 });

      data = await getAllDataWithFilterBox(
        `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: 1 }),
      );
    }
    if (arrChecked.staff) {
      setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: 0 });

      data = await getAllDataWithFilterBox(
        `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: 0 }),
      );
    }
    if (arrChecked.admin && arrChecked.staff) {
      setQueryParams({ ...queryParams, page: 1, pageSize: 10, types: [0, 1] });
      data = await getAllDataWithFilterBox(
        `User/query` + queryToString({ ...queryParams, page: 1, pageSize: 10, types: [0, 1] }),
      );
    }
    setTotalRows(data.totalRecord);
    setDataState(data.source);
    setShowDropdown(false);

    setLoading(false);
  };

  const displayTitleType = () => {
    if (arrChecked.admin && arrChecked.staff) {
      return 'All';
    }
    if (arrChecked.admin) {
      return 'Admin';
    }
    if (arrChecked.staff) {
      return 'Staff';
    }
    return 'Type';
  };

  return (
    <>
      <Dropdown>
        <DropdownButton
          variant="outline-dark"
          title={displayTitleType()}
          id="dropdown-basic"
          onToggle={toggleDropdown}
          show={showDropdown}
        >
          <div className={styles.menuDropdown}>
            <span className={styles.typeTitle}>Select type(s)</span>
            <InputGroup></InputGroup>
            <Form.Check
              type={'checkbox'}
              id={`admin`}
              label={`Admin`}
              onChange={(e) => handleChangeCheckbox(e, 'admin')}
              checked={arrChecked.admin}
            />
            <Form.Check
              type={'checkbox'}
              id={`staff`}
              label={`Staff`}
              onChange={(e) => handleChangeCheckbox(e, 'staff')}
              checked={arrChecked.staff}
            />
            <div className={styles.wrapButton}>
              <Button variant="danger" size="sm" style={{ minWidth: '60px' }} onClick={onSubmitType}>
                OK
              </Button>
              <Button variant="light" size="sm" className={styles.btnCancel} onClick={onCancelType}>
                Cancel
              </Button>
            </div>
          </div>
        </DropdownButton>
      </Dropdown>
    </>
  );
};

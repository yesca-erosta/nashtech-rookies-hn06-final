import { useState } from 'react';
import { Button, Dropdown, DropdownButton, Form, InputGroup } from 'react-bootstrap';
import { getAllData, getAllDataWithFilterBox } from '../../../apiServices';
import { queryToString } from '../../../lib/helper';
import styles from './TypeFilter.module.scss';

export const TypeFilter = ({ setDataState }) => {
  const [arrChecked, setArrChecked] = useState({ admin: false, staff: false });

  const [showDropdown, setShowDropdown] = useState(false);

  const toggleDropdown = () => {
    setShowDropdown(!showDropdown);
  };

  const onCloseType = () => {
    setArrChecked({ admin: false, staff: false });
    setShowDropdown(false);
  };

  const handleChangeCheckbox = (e, type) => {
    setArrChecked({ ...arrChecked, [type]: e.target.checked });
  };

  const onSubmitType = async () => {
    let data = await getAllData(`User`);
    if (arrChecked.admin) {
      data = await getAllDataWithFilterBox(`User/query` + queryToString({ type: 1 }));
    }
    if (arrChecked.staff) {
      data = await getAllDataWithFilterBox(`User/query` + queryToString({ type: 0 }));
    }
    if (arrChecked.admin && arrChecked.staff) {
      data = await getAllData(`User`);
    }
    
    setDataState(data);
    setShowDropdown(false);
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
              <Button variant="light" size="sm" className={styles.btnCancel} onClick={onCloseType}>
                Cancel
              </Button>
            </div>
          </div>
        </DropdownButton>
      </Dropdown>
    </>
  );
};

import { useState } from 'react';
import { Button, Dropdown, DropdownButton, Form, InputGroup } from 'react-bootstrap';
import styles from './TypeFilter.module.scss';

export const TypeFilter = () => {
  const [arrChecked, setArrChecked] = useState({ admin: false, staff: false });

  const [arrCheckedConfirm, setArrCheckedConfirm] = useState({ admin: false, staff: false });

  const [showDropdown, setShowDropdown] = useState(false);

  const toggleDropdown = () => {
    setShowDropdown(!showDropdown);
  };

  const onCloseType = () => {
    setArrChecked({ admin: false, staff: false });
    setArrCheckedConfirm({ admin: false, staff: false });
    setShowDropdown(false);
  };

  const handleChangeCheckbox = (e, type) => {
    setArrChecked({ ...arrChecked, [type]: e.target.checked });
  };

  const onSubmitType = () => {
    setArrCheckedConfirm(arrChecked);
    setShowDropdown(false);
  };

  return (
    <>
      <Dropdown>
        <DropdownButton
          variant="outline-dark"
          title="Type"
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

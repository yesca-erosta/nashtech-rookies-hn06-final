import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useState } from 'react';
import { Button, Form, InputGroup } from 'react-bootstrap';

export const SearchUser = ({ onSearch }) => {
  const [value, setValue] = useState('');

  const onChange = (e) => {
    setValue(e.target.value);
  };

  const handleOnChangeEnter = (e) => {
    if (e.key === 'Enter') {
      onSearch(value);
    }
  };

  return (
    <InputGroup className="m-3 inputSearch">
      <Form.Control
        id="dhv-page-header-functions-search-input"
        aria-label="Recipient's username"
        aria-describedby="basic-addon2"
        value={value}
        onChange={onChange}
        onKeyUp={handleOnChangeEnter}
      />
      <Button variant="outline-secondary" id="button-addon2" type="submit" onClick={() => onSearch(value)}>
        <FontAwesomeIcon icon={faSearch} />
      </Button>
    </InputGroup>
  );
};

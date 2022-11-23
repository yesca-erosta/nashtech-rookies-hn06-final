import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useState } from 'react';
import { Button, Form, InputGroup } from 'react-bootstrap';

export const SearchUser = () => {
  const [value, setValue] = useState('');

  const onChange = (e) => {
    setValue(e.target.value);
  };

  const onSubmit = () => {
    console.log('value', value);
  };
  return (
    <InputGroup className="m-3">
      <Form.Control
        id="dhv-page-header-functions-search-input"
        aria-label="Recipient's username"
        aria-describedby="basic-addon2"
        value={value}
        onChange={onChange}
      />
      <Button variant="outline-secondary" id="button-addon2" type="submit" onClick={onSubmit}>
        <FontAwesomeIcon icon={faSearch} />
      </Button>
    </InputGroup>
  );
};

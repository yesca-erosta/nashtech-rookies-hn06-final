import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useNavigate } from 'react-router-dom';
import { useAppContext } from '../../../context/RequiredAuth/authContext';
import { dateStrToDate } from '../../../lib/helper';
import styles from '../CreateAsset/createAsset.module.scss';

const cx = classNames.bind(styles);

function CreateAsset() {
  const { token, id } = useAppContext();
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [category, setCategory] = useState('LA');
  const [specification, setSpecification] = useState('');
  const [installed, setInstalled] = useState('');
  const [state, setState] = useState();
  const [disableEdit, setDisableEdit] = useState(true);

  const { getOneAsset } = useAppContext();

  console.log('getOneAsset', getOneAsset);

  useEffect(() => {
    if (getOneAsset) {
      setName(getOneAsset.assetName);
      setCategory(getOneAsset.category.name);
      setSpecification(getOneAsset.specification);
      setInstalled(dateStrToDate(getOneAsset.installedDate));
      setState(parseInt(getOneAsset.state));
    }
  }, [getOneAsset]);

  const onChangeChecked = (e) => {
    setState(parseInt(e.target.value) ?? 0);
  };

  useEffect(() => {
    if (Boolean(name) && Boolean(category) && Boolean(specification) && Boolean(installed) && state !== undefined) {
      setDisableEdit(false);
    } else {
      setDisableEdit(true);
    }
  }, [name, category, specification, installed, state]);

  const handleUpdate = async () => {
    try {
      const response = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Asset`, {
        method: 'PUT',
        body: JSON.stringify({
          Id: id,
          assetName: name,
          categoryId: category,
          specification: specification,
          installedDate: installed,
          state: state,
        }),
        headers: {
          Accept: 'application/json',
          Authorization: `Bearer ${token.token}`,
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
        },
      });

      if (response.status === 200) {
        navigate('/manageasset');
      }
    } catch (error) {
      console.log('error');
    }

    return null;
  };

  return (
    <div className={cx('container')}>
      <h3 className={cx('title')}>Edit Asset</h3>

      <Form className={cx('form')}>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}> Name</Form.Label>
          <Form.Control
            type="text"
            placeholder="Enter name"
            className={cx('input')}
            value={name}
            onChange={(e) => {
              setName(e.target.value);
            }}
          />
        </Form.Group>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Category</Form.Label>
          <Form.Control type="text" className={cx('input')} value={category} disabled readOnly />
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}> Specification</Form.Label>
          <textarea
            cols="40"
            rows="5"
            placeholder="Enter specification"
            className={cx('input-specification')}
            value={specification}
            onChange={(e) => {
              setSpecification(e.target.value);
            }}
          ></textarea>
        </Form.Group>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Installed Date</Form.Label>
          <Form.Control
            type="date"
            onKeyDown={(e) => e.preventDefault()}
            className={cx('input')}
            value={installed}
            onChange={(e) => {
              setInstalled(e.target.value);
            }}
          />
        </Form.Group>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input-state')}>State</Form.Label>

          <div key={`state-radio`} className={cx('input-radio-state')} onChange={onChangeChecked}>
            <Form.Check
              label="Available"
              name="state"
              id={1}
              value={1}
              type="radio"
              defaultChecked={getOneAsset.state === 1}
            />
            <Form.Check
              label="Not available"
              name="state"
              id={0}
              value={0}
              type="radio"
              defaultChecked={getOneAsset.state === 0}
            />
            <Form.Check
              label="Waiting for recycling"
              name="state"
              id={3}
              value={3}
              type="radio"
              defaultChecked={getOneAsset.state === 3}
            />
            <Form.Check
              label="Recycled"
              name="state"
              id={2}
              value={2}
              type="radio"
              defaultChecked={getOneAsset.state === 2}
            />
          </div>
        </Form.Group>
        <div className={cx('button')}>
          <Button variant="danger" onClick={handleUpdate} disabled={disableEdit}>
            Save
          </Button>

          <Button variant="outline-success" className={cx('cancel-button')} href="/manageasset">
            Cancel
          </Button>
        </div>
      </Form>
    </div>
  );
}

export default CreateAsset;

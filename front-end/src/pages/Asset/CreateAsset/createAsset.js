import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from './createAsset.module.scss';
import { useState, useEffect, useMemo } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAppContext } from '../../../context/RequiredAuth/authContext';
import { createData, getAllData } from '../../../apiServices';
import { InputGroup } from 'react-bootstrap';

import { GoTriangleDown } from 'react-icons/go';
import { HiPlusSm } from 'react-icons/hi';
import { ASSET, CATEGORY, USER } from '../../../constants';

const cx = classNames.bind(styles);

function CreateAsset() {
  const [name, setName] = useState('');
  //   const [category, setCategory] = useState('');
  const [specification, setSpecification] = useState('');
  const [installedDate, setInstalledDate] = useState('');
  const [checkbox, setCheckbox] = useState();
  const [disabled, setDisable] = useState(true);
  const [showCategory, setShowCategory] = useState(false);
  const [createCategory, setCreateCategory] = useState(false);
  const [errorCategoryName2, setErrorCategoryName2] = useState(false);
  const [errorCategoryId, setErrorCategoryId] = useState(false);
  const [errorCategoryId3, setErrorCategoryId3] = useState(false);
  const [errorCategoryName, setErrorCategoryName] = useState(false);
  const [disableCategory, setDisableCategory] = useState(true);
  const [categories, setCategories] = useState([]);

  const navigate = useNavigate();

  //   const [categoryName, setCategoryName] = useState('');
  //   const [categoryId, setCategoryId] = useState('');

  const initCategory = { id: '', name: '' };
  const [category, setCategory] = useState(initCategory);

  const [dataAdd, setDataAdd] = useState({
    assetName: '',
    categoryId: '',
    specification: '',
    installedDate: '',
    state: '',
  });

  useEffect(() => {
    if (category.id && category.name) {
      setDataAdd({ ...dataAdd, categoryId: category.id });
    }
  }, [category.id, category.name]);

  const [arrMsg, setArrMsg] = useState({
    AssetName: '',
    CategoryId: '',
    Specification: '',
    InstalledDate: '',
    State: '',
  });

  const handleCreate = async () => {
    const res = await createData(ASSET, dataAdd);

    console.log('res', res);
    if (res.code === 'ERR_BAD_REQUEST') {
      setArrMsg(res?.response?.data?.errors);
      if (res?.response?.data?.errors?.requestModel) {
        alert('Please input all fields');
      }
    } else {
      navigate('/manageasset');
    }
  };

  // const { setNewAsset } = useAppContext();

  // const handleCreate = async () => {
  //     try {
  //         const response = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Asset`, {
  //             method: 'POST',
  //             body: JSON.stringify({
  //                 assetName: name,
  //                 categoryId: categoryName,
  //                 specification: specification,
  //                 installedDate: installedDate,
  //                 state: checkbox,
  //             }),
  //             headers: {
  //                 Accept: 'application/json',
  //                 Authorization: `Bearer ${token.token}`,
  //                 'Content-Type': 'application/json',
  //                 'Access-Control-Allow-Origin': '*',
  //             },
  //         });

  //         const data = await response.json();

  //         if (response.status === 200) {
  //             navigate('/manageasset');
  //             setNewAsset(data);
  //         }

  //         if (!/^[a-z A-Z][a-z A-Z 0-9]+$/.test(name)) {
  //             setErrorAssetName2(true);
  //         }

  //         if (name.length < 6 || name.length > 50) {
  //             setErrorAssetName(true);
  //         }

  //         if (!/^[a-z A-Z][a-z A-Z 0-9]+$/.test(specification)) {
  //             setErrorSpecification2(true);
  //         }

  //         if (specification.length < 6 || specification.length > 50) {
  //             setErrorSpecification(true);
  //         }
  //     } catch (error) {
  //         console.log('error');
  //     }

  //     return null;
  // };

  useEffect(() => {
    if (Boolean(name) && Boolean(category) && Boolean(specification) && Boolean(installedDate) && checkbox !== undefined) {
      setDisable(true);
    } else {
      setDisable(false);
    }
  }, [name, category, specification, installedDate, checkbox]);

  const getDataCategory = async () => {
    const data = await getAllData('Category');
    setCategories(data);
  };

  useEffect(() => {
    getDataCategory();
  }, []);

  const handleShowCategory = () => {
    setShowCategory((pre) => !pre);
  };

  const handleCreateCategory = () => {
    setShowCategory(false);
    setCreateCategory((pre) => !pre);
  };

  useEffect(() => {
    if (category.id && category.name) {
      setDisableCategory(false);
    } else {
      setDisableCategory(true);
    }
  }, [category.id, category.name]);

  const [arrMsgCategory, setArrMsgCategory] = useState('');

  console.log('arrMsgCategory', arrMsgCategory);
  const onCreateCategory = async () => {
    const res = await createData(CATEGORY, createCategoryHoan);

    console.log('res', res);
    if (res.code === 'ERR_BAD_REQUEST') {
      setArrMsgCategory(res?.response?.data);
    } else {
      getDataCategory();
      setDataAdd({ ...dataAdd, categoryId: createCategoryHoan.id });
      setCategory(createCategoryHoan);
      setCreateCategory(false);
      setArrMsgCategory('');
    }
  };

  const [createCategoryHoan, setCreateCategoryHoan] = useState(initCategory);

  const onChangeCategory = (e) => {
    setCreateCategoryHoan({ ...createCategoryHoan, [e.target.name]: e.target.value });
  };

  const handleChangeAdd = (e) => {
    if (e.target.name === 'state') {
      setDataAdd({ ...dataAdd, [e.target.name]: parseInt(e.target.value) });
    } else {
      setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
    }
  };

  const handleCancelCategory = () => {
    setCreateCategory(false);
    setArrMsgCategory('');
  };

  const isInputComplete = useMemo(() => {
    return Object.values(dataAdd).every((x) => x !== null && x !== '');
  }, [dataAdd]);

  return (
    <div className={cx('container')}>
      <h3 className={cx('title')}>Create New Asset</h3>

      <Form className={cx('form')}>
        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Name</Form.Label>
          <Form.Control
            isInvalid={arrMsg.AssetName}
            type="text"
            className={cx('input')}
            placeholder="Enter name"
            name="assetName"
            onChange={handleChangeAdd}
          />
        </Form.Group>
        {arrMsg.AssetName && <p className={cx('msgError')}>{arrMsg.AssetName[0]}</p>}

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Category</Form.Label>
          <InputGroup>
            <Form.Control placeholder={'Category'} value={category.name} />
            <InputGroup.Text
              style={{ backgroundColor: 'transparent', fontSize: 20, cursor: 'pointer' }}
              onClick={handleShowCategory}
            >
              <GoTriangleDown />
            </InputGroup.Text>
          </InputGroup>
        </Form.Group>

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Specification</Form.Label>
          <Form.Group className="w-100" controlId="exampleForm.ControlTextarea1">
            <Form.Control
              isInvalid={arrMsg.Specification}
              type="text"
              name="specification"
              onChange={handleChangeAdd}
              as="textarea"
              className={cx('input-specification')}
              rows={5}
              cols={40}
              placeholder="Enter specification"
            />
          </Form.Group>
        </Form.Group>
        {arrMsg.Specification && <p className={cx('msgError')}>{arrMsg.Specification[0]}</p>}

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input')}>Installed Date</Form.Label>
          <Form.Control
            isInvalid={arrMsg.InstalledDate}
            type="date"
            onKeyDown={(e) => e.preventDefault()}
            className={cx('input')}
            name="installedDate"
            onChange={handleChangeAdd}
          />
        </Form.Group>
        {arrMsg.InstalledDate && <p className={cx('msgError')}>{arrMsg.InstalledDate[0]}</p>}

        <Form.Group className={cx('common-form')}>
          <Form.Label className={cx('title_input-state')}>State</Form.Label>
          <div key={`state-radio`} onChange={handleChangeAdd} className={cx('input-radio-state')}>
            <Form.Check label="Not Available" name="state" type="radio" value={0} id={`state-radio-1`} />
            <Form.Check label="Available" name="state" type="radio" value={1} id={`state-radio-2`} />
          </div>
        </Form.Group>

        <div className={cx('button')}>
          <Button variant="danger" onClick={handleCreate} disabled={!isInputComplete}>
            Save
          </Button>

          <Button variant="outline-success" className={cx('cancel-button')} href="/manageasset">
            Cancel
          </Button>
        </div>
      </Form>

      {showCategory && (
        <div className={cx('container_category')}>
          {categories?.map((item, index) => (
            <div
              className={cx('item')}
              key={index}
              name={'categoryId'}
              onClick={() => {
                setCategory({ id: item.id, name: item.name });
                setShowCategory(false);
              }}
            >
              {item.name}
            </div>
          ))}

          <div className={cx('addNew')} onClick={handleCreateCategory}>
            <div>
              <HiPlusSm style={{ color: 'red', fontSize: 20, marginRight: 6, marginBottom: 3 }} />
            </div>
            <div>Add new category</div>
          </div>
        </div>
      )}

      {createCategory && (
        <div className={cx('container_createCategory')}>
          <div className={cx('container_title')}>Create New Category</div>

          <Form.Group>
            <Form.Label>Category Name:</Form.Label>
            <Form.Control type="text" name="name" placeholder="Enter Category Name" onChange={onChangeCategory} />
          </Form.Group>

          <Form.Group>
            <Form.Label>Category ID:</Form.Label>
            <Form.Control type="text" name="id" placeholder="Enter Category Id" onChange={onChangeCategory} />
          </Form.Group>

          {arrMsgCategory && <p className={cx('msgError')}>{arrMsgCategory}</p>}

          <div className={cx('btn_create-category')}>
            <Button
              variant="danger"
              onClick={onCreateCategory}
              disabled={!createCategoryHoan?.name || !createCategoryHoan?.id}
            >
              Save
            </Button>
            <Button variant="outline-primary" style={{ marginLeft: 20 }} onClick={handleCancelCategory}>
              Cancel
            </Button>
          </div>
        </div>
      )}
    </div>
  );
}

export default CreateAsset;

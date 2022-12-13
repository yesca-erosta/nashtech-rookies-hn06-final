import classNames from 'classnames/bind';
import { useEffect, useMemo, useRef, useState } from 'react';
import { InputGroup } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useNavigate } from 'react-router-dom';
import { createData, getAllData } from '../../../apiServices';
import styles from './createAsset.module.scss';

import { GoTriangleDown } from 'react-icons/go';
import { HiPlusSm } from 'react-icons/hi';
import { ASSET, CATEGORY } from '../../../constants';
import DatePicker from 'react-datepicker';

const cx = classNames.bind(styles);

function CreateAsset() {
    const [showCategory, setShowCategory] = useState(false);
    const [createCategory, setCreateCategory] = useState(false);
    const [categories, setCategories] = useState([]);

    const navigate = useNavigate();

    const initCategory = { id: '', name: '' };
    const [category, setCategory] = useState(initCategory);

    const [loading, setLoading] = useState(false);

    const [dataAdd, setDataAdd] = useState({
        assetName: '',
        categoryId: '',
        specification: '',
        installedDate: new Date(),
        state: '',
    });

    useEffect(() => {
        if (category.id && category.name) {
            setDataAdd({ ...dataAdd, categoryId: category.id });
        }

        // I dont want render when dataAdd changed
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [category.id, category.name]);

    const [arrMsg, setArrMsg] = useState({
        AssetName: '',
        CategoryId: '',
        Specification: '',
        InstalledDate: '',
        State: '',
    });

    const handleCreate = async () => {
        setLoading(true);

        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object dataAdd
        Object.keys(dataAdd).map((k) => (dataAdd[k] = typeof dataAdd[k] == 'string' ? dataAdd[k].trim() : dataAdd[k]));
        const d = new Date(dataAdd.installedDate).toLocaleDateString('fr-CA');

        const { installedDate, ...otherData } = dataAdd;

        otherData.installedDate = d;

        const res = await createData(ASSET, otherData);

        if (res.code === 'ERR_BAD_REQUEST') {
            setArrMsg(res?.response?.data?.errors);
            if (res?.response?.data?.errors?.requestModel) {
                alert('Please input all fields');
            }
        } else {
            navigate('/manageasset');
        }
        setLoading(false);
    };

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

    const [arrMsgCategory, setArrMsgCategory] = useState('');
    const [arrMsgCategoryHoan, setArrMsgCategoryHoan] = useState();

    const onCreateCategory = async () => {
        setArrMsgCategoryHoan();
        setArrMsgCategory('');
        const res = await createData(CATEGORY, createCategoryHoan);

        if (res.code === 'ERR_BAD_REQUEST') {
            if (res?.response?.data?.errors) {
                setArrMsgCategoryHoan(res?.response?.data?.errors);
            } else {
                setArrMsgCategory(res?.response?.data);
            }
        } else {
            getDataCategory();
            setDataAdd({ ...dataAdd, categoryId: createCategoryHoan.id });
            setCategory(createCategoryHoan);
            setCreateCategory(false);
            setArrMsgCategory('');
            setArrMsgCategoryHoan('');
        }
    };

    const [createCategoryHoan, setCreateCategoryHoan] = useState(initCategory);

    const onChangeCategory = (e) => {
        setCreateCategoryHoan({ ...createCategoryHoan, [e.target.name]: e.target.value.trim() });
    };

    const onChangeDate = (date) => {
        setDataAdd({ ...dataAdd, installedDate: date });
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
        setArrMsgCategoryHoan('');
    };

    const isInputComplete = useMemo(() => {
        return Object.values(dataAdd).every((x) => x !== null && x !== '');
    }, [dataAdd]);

    const ref = useRef();

    useEffect(() => {
        const checkIfClickedOutside = (e) => {
            if (showCategory && ref.current && !ref.current.contains(e.target)) {
                setShowCategory(false);
            }
        };
        document.addEventListener('mousedown', checkIfClickedOutside);

        return () => {
            document.removeEventListener('mousedown', checkIfClickedOutside);
        };
    }, [showCategory]);

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New Asset</h3>

            {loading ? (
                <div style={{ fontSize: '24px', textAlign: '-webkit-center', fontWeight: 'bold', padding: '24px' }}>
                    Loading...
                </div>
            ) : (
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
                            value={dataAdd.assetName}
                        />
                    </Form.Group>
                    {arrMsg.AssetName && <p className={cx('msgErrorBg')}>{arrMsg.AssetName[0]}</p>}
                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Category</Form.Label>
                        <InputGroup>
                            <Form.Control
                                placeholder={'Category'}
                                value={category.name}
                                readOnly
                                style={{ cursor: 'pointer' }}
                                onClick={handleShowCategory}
                            />
                            <InputGroup.Text
                                className={cx('input-gr-text-category')}
                                style={{ backgroundColor: 'transparent', fontSize: 20, cursor: 'pointer' }}
                                onClick={handleShowCategory}
                            >
                                <GoTriangleDown />
                            </InputGroup.Text>
                        </InputGroup>
                    </Form.Group>

                    {showCategory && (
                        <div className={cx('container_category')} ref={ref}>
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
                                value={dataAdd.specification}
                            />
                        </Form.Group>
                    </Form.Group>
                    {arrMsg.Specification && <p className={cx('msgErrorBg')}>{arrMsg.Specification[0]}</p>}

                    <Form.Group className={cx('common-form')}>
                        <Form.Label className={cx('title_input')}>Installed Date</Form.Label>

                        <DatePicker
                            name="installedDate"
                            selected={dataAdd.installedDate}
                            className={`${arrMsg.InstalledDate ? 'border-danger' : ''} form-control w-full `}
                            onChange={(date) => onChangeDate(date)}
                            placeholderText="dd/MM/yyyy"
                            dateFormat="dd/MM/yyyy"
                        />
                    </Form.Group>
                    {arrMsg.InstalledDate && <p className={cx('msgErrorBg')}>{arrMsg.InstalledDate[0]}</p>}

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

                        <Button
                            variant="outline-secondary"
                            className={cx('cancel-button')}
                            onClick={() => navigate('/manageasset')}
                        >
                            Cancel
                        </Button>
                    </div>
                </Form>
            )}

            {createCategory && (
                <div className={cx('container_createCategory')}>
                    <div className={cx('container_title')}>Create New Category</div>

                    <Form.Group>
                        <Form.Label>Category Name:</Form.Label>
                        <Form.Control
                            isInvalid={
                                arrMsgCategoryHoan?.Name ||
                                arrMsgCategory === 'Category is already existed. Please enter a different category'
                            }
                            type="text"
                            name="name"
                            placeholder="Enter Category Name"
                            onChange={onChangeCategory}
                        />
                    </Form.Group>
                    {arrMsgCategory === 'Category is already existed. Please enter a different category' && (
                        <p className={cx('msgError')}>{arrMsgCategory}</p>
                    )}
                    {arrMsgCategoryHoan?.Name && <p className={cx('msgError')}>{arrMsgCategoryHoan?.Name[0]}</p>}
                    <Form.Group>
                        <Form.Label>Category ID:</Form.Label>
                        <Form.Control
                            isInvalid={
                                arrMsgCategoryHoan?.Id ||
                                arrMsgCategory === 'CategoryID is already existed. Please enter a different CategoryID'
                            }
                            type="text"
                            name="id"
                            placeholder="Enter Category Id"
                            onChange={onChangeCategory}
                        />
                    </Form.Group>

                    {arrMsgCategory === 'CategoryID is already existed. Please enter a different CategoryID' && (
                        <p className={cx('msgError')}>{arrMsgCategory}</p>
                    )}
                    {!arrMsgCategory && arrMsgCategoryHoan?.Id && (
                        <p className={cx('msgError')}>{arrMsgCategoryHoan?.Id[0]}</p>
                    )}

                    <div className={cx('btn_create-category')}>
                        <Button
                            variant="danger"
                            onClick={onCreateCategory}
                            disabled={!createCategoryHoan?.name || !createCategoryHoan?.id}
                        >
                            Save
                        </Button>
                        <Button variant="outline-secondary" style={{ marginLeft: 20 }} onClick={handleCancelCategory}>
                            Cancel
                        </Button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default CreateAsset;

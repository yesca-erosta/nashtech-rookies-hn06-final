import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from './createAsset.module.scss';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAppContext } from '../../../context/RequiredAuth/authContext';
import { getAllData } from '../../../apiServices';
import { InputGroup } from 'react-bootstrap';

import { GoTriangleDown } from 'react-icons/go';
import { HiPlusSm } from 'react-icons/hi';

const cx = classNames.bind(styles);

function CreateAsset() {
    const [name, setName] = useState('');
    const [category, setCategory] = useState('');
    const [specification, setSpecification] = useState('');
    const [installedDate, setInstalledDate] = useState('');
    const [checkbox, setCheckbox] = useState();
    const [disabled, setDisable] = useState(true);
    const [addCategory, setAddCategory] = useState(false);
    const [createCategory, setCreateCategory] = useState(false);
    const [errorCategoryName2, setErrorCategoryName2] = useState(false);
    const [errorCategoryId, setErrorCategoryId] = useState(false);
    const [errorCategoryId3, setErrorCategoryId3] = useState(false);
    const [errorCategoryName, setErrorCategoryName] = useState(false);
    const [disableCategory, setDisableCategory] = useState(true);
    const [categories, setCategories] = useState([]);

    const [errorAssetName, setErrorAssetName] = useState(false);
    const [errorAssetName2, setErrorAssetName2] = useState(false);
    const [errorAssetName3, setErrorAssetName3] = useState(false);
    const [errorSpecification, setErrorSpecification] = useState(false);
    const [errorSpecification2, setErrorSpecification2] = useState(false);

    const { token } = useAppContext();
    const navigate = useNavigate();

    const [categoryName, setCategoryName] = useState('');
    const [categoryId, setCategoryId] = useState('');

    const handleChecked = (e) => {
        if (e.target.id === '2') {
            setCheckbox(0);
        }

        if (e.target.id === '1') {
            setCheckbox(1);
        }
    };

    const { setNewAsset } = useAppContext();

    const handleCreate = async () => {
        try {
            const response = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Asset`, {
                method: 'POST',
                body: JSON.stringify({
                    assetName: name,
                    categoryId: categoryName,
                    specification: specification,
                    installedDate: installedDate,
                    state: checkbox,
                }),
                headers: {
                    Accept: 'application/json',
                    Authorization: `Bearer ${token.token}`,
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                },
            });

            const data = await response.json();

            if (response.status === 200) {
                navigate('/manageasset');
                setNewAsset(data);
            }

            if (!/^[a-z A-Z][a-z A-Z 0-9]+$/.test(name)) {
                setErrorAssetName2(true);
            }

            if (name.length < 6 || name.length > 50) {
                setErrorAssetName(true);
            }

            if (!/^[a-z A-Z][a-z A-Z 0-9]+$/.test(specification)) {
                setErrorSpecification2(true);
            }

            if (specification.length < 6 || specification.length > 50) {
                setErrorSpecification(true);
            }
        } catch (error) {
            console.log('error');
        }

        return null;
    };

    useEffect(() => {
        if (
            Boolean(name) &&
            Boolean(category) &&
            Boolean(specification) &&
            Boolean(installedDate) &&
            checkbox !== undefined
        ) {
            setDisable(true);
        } else {
            setDisable(false);
        }
    }, [name, category, specification, installedDate, checkbox]);

    const getData = async () => {
        const data = await getAllData('Category');
        setCategories(data);
    };

    useEffect(() => {
        getData();
    }, []);

    const handleAddCategory = () => {
        setAddCategory((pre) => !pre);
    };

    const handleCreateCategory = () => {
        setAddCategory(false);
        setCreateCategory((pre) => !pre);
    };

    useEffect(() => {
        if (categoryId && categoryName) {
            setDisableCategory(false);
        } else {
            setDisableCategory(true);
        }
    }, [categoryId, categoryName]);

    const onCreateCategory = async () => {
        try {
            const response = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Category`, {
                method: 'POST',
                body: JSON.stringify({
                    id: categoryId,
                    name: categoryName,
                }),
                headers: {
                    Accept: 'application/json',
                    Authorization: `Bearer ${token.token}`,
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                },
            });

            const data = await response.json();

            if (response.status === 200) {
                setCreateCategory((pre) => !pre);
                setCategories((prevCategories) => [...prevCategories, data]);
                setCategoryName(data.id);
                setCategory(data.name);
            }

            if (categoryName.length < 1 || categoryName.length > 50) {
                setErrorCategoryName2(true);
            }
            if (!/^[a-z A-Z][a-z A-Z 0-9]+$/.test(categoryName)) {
                setErrorCategoryName(true);
            }

            if (categoryId.length > 8 || categoryId.length < 2) {
                setErrorCategoryId(true);
            }
            if (!/^([A-Z]{0,50})$/.test(categoryId)) {
                setErrorCategoryId3(true);
            }
        } catch (error) {
            console.log('error');
        }

        return null;
    };

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New Asset</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Name</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter name"
                        className={cx('input')}
                        value={name}
                        onFocus={() => {
                            setErrorAssetName(false);
                            setErrorAssetName2(false);
                            setErrorAssetName3(false);
                        }}
                        onChange={(e) => setName(e.target.value)}
                    />
                </Form.Group>
                {errorAssetName && <div className={cx('errorMessage1')}>the asset name should have 6-50 characters!</div>}
                {errorAssetName3 && <div className={cx('errorMessage1')}>the asset name is required!</div>}
                {errorAssetName2 && (
                    <div className={cx('errorMessage1')}>the asset name should contain the alphaber and numeric!</div>
                )}

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Category</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Category'} value={category} />

                        <InputGroup.Text
                            style={{ backgroundColor: 'transparent', fontSize: 20, cursor: 'pointer' }}
                            onClick={handleAddCategory}
                        >
                            <GoTriangleDown />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Specification</Form.Label>
                    <textarea
                        cols="40"
                        rows="5"
                        placeholder="Enter specification"
                        className={cx('input-specification')}
                        value={specification}
                        onFocus={() => {
                            setErrorSpecification(false);
                            setErrorSpecification2(false);
                        }}
                        onChange={(e) => setSpecification(e.target.value)}
                    ></textarea>
                </Form.Group>
                {errorSpecification && (
                    <div className={cx('errorMessage1')}>the specification should have 6-50 characters!</div>
                )}
                {errorSpecification2 && (
                    <div className={cx('errorMessage1')}>the specification should contain the alphaber and numeric!</div>
                )}

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Installed Date</Form.Label>
                    <Form.Control
                        type="date"
                        onKeyDown={(e) => e.preventDefault()}
                        className={cx('input')}
                        value={installedDate}
                        id="date"
                        onChange={(e) => setInstalledDate(e.target.value)}
                    />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input-state')}>State</Form.Label>
                    <div
                        key={`gender-radio`}
                        className={cx('input-radio-state')}
                        onChange={(e) => {
                            handleChecked(e);
                        }}
                    >
                        <Form.Check label="Available" name="btn" id={1} type="radio" />
                        <Form.Check label="Not available" name="btn" id={2} type="radio" />
                    </div>
                </Form.Group>

                <div className={cx('button')}>
                    <Button variant="danger" onClick={handleCreate} disabled={!disabled}>
                        Save
                    </Button>

                    <Button variant="outline-success" className={cx('cancel-button')} href="/manageasset">
                        Cancel
                    </Button>
                </div>
            </Form>

            {addCategory && (
                <div className={cx('container_category')}>
                    {categories?.map((item, index) => (
                        <div
                            className={cx('item')}
                            key={index}
                            name={'categoryId'}
                            onClick={() => {
                                setCategoryName(item.id);
                                setCategory(item.name);
                                setAddCategory(false);
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
                        <Form.Control
                            type="text"
                            placeholder="Enter categoryName"
                            value={categoryName}
                            onFocus={() => {
                                setErrorCategoryName(false);
                                setErrorCategoryName2(false);
                            }}
                            onChange={(e) => setCategoryName(e.target.value)}
                        />
                    </Form.Group>
                    {errorCategoryName && <div className={cx('errorMessage')}>should contain the alphabet and numeric!</div>}
                    {errorCategoryName2 && <div className={cx('errorMessage')}>name should have 1-50 characters!</div>}

                    <Form.Group>
                        <Form.Label> Category ID:</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="Enter categoryId"
                            value={categoryId}
                            onFocus={() => {
                                setErrorCategoryId(false);
                                setErrorCategoryId3(false);
                            }}
                            onChange={(e) => setCategoryId(e.target.value)}
                        />
                    </Form.Group>
                    {errorCategoryId && <div className={cx('errorMessage')}>the Category ID should have 2-8 characters</div>}
                    {errorCategoryId3 && <div className={cx('errorMessage')}>the Category ID is invalid!</div>}

                    <div className={cx('btn_create-category')}>
                        <Button variant="danger" onClick={onCreateCategory} disabled={disableCategory}>
                            Save
                        </Button>
                        <Button
                            variant="outline-primary"
                            style={{ marginLeft: 20 }}
                            onClick={() => setCreateCategory((pre) => !pre)}
                        >
                            Cancel
                        </Button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default CreateAsset;

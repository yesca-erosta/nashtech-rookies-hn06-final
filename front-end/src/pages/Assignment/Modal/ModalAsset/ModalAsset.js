import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import { Button, Form, InputGroup, Modal, Table } from 'react-bootstrap';
import { BsSearch } from 'react-icons/bs';
import { GoTriangleDown, GoTriangleUp } from 'react-icons/go';
import { getAllDataWithFilterBox } from '../../../../apiServices';
import { queryToStringForAsset } from '../../../../lib/helper';
import styles from '../../CreateAssignment/createAssignment.module.scss';

export const ModalAsset = ({ setIsShowListAsset, setAsset }) => {
    const cx = classNames.bind(styles);

    const [dataAsset, setDataAsset] = useState([]);

    console.log({ dataAsset });
    const [queryParams, setQueryParams] = useState({
        page: 1,
        pageSize: 10,
        sort: 'AssetCodeAcsending',
        states: '1',
    });

    // get data asset
    const GetDataAsset = async () => {
        const data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        setDataAsset(data.source);
    };

    useEffect(() => {
        GetDataAsset();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const [isAssetCode, setIsAssetCode] = useState(false);
    const [isAssetName, setIsAssetName] = useState(false);
    const [isCategory, setIsCategory] = useState(false);

    const handleIsAssetCode = async () => {
        setIsAssetCode((pre) => !pre);
        setIsAssetName(false);
        setIsCategory(false);
        if (!isAssetCode) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `AssetCodeDescending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `AssetCodeDescending`,
                    }),
            );
            setDataAsset(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `AssetCodeAcsending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `AssetCodeAcsending`,
                    }),
            );
            setDataAsset(data.source);
        }
    };

    const handleIsAssetName = async () => {
        setIsAssetName((pre) => !pre);
        setIsAssetCode(false);
        setIsCategory(false);

        if (!isAssetName) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `AssetNameDescending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `AssetNameDescending`,
                    }),
            );
            setDataAsset(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `AssetNameAcsending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `AssetNameAcsending`,
                    }),
            );
            setDataAsset(data.source);
        }
    };

    const handleIsCategory = async () => {
        setIsCategory((pre) => !pre);

        setIsAssetName(false);
        setIsAssetCode(false);

        if (!isCategory) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `AssetCategoryNameDescending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `AssetCategoryNameDescending`,
                    }),
            );
            setDataAsset(data.source);
        } else {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, sort: `AssetCategoryNameAcsending` });

            const data = await getAllDataWithFilterBox(
                `Asset/query` +
                    queryToStringForAsset({
                        ...queryParams,
                        page: 1,
                        pageSize: 10,
                        sort: `AssetCategoryNameAcsending`,
                    }),
            );
            setDataAsset(data.source);
        }
    };

    const [dataSelected, setDataSelected] = useState('');

    const onChangeAsset = (e) => {
        setDataSelected(e.target.id);
    };

    const checkNameByAssetCode = (assetCode) => {
        setIsShowListAsset(false);
        const assetSelected = dataAsset?.find((data) => data.assetCode === assetCode);
        setAsset(assetSelected);
    };
    const [search, setSearch] = useState();

    const handleSearch = async (value) => {
        setSearch(value);

        let data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        if (value) {
            setQueryParams({ ...queryParams, page: 1, pageSize: 10, valueSearch: value });
            data = await getAllDataWithFilterBox(
                `Asset/query` + queryToStringForAsset({ ...queryParams, page: 1, pageSize: 10, valueSearch: value }),
            );
        } else {
            delete queryParams.valueSearch;
            setQueryParams(queryParams);
            data = await getAllDataWithFilterBox(`Asset/query` + queryToStringForAsset(queryParams));
        }
        setDataAsset(data.source);
    };

    const handleOnChangeEnter = (e) => {
        if (e.key === 'Enter') {
            handleSearch(search);
        }
    };

    return (
        <div className={cx('table_container')}>
            <div className={cx('header_search')}>
                <h4 className={cx('title_search')}>Select Asset</h4>
                <InputGroup style={{ width: 200 }}>
                    <Form.Control
                        className={cx('input_search')}
                        onKeyUp={handleOnChangeEnter}
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />

                    <InputGroup.Text style={{ cursor: 'pointer' }}>
                        <button
                            className={cx('icon_search')}
                            onClick={() => handleSearch(search)}
                            onKeyUp={handleOnChangeEnter}
                        >
                            <BsSearch style={{ border: 'none' }} />
                        </button>
                    </InputGroup.Text>
                </InputGroup>
            </div>
            <Table>
                <thead>
                    <tr>
                        <th></th>
                        <th>
                            <>
                                <div className={cx('title_table')}>
                                    <div>Asset Code</div>
                                    <button className={cx('triagle')} onClick={handleIsAssetCode}>
                                        {isAssetCode ? <GoTriangleUp /> : <GoTriangleDown />}
                                    </button>
                                </div>
                            </>
                        </th>
                        <th>
                            <>
                                <div className={cx('title_table')}>
                                    <div>Asset Name</div>
                                    <button className={cx('triagle')} onClick={handleIsAssetName}>
                                        {isAssetName ? <GoTriangleUp /> : <GoTriangleDown />}
                                    </button>
                                </div>
                            </>
                        </th>
                        <th>
                            <>
                                <div className={cx('title_table')}>
                                    <div>Category</div>
                                    <button className={cx('triagle')} onClick={handleIsCategory}>
                                        {isCategory ? <GoTriangleUp /> : <GoTriangleDown />}
                                    </button>
                                </div>
                            </>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {dataAsset?.map((item) => (
                        <tr key={item.assetCode}>
                            <td>
                                <Form.Check onChange={onChangeAsset} type="radio" id={item.assetCode} name="assetRadio" />
                            </td>
                            <td>{item.assetCode}</td>
                            <td>{item.assetName}</td>
                            <td>{item.category.name}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal.Footer>
                <Button variant="danger" onClick={() => checkNameByAssetCode(dataSelected)}>
                    Save
                </Button>
                <Button variant="outline-secondary" onClick={() => setIsShowListAsset(false)} style={{ marginLeft: 20 }}>
                    Cancel
                </Button>
            </Modal.Footer>
        </div>
    );
};

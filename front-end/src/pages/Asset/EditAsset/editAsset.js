import classNames from 'classnames/bind';
import { useMemo, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useLocation, useNavigate } from 'react-router-dom';
import { updateData } from '../../../apiServices';
import { ASSET } from '../../../constants';
import { dateStrToDate } from '../../../lib/helper';
import styles from '../CreateAsset/createAsset.module.scss';

const cx = classNames.bind(styles);

function CreateAsset() {
    const location = useLocation();

    const { asset } = location?.state;

    const initAsset = {
        assetName: asset.assetName,
        categoryId: asset.categoryId,
        specification: asset.specification,
        installedDate: asset.installedDate,
        state: asset.state,
    };

    const [data, setData] = useState(initAsset);

    const navigate = useNavigate();

    const onChange = (e) => {
        if (e.target.name === 'state') {
            // value is 99 because if the value is 0, the user cannot click on the text to select it
            setData({ ...data, [e.target.name]: parseInt(e.target.value) === 99 ? 0 : parseInt(e.target.value) });
        } else {
            setData({ ...data, [e.target.name]: e.target.value });
        }
    };

    const [arrMsg, setArrMsg] = useState('');

    const handleUpdate = async () => {
        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object data
        Object.keys(data).map((k) => (data[k] = typeof data[k] == 'string' ? data[k].trim() : data[k]));

        const res = await updateData(`${ASSET}/${asset.id}`, data);

        if (res.code === 'ERR_BAD_REQUEST') {
            if (res?.response?.data?.errors) {
                setArrMsg(res?.response?.data?.errors);
            } else {
                alert(res?.response?.data);
                navigate('/manageasset');
            }
        } else {
            navigate('/manageasset');
        }
    };

    const isInputComplete = useMemo(() => {
        return Object.values(data).every((x) => x !== null && x !== '');
    }, [data]);

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Edit Asset</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Name</Form.Label>
                    <Form.Control
                        isInvalid={arrMsg?.AssetName}
                        type="text"
                        placeholder="Enter name"
                        name="assetName"
                        value={data.assetName}
                        onChange={onChange}
                        className={cx('input')}
                    />
                </Form.Group>
                {arrMsg?.AssetName && <p className={cx('msgErrorEdit')}>{arrMsg?.AssetName[0]}</p>}

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Category</Form.Label>
                    <Form.Control type="text" className={cx('input')} value={asset.category.name} disabled readOnly />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Specification</Form.Label>
                    <Form.Control
                        isInvalid={arrMsg?.Specification}
                        rows={5}
                        cols={40}
                        placeholder="Enter specification"
                        name="specification"
                        onChange={onChange}
                        value={data.specification}
                        className={cx('input-specification')}
                        type="text"
                        as="textarea"
                    />
                </Form.Group>
                {arrMsg?.Specification && <p className={cx('msgErrorEdit')}>{arrMsg?.Specification[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Installed Date</Form.Label>
                    <Form.Control
                        isInvalid={arrMsg?.InstalledDate}
                        type="date"
                        name="installedDate"
                        value={dateStrToDate(data.installedDate)}
                        onChange={onChange}
                        className={cx('input')}
                    />
                </Form.Group>
                {arrMsg?.InstalledDate && <p className={cx('msgErrorEdit')}>{arrMsg?.InstalledDate[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input-state')}>State</Form.Label>

                    <div key={`state-radio`} onChange={onChange} className={cx('input-radio-state')}>
                        <Form.Check
                            label="Available"
                            name="state"
                            id={1}
                            value={1}
                            type="radio"
                            defaultChecked={asset.state === 1}
                        />
                        <Form.Check
                            label="Not available"
                            name="state"
                            id={99}
                            value={99}
                            type="radio"
                            defaultChecked={asset.state === 0}
                        />
                        <Form.Check
                            label="Waiting for recycling"
                            name="state"
                            id={3}
                            value={3}
                            type="radio"
                            defaultChecked={asset.state === 3}
                        />
                        <Form.Check
                            label="Recycled"
                            name="state"
                            id={2}
                            value={2}
                            type="radio"
                            defaultChecked={asset.state === 2}
                        />
                    </div>
                </Form.Group>
                <div className={cx('button')}>
                    <Button variant="danger" onClick={handleUpdate} disabled={!isInputComplete}>
                        Save
                    </Button>

                    <Button
                        variant="outline-secondary"
                        className={cx('cancel-button')}
                        onClick={() => {
                            navigate('/manageasset');
                        }}
                    >
                        Cancel
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default CreateAsset;

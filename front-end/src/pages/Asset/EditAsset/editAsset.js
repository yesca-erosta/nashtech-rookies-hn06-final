import classNames from 'classnames/bind';
import { useMemo, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useLocation, useNavigate } from 'react-router-dom';
import { updateData } from '../../../apiServices';
import { ASSET } from '../../../constants';
import styles from '../CreateAsset/createAsset.module.scss';
import styless from '../../../components/scssAsterisk/assterisk.module.scss';
import DatePicker from 'react-datepicker';
import { Loading } from '../../../components/Loading/Loading';

const cx = classNames.bind(styles);
const cxx = classNames.bind(styless);

function EditAsset() {
    const location = useLocation();

    const { asset } = location?.state;

    const initAsset = {
        assetName: asset.assetName,
        categoryId: asset.categoryId,
        specification: asset.specification,
        installedDate: new Date(asset.installedDate),
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

    const onChangeDate = (date) => {
        setData({ ...data, installedDate: date });
    };

    const [arrMsg, setArrMsg] = useState('');

    const [loading, setLoading] = useState(false);
    const handleUpdate = async () => {
        setLoading(true);
        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object data
        Object.keys(data).map((k) => (data[k] = typeof data[k] == 'string' ? data[k].trim() : data[k]));

        const d = new Date(data.installedDate).toLocaleDateString('fr-CA');

        const { installedDate, ...otherData } = data;

        otherData.installedDate = d;

        const res = await updateData(`${ASSET}/${asset.id}`, otherData);

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
        setLoading(false);
    };

    const isInputComplete = useMemo(() => {
        return Object.values(data).every((x) => x !== null && x !== '');
    }, [data]);

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Edit Asset</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Name {<b className={cxx('asterisk')}>*</b>}</Form.Label>
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
                    <Form.Label className={cx('title_input')}>Category {<b className={cxx('asterisk')}>*</b>}</Form.Label>
                    <Form.Control type="text" className={cx('input')} value={asset.category.name} disabled readOnly />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>
                        Specification {<b className={cxx('asterisk')}>*</b>}
                    </Form.Label>
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
                    <Form.Label className={cx('title_input')}>
                        Installed Date {<b className={cxx('asterisk')}>*</b>}
                    </Form.Label>
                    <DatePicker
                        name="installedDate"
                        selected={data.installedDate}
                        className={`${arrMsg?.InstalledDate ? 'border-danger' : ''} form-control w-full `}
                        onChange={(date) => onChangeDate(date)}
                        placeholderText="dd/MM/yyyy"
                        dateFormat="dd/MM/yyyy"
                    />
                </Form.Group>
                {arrMsg?.InstalledDate && <p className={cx('msgErrorEdit')}>{arrMsg?.InstalledDate[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input-state')}>State {<b className={cxx('asterisk')}>*</b>}</Form.Label>

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

            {loading && <Loading />}
        </div>
    );
}

export default EditAsset;

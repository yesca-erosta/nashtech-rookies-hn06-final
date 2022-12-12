import classNames from 'classnames/bind';
import { useMemo } from 'react';
import { useEffect, useState } from 'react';
import { InputGroup } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { BsSearch } from 'react-icons/bs';
import { useNavigate } from 'react-router-dom';
import { createData } from '../../../apiServices';
import { ASSIGNMENT } from '../../../constants';
import { ModalAsset } from '../Modal/ModalAsset/ModalAsset';
import { ModalUser } from '../Modal/ModalUser/ModalUser';
import styles from './createAssignment.module.scss';
import DatePicker from 'react-datepicker';

const cx = classNames.bind(styles);

function CreateAssignment() {
    const navigate = useNavigate();

    // Date time now
    var dateObj = new Date();
    var month = dateObj.getUTCMonth() + 1; //months from 1-12
    if (month.toString().length === 1) {
        month = `0${month}`;
    }
    var day = dateObj.getUTCDate();
    if (day.toString().length === 1) {
        day = `0${day}`;
    }
    var year = dateObj.getUTCFullYear();

    const newdate = year + '-' + month + '-' + day;

    const [isShowListUser, setIsShowListUser] = useState(false);
    const [isShowListAsset, setIsShowListAsset] = useState(false);
    const [asset, setAsset] = useState({
        id: '',
        assetName: '',
        assetCode: '',
    });

    const [user, setUser] = useState({
        id: '',
        fullName: '',
        staffCode: '',
    });

    const [dataAdd, setDataAdd] = useState({
        assignedToId: '',
        assetId: '',
        assignedDate: new Date(),
        note: '',
    });

    useEffect(() => {
        if (user?.fullName && user?.id) setDataAdd({ ...dataAdd, assignedToId: user?.id });

        // I dont want re-render page when dataAdd change
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [user?.fullName, user?.id]);

    useEffect(() => {
        if (asset?.id && asset?.assetName) setDataAdd({ ...dataAdd, assetId: asset?.id });

        // I dont want re-render page when dataAdd change
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [asset?.assetName, asset?.id]);

    const onChange = (e) => {
        setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
    };

    const onChangeDate = (date) => {
        setDataAdd({ ...dataAdd, assignedDate: date });
    };

    const [arrMsg, setArrMsg] = useState([]);

    const handleCreate = async () => {
        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object dataAdd
        Object.keys(dataAdd).map((k) => (dataAdd[k] = typeof dataAdd[k] == 'string' ? dataAdd[k].trim() : dataAdd[k]));

        const d = new Date(dataAdd.assignedDate).toLocaleDateString('fr-CA');

        const { assignedDate, ...otherData } = dataAdd;

        otherData.assignedDate = d;

        const res = await createData(ASSIGNMENT, otherData);

        if (res.code === 'ERR_BAD_REQUEST') {
            setArrMsg(res?.response?.data?.errors);
            if (res?.response?.status === 409) {
                setArrMsg({ Asset: [res?.response?.data] });
            }
            if (res?.response?.data?.errors?.requestModel) {
                alert('Please input all fields');
            }
        } else {
            navigate('/manageassignment');
        }
    };

    const isInputComplete = useMemo(() => {
        const { note, ...otherData } = dataAdd;
        return Object.values(otherData).every((x) => x !== null && x !== '');
    }, [dataAdd]);

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New Assignment</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>User</Form.Label>
                    <InputGroup>
                        <Form.Control
                            placeholder={'Enter user'}
                            style={{ width: 600, position: 'relative' }}
                            readOnly
                            value={user?.fullName}
                            onClick={() => setIsShowListUser(true)}
                        />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListUser(true)}>
                            <BsSearch />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Asset</Form.Label>
                    <InputGroup>
                        <Form.Control
                            isInvalid={arrMsg?.Asset}
                            placeholder={'Enter asset'}
                            readOnly
                            value={asset?.assetName}
                            onClick={() => setIsShowListAsset(true)}
                        />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListAsset(true)}>
                            <BsSearch />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>
                {arrMsg?.Asset && <p className={cx('msgErrorBg')}>{arrMsg?.Asset[0]}</p>}
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Assigned Date</Form.Label>
                    <DatePicker
                        name="assignedDate"
                        selected={dataAdd.assignedDate}
                        className={`${arrMsg?.AssignedDate ? 'border-danger' : ''} form-control w-full `}
                        onChange={(date) => onChangeDate(date)}
                        placeholderText="Click to select a date"
                        dateFormat="dd/MM/yyyy"
                    />
                </Form.Group>
                {arrMsg?.AssignedDate && <p className={cx('msgErrorBg')}>{arrMsg?.AssignedDate[0]}</p>}

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Note</Form.Label>
                    <Form.Group className="w-100">
                        <Form.Control
                            isInvalid={arrMsg?.Note}
                            type="text"
                            as="textarea"
                            rows={5}
                            cols={40}
                            placeholder="Enter note"
                            name="note"
                            onChange={onChange}
                            value={dataAdd?.note}
                        />
                    </Form.Group>
                </Form.Group>
                {arrMsg?.Note && <p className={cx('msgErrorBg')}>{arrMsg?.Note[0]}</p>}

                <div className={cx('button')}>
                    <Button variant="danger" onClick={handleCreate} disabled={!isInputComplete}>
                        Save
                    </Button>

                    <Button
                        variant="outline-secondary"
                        className={cx('cancel-button')}
                        onClick={() => navigate('/manageassignment')}
                    >
                        Cancel
                    </Button>
                </div>
            </Form>

            {isShowListUser && (
                <ModalUser
                    isShowListUser={isShowListUser}
                    setIsShowListUser={setIsShowListUser}
                    setUser={setUser}
                    data={user}
                />
            )}

            {isShowListAsset && (
                <ModalAsset
                    isShowListAsset={isShowListAsset}
                    setIsShowListAsset={setIsShowListAsset}
                    setAsset={setAsset}
                    data={asset}
                />
            )}
        </div>
    );
}

export default CreateAssignment;

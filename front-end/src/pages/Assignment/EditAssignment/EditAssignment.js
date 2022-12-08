import classNames from 'classnames/bind';
import { useMemo } from 'react';
import { useEffect, useState } from 'react';
import { InputGroup } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useLocation, useNavigate } from 'react-router-dom';
import { updateData } from '../../../apiServices';
import { ASSIGNMENT } from '../../../constants';
import { dateStrToDate } from '../../../lib/helper';
import { ModalAsset } from '../Modal/ModalAsset/ModalAsset';
import { ModalUser } from '../Modal/ModalUser/ModalUser';
import styles from '../CreateAssignment/createAssignment.module.scss';
import { GoTriangleDown } from 'react-icons/go';

const cx = classNames.bind(styles);

function EditAssignment() {
    const location = useLocation();
    const { assignment } = location?.state;

    const navigate = useNavigate();
    const [isShowListUser, setIsShowListUser] = useState(false);
    const [isShowListAsset, setIsShowListAsset] = useState(false);
    const [asset, setAsset] = useState({
        id: '',
        assetName: '',
    });

    const [user, setUser] = useState({
        id: '',
        fullName: '',
    });


    const [dataAdd, setDataAdd] = useState({
        assignedToId: assignment.userId,
        assetId: assignment.assetId,
        assignedDate: assignment.assignedDate,
        note: assignment.note,
        fullName: assignment.fullName,
        assetName: assignment.assetName,
    });

    useEffect(() => {
        if (user.id !== '' && user.fullName !== '') {
            setDataAdd({ ...dataAdd, assignedToId: user.id, fullName: user.fullName });
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [user.fullName, user.id]);

    useEffect(() => {
        if (asset.id !== '' && asset.assetName !== '') {
            setDataAdd({ ...dataAdd, assetName: asset.assetName, assetId: asset.id });
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [asset.assetName, asset.id]);

    const onChange = (e) => {
        setDataAdd({ ...dataAdd, [e.target.name]: e.target.value });
    };

    const [arrMsg, setArrMsg] = useState([]);

    const handleUpdate = async () => {
        // Trim() all value dataAdd
        // KEYSEARCH: trim all properties of an object dataAdd
        Object.keys(dataAdd).map((k) => (dataAdd[k] = typeof dataAdd[k] == 'string' ? dataAdd[k].trim() : dataAdd[k]));

        const { fullName, assetName, ...otherData } = dataAdd;

        const res = await updateData(`${ASSIGNMENT}/${assignment.id}`, otherData);

        if (res.code === 'ERR_BAD_REQUEST') {
            setArrMsg(res?.response?.data?.errors);
            if (res?.response?.data?.errors?.requestModel) {
                alert('Please input all fields');
            }
        } else {
            navigate('/manageassignment');
        }
    };

    const isInputComplete = useMemo(() => {
        return Object.values(dataAdd).every((x) => x !== null && x !== '');
    }, [dataAdd]);

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Edit Assignment</h3>

            <Form className={cx('form')}>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>User</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter user'} style={{ width: 600 }} readOnly value={dataAdd?.fullName} />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListUser(true)}>
                            <GoTriangleDown />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Asset</Form.Label>
                    <InputGroup>
                        <Form.Control placeholder={'Enter asset'} readOnly value={dataAdd.assetName} />
                        <InputGroup.Text style={{ cursor: 'pointer' }} onClick={() => setIsShowListAsset(true)}>
                            <GoTriangleDown />
                        </InputGroup.Text>
                    </InputGroup>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Assigned Date</Form.Label>

                    <Form.Control
                        isInvalid={arrMsg?.AssignedDate}
                        type="date"
                        name="assignedDate"
                        onChange={onChange}
                        value={dateStrToDate(dataAdd?.assignedDate)}
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
                    <Button variant="danger" onClick={handleUpdate} disabled={!isInputComplete}>
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

            {isShowListUser && <ModalUser setIsShowListUser={setIsShowListUser} setUser={setUser} />}

            {isShowListAsset && <ModalAsset setIsShowListAsset={setIsShowListAsset} setAsset={setAsset} />}
        </div>
    );
}

export default EditAssignment;

import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from './createAsset.module.scss';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuthContext } from '../../../context/RequiredAuth/authContext';

const cx = classNames.bind(styles);

function CreateAsset() {
    const [name, setName] = useState('');
    const [category, setCategory] = useState('');
    const [specification, setSpecification] = useState('');
    const [installedDate, setInstalledDate] = useState('');
    const [checkbox, setCheckbox] = useState();
    const [disabled, setDisable] = useState(true);

    const { token } = useAuthContext();
    const navigate = useNavigate();

    const handleChecked = (e) => {
        if (e.target.id === '2') {
            setCheckbox(0);
        }

        if (e.target.id === '1') {
            setCheckbox(1);
        }
    };

    const handleCreate = async () => {
        try {
            const response = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Asset`, {
                method: 'POST',
                body: JSON.stringify({
                    assetName: name,
                    categoryId: category,
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

            if (response.status === 200) {
                navigate('/manageasset');
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
            setDisable(false);
        } else {
            setDisable(true);
        }
    }, [name, category, specification, installedDate, checkbox]);

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Create New Asset</h3>

            <Form>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Name</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter name"
                        className={cx('input')}
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Category</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter category"
                        className={cx('input')}
                        value={category}
                        onChange={(e) => setCategory(e.target.value)}
                    />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Specification</Form.Label>
                    <textarea
                        cols="40"
                        rows="5"
                        placeholder="Enter specification"
                        className={cx('input-specification')}
                        value={specification}
                        onChange={(e) => setSpecification(e.target.value)}
                    ></textarea>
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Installed Date</Form.Label>
                    <Form.Control
                        type="date"
                        className={cx('input')}
                        value={installedDate}
                        onChange={(e) => setInstalledDate(e.target.value)}
                    />
                </Form.Group>

                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>State</Form.Label>
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
                    <Button variant="danger" onClick={handleCreate} disabled={disabled}>
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

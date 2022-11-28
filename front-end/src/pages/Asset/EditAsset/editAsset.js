import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from '../CreateAsset/createAsset.module.scss';
import { useAuthContext } from '../../../context/RequiredAuth/authContext';
import { useState } from 'react';

const cx = classNames.bind(styles);

function CreateAsset() {
    const { token, id } = useAuthContext();

    const [name, setName] = useState('');
    const [category, setCategory] = useState('');
    const [specification, setSpecification] = useState('');
    const [installed, setInstalled] = useState('');
    const [state, setState] = useState();

    const handleChecked = (e) => {
        if (e.target.id === '2') {
            setState(0);
        }
        if (e.target.id === '1') {
            setState(1);
        }
        if (e.target.id === '4') {
            setState(2);
        }
        if (e.target.id === '3') {
            setState(3);
        }
    };

    const handleUpdate = async () => {
        try {
            const response = await fetch(`https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Asset`, {
                method: 'POST',
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

            console.log(category);

            if (response.status === 200) {
                console.log('sucsses');
            }
        } catch (error) {
            console.log('error');
        }

        return null;
    };

    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Edit Asset</h3>

            <Form>
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
                    <Form.Select
                        className={cx('input')}
                        onChange={(e) => {
                            setCategory(e);
                        }}
                    >
                        <option>Laptop</option>
                        <option>Monitor</option>
                        <option>Personal Computer</option>
                    </Form.Select>
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
                        className={cx('input')}
                        value={installed}
                        onChange={(e) => {
                            setInstalled(e.target.value);
                        }}
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
                        <Form.Check label="Available" name="gender" id={1} type="radio" />
                        <Form.Check label="Not available" name="gender" id={2} type="radio" />
                        <Form.Check label="Waiting for recycling" name="gender" id={3} type="radio" />
                        <Form.Check label="Recycled" name="gender" id={4} type="radio" />
                    </div>
                </Form.Group>
                <div className={cx('button')}>
                    <Button variant="danger" onClick={handleUpdate}>
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

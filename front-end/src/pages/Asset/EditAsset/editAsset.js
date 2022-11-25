import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import classNames from 'classnames/bind';
import styles from '../CreateAsset/createAsset.module.scss';

const cx = classNames.bind(styles);

function CreateAsset() {
    return (
        <div className={cx('container')}>
            <h3 className={cx('title')}>Edit Asset</h3>

            <Form>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}> Name</Form.Label>
                    <Form.Control type="text" placeholder="Enter name" className={cx('input')} />
                </Form.Group>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Category</Form.Label>
                    <Form.Select className={cx('input')}>
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
                    ></textarea>
                </Form.Group>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>Installed Date</Form.Label>
                    <Form.Control type="date" className={cx('input')} />
                </Form.Group>
                <Form.Group className={cx('common-form')}>
                    <Form.Label className={cx('title_input')}>State</Form.Label>

                    <div key={`gender-radio`} className={cx('input-radio-state')}>
                        <Form.Check label="Available" name="gender" type="radio" />
                        <Form.Check label="Not available" name="gender" type="radio" />
                        <Form.Check label="Waiting for recycling" name="gender" type="radio" />
                        <Form.Check label="Recycled" name="gender" type="radio" />
                    </div>
                </Form.Group>
                <div className={cx('button')}>
                    <Button variant="danger">Save</Button>

                    <Button variant="outline-success" className={cx('cancel-button')} href="/manageasset">
                        Cancel
                    </Button>
                </div>
            </Form>
        </div>
    );
}

export default CreateAsset;

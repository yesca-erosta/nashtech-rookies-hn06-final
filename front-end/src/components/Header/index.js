import classNames from 'classnames/bind';
import styles from './header.module.scss';

import Dropdown from 'react-bootstrap/Dropdown';
import 'bootstrap/dist/css/bootstrap.min.css';
import NavDropdown from 'react-bootstrap/NavDropdown';

const cx = classNames.bind(styles);

function Header() {
    return (
        <header className={cx('wrapper')}>
            <div className={cx('inner')}>
                <div className={cx('inner-page')}>Home {'>'} </div>

                <div className={cx('inner-name')}>
                    <div>Binhnv</div>
                    <div>
                        <Dropdown>
                            <Dropdown.Toggle variant="" style={{ color: 'white' }} />

                            <Dropdown.Menu style={{ color: 'white' }}>
                                <Dropdown.Item>Action1</Dropdown.Item>
                                <Dropdown.Item>Action2</Dropdown.Item>
                                <Dropdown.Item>Change password</Dropdown.Item>
                                <NavDropdown.Divider />
                                <Dropdown.Item>Logout</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                </div>
            </div>
        </header>
    );
}

export default Header;

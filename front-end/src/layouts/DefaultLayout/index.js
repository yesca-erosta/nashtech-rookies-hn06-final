import classNames from 'classnames/bind';
import Header from '../../components/Header/index';
import Sidebar from '../../components/Sidebar/Sidebar';
import styles from './defaultLayout.module.scss';

const cx = classNames.bind(styles);

function DefaultLayout({ children }) {
    return (
        <div className={cx('wrapper')}>
            <Header />
            <div className={cx('container')}>
                <Sidebar />
                <div className={cx('content')}>{children}</div>
            </div>
        </div>
    );
}

export default DefaultLayout;

// These are static elements that don't change when going back and forth between pages

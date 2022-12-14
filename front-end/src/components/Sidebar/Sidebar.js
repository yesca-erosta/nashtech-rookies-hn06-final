import styles from './sideBar.module.scss';
import classNames from 'classnames/bind';
import config from '../../config';
import Menu, { MenuItem } from './Menu';
import { useAppContext } from '../../context/RequiredAuth/authContext';

const cx = classNames.bind(styles);

function Sidebar() {
    const { token } = useAppContext();

    return (
        <aside className={cx('wrapper')}>
            <div className={cx('logo')}>
                <img
                    src="https://vcdn1-kinhdoanh.vnecdn.net/2020/01/16/12-4472-1579159249-1579160129-3325-6795-1579160166.png?w=0&h=0&q=100&dpr=2&fit=crop&s=D8mMIyEsQ_DZ66aSj0yNjQ"
                    alt=""
                />
                <div>Online Asset Management</div>
            </div>

            <Menu>
                <MenuItem title="Home" to={config.routes.home} />
                {token.type === 1 && (
                    <>
                        <MenuItem title="Manage User" to={config.routes.user} />
                        <MenuItem title="Manage Asset" to={config.routes.asset} />
                        <MenuItem title="Manage Assignment" to={config.routes.assignment} />
                        <MenuItem title="Request for Returning" to={config.routes.requestForReturning} />
                        <MenuItem title="Report" to={config.routes.report} />{' '}
                    </>
                )}
            </Menu>
        </aside>
    );
}

export default Sidebar;

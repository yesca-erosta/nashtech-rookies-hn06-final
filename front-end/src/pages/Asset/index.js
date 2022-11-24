import Table from 'react-bootstrap/Table';
import classNames from 'classnames/bind';
import styles from './asset.module.scss';

import { BsFillPencilFill, BsSearch } from 'react-icons/bs';
import { TiDeleteOutline } from 'react-icons/ti';
import { GoTriangleDown } from 'react-icons/go';
import { FaFilter } from 'react-icons/fa';

import Pagination from 'react-bootstrap/Pagination';
import { Button } from 'react-bootstrap';

import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';

const cx = classNames.bind(styles);

function Asset() {
    let active = 1;

    let items = [];
    for (let number = 1; number <= 3; number++) {
        items.push(
            <Pagination.Item key={number} active={number === active}>
                {number}
            </Pagination.Item>,
        );
    }

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Asset List</h1>
            </div>

            <div className={cx('filterbox')}>
                <div>
                    <InputGroup>
                        <Form.Control />

                        <InputGroup.Text>
                            <button className={cx('input')}>
                                <FaFilter />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>

                <div>
                    <InputGroup>
                        <Form.Control />

                        <InputGroup.Text>
                            <button className={cx('input')}>
                                <FaFilter />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>

                <div>
                    <InputGroup>
                        <Form.Control />

                        <InputGroup.Text>
                            <button className={cx('input')}>
                                <BsSearch />
                            </button>
                        </InputGroup.Text>
                    </InputGroup>
                </div>

                <Button variant="danger">Create new asset</Button>
            </div>

            <div className={cx('table')}>
                <Table responsive="sm">
                    <thead>
                        <tr>
                            <th>
                                <div className={cx('title')}>
                                    <div>Asset Code</div>
                                    <button className={cx('triagle')}>
                                        <GoTriangleDown />
                                    </button>
                                </div>
                            </th>
                            <th>
                                <div className={cx('title')}>
                                    <div>Asset Name</div>
                                    <button className={cx('triagle')}>
                                        <GoTriangleDown />
                                    </button>
                                </div>
                            </th>
                            <th>
                                <div className={cx('title')}>
                                    <div> Category</div>
                                    <button className={cx('triagle')}>
                                        <GoTriangleDown />
                                    </button>
                                </div>
                            </th>
                            <th>
                                <div className={cx('title')}>
                                    <div> State</div>
                                    <button className={cx('triagle')}>
                                        <GoTriangleDown />
                                    </button>
                                </div>
                            </th>
                            <th>
                                <div className={cx('title')}>
                                    <div> Asset</div>
                                    <button className={cx('triagle')}>
                                        <GoTriangleDown />
                                    </button>
                                </div>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Table cell</td>
                            <td>Table cell</td>
                            <td>Table cell</td>
                            <td>Table cell</td>
                            <td>Table cell</td>

                            <div className={cx('actions')}>
                                <button className={cx('pen')} disabled={false}>
                                    <BsFillPencilFill />
                                </button>
                                <button className={cx('delete')} disabled={false}>
                                    <TiDeleteOutline />
                                </button>
                            </div>
                        </tr>
                    </tbody>
                </Table>
            </div>

            <div className={cx('paging')}>
                <Pagination>
                    <Pagination.Item disabled>Previous</Pagination.Item>
                    {items}
                    <Pagination.Item>Next</Pagination.Item>
                </Pagination>
            </div>
        </div>
    );
}

export default Asset;

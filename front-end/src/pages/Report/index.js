import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import styles from './report.module.scss';

import { Button } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { getAllDataWithFilterBox } from '../../apiServices';

const cx = classNames.bind(styles);

function Report() {
    const columns = [
        {
            name: 'Category',
            selector: (row) => row.category.name,
            sortable: true,
        },
        {
            name: 'Total',
            selector: (row) => row.total,
            sortable: true,
        },
        {
            name: 'Assigned',
            sortable: true,
            selector: (row) => row.assigned,
        },
        {
            name: 'Available',
            selector: (row) => row.available,
            sortable: true,
        },
        {
            name: 'Not Available',
            selector: (row) => row.notAvailable,
            sortable: true,
        },
        {
            name: 'Waiting for recycling',
            selector: (row) => row.waitingForRecycling,
            sortable: true,
        },
        {
            name: 'Recycled',
            selector: (row) => row.recycled,
            sortable: true,
        },
    ];

    const [loading, setLoading] = useState(false);

    const [dataReport, setDataReport] = useState([]);

    // Get Data
    const getData = async () => {
        setLoading(true);

        const data = await getAllDataWithFilterBox(`Report`);

        setDataReport(data);
        setLoading(false);
    };

    useEffect(() => {
        getData();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Request List</h1>
            </div>

            <Button variant="danger" className={cx('btn_export')}>
                Export
            </Button>

            <div className={cx('main_table')}>
                <DataTable
                    title="Assignments"
                    columns={columns}
                    data={dataReport}
                    noHeader
                    defaultSortAsc={true}
                    highlightOnHover
                    noDataComponent={'There are no records to display'}
                    dense
                    progressPending={loading}
                    sortServer
                />
            </div>
        </div>
    );
}

export default Report;

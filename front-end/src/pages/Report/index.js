import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import styles from './report.module.scss';
import { Button } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { createData, getAllDataWithFilterBox } from '../../apiServices';
import { CSVLink } from 'react-csv';
import { Loading } from '../../components/Loading/Loading';

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
            selector: (row) => row.assigned,
            sortable: true,
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

    const a = dataReport.map((item) => {
        const b = { ...item, category: item.category.name };
        const { categoryId, id, ...otherData } = b;

        return otherData;
    });

    // Get Data
    const getData = async () => {
        setLoading(true);
        await createData(`Report`, null);
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
                <CSVLink data={a} filename={'Report_Data'} style={{ color: 'white', textDecoration: 'none' }}>
                    Export
                </CSVLink>
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
                />
            </div>

            {loading && <Loading />}
        </div>
    );
}

export default Report;

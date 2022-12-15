import classNames from 'classnames/bind';
import { useEffect, useState } from 'react';
import styles from './report.module.scss';
import { Button, Col, Row } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { createData, getAllDataWithFilterBox } from '../../apiServices';
import { Loading } from '../../components/Loading/Loading';
import axios from 'axios';
import fileDownload from 'js-file-download';
import { useAppContext } from '../../context/RequiredAuth/authContext';
import ReactPaginate from 'react-paginate';

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

    const { token } = useAppContext();
    const handleClickExport = async (url, fileName) => {
        axios({
            url,
            method: 'POST',
            headers: {
                Accept: 'application/json',
                Authorization: `Bearer ${token.token}`,
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
            responseType: 'blob',
        }).then((res) => {
            fileDownload(res.data, fileName);
        });
    };

    const [selectedPage, setSelectedPage] = useState(1);

    const handlePageClick = async (event) => {
        setSelectedPage(event.selected + 1);
    };

    const CustomPagination = (e) => {
        const count = Math.ceil(dataReport.length / 30);
        return (
            <Row className="mx-0">
                <Col className="d-flex justify-content-end" sm="12">
                    <ReactPaginate
                        previousLabel={'Previous'}
                        nextLabel={'Next'}
                        forcePage={selectedPage !== 0 ? selectedPage - 1 : 0}
                        onPageChange={handlePageClick}
                        pageCount={count || 1}
                        breakLabel={'...'}
                        pageRangeDisplayed={2}
                        marginPagesDisplayed={2}
                        activeClassName={'active '}
                        pageClassName={'page-item text-color'}
                        nextLinkClassName={'page-link text-color'}
                        nextClassName={'page-item next text-color'}
                        previousClassName={'page-item prev text-color'}
                        previousLinkClassName={'page-link text-color'}
                        pageLinkClassName={'page-link text-color'}
                        breakClassName="page-item text-color"
                        breakLinkClassName="page-link text-color"
                        containerClassName={'pagination react-paginate pagination-sm justify-content-end pr-1 mt-3'}
                    />
                </Col>
            </Row>
        );
    };

    return (
        <div className={cx('container')}>
            <div className={cx('title_asset')}>
                <h1>Report</h1>
            </div>

            <Button
                variant="danger"
                className={cx('btn_export')}
                onClick={() =>
                    handleClickExport(
                        'https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api/Report/export',
                        'Report.xlsx',
                    )
                }
            >
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
                    pagination
                    paginationComponent={CustomPagination}
                />
            </div>

            {loading && <Loading />}
        </div>
    );
}

export default Report;

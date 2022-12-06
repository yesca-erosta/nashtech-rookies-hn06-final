import { useState } from 'react';
import { useEffect } from 'react';
import { Form } from 'react-bootstrap';
import DataTable from 'react-data-table-component';
import { getAllDataWithFilterBox } from '../../../../apiServices';
import { queryToString } from '../../../../lib/helper';

export const ModalUser = () => {
    const columns = [
        {
            name: 'Select',
            selector: (row) => row.fullName,
            sortable: true,
            cell: (row) => {
                return (
                    <Form>
                        <div key={`inline-radio`} className="mb-3">
                            <Form.Check inline name={row.staffCode} type={'radio'} id={row.staffCode} />
                        </div>
                    </Form>
                );
            },
        },
        {
            name: 'Staff Code',
            selector: (row) => row.staffCode,
            sortable: true,
        },
        {
            name: 'Full Name',
            selector: (row) => row.fullName,
            sortable: true,
        },
        {
            name: 'Type',
            selector: (row) => row.type,
            sortable: true,
        },
    ];

    const [dataUser, setdataUser] = useState([]);
    const [loading, setLoading] = useState(false);
    const [queryParams] = useState({
        page: 1,
        pageSize: 10,
    });
    // get data user
    const GetDataUser = async () => {
        const data = await getAllDataWithFilterBox(`User/query` + queryToString(queryParams));
        setdataUser(data.source);
    };

    useEffect(() => {
        GetDataUser();

        // I want call a function when first render
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const msgNoData = () => {
        if (loading) {
            return (
                <div style={{ fontSize: '24px', textAlign: '-webkit-center', fontWeight: 'bold', padding: '24px' }}>
                    Loading...
                </div>
            );
        } else {
            return <div style={{ marginTop: '30px', textAlign: '-webkit-center' }}>There are no records to display</div>;
        }
    };

    const customStyles = {
        table: {
            style: {
                border: '1px solid red',
            },
        },
    };

    return dataUser ? (
        <DataTable
            title="Assets"
            columns={columns}
            data={dataUser}
            noHeader
            defaultSortAsc={true}
            highlightOnHover
            noDataComponent={'There are no records to display'}
            dense
            progressPending={loading}
            customStyles={customStyles}
            // sortServer
            // onSort={handleSort}
        />
    ) : (
        msgNoData()
    );
};

import { createContext, useContext, useEffect, useState } from 'react';
import React from 'react';
import { getAllDataWithFilterBox } from '../apiServices';
import { queryToString } from '../lib/helper';

const UserListContext = createContext({
  users: [],
  setUsers: () => {},
  totalRows: Number,
  setTotalRows: () => {},
  perPage: Number,
  setPerPage: () => {},
  loading: Boolean,
  setLoading: () => {},
  queryParams: String,
  setQueryParams: () => {},
});

export const useUserListContext = () => useContext(UserListContext);

const UsersListProvider = ({ children }) => {
  const [users, setUsers] = useState([]);
  const [totalRows, setTotalRows] = useState(0);
  const [perPage, setPerPage] = useState(10);
  const [loading, setLoading] = useState(false);
  const [queryParams, setQueryParams] = useState({
    page: 1,
    pageSize: 10,
    types: '0,1',
    sort: 'NameAcsending',
  });
  const fetchUsers = async (page) => {
    setLoading(true);
    setQueryParams({ ...queryParams, page: page, pageSize: perPage });

    const data = await getAllDataWithFilterBox(
      `User/query` + queryToString({ ...queryParams, page: page, pageSize: perPage }),
    );

    setUsers(data.source);
    setTotalRows(data.totalRecord);
    setLoading(false);
  };

  useEffect(() => {
    fetchUsers(1); // fetch page 1 of users
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const contextValue = {
    users,
    setUsers,
    loading,
    setLoading,
    totalRows,
    setTotalRows,
    perPage,
    setPerPage,
    queryParams,
    setQueryParams,
  };

  return <UserListContext.Provider value={contextValue}>{children}</UserListContext.Provider>;
};

export default UsersListProvider;

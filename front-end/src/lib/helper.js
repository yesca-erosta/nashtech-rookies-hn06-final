export const dateStrToStr = (date) => {
  return date ? date.slice(8, 10) + '/' + date.slice(5, 7) + '/' + date.slice(0, 4) : '';
};

export const queryToString = (query) => {
  const checkQuery = (queryParams, queryName) => {
    if (queryParams === 0) {
      return `${queryName}=0&`;
    }
    if (queryParams) {
      return `${queryName}=${queryParams}&`;
    } else {
      return `${queryName}=&`;
    }
  };

  return (
    '?' +
    `page=${query.page ?? 1}&` +
    `pageSize=${query.pageSize ?? 10}&` +
    `${checkQuery(query.valueSearch, 'valueSearch')}` +
    `${checkQuery(query.type, 'type')}` +
    `sort=${query.sort ?? 'StaffCodeAcsending'}`
  );
};

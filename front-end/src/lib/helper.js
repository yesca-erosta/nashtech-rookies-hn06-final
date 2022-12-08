export const dateStrToStr = (date) => {
  return date ? date.slice(8, 10) + '/' + date.slice(5, 7) + '/' + date.slice(0, 4) : '';
};

export const dateStrToDate = (date) => {
  return date ? date.slice(0, 10) : '';
};

export const queryToString = (query) => {
  const checkQuery = (queryParams, queryName) => {
    // cach 1
    // if (queryParams === 0) {
    //   return `${queryName}=0&`;
    // }
    // if (queryParams) {
    //   return `${queryName}=${queryParams}&`;
    // } else {
    //   return `${queryName}=&`;
    // }

    return queryParams === 0 ? `${queryName}=0&` : queryParams ? `${queryName}=${queryParams}&` : `${queryName}=&`;
  };

  return (
    '?' +
    `page=${query.page ?? 1}&` +
    `pageSize=${query.pageSize ?? 10}&` +
    `${checkQuery(query.valueSearch, 'valueSearch')}` +
    `${checkQuery(query.types, 'types')}` +
    `sort=${query.sort ?? 'NameAcsending'}`
  );
};

export const queryToStringForAsset = (query) => {
  const checkQuery = (queryParams, queryName) => {
    return queryParams === 0 ? `${queryName}=0&` : queryParams ? `${queryName}=${queryParams}&` : `${queryName}=&`;
  };

  return (
    '?' +
    `page=${query.page ?? 1}&` +
    `pageSize=${query.pageSize ?? 10}&` +
    `${checkQuery(query.valueSearch, 'valueSearch')}` +
    `${checkQuery(query.states, 'states')}` +
    `${checkQuery(query.category, 'category')}` +
    `sort=${query.sort ?? 'AssetCodeAcsending'}`
  );
};


export const queryToStringForAssignments = (query) => {
  const checkQuery = (queryParams, queryName) => {
    return queryParams === 0 ? `${queryName}=0&` : queryParams ? `${queryName}=${queryParams}&` : `${queryName}=&`;
  };


  //TODO: change backend
  return (
    '?' +
    `page=${query.page ?? 1}&` +
    `pageSize=${query.pageSize ?? 10}&` +
    `${checkQuery(query.valueSearch, 'valueSearch')}` +
    `${checkQuery(query.states, 'states')}` +
    `${checkQuery(query.date, 'date')}` +
    `sort=${query.sort ?? 'AssetCodeAcsending'}`
  );
};
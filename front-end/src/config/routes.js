const routes = {
  home: { path: '/', name: 'Home' },
  user: { path: '/manageruser', name: 'Manage User' },
  asset: { path: '/manageasset', name: 'Manage Asset' },
  assignment: { path: '/manageassignment', name: 'Manage Assignment' },
  requestForReturning: { path: '/requestforreturning', name: 'Request For Returning' },
  report: { path: '/report', name: 'Report' },
  createUser: { path: '/manageruser/createuser', name: 'Manage User > Create New User' },
  editUser: { path: '/manageruser/edituser', name: 'Manage User > Edit User' },
  createNewAsset: { path: '/manageasset/createnewasset', name: 'Manage Asset > Create New Asset' },
  editAset: { path: '/manageasset/editasset', name: 'Manage Asset > Edit Asset' },
  createnewAssignment: { path: '/manageassignment/createnewassignment', name: 'Manage Assignment > Create New Assignment' },
  notFound: { path: '*' },
};

export default routes;

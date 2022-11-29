import Home from '../pages/Home';
import User from '../pages/User';
import Asset from '../pages/Asset';
import Assignment from '../pages/Assignment';
import Request from '../pages/Request';
import Report from '../pages/Report';
import config from '../config';
import CreateUser from '../pages/User/CreateUser/createUser';
import EditUser from '../pages/User/EditUser/editUser';
import CreateAsset from '../pages/Asset/CreateAsset/createAsset';
import EditAsset from '../pages/Asset/EditAsset/editAsset';
import PageOne from '../pages/Asset/Paging/pageOne';
import NotFound from '../pages/NotFound/notFound';

// route public
export const publicRoutes = [
  { path: config.routes.home.path, component: Home },
  { path: config.routes.user.path, component: User },
  { path: config.routes.asset.path, component: Asset },
  { path: config.routes.assignment.path, component: Assignment },
  { path: config.routes.requestForReturning.path, component: Request },
  { path: config.routes.report.path, component: Report },
  { path: config.routes.createUser.path, component: CreateUser },
  { path: config.routes.editUser.path, component: EditUser },
  { path: config.routes.createNewAsset.path, component: CreateAsset },
  { path: config.routes.editAset.path, component: EditAsset },
  { path: config.routes.pageAsset.path, component: PageOne },
  { path: config.routes.notFound.path, component: NotFound },
];

export const privateRoutes = [];

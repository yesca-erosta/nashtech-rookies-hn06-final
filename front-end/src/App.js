import { BrowserRouter as Router, Routes, Route, Outlet } from 'react-router-dom';
import { publicRoutes } from './routes/index';
import DefaultLayout from './layouts/DefaultLayout';
import { Fragment } from 'react';
import AuthProvider from './context/RequiredAuth/authContext';
import RequiredAuth from './context/RequiredAuth/requiredAuth';
import Login from './components/login/login';
import NotFound from './pages/NotFound/notFound';
import UsersProvider from './context/userListContext';

function App() {
  return (
    <div>
      <AuthProvider>
        <UsersProvider>
          <Router>
            <Routes>
              <Route path="/login" element={<Login />} />
              <Route path="*" element={<NotFound />} />

              {publicRoutes.map((route, index) => {
                const Page = route.component;
                let Layout = DefaultLayout;

                if (route.layout === null) {
                  Layout = Fragment;
                }

                return (
                  <Route
                    key={index}
                    path={route.path}
                    element={
                      <RequiredAuth>
                        <Layout>
                          <Page />
                        </Layout>
                      </RequiredAuth>
                    }
                  />
                );
              })}
            </Routes>
            <Outlet />
          </Router>
        </UsersProvider>
      </AuthProvider>
    </div>
  );
}

export default App;

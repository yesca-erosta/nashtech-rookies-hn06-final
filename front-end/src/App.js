import { Fragment } from 'react';
import { BrowserRouter as Router, Outlet, Route, Routes } from 'react-router-dom';
import Login from './components/login/login';
import AuthProvider from './context/RequiredAuth/authContext';
import RequiredAuth from './context/RequiredAuth/requiredAuth';
import DefaultLayout from './layouts/DefaultLayout';
import NotFound from './pages/NotFound/notFound';
import { publicRoutes } from './routes/index';

function App() {
  return (
    <div>
      <AuthProvider>
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
      </AuthProvider>
    </div>
  );
}

export default App;

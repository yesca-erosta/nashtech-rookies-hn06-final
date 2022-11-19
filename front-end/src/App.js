import { BrowserRouter as Router, Routes, Route, Outlet } from 'react-router-dom';
import { publicRoutes } from './routes/index';
import DefaultLayout from './layouts/DefaultLayout';
import Login from './components/login/login';

function App() {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="" element={<Login />} />
                </Routes>

                <Routes>
                    {publicRoutes.map((route, index) => {
                        const Layout = route.layout || DefaultLayout;
                        const Page = route.component;
                        return (
                            <Route
                                key={index}
                                path={route.path}
                                element={
                                    <Layout>
                                        <Page />
                                    </Layout>
                                }
                            />
                        );
                    })}
                </Routes>
            </div>
            <Outlet />
        </Router>
    );
}

export default App;

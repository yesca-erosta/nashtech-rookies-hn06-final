import { useAuthContext } from './authContext';
import { Navigate } from 'react-router-dom';

function RequiredAuth(props) {
    const { children } = props;
    const { isAuthenticated } = useAuthContext();
    return isAuthenticated ? children : <Navigate to="/login" />;
}

export default RequiredAuth;

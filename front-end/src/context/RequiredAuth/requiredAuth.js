import { useAuthContext } from './authContext';
import { Navigate } from 'react-router-dom';

function RequiredAuth(props) {
    const { children } = props;
    const { isAuthenticated } = useAuthContext();
    return isAuthenticated || localStorage.getItem('localStorage') ? children : <Navigate to="/login" />;
}

export default RequiredAuth;

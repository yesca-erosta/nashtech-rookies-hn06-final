import { useAuthContext } from './authContext';
import { Navigate } from 'react-router-dom';
import { TOKEN_KEY } from '../../constants';

function RequiredAuth(props) {
    const { children } = props;
    const { isAuthenticated } = useAuthContext();
    return isAuthenticated || localStorage.getItem(TOKEN_KEY) ? children : <Navigate to="/login" />;
}

export default RequiredAuth;

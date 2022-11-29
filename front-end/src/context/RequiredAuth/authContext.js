import { createContext, useContext, useState } from 'react';

const AuthContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
    token: null,
    oldPasswordLogin: null,
    id: null,
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [token, setToken] = useState({});
    const [oldPasswordLogin, setOldPasswordLogin] = useState('');
    const [id, setId] = useState();

    const contextValue = {
        isAuthenticated,
        setIsAuthenticated,
        setToken,
        token,
        oldPasswordLogin,
        setOldPasswordLogin,
        id,
        setId,
    };

    return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

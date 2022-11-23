import { createContext, useContext, useState } from 'react';

const AuthContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
    token: null,
    oldPassword: null,
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [token, setToken] = useState({});
    const [oldPassword, setOldPassword] = useState('');

    const contextValue = { isAuthenticated, setIsAuthenticated, setToken, token, oldPassword, setOldPassword };

    return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

import { createContext, useContext, useState } from 'react';

const AuthContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
    user: null,
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState({});

    const contextValue = { isAuthenticated, setIsAuthenticated, setUser, user };

    return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

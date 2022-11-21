import { createContext, useContext, useState } from 'react';

const AuthContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
});

export const useAuthContext = () => useContext(AuthContext);

const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    const contextValue = { isAuthenticated, setIsAuthenticated };

    return <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>;
};

export default AuthProvider;

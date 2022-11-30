import { createContext, useContext, useState } from 'react';

const AppContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
    token: null,
    oldPasswordLogin: null,
    id: null,
    newAsset: null,
    getOneAsset: null,
});

export const useAppContext = () => useContext(AppContext);

const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [token, setToken] = useState({});
    const [oldPasswordLogin, setOldPasswordLogin] = useState('');
    const [id, setId] = useState();
    const [newAsset, setNewAsset] = useState();
    const [getOneAsset, setGetOneAsset] = useState();

    const contextValue = {
        isAuthenticated,
        setIsAuthenticated,
        setToken,
        token,
        oldPasswordLogin,
        setOldPasswordLogin,
        id,
        setId,
        newAsset,
        setNewAsset,
        getOneAsset,
        setGetOneAsset,
    };

    return <AppContext.Provider value={contextValue}>{children}</AppContext.Provider>;
};

export default AuthProvider;

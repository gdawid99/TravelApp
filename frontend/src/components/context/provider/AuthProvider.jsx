import { useEffect, useRef, useState } from "react";
import { authApi, refreshTokenFun, setApiAccessToken } from "../../../services/api";
import { AuthContext } from "../AuthContext";
import { useDialogContext } from "../../../hooks/useDialogContext";

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(null);
    const [loading, setLoading] = useState(false);
    const { openDialog } = useDialogContext();

    const login = async (email, password) => {
        setLoading(true);
        try {
            const res = await authApi.login({ Email: email, Password: password});

            const { accessToken, userData } = res.data;

            setApiAccessToken(accessToken);
            setToken(accessToken);
            setUser(userData);
        }
        catch (err) {
            console.log(err);
        }
        finally {
            setLoading(false);
        }
    };

    const logout = () => {
        setToken(null);
        setApiAccessToken(null);
        setUser(null);
    };

    useEffect(() => {
        const autoLogin = async () => {
            setLoading(true);

            try {
                const res = await refreshTokenFun();

                setToken(res.accessToken)
                setUser(res.userData);
            }
            catch (err) {
                console.log(err);
            }
            setLoading(false);
        };

        autoLogin();
    }, []);

    useEffect(() => {
        const handler = () => { 
            logout();
            openDialog('loginDialog');
        };
        window.addEventListener('auth:logout', handler);

        return () => window.removeEventListener('auth:logout', handler);
    }, [openDialog]);

    return (
        <AuthContext.Provider value={{user, token, loading, login, logout}}>
            {!loading && children}
        </AuthContext.Provider>
    )

}
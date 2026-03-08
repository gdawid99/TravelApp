import { useEffect, useRef, useState } from "react";
import { authApi, refreshTokenFun } from "../../../services/api";
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

            const { accessToken, accessTokenExpiresAt, refreshToken, refreshTokenExpiresAt, userData } = res.data;

            setToken(accessToken);
            localStorage.setItem('accessToken', accessToken);
            localStorage.setItem('refreshToken', refreshToken);
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
        setUser(null);
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
    };

    useEffect(() => {
        const autoLogin = async () => {
            setLoading(true);

            const storedToken = localStorage.getItem('refreshToken');
            if (storedToken) {
                try {
                    const res = await refreshTokenFun();

                    const { accessToken, refreshToken, userData } = res;

                    console.log("REFRESH");
                    console.log(accessToken);
                    console.log(refreshToken);

                    setToken(accessToken);
                    setUser(userData);
                }
                catch (err) {
                    localStorage.removeItem('accessToken');
                    localStorage.removeItem('refreshToken');
                    console.log(err);
                }
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
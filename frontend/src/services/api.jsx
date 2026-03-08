import axios from 'axios'

let refreshPromise = null;
let apiAccessToken = null;

export const setApiAccessToken = (token) => {
    apiAccessToken = token;
}

export const refreshTokenFun = async () => {
    if (!refreshPromise) {
        refreshPromise = refreshApi.post('/auth/refresh');
    }

    try {
        const res = await refreshPromise;

        const { accessToken } = res.data;

        setApiAccessToken(accessToken);

        return res.data;
    }
    finally {
        refreshPromise = null;
    }
}


export const api = axios.create({
    baseURL: 'http://localhost:5164/api/v1',
    timeout: 5000,
    withCredentials: true
})

export const refreshApi = axios.create({
    baseURL: 'http://localhost:5164/api/v1',
    timeout: 5000,
    withCredentials: true
})

api.interceptors.request.use((config) => {
    if (apiAccessToken) {
        config.headers.Authorization = `Bearer ${apiAccessToken}`;
    }
    return config;
})

api.interceptors.response.use(
    (response) => response,
    async (error) => {

        const originalRequest = error.config;

        if (originalRequest.url === '/auth/refresh') {
            return Promise.reject(error);
        }

        if (error.response?.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                const res = await refreshTokenFun();
                originalRequest.headers.Authorization = `Bearer ${res.accessToken}`;
                return api(originalRequest);
            }
            catch (refreshError) {
                window.dispatchEvent(new Event("auth:logout"));
                return Promise.reject(refreshError);
            }
        }
        return Promise.reject(error);
    }
)

export const usersApi = {
    getAll: () => api.get('/user'),
    getById: (id) => api.get(`/user/${id}`),
    create: (data) => api.post('/user/create', data)
}

export const authApi = {
    login: (data) => api.post('/auth/login', data),
    refresh: (data) => refreshApi.post('/auth/refresh', data)
}

export const tripsApi = {
    //...
}
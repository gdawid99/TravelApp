import axios from 'axios'

const api = axios.create({
    baseURL: 'http://localhost:5164/api/v1',
    timeout: 5000,
})

export const usersApi = {
    getAll: () => api.get('/user'),
    getById: (id) => api.get(`/user/${id}`),
    create: (data) => api.post('/user/create', data)
}

export const tripsApi = {
    //...
}
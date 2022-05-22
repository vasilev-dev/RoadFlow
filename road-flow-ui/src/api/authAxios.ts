import axios from 'axios';
import JwtTokenStore from '../stores/jwtTokenStore';

const AuthAxios = axios.create();
const jwtTokenStore = new JwtTokenStore();

AuthAxios.interceptors.request.use(
    config => {
        config.headers = {
            Authorization: `Bearer ${jwtTokenStore.accessToken}`,
            Accept: 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded',
        };
        return config;
    },
    error => Promise.reject(error)
);

axios.interceptors.response.use(
    response => response,
    async function (error) {
        const originalRequest = error.config;
        // todo add this logic
        // if refresh token is expired -> navigate to login page
        // if access token is expired -> get new access token by refresh token
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            const accessToken = 'todo'; // todo get refresh token from api
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + accessToken;
            return AuthAxios(originalRequest);
        }
        // todo add 403
        return Promise.reject(error);
    }
);

export default AuthAxios;

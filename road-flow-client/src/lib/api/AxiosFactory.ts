import axios from 'axios';
import TokenService from '../../features/authentication/services/TokenService';
// eslint-disable-next-line import/no-cycle
import AuthenticationApiService from '../../features/authentication/services/AuthenticationApiService';

const API = axios.create({
  baseURL: import.meta.env.VITE_API_GATEWAY_URL,
});

API.interceptors.request.use(
  (config) => {
    const accessToken = TokenService.getAccessToken();
    if (accessToken) {
      // eslint-disable-next-line no-param-reassign
      config.headers = {
        Authorization: `Bearer ${accessToken}`,
        Accept: 'application/json',
      };
    }
    return config;
  },
  async (error) => Promise.reject(error)
);

API.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    // eslint-disable-next-line no-underscore-dangle
    if (error?.response?.status === 401 && !originalRequest._retry) {
      try {
        const refreshToken = TokenService.getRefreshToken();
        if (!refreshToken) {
          // noinspection ExceptionCaughtLocallyJS
          throw new Error('Refresh token deleted');
        }
        const tokenResponse = await AuthenticationApiService.refreshToken(
          refreshToken
        );
        TokenService.setAccessToken(
          tokenResponse.accessToken,
          tokenResponse.expirationAccessTokenTime
        );
        TokenService.setRefreshToken(
          tokenResponse.refreshToken,
          tokenResponse.expirationRefreshTokenTime
        );
        // eslint-disable-next-line no-underscore-dangle
        originalRequest._retry = true;
        return await API(originalRequest);
      } catch (e) {
        window.location.href = '/sign-in';
      }
    }
    return Promise.reject(error);
  }
);

export default API;

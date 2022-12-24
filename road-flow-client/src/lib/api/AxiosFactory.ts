import axios from 'axios';
import TokenService from '../../features/authentication/services/TokenService';
// eslint-disable-next-line import/no-cycle
import AuthenticationApiService from '../../features/authentication/services/AuthenticationApiService';
import ErrorCode from '../errors/ErrorCode';

const API = axios.create({
  baseURL: import.meta.env.VITE_API_GATEWAY_URL,
});

function accessTokenIsExpired(error: any): boolean {
  return (
    error?.response?.status === 401 &&
    error?.response?.data?.errorCode !== ErrorCode.WrongUsernameOrPassword &&
    error?.response?.data?.errorCode !== ErrorCode.UserNotActivated &&
    // eslint-disable-next-line no-underscore-dangle
    !error?.config?._retry
  );
}

async function refreshToken(originalRequest: any): Promise<void> {
  try {
    const token = TokenService.getRefreshToken();
    if (!token) {
      // noinspection ExceptionCaughtLocallyJS
      throw new Error('Refresh token deleted');
    }
    const tokenResponse = await AuthenticationApiService.refreshToken(token);
    TokenService.setAccessToken(
      tokenResponse.accessToken,
      tokenResponse.expirationAccessTokenTime
    );
    TokenService.setRefreshToken(
      tokenResponse.refreshToken,
      tokenResponse.expirationRefreshTokenTime
    );
    // eslint-disable-next-line no-underscore-dangle,no-param-reassign
    originalRequest._retry = true;
    return await API(originalRequest);
  } catch (e) {
    window.location.href = '/sign-in';
    return Promise.resolve();
  }
}

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
    if (accessTokenIsExpired(error)) {
      await refreshToken(error.config);
    }
    return Promise.reject(error);
  }
);

export default API;

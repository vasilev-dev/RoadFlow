// eslint-disable-next-line import/no-cycle
import API from '../../../lib/api/AxiosFactory';
import { TokenResponse } from '../../../types/responses/TokenResponse';

export default class AuthenticationApiService {
  public static async signUp(
    email: string,
    username: string,
    password: string
  ): Promise<void> {
    return API.post('/sign-up', {
      email,
      username,
      password,
    });
  }

  public static async signIn(
    username: string,
    password: string
  ): Promise<TokenResponse> {
    const { data } = await API.post<TokenResponse>('/sign-in', {
      username,
      password,
    });

    return data;
  }

  public static async refreshToken(
    refreshToken: string
  ): Promise<TokenResponse> {
    const { data } = await API.post<TokenResponse>('/refresh-token', {
      refreshToken,
    });

    return data;
  }
}

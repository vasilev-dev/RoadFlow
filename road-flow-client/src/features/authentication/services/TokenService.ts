import Cookies, { CookieSetOptions } from 'universal-cookie';

export default class TokenService {
  private static cookies = new Cookies();

  private static ACCESS_TOKEN_COOKIE_NAME = 'access_token';

  private static EXPIRATION_ACCESS_TOKEN_COOKIE_NAME =
    'expiration_access_token_time';

  private static REFRESH_TOKEN_COOKIE_NAME = 'refresh_token';

  private static EXPIRATION_REFRESH_TOKEN_COOKIE_NAME =
    'expiration_refresh_token_time';

  private static privateCookieOptions: CookieSetOptions = {
    path: '/',
    httpOnly: false,
    secure: false,
    sameSite: 'strict',
  };

  public static setAccessToken(
    accessToken: string,
    expirationAccessTokenTime: Date
  ): void {
    this.cookies.set(this.ACCESS_TOKEN_COOKIE_NAME, accessToken, {
      ...this.privateCookieOptions,
      expires: new Date(expirationAccessTokenTime),
    });
    this.setExpirationAccessTokenTime(expirationAccessTokenTime);
  }

  private static setExpirationAccessTokenTime(
    expirationAccessTokenTime: Date
  ): void {
    this.cookies.set(
      this.EXPIRATION_ACCESS_TOKEN_COOKIE_NAME,
      expirationAccessTokenTime,
      this.privateCookieOptions
    );
  }

  public static setRefreshToken(
    refreshToken: string,
    expirationRefreshTokenTime: Date
  ): void {
    this.cookies.set(this.REFRESH_TOKEN_COOKIE_NAME, refreshToken, {
      ...this.privateCookieOptions,
      expires: new Date(expirationRefreshTokenTime),
    });
    this.setExpirationRefreshTokenTime(expirationRefreshTokenTime);
  }

  private static setExpirationRefreshTokenTime(
    expirationRefreshTokenTime: Date
  ): void {
    this.cookies.set(
      this.EXPIRATION_REFRESH_TOKEN_COOKIE_NAME,
      expirationRefreshTokenTime,
      this.privateCookieOptions
    );
  }

  public static getAccessToken(): string | undefined {
    return this.cookies.get(this.ACCESS_TOKEN_COOKIE_NAME);
  }

  public static getRefreshToken(): string | undefined {
    return this.cookies.get(this.REFRESH_TOKEN_COOKIE_NAME);
  }
}

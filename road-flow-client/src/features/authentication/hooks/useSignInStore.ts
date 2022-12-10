import create from 'zustand';
import ErrorHandler from '../../../lib/errors/ErrorHandler';
import AuthenticationApiService from '../services/AuthenticationApiService';
import TokenService from '../services/TokenService';

interface SignInStoreProps {
  errorMessage?: string;
  isLoading: boolean;
  signIn: (username: string, password: string) => Promise<void>;
}

const useSignInStore = create<SignInStoreProps>((set) => ({
  error: undefined,
  isLoading: false,
  signIn: async (username: string, password: string) => {
    set({ errorMessage: undefined, isLoading: true });
    try {
      const tokenResponse = await AuthenticationApiService.signIn(
        username,
        password
      );
      TokenService.setAccessToken(
        tokenResponse.accessToken,
        tokenResponse.expirationAccessTokenTime
      );
      TokenService.setRefreshToken(
        tokenResponse.refreshToken,
        tokenResponse.expirationRefreshTokenTime
      );
    } catch (error) {
      set({ errorMessage: ErrorHandler.getMessageFromError(error) });
    } finally {
      set({ isLoading: false });
    }
  },
}));

export default useSignInStore;

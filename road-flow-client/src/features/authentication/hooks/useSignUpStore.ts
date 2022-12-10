import create from 'zustand';
import ErrorHandler from '../../../lib/errors/ErrorHandler';
import AuthenticationApiService from '../services/AuthenticationApiService';

interface SignUpStoreProps {
  errorMessage?: string;
  isLoading: boolean;
  signUp: (email: string, username: string, password: string) => Promise<void>;
}

const useSignUpStore = create<SignUpStoreProps>((set) => ({
  error: undefined,
  isLoading: false,
  signUp: async (email: string, username: string, password: string) => {
    set({ errorMessage: undefined, isLoading: true });
    try {
      await AuthenticationApiService.signUp(email, username, password);
    } catch (error) {
      set({ errorMessage: ErrorHandler.getMessageFromError(error) });
    } finally {
      set({ isLoading: false });
    }
  },
}));

export default useSignUpStore;

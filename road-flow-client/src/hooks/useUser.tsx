import TokenService from '../features/authentication/services/TokenService';

function useUser() {
  return { authorized: !!TokenService.getAccessToken() };
}

export default useUser;

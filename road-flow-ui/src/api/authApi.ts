import axios from 'axios';
import {identityEndpoints} from './apiEndpoints';
import TokenResponse from './types/tokenResponse';

// todo use axios instance for identity server
const IDENTITY_SERVER_URL = process.env.REACT_APP_IDENTITY_SERVER_URL;

abstract class AuthApi {
    public static async signUp(email: string, username: string, password: string): Promise<TokenResponse> {
        const response = await axios.post<TokenResponse>(IDENTITY_SERVER_URL + identityEndpoints.user.signUp, {
            email,
            username,
            password,
        });

        return response.data;
    }
}

export default AuthApi;

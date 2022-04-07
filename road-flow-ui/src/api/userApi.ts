import axios from "axios";
import {identityEndpoints} from "./apiEndpoints";

const IDENTITY_SERVER_URL = process.env.REACT_APP_IDENTITY_SERVER_URL;

export async function signUp(email: string, username: string, password: string): Promise<void> {
    await axios.post(IDENTITY_SERVER_URL + identityEndpoints.user.signUp, {
        email,
        username,
        password
    });
}

import axios, {AxiosError} from "axios";
import {ClientErrorMessage} from "../api/clientErrors";
import {UseToastOptions} from "@chakra-ui/react";

export function handleClientError(error: unknown, handleFn: (errorMessage: string) => void) {
    const somethingWentWrongMessage = 'Something went wrong 🥺'

    if (!axios.isAxiosError(error)) {
        handleFn(somethingWentWrongMessage);
        throw error;
    }

    const axiosError = error as AxiosError;
    const errorCode = axiosError.response?.data?.errorCode;

    if (!errorCode) {
        handleFn(somethingWentWrongMessage);
        throw error;
    }

    const clientErrorMessage = ClientErrorMessage.get(errorCode);

    if (clientErrorMessage) {
        handleFn(clientErrorMessage);
        return;
    }

    handleFn(somethingWentWrongMessage);
    throw new Error(`Not implemented error code: ${errorCode}`);
}

export function getToastDefaultParams(errorMessage: string): UseToastOptions {
    return {
        title: 'Oops...',
        description: errorMessage,
        status: 'error',
        duration: 9000,
        isClosable: true
    }
}

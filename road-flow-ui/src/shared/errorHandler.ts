import axios, {AxiosError} from "axios";
import {ClientErrorMessage} from "../api/clientErrors";
import {UseToastOptions} from "@chakra-ui/react";

// todo перенести это в настройки axios, ловить когда сервер не отвечает
// сделать компонент-обертку на ошибки и открывать toast (нужен store)
export function handleClientError(error: unknown, handleFn: (errorMessage: string) => void) {
    const somethingWentWrongMessage = 'Something went wrong 🥺'

    if (!axios.isAxiosError(error)) {
        handleFn(somethingWentWrongMessage);
        throw error;
    }

    const axiosError = error as AxiosError;
    const errorCode = axiosError.response?.data?.errorCode;

    if (!errorCode) {
        console.log(axiosError)

        handleFn(somethingWentWrongMessage);
        throw error;
    }

    if (ClientErrorMessage.has(errorCode)) {
        // @ts-ignore
        handleFn(ClientErrorMessage.get(errorCode));
        return;
    }

    handleFn(somethingWentWrongMessage);
    console.log(4);
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

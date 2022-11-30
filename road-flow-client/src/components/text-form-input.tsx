import React from 'react';
import {
    FormControl,
    FormErrorMessage,
    FormLabel, Icon,
    Input,
    InputGroup,
    InputProps, InputRightElement
} from "@chakra-ui/react";
import {IconType} from "react-icons";

type TextFormInputProps = InputProps & {
    name: string,
    label?: string,
    required?: boolean,
    errorMessage: string | undefined,
    register: any,
    icon?: IconType
};

const TextFormInput: React.FC<TextFormInputProps> = (
    {
        name,
        label,
        required,
        errorMessage,
        register,
        icon,
        ...inputProps
    }) => {

    const DefaultInput: JSX.Element = <Input {...inputProps} {...register(name, {required: required})} />

    const InputWithIcon: JSX.Element  = (
        <InputGroup>
            <InputRightElement pointerEvents='none'>
                <Icon as={icon} />
            </InputRightElement>
            {DefaultInput}
        </InputGroup>
    );

    const renderInput = (): JSX.Element => icon ? InputWithIcon : DefaultInput;

    return (
        <FormControl isInvalid={!!errorMessage} isRequired={required}>
            {label && <FormLabel>{label}</FormLabel>}
            {renderInput()}
            <FormErrorMessage>{errorMessage}</FormErrorMessage>
        </FormControl>
    );
};

export default TextFormInput;

import {Field, useField} from 'formik';
import React from 'react';
import {
    Input,
    InputGroup,
    InputRightElement,
    Icon, InputProps,
    FormControl,
    FormLabel,
    FormErrorMessage
} from '@chakra-ui/react';
import {IconType} from 'react-icons';

type TextFieldProps = InputProps & {
    label: string;
    icon?: IconType;
}

const TextField: React.FC<TextFieldProps> = ({ label, icon, ...props }) => {
    // @ts-ignore
    const [field, meta] = useField(props);
    const {m, mt, mr, mb, ml, p, pt, pr, pb, pl, ...inputProps} = props;
    const formControlProps = {m, mt, mr, mb, ml, p, pt, pr, pb, pl};

    const DefaultInput = <Field as={Input} {...field} {...inputProps} />;
    const InputWithIcon = (
        <InputGroup>
            <InputRightElement
                pointerEvents='none'
                children={<Icon as={icon}/>}
            />
            {DefaultInput}
        </InputGroup>
    );

    const renderInput = () => !!icon ? InputWithIcon : DefaultInput;

    return (
        <FormControl isInvalid={!!meta.error && meta.touched} {...formControlProps}>
            <FormLabel>{label}</FormLabel>
            {renderInput()}
            <FormErrorMessage>{meta.error}</FormErrorMessage>
        </FormControl>
    );
};


export default TextField;

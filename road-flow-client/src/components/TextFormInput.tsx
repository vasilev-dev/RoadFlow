import React from 'react';
import {
  FormControl,
  FormErrorMessage,
  FormLabel,
  Icon,
  Input,
  InputGroup,
  InputProps,
  InputRightElement,
} from '@chakra-ui/react';
import { IconType } from 'react-icons';

type TextFormInputProps = InputProps & {
  name: string;
  label?: string;
  required?: boolean;
  errorMessage: string | undefined;
  // eslint-disable-next-line
  register: any;
  icon?: IconType;
};

function TextFormInput({
  name,
  label,
  required,
  errorMessage,
  register,
  icon,
  ...inputProps
}: TextFormInputProps) {
  const DefaultInput: JSX.Element = (
    // eslint-disable-next-line react/jsx-props-no-spreading
    <Input {...inputProps} {...register(name, { required })} />
  );

  const InputWithIcon: JSX.Element = (
    <InputGroup>
      <InputRightElement pointerEvents="none">
        <Icon as={icon} />
      </InputRightElement>
      {DefaultInput}
    </InputGroup>
  );

  const renderInput = (): JSX.Element => (icon ? InputWithIcon : DefaultInput);

  return (
    <FormControl isInvalid={!!errorMessage} isRequired={required}>
      {label && <FormLabel>{label}</FormLabel>}
      {renderInput()}
      <FormErrorMessage>{errorMessage}</FormErrorMessage>
    </FormControl>
  );
}

TextFormInput.defaultProps = {
  label: undefined,
  required: false,
  icon: undefined,
};

export default TextFormInput;

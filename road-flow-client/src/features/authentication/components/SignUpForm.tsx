import React from 'react';
import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { Button } from '@chakra-ui/react';
import { AiOutlineUser } from 'react-icons/all';
import { MdOutlineAlternateEmail } from 'react-icons/md';
import shallow from 'zustand/shallow';
import usernameValidator from '../../../lib/validators/UsernameValidator';
import passwordValidator from '../../../lib/validators/PasswordValidator';
import TextFormInput from '../../../components/TextFormInput';
import useSignUpStore from '../hooks/useSignUpStore';
import ErrorMessage from '../../../components/ErrorMessage';

const schema = yup.object().shape({
  email: yup.string().required('Email is required').email('Incorrect email'),
  username: usernameValidator,
  password: passwordValidator,
  confirmPassword: yup
    .string()
    .oneOf([yup.ref('password'), null], 'Password must match'),
});

type SignUpFormInputs = {
  email: string;
  username: string;
  password: string;
  confirmPassword: string;
};

function SignUpForm() {
  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm<SignUpFormInputs>({
    mode: 'onBlur',
    resolver: yupResolver(schema),
  });

  const { errorMessage, isLoading, signUp } = useSignUpStore(
    (state) => ({
      isLoading: state.isLoading,
      errorMessage: state.errorMessage,
      signUp: state.signUp,
    }),
    shallow
  );

  const onSubmit = async (values: SignUpFormInputs): Promise<void> => {
    await signUp(values.email, values.username, values.password);
  };

  return (
    <form>
      <TextFormInput
        name="email"
        label="Email"
        errorMessage={errors?.email?.message}
        register={register}
        type="email"
        placeholder="name@mail.com"
        required
        icon={MdOutlineAlternateEmail}
      />

      <TextFormInput
        name="username"
        label="Username"
        errorMessage={errors?.username?.message}
        register={register}
        type="text"
        placeholder="ElonMask"
        required
        icon={AiOutlineUser}
      />

      <TextFormInput
        name="password"
        label="Password"
        errorMessage={errors?.password?.message}
        register={register}
        type="password"
        required
      />

      <TextFormInput
        name="confirmPassword"
        label="Confirm password"
        errorMessage={errors?.confirmPassword?.message}
        register={register}
        type="password"
        required
      />

      <Button
        onClick={handleSubmit(onSubmit)}
        colorScheme="blue"
        disabled={!isValid}
        isLoading={isLoading}
      >
        Login
      </Button>

      <ErrorMessage title="Sing up failed" errorMessage={errorMessage} />
    </form>
  );
}

export default SignUpForm;

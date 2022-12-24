import React from 'react';
import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import { AiOutlineUser } from 'react-icons/all';
import { Button } from '@chakra-ui/react';
import shallow from 'zustand/shallow';
import { useNavigate } from 'react-router-dom';
import ErrorMessage from '../../../components/ErrorMessage';
import useSignInStore from '../hooks/useSignInStore';
import TextFormInput from '../../../components/TextFormInput';
import passwordValidator from '../../../lib/validators/PasswordValidator';
import usernameValidator from '../../../lib/validators/UsernameValidator';

const schema = yup.object().shape({
  username: usernameValidator,
  password: passwordValidator,
});

type SignInFormInputs = {
  username: string;
  password: string;
};

function SignInForm() {
  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm<SignInFormInputs>({
    mode: 'onBlur',
    resolver: yupResolver(schema),
  });

  const { errorMessage, isLoading, signIn } = useSignInStore(
    (state) => ({
      isLoading: state.isLoading,
      errorMessage: state.errorMessage,
      signIn: state.signIn,
    }),
    shallow
  );

  const navigate = useNavigate();

  const onSubmit = async (values: SignInFormInputs): Promise<void> => {
    await signIn(values.username, values.password);
    navigate('/');
  };

  return (
    <form>
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

      <Button
        type="submit"
        onClick={handleSubmit(onSubmit)}
        colorScheme="blue"
        disabled={!isValid}
        isLoading={isLoading}
      >
        Login
      </Button>

      <ErrorMessage title="Login failed" errorMessage={errorMessage} />
    </form>
  );
}

export default SignInForm;

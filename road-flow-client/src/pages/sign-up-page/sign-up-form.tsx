import React from 'react';
import * as yup from 'yup';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import {Button} from "@chakra-ui/react";
import usernameValidator from "../../shared/validators/username-validator";
import passwordValidator from "../../shared/validators/password-validator";
import TextFormInput from "../../components/text-form-input";
import {AiOutlineUser} from "react-icons/all";
import {MdOutlineAlternateEmail, MdPassword} from 'react-icons/md';

const schema = yup.object().shape({
    email: yup.string().required('Email is required').email('Incorrect email'),
    username: usernameValidator,
    password: passwordValidator
});

type LoginFormInputs = {
    email: string;
    username: string;
    password: string;
};

const SignUpForm: React.FC = () => {
    const { register, handleSubmit, formState: {errors} } = useForm<LoginFormInputs>({
        mode: 'onBlur',
        resolver: yupResolver(schema),
    });

    const onSubmit = (values: LoginFormInputs) => console.log(values);

    return (
        <form>
            <TextFormInput
                name='email'
                label='Email'
                errorMessage={errors?.email?.message}
                register={register}
                type='email'
                placeholder='name@mail.com'
                required={true}
                icon={MdOutlineAlternateEmail}
            />

            <TextFormInput
                name='username'
                label='Username'
                errorMessage={errors?.username?.message}
                register={register}
                type='text'
                placeholder='ElonMask'
                required={true}
                icon={AiOutlineUser}
            />

            <TextFormInput
                name='password'
                label='Password'
                errorMessage={errors?.password?.message}
                register={register}
                type='password'
                required={true}
            />

            <Button
                onClick={handleSubmit(onSubmit)}
                colorScheme='blue'
                disabled={!!errors.email || !!errors.password}
            >
                Login
            </Button>
        </form>
    );
}

export default SignUpForm;

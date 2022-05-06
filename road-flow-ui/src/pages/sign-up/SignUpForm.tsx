import {Box, Button, Heading, useToast} from '@chakra-ui/react';
import {Form, Formik} from 'formik';
import TextField from '../../components/TextField';
import {MdOutlineAlternateEmail, MdPassword} from 'react-icons/md';
import {AiOutlineUser} from 'react-icons/ai';
import {signUp} from '../../api/userApi';
import {getToastDefaultParams, handleClientError} from '../../shared/errorHandler';
import * as yup from 'yup';
import React from "react";

const SignUpForm: React.FC = () => {
    const toast = useToast();

    const initialValues: SignUpFormDto = {
        email: '',
        username: '',
        password: '',
        confirmPassword: ''
    };

    async function onSubmit(values: SignUpFormDto): Promise<void> {
        try {
            await signUp(values.email, values.username, values.password);
        }
        catch (error) {
            handleClientError(error, (errorMessage) => toast(getToastDefaultParams(errorMessage)));
        }
        finally {
        }
    }

    return (
        <>
            <Heading>Sign up</Heading>

            <Box pt={5}>
                <Formik
                    initialValues={initialValues}
                    validationSchema={validationScheme}
                    onSubmit={(values) => onSubmit(values)}>
                    {formik => (
                        <Form onSubmit={formik.handleSubmit}>
                            <TextField type={'text'} label={'Email'} name="email" placeholder="example@mail.com" mb={4}
                                       icon={MdOutlineAlternateEmail}/>
                            <TextField type={'text'} label={'Username'} name="username" placeholder="Ryan Gosling"
                                       mb={4} icon={AiOutlineUser}/>
                            <TextField type={'password'} label={'Password'} name="password" mb={4} icon={MdPassword}/>
                            <TextField type={'password'} label={'Confirm password'} name="confirmPassword" mb={4}/>

                            <Button type='submit' w={'100%'} colorScheme='blue' isLoading={false} mt={5}>Create an
                                account</Button>
                        </Form>
                    )}
                </Formik>
            </Box>
        </>
    )
}

const validationScheme = yup.object({
    email: yup.string()
        .required('Email is required')
        .email('Invalid email'),

    username: yup.string()
        .required('Username is required')
        .min(3, 'Minimum length of username is 3 characters')
        .max(32, 'Maximum length of username is 32 characters'),

    password: yup.string()
        .required('Password is required')
        .min(8, 'Minimum length of password is 8 characters')
        .max(32, 'Maximum length of password is 32 characters')
        .matches(/[A-Z]+/, 'Password must contains at a least one uppercase letter')
        .matches(/[a-z]+/, 'Password must contains at a least one lowercase letter')
        .matches(/\d+/, 'Password must contains at a least one digit')
        .matches(/[@$!%*?&]+/, 'Password must contains at a least one special symbol'),

    confirmPassword: yup.string()
        .required('Confirm password is required')
        .oneOf([yup.ref('password'), null], 'Passwords must match')
});

type SignUpFormDto = {
    email: string;
    username: string;
    password: string;
    confirmPassword: string;
}

export default SignUpForm;

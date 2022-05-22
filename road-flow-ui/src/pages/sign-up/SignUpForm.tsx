import {Box, Button, Heading, useToast} from '@chakra-ui/react';
import {Form, Formik} from 'formik';
import TextField from '../../components/TextField';
import {MdOutlineAlternateEmail, MdPassword} from 'react-icons/md';
import {AiOutlineUser} from 'react-icons/ai';
import {getToastDefaultParams, handleClientError} from '../../shared/errorHandler';
import * as yup from 'yup';
import React, {useState} from 'react';
import {observer} from 'mobx-react-lite';
import AuthApi from '../../api/authApi';
import JwtTokenStore from '../../stores/jwtTokenStore';
import {useNavigate} from 'react-router-dom';

const SignUpForm: React.FC = () => {
    const toast = useToast();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const initialValues: SignUpFormDto = {
        email: '',
        username: '',
        password: '',
        confirmPassword: '',
    };

    const onSubmit = async (values: SignUpFormDto): Promise<void> => {
        setIsLoading(true);
        try {
            const {accessToken, expirationAccessTokenTime, refreshToken, expirationRefreshTokenTime} =
                await AuthApi.signUp(values.email, values.username, values.password);
            saveTokens(accessToken, expirationAccessTokenTime, refreshToken, expirationRefreshTokenTime);
            navigate('/home');
        } catch (error) {
            handleClientError(error, errorMessage => toast(getToastDefaultParams(errorMessage)));
        } finally {
            setIsLoading(false);
        }
    };

    const saveTokens = (
        accessToken: string,
        expirationAccessTokenTime: Date,
        refreshToken: string,
        expirationRefreshTokenTime: Date
    ): void => {
        const jwtCookieStore = new JwtTokenStore();
        try {
            jwtCookieStore.setAccessToken(accessToken, expirationAccessTokenTime);
            jwtCookieStore.setRefreshToken(refreshToken, expirationRefreshTokenTime);
            // todo save user info to cookie
        } catch (error) {
            toast(getToastDefaultParams('Success registration, but sign in is failed. Try sign in later...'));
            throw error;
        }
    };

    return (
        <>
            <Heading>Sign up</Heading>

            <Box mt={4}>
                <Formik
                    enableReinitialize
                    initialValues={initialValues}
                    validationSchema={validationScheme}
                    onSubmit={async values => await onSubmit(values)}
                >
                    {({handleSubmit}) => (
                        <Form onSubmit={handleSubmit}>
                            <TextField
                                type={'text'}
                                label={'Email'}
                                name='email'
                                placeholder='example@mail.com'
                                mb={2}
                                icon={MdOutlineAlternateEmail}
                            />
                            <TextField
                                type={'text'}
                                label={'Username'}
                                name='username'
                                placeholder='Ryan Gosling'
                                mb={2}
                                icon={AiOutlineUser}
                            />
                            <TextField type={'password'} label={'Password'} name='password' mb={2} icon={MdPassword} />
                            <TextField type={'password'} label={'Confirm password'} name='confirmPassword' mb={2} />

                            <Button type='submit' w={'100%'} colorScheme='blue' isLoading={isLoading} mt={2}>
                                Create an account
                            </Button>
                        </Form>
                    )}
                </Formik>
            </Box>
        </>
    );
};

const validationScheme = yup.object({
    email: yup.string().required('Email is required').email('Invalid email'),

    username: yup
        .string()
        .required('Username is required')
        .min(3, 'Minimum length of username is 3 characters')
        .max(32, 'Maximum length of username is 32 characters'),

    password: yup
        .string()
        .required('Password is required')
        .min(8, 'Minimum length of password is 8 characters')
        .max(32, 'Maximum length of password is 32 characters')
        .matches(/[A-Z]+/, 'Password must contains at a least one uppercase letter')
        .matches(/[a-z]+/, 'Password must contains at a least one lowercase letter')
        .matches(/\d+/, 'Password must contains at a least one digit')
        .matches(/[@$!%*?&]+/, 'Password must contains at a least one special symbol'),

    confirmPassword: yup
        .string()
        .required('Confirm password is required')
        .oneOf([yup.ref('password'), null], 'Passwords must match'),
});

type SignUpFormDto = {
    email: string;
    username: string;
    password: string;
    confirmPassword: string;
};

export default observer(SignUpForm);

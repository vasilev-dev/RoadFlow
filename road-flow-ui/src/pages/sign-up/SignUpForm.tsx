import {Box, Button, Flex, Heading, Text, useToast} from '@chakra-ui/react';
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
import usernameValidator from '../../shared/validators/usernameValidator';
import passwordValidator from '../../shared/validators/passwordValidator';

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
            <Flex flexDir={'row'}>
                <Heading>
                    Sign up&nbsp;
                    <Text color={'blue.500'} display={'inline-block'}>
                        RoadFlow
                    </Text>
                </Heading>
            </Flex>

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
                                placeholder='RyanGosling'
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

    username: usernameValidator,

    password: passwordValidator,

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

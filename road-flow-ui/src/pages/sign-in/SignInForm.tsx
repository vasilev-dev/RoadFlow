import React from 'react';
import * as yup from 'yup';
import {Heading, Text, Flex, Box, Button, useToast, Image} from '@chakra-ui/react';
import {Form, Formik} from 'formik';
import TextField from '../../components/TextField';
import {MdPassword} from 'react-icons/md';
import {AiOutlineUser} from 'react-icons/ai';
import {useNavigate} from 'react-router-dom';
import usernameValidator from '../../shared/validators/usernameValidator';
import passwordValidator from '../../shared/validators/passwordValidator';

const SignInForm: React.FC = () => {
    const toast = useToast();
    const navigate = useNavigate();

    const initialValues: SignInFormValue = {
        username: '',
        password: '',
    };

    const onSubmit = async (values: SignInFormValue): Promise<void> => {
        console.log(values);
    };

    return (
        <>
            <Flex flexDir={'row'}>
                <Heading>
                    Sign in&nbsp;
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
                                label={'Username'}
                                name='username'
                                placeholder='RyanGosling'
                                mb={2}
                                icon={AiOutlineUser}
                            />
                            <TextField type={'password'} label={'Password'} name='password' mb={2} icon={MdPassword} />

                            <Button type='submit' w={'100%'} colorScheme={'blue'} isLoading={false} mt={4}>
                                Sign In
                            </Button>
                        </Form>
                    )}
                </Formik>
            </Box>
        </>
    );
};

const validationScheme = yup.object({
    username: usernameValidator,
    password: passwordValidator,
});

type SignInFormValue = {
    username: string;
    password: string;
};

export default SignInForm;

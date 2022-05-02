import {
    Grid,
    GridItem,
    Text,
    Link,
    Flex,
    Heading,
    Box,
    Image,
    Center,
    Icon,
    Button,
    Divider, useToast,
} from '@chakra-ui/react';
import {Link as ReachLink} from 'react-router-dom';
import AnimatedCar from '../assets/animated-car.gif';
import {MdOutlineAlternateEmail, MdPassword} from 'react-icons/md';
import {AiOutlineUser} from 'react-icons/ai';
import {FcGoogle} from 'react-icons/fc';
// @ts-ignore
import LightSpeed from 'react-reveal/LightSpeed';
import * as yup from "yup";
import {Form, Formik} from "formik";
import {signUp} from "../api/userApi";
import TextField from "../components/TextField";
import {getToastDefaultParams, handleClientError} from "../shared/errorHandler";
import {useState} from "react";

function SignUp() {
    return (
        <Grid templateColumns='repeat(2, 1fr)'>
            <GridItem minH={'100vh'} py={[4, 10]} px={[7, 10, 20]} colSpan={[2, 1]} order={[2, 1]}>
                <Flex flexDir={'column'} h={'100%'}>
                    <Box flexGrow={0}>
                        <HaveAnAccount/>
                    </Box>
                    <Flex py={5} h={'full'} flexGrow={1} flexDir={'column'} justifyContent={'center'}>
                        <SignUpForm/>
                    </Flex>
                </Flex>
            </GridItem>
            <GridItem h={'100vh'} bg='#472d6a' colSpan={[2, 1]} order={[1, 2]} py={10} px={[7, 10, 20]} textAlign={'center'}>
                <Flex flexDir={'column'} justifyContent={'center'} h={'100%'}>
                    <LightSpeed left>
                        <Heading textColor={'white'}>Welcome to RoadFlow</Heading>
                        <Text py={5} fontWeight={200} color={'gray.200'}>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                        </Text>
                    </LightSpeed>
                    <Image src={AnimatedCar} alt='Animated car'/>
                </Flex>
            </GridItem>
        </Grid>
    );
}

function HaveAnAccount() {
    return (
        <Flex dir={'row'} gap={2}>
            <Text color={'gray.400'}>Have an account?</Text>
            <Link as={ReachLink} to='/sign-in' color={'#3182ce'} fontWeight={600}>Sign In</Link>
        </Flex>
    );
}

type SignUpFormDto = {
    email: string;
    username: string;
    password: string;
    confirmPassword: string;
}

function SignUpForm() {
    const toast = useToast();
    const [isLoading, setIsLoading] = useState(false);

    const initialValues: SignUpFormDto = {
        email: '',
        username: '',
        password: '',
        confirmPassword: ''
    };

    const validationScheme = yup.object({
        email: yup.string()
            .required("Email is required")
            .email("Invalid email"),

        username: yup.string()
            .required("Username is required")
            .min(3, "Minimum length of username is 3 characters")
            .max(32, "Maximum length of username is 32 characters"),

        password: yup.string()
            .required("Password is required")
            .min(8, "Minimum length of password is 8 characters")
            .max(32, "Maximum length of password is 32 characters")
            .matches(/[A-Z]+/, "Password must contains at a least one uppercase letter")
            .matches(/[a-z]+/, "Password must contains at a least one lowercase letter")
            .matches(/\d+/, "Password must contains at a least one digit")
            .matches(/[@$!%*?&]+/, "Password must contains at a least one special symbol"),

        confirmPassword: yup.string()
            .required("Confirm password is required")
            .oneOf([yup.ref('password'), null], 'Passwords must match')
    })

    async function onSubmit(values: SignUpFormDto): Promise<void> {
        try {
            setIsLoading(true);
            await signUp(values.email, values.username, values.password);
        }
        catch (error) {
            handleClientError(error, (errorMessage) => toast(getToastDefaultParams(errorMessage)));
        }
        finally {
            setIsLoading(false);
        }
    }

    return (
        <>
            <Heading>Sign up</Heading>

            <Box py={5}>
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

            <Divider mb={[1, 4]}/>

            <Text color={'gray.400'} align={'center'}>Or sign up with</Text>

            <Center mt={[1, 4]}>
                <Button colorScheme='whiteAlpha' border={'1px'} borderColor={'gray.200'} size={'sm'}>
                    <Icon as={FcGoogle} fontSize={[14, 26]}/>
                </Button>
            </Center>
        </>
    );
}

export default SignUp;

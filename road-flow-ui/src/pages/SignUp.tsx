import {
    Grid,
    GridItem,
    Text,
    Link,
    Flex,
    Heading,
    Box,
    FormControl,
    FormLabel,
    Input,
    InputGroup,
    InputRightElement,
    Image,
    Center,
    Icon, Button, Divider, FormErrorMessage
} from '@chakra-ui/react';
import {Link as ReachLink} from 'react-router-dom';
import AnimatedCar from '../assets/animated-car.gif';
import {MdOutlineAlternateEmail, MdPassword} from 'react-icons/md';
import {AiOutlineUser} from 'react-icons/ai';
import {FcGoogle} from 'react-icons/fc';
// @ts-ignore
import LightSpeed from 'react-reveal/LightSpeed';
import {useForm} from 'react-hook-form';
import emailPattern from "../shared/form/emailPattern";
import passwordPattern from "../shared/form/passwordPattern";

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

function SignUpForm() {
    const {
        handleSubmit,
        register,
        formState: { errors, isSubmitting },
        getValues
    } = useForm();

    function onSubmit(values: any): Promise<void> {
        return new Promise((resolve) => {
            setTimeout(() => {
                alert(JSON.stringify(values, null, 2));
                resolve();
            }, 3000)
        })
    }

    return (
        <>
            <Heading>Sign up</Heading>

            <Box py={5}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <FormControl mb={4} isInvalid={errors.email}>
                        <FormLabel htmlFor='email'>Email address</FormLabel>
                        <InputGroup size={'sm'}>
                            <InputRightElement
                                pointerEvents='none'
                                children={<Icon as={MdOutlineAlternateEmail}/>}
                            />
                            <Input
                                id='email'
                                type='email'
                                placeholder={"example@mail.com"}
                                {...register('email', {
                                    required: 'Email is required',
                                    pattern: emailPattern
                                })}
                            />
                        </InputGroup>
                        <FormErrorMessage fontSize={'sm'}>
                            {errors.email && errors.email.message}
                        </FormErrorMessage>
                    </FormControl>

                    <FormControl mb={4} isInvalid={errors.username}>
                        <FormLabel htmlFor='username'>Username</FormLabel>
                        <InputGroup size={'sm'}>
                            <InputRightElement
                                pointerEvents='none'
                                children={<Icon as={AiOutlineUser}/>}
                            />
                            <Input
                                id='username'
                                type='text'
                                required={true}
                                placeholder={"Ryan Gosling"}
                                {...register('username', {
                                    required: 'Username is required',
                                    minLength: {
                                        value: 3,
                                        message: 'Minimum length is 3'
                                    },
                                    maxLength: {
                                        value: 32,
                                        message: 'Maximum length is 32'
                                    }
                                })}
                                isInvalid={errors.username}
                            />
                        </InputGroup>
                        <FormErrorMessage fontSize={'sm'}>
                            {errors.username && errors.username.message}
                        </FormErrorMessage>
                    </FormControl>

                    <FormControl mb={4} isInvalid={errors.password}>
                        <FormLabel htmlFor='password'>Password</FormLabel>
                        <InputGroup size={'sm'}>
                            <InputRightElement
                                pointerEvents='none'
                                children={<Icon as={MdPassword}/>}
                            />
                            <Input
                                id='password'
                                type='password'
                                required={true}
                                {...register('password', {
                                    required: 'Password is required',
                                    minLength: {
                                        value: 8,
                                        message: 'Minimum length is 8'
                                    },
                                    maxLength: {
                                        value: 32,
                                        message: 'Maximum length is 32'
                                    },
                                    pattern: passwordPattern
                                })}
                            />
                        </InputGroup>
                        <FormErrorMessage fontSize={'sm'}>
                            {errors.password && errors.password.message}
                        </FormErrorMessage>
                    </FormControl>

                    <FormControl mb={4} isInvalid={errors.confirmPassword}>
                        <FormLabel htmlFor='confirm-password'>Confirm password</FormLabel>
                        <Input
                            id='confirm-password'
                            type='password'
                            required={true}
                            size={'sm'}
                            {...register('confirmPassword', {
                                validate: value => value === getValues('password') || 'Passwords don\'t match'
                            })}
                        />
                        <FormErrorMessage fontSize={'sm'}>
                            {errors.confirmPassword && errors.confirmPassword.message}
                        </FormErrorMessage>
                    </FormControl>

                    <Button w={'100%'} colorScheme='blue' isLoading={isSubmitting} type='submit'>Create an account</Button>
                </form>
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

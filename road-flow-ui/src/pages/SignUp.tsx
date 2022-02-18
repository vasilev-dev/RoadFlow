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
    Icon, Button, Divider
} from '@chakra-ui/react';
import { Link as ReachLink } from 'react-router-dom';
import AnimatedCar from '../assets/animated-car.gif';
import {MdOutlineAlternateEmail, MdPassword} from 'react-icons/md';
import {AiOutlineUser} from 'react-icons/ai';
import {FcGoogle} from 'react-icons/fc';
// @ts-ignore
import Fade from 'react-reveal/Fade';

function SignUp() {
    return (
        <Grid w={'100vw'} h={'100vh'} templateColumns='repeat(2, 1fr)'>
            <GridItem w='100%' h='100%' px={40} py={10}>
                <HaveAnAccount/>
                <SignUpForm/>
            </GridItem>
            <GridItem w='100%' h='100%' bg='#472d6a'>
                <Center h={'100%'} w={'100%'}>
                    <Image src={AnimatedCar} alt='Animated car' mt={-20}/>
                </Center>
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
    return (
        <Box pt={24}>
            <Fade top>
                <Heading>Welcome to RoadFlow</Heading>
            </Fade>
            <Text py={5} fontWeight={200} color={'gray.400'}>Lorem ipsum dolor sit amet, consectetur adipisicing elit.</Text>

            <Box py={5}>
                <FormControl mb={4}>
                    <FormLabel htmlFor='email'>Email address</FormLabel>
                    <InputGroup size={'sm'}>
                        <InputRightElement
                            pointerEvents='none'
                            children={<Icon as={MdOutlineAlternateEmail} />}
                        />
                        <Input
                            id='email'
                            type='email'
                            required={true}
                            placeholder={"example@mail.com"}
                            //value={input}
                            //onChange={handleInputChange}
                        />
                    </InputGroup>

                </FormControl>

                <FormControl mb={4}>
                    <FormLabel htmlFor='username'>Username</FormLabel>
                    <InputGroup size={'sm'}>
                        <InputRightElement
                            pointerEvents='none'
                            children={<Icon as={AiOutlineUser} />}
                        />
                        <Input
                            id='username'
                            type='text'
                            required={true}
                            placeholder={"Ryan Gosling"}
                            //value={input}
                            //onChange={handleInputChange}
                        />
                    </InputGroup>
                </FormControl>

                <FormControl mb={4}>
                    <FormLabel htmlFor='email'>Password</FormLabel>
                    <InputGroup size={'sm'}>
                        <InputRightElement
                            pointerEvents='none'
                            children={<Icon as={MdPassword} />}
                        />
                        <Input
                            id='username'
                            type='password'
                            required={true}
                            //value={input}
                            //onChange={handleInputChange}
                        />
                    </InputGroup>
                </FormControl>

                <FormControl>
                    <FormLabel htmlFor='confirm-password'>Confirm password</FormLabel>
                    <Input
                        id='confirm-password'
                        type='password'
                        required={true}
                        size={'sm'}
                        //value={input}
                        //onChange={handleInputChange}
                    />
                </FormControl>
            </Box>

            <Button w={'100%'} colorScheme='blue' mt={4}>Create an account</Button>

            <Divider my={4} />

            <Text color={'gray.400'} align={'center'}>Or sign up with</Text>

            <Center mt={4}>
                <Button colorScheme='whiteAlpha' border={'1px'} borderColor={'gray.200'}>
                    <Icon as={FcGoogle} fontSize={28}/>
                </Button>
            </Center>
        </Box>
    );
}



export default SignUp;

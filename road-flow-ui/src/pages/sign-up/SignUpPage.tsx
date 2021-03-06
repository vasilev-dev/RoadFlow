import {Grid, GridItem, Text, Flex, Heading, Box, Image, Divider} from '@chakra-ui/react';
import AnimatedCar from '../../assets/animated-car.gif';
import SignUpForm from './SignUpForm';
import SignWith from '../../components/SignWith';
import React from 'react';
import FixedColorThemeSwitcher from '../../components/FixedColorThemeSwitcher';
import LinkWithDescription from '../../components/LinkWithDescription';
import MotionBox from '../../components/MotionBox';

const SignUpPage: React.FC = () => {
    return (
        <>
            <FixedColorThemeSwitcher />

            <Grid templateColumns={'repeat(2, 1fr)'}>
                <GridItem minH={'100vh'} w={'full'} py={[4, 8]} px={[7, 10, 20]} colSpan={[2, 1]} order={[2, 1]}>
                    <Flex flexDir={'row'} h={'100%'} w={'full'} justifyContent={'center'}>
                        <Flex flexDir={'column'} w={'full'} maxW={'550px'}>
                            <Box flexGrow={0}>
                                <LinkWithDescription
                                    title={'Sign In'}
                                    description={'Have an account?'}
                                    to={'/sign-in'}
                                />
                            </Box>
                            <Flex flexGrow={1} py={[2, 4]} flexDir={'column'} justifyContent={'center'}>
                                <SignUpForm />
                            </Flex>
                            <Box>
                                <Divider my={4} />
                                <SignWith isSignIn={false} />
                            </Box>
                        </Flex>
                    </Flex>
                </GridItem>

                <GridItem
                    minH={'100vh'}
                    bg='#472d6a'
                    colSpan={[2, 1]}
                    order={[1, 2]}
                    py={10}
                    px={[7, 10, 20]}
                    textAlign={'center'}
                >
                    <Flex flexDir={'column'} justifyContent={'center'} h={'100%'} textColor={'white'}>
                        <MotionBox
                            animate={{opacity: [0, 1]}}
                            // @ts-ignore
                            transition={{ease: 'easeIn', duration: 1.5}}
                        >
                            <Heading>Welcome ????</Heading>
                            <Text py={5} fontWeight={200}>
                                Service for creating roadmaps
                            </Text>
                        </MotionBox>
                        <Image src={AnimatedCar} alt='Animated car' />
                    </Flex>
                </GridItem>
            </Grid>
        </>
    );
};

export default SignUpPage;

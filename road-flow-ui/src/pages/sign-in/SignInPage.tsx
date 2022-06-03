import React from 'react';
import FixedColorThemeSwitcher from '../../components/FixedColorThemeSwitcher';
import {Box, Divider, Flex, Grid, GridItem, Heading, Image, Text, useMediaQuery} from '@chakra-ui/react';
import LinkWithDescription from '../../components/LinkWithDescription';
import SignInForm from './SignInForm';
import SignWith from '../../components/SignWith';
import AnimatedCar from '../../assets/sign-in-car.gif';
import MotionBox from '../../components/MotionBox';

const SignInPage: React.FC = () => {
    const [isMobile] = useMediaQuery('(max-width: 30em)');

    return (
        <>
            <FixedColorThemeSwitcher />

            <Grid templateColumns={'repeat(2, 1fr)'}>
                <GridItem minH={'100vh'} py={[4, 8]} px={[7, 10, 20]} colSpan={[2, 1]}>
                    <Flex flexDir={'row'} h={'100%'} w={'full'} justifyContent={'center'}>
                        <Flex flexDir={'column'} w={'full'} maxW={'550px'}>
                            <Box flexGrow={0}>
                                <LinkWithDescription
                                    title={'Sign Up'}
                                    description={"Haven't an account?"}
                                    to={'/sign-up'}
                                />
                            </Box>
                            <Flex flexGrow={1} py={[2, 4]} flexDir={'column'} justifyContent={'center'}>
                                <SignInForm />
                            </Flex>
                            <Flex flexDir={'column'} alignItems={'center'}>
                                <LinkWithDescription
                                    title={'Restore'}
                                    description={'Forgot password?'}
                                    to={'/restore'}
                                />
                                <Divider my={4} />
                                <SignWith isSignIn />
                            </Flex>
                        </Flex>
                    </Flex>
                </GridItem>

                <GridItem
                    hidden={isMobile}
                    minH={'100vh'}
                    bg='#e76081'
                    colSpan={[2, 1]}
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
                            <Heading>Hello again 👋</Heading>
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

export default SignInPage;

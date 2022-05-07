import {
    Grid,
    GridItem,
    Text,
    Flex,
    Heading,
    Box,
    Image,
    Divider
} from '@chakra-ui/react';
import AnimatedCar from '../../assets/animated-car.gif';
// @ts-ignore
import LightSpeed from 'react-reveal/LightSpeed';
import HaveAnAccount from "./HaveAnAccount";
import SignUpForm from "./SignUpForm";
import SignUpWith from "./SignUpWith";
import React from "react";
import FixedColorThemeSwitcher from "../../components/FixedColorThemeSwitcher";

const SignUpPage: React.FC = () => {
    return (
        <>
            <FixedColorThemeSwitcher/>

            <Grid templateColumns={'repeat(2, 1fr)'}>
                <GridItem minH={'100vh'} py={[4, 8]} px={[7, 10, 20]} colSpan={[2, 1]} order={[2, 1]}>
                    <Flex flexDir={'column'} h={'100%'}>
                        <Box flexGrow={0}>
                            <HaveAnAccount/>
                        </Box>
                        <Flex flexGrow={1} py={[2, 4]} flexDir={'column'} justifyContent={'center'}>
                            <SignUpForm/>
                        </Flex>
                        <Box>
                            <Divider my={4}/>
                            <SignUpWith/>
                        </Box>
                    </Flex>
                </GridItem>

                <GridItem minH={'100vh'} bg='#472d6a' colSpan={[2, 1]} order={[1, 2]} py={10} px={[7, 10, 20]} textAlign={'center'}>
                    <Flex flexDir={'column'} justifyContent={'center'} h={'100%'}>
                        <LightSpeed left>
                            <Heading textColor={'white'}>Welcome to RoadFlow</Heading>
                            <Text py={5} fontWeight={200} color={'gray.200'}>
                                Service for creating roadmaps
                            </Text>
                        </LightSpeed>
                        <Image src={AnimatedCar} alt='Animated car'/>
                    </Flex>
                </GridItem>
            </Grid>
        </>
    );
}

export default SignUpPage;

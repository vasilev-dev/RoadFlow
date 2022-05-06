import {Flex, Link, Text} from "@chakra-ui/react";
import {Link as ReachLink} from "react-router-dom";
import React from "react";

const HaveAnAccount: React.FC = () => {
    return (
        <Flex dir={'row'} gap={2}>
            <Text color={'gray.400'}>Have an account?</Text>
            <Link as={ReachLink} to='/sign-in' color={'#3182ce'} fontWeight={600}>Sign In</Link>
        </Flex>
    );
}

export default HaveAnAccount;

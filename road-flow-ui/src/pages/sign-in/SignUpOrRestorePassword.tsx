import React from 'react';
import {Flex} from '@chakra-ui/react';
import LinkWithDescription from '../../components/LinkWithDescription';

const SignUpOrRestorePassword: React.FC = () => {
    return (
        <Flex flexDir={['column', 'row']}>
            <LinkWithDescription title={'Sign Up'} description={"Haven't an account?"} to={'/sign-up'} />
            or
            <LinkWithDescription title={'Restore password'} description={'Forgot password?'} to={'/sign-up'} />
        </Flex>
    );
};

export default SignUpOrRestorePassword;

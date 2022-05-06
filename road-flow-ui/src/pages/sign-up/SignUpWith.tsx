import {Center, IconButton, Text} from '@chakra-ui/react';
import {FcGoogle} from 'react-icons/fc';
import React from 'react';

const SignUpWith: React.FC = () => {
    return (
        <>
            <Text color={'gray.400'} align={'center'}>Or sign up with</Text>

            <Center mt={[1, 4]}>
                <IconButton aria-label={'Sign up with Google'} colorScheme='whiteAlpha' size={'lg'} icon={<FcGoogle/>}/>
            </Center>
        </>
    );
}

export default SignUpWith;

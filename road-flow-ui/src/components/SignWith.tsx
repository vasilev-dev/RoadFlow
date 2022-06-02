import {Center, IconButton, Text} from '@chakra-ui/react';
import {FcGoogle} from 'react-icons/fc';
import React from 'react';

const SignWith: React.FC<SignWithProps> = ({isSignIn}) => {
    const typeOfSign = isSignIn ? 'in' : 'up';

    return (
        <>
            <Text color={'gray.400'} align={'center'}>
                Or sign {typeOfSign} with
            </Text>

            <Center mt={[1, 4]}>
                <IconButton
                    aria-label={`Sign ${typeOfSign} with Google`}
                    colorScheme='whiteAlpha'
                    size={'lg'}
                    icon={<FcGoogle />}
                />
            </Center>
        </>
    );
};

type SignWithProps = {
    isSignIn: boolean;
};

export default SignWith;

import React from 'react';
import {Box, IconButton, useColorMode} from '@chakra-ui/react';
import {BsMoonFill, BsFillSunFill} from 'react-icons/bs';
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-ignore
import Flip from 'react-reveal/Flip';

const FixedColorThemeSwitcher: React.FC = () => {
    const {colorMode, toggleColorMode} = useColorMode();

    const getIcon = (): JSX.Element => {
        const icon = colorMode === 'dark' ? <BsFillSunFill /> : <BsMoonFill />;

        return (
            <Flip duration={1000} left spy={colorMode}>
                {icon}
            </Flip>
        );
    };

    return (
        <Box pos={'fixed'} top={0} right={1} p={3}>
            <IconButton
                icon={getIcon()}
                aria-label={'Switch color theme'}
                bgColor={'transparent'}
                _hover={{
                    background: 'transparent',
                }}
                _active={{
                    background: 'transparent',
                }}
                _focus={{
                    outline: 'none',
                }}
                onClick={() => toggleColorMode()}
            />
        </Box>
    );
};

export default FixedColorThemeSwitcher;

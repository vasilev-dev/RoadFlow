import React from 'react';
import {Box, IconButton, useColorMode} from '@chakra-ui/react';
import {BsMoonFill, BsFillSunFill} from 'react-icons/bs';
import MotionBox from './MotionBox';

const FixedColorThemeSwitcher: React.FC = () => {
    const {colorMode, toggleColorMode} = useColorMode();

    const getIcon = (): JSX.Element => {
        const darkColorMode = colorMode === 'dark';

        const icon = darkColorMode ? <BsFillSunFill /> : <BsMoonFill />;

        const sunAnimation = {
            opacity: [1, 0],
            y: [0, 25],
        };

        const moonAnimation = {
            opacity: [0, 1],
            y: [-25, 0],
        };

        // todo
        // https://stackoverflow.com/questions/63864386/react-framer-motion-onclick-activate-only-the-animation
        const animation = darkColorMode ? sunAnimation : moonAnimation;

        return (
            // <MotionBox
            //     onTap={toggleColorMode}
            //     animate={animation}
            //     // @ts-ignore
            //     transition={{ease: 'easeIn', duration: 1.5}}
            // >
            //     {icon}
            // </MotionBox>
            <MotionBox>{icon}</MotionBox>
        );
    };

    return (
        <Box pos={'fixed'} top={0} right={1} py={[2, 6]} px={2}>
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

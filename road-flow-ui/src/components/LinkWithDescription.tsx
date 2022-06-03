import React from 'react';
import {Flex, Link, Text} from '@chakra-ui/react';
import {Link as ReachLink} from 'react-router-dom';

const LinkWithDescription: React.FC<LinkWithDescriptionProps> = ({title, description, to}) => {
    return (
        <Flex dir={'row'} gap={2}>
            <Text color={'gray.400'}>{description}</Text>
            <Link as={ReachLink} to={to} textColor={'blue.500'} fontWeight={600}>
                {title}
            </Link>
        </Flex>
    );
};

type LinkWithDescriptionProps = {
    title: string;
    description: string;
    to: string;
};

export default LinkWithDescription;

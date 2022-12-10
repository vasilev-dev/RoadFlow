import React from 'react';
import {
  Alert,
  AlertDescription,
  AlertIcon,
  AlertTitle,
  Box,
} from '@chakra-ui/react';

type ErrorMessageProps = {
  title: string;
  errorMessage: string | null | undefined;
};

function ErrorMessage({ title, errorMessage }: ErrorMessageProps) {
  if (errorMessage) {
    return null;
  }

  return (
    <Alert status="error">
      <AlertIcon />
      <Box>
        <AlertTitle>{title}</AlertTitle>
        <AlertDescription>{errorMessage}</AlertDescription>
      </Box>
    </Alert>
  );
}

export default ErrorMessage;

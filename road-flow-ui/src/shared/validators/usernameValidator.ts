import * as yup from 'yup';

const usernameValidator = yup
    .string()
    .required('Username is required')
    .min(3, 'Minimum length of username is 3 characters')
    .max(32, 'Maximum length of username is 32 characters')
    .matches(/^[A-Za-z]/, 'Username must starts with latin letter')
    .matches(/[A-Za-z0-9_]$/, 'Username must contain only latin letters, digits and underscore');

export default usernameValidator;

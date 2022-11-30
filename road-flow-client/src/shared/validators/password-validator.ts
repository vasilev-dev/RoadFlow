import * as yup from 'yup';

const passwordValidator = yup
    .string()
    .required('Password is required')
    .min(8, 'Minimum length of password is 8 characters')
    .max(32, 'Maximum length of password is 32 characters')
    .matches(/[A-Z]+/, 'Password must contains at a least one uppercase letter')
    .matches(/[a-z]+/, 'Password must contains at a least one lowercase letter')
    .matches(/\d+/, 'Password must contains at a least one digit')
    .matches(/[@$!%*?&]+/, 'Password must contains at a least one special symbol');

export default passwordValidator;
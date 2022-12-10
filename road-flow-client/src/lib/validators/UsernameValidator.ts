import * as yup from 'yup';

const usernameValidator = yup
  .string()
  .required('Username is required')
  .min(3, 'Minimum length of username is 3 characters')
  .max(32, 'Maximum length of username is 32 characters')
  .matches(/[a-zA-Z]+/, 'Username must contains at a least one latin letter')
  .matches(
    /^[A-Za-z\d_.]+$/,
    'Username can contains only latin letters, digits, underscores and dots'
  );

export default usernameValidator;

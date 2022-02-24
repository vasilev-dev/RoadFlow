import {ValidationRule} from "react-hook-form";

const emailPattern: ValidationRule<RegExp> = {
  value: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
  message: 'Invalid email address'
};

export default emailPattern;

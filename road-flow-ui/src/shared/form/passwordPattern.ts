import {ValidationRule} from "react-hook-form";

const passwordPattern: ValidationRule<RegExp> = {
    value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$/,
    message: 'Password must contains at least one digit, uppercase and lowercase letter, special symbol'
};

export default passwordPattern;

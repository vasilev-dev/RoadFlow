import * as yup from 'yup';

const schema = yup.object().shape({
  email: yup.string().required('Email is required').email('Incorrect email')
});

type SignUpFormInputs = {
  email: string;
};

type SignInFormInputs = {
  email: string;
};

function ConfirmEmailForm() {
  return (

  );
}

export default ConfirmEmailForm;

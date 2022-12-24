import GuardedElement from './GuardedElement';
import useUser from '../hooks/useUser';

type AuthorizationGuardedElementProps = {
  page: JSX.Element;
};

function AuthorizationGuardedElement({
  page,
}: AuthorizationGuardedElementProps) {
  const { authorized } = useUser();

  return (
    <GuardedElement
      page={page}
      available={authorized}
      alternativePath="/sign-in"
    />
  );
}

export default AuthorizationGuardedElement;

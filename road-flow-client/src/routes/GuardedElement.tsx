import { Navigate } from 'react-router-dom';

export type GuardedElementProps = {
  page: JSX.Element;
  available: boolean;
  alternativePath: string;
};

function GuardedElement({
  page,
  available,
  alternativePath,
}: GuardedElementProps) {
  return available ? page : <Navigate to={alternativePath} />;
}

export default GuardedElement;

import { Outlet, Navigate } from 'react-router-dom';

type GuardedRouteProps = {
  alternativePath: string;
  available: boolean;
};

function GuardedRoute({ alternativePath, available }: GuardedRouteProps) {
  return available ? <Outlet /> : <Navigate to={alternativePath} />;
}

export default GuardedRoute;

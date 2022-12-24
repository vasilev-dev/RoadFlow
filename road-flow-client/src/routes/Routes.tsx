import { BrowserRouter, Route, Routes as ReactRouters } from 'react-router-dom';
import React from 'react';
import SignInPage from '../pages/SignInPage';
import SignUpPage from '../pages/SignUpPage';
import HomePage from '../pages/HomePage';
import AuthorizationGuardedElement from './AuthorizationGuardedElement';
import ConfirmEmailPage from '../pages/ConfirmEmailPage';

function Routes() {
  return (
    <BrowserRouter>
      <ReactRouters>
        <Route path="/sign-in" element={<SignInPage />} />
        <Route path="/sign-up" element={<SignUpPage />} />
        <Route path="/confirm-email" element={<ConfirmEmailPage />} />
        <Route
          path="/"
          element={<AuthorizationGuardedElement page={<HomePage />} />}
        />
      </ReactRouters>
    </BrowserRouter>
  );
}

export default Routes;

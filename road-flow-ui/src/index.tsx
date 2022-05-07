import React from 'react';
import {createRoot} from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {ChakraProvider} from '@chakra-ui/react'
import {BrowserRouter, Routes, Route} from 'react-router-dom';
import Home from './pages/Home';
import SignUpPage from './pages/sign-up/SignUpPage';
import SignIn from './pages/SignIn';
import theme from './theme';
import './index.css'

const container = document.getElementById('root');
const root = createRoot(container!);

root.render(
    <React.StrictMode>
        <ChakraProvider theme={theme}>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<App/>}/>
                    <Route path="/home" element={<Home/>}/>
                    <Route path="/sign-up" element={<SignUpPage/>}/>
                    <Route path="/sign-in" element={<SignIn/>}/>
                </Routes>
            </BrowserRouter>
        </ChakraProvider>
    </React.StrictMode>,
);

reportWebVitals();

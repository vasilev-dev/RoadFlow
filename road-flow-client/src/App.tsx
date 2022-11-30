import React from "react";
import {Routes, Route, BrowserRouter} from "react-router-dom";
import SignInPage from "./pages/sign-in-page/sign-in-page";
import SignUpPage from "./pages/sign-up-page/sign-up-page";
import HomePage from "./pages/home-page/home-page";

const App: React.FC = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/sign-in" element={<SignInPage/>}/>
                <Route path="/sign-up" element={<SignUpPage/>}/>

                <Route path="/" element={<HomePage/>}/>
            </Routes>
        </BrowserRouter>
    )
}

export default App

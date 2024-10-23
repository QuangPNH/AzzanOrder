import React from 'react';
import * as ReactDOM from "react-dom/client";
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import "./index.css";
import App from "./App";
import MenuPage from "./components/Menu";
import About from "./components/AboutUsPage"
import Feedback from "./components/Feedback"
import Voucher from "./components/VoucherScreen"
import Order from "./components/OrderTrackScreen"
import Profile from "./components/MemberProfile"

// 2. Define your routes
const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
    },
    {
        path: "menu",
        element: <MenuPage />,
    },
    {
        path: "about",
        element: <About />,
    },
    {
        path: "feedback",
        element: <Feedback />,
    },
    {
        path: "voucher",
        element: <Voucher />,
    },
    {
        path: "order",
        element: <Order />,
    },
    {
        path: "profile",
        element: <Profile />,
    },
]);

// 3. Wrap your app with the Router component
const rootElement = document.getElementById('root')
ReactDOM.createRoot(rootElement).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
)
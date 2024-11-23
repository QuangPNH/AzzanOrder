import React from 'react';
import * as ReactDOM from "react-dom/client";
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import "./index.css";
import App from "./App";
import Homepage from "./components/Homepage";
import MenuPage from "./components/Menu";
import About from "./components/AboutUsPage";
import Feedback from "./components/Feedback";
import Voucher from "./components/VoucherScreen";
import Order from "./components/OrderTrackScreen";
import Profile from "./components/MemberProfile";
import Notification from "./components/NotificationPage";
import VoucherCart from './components/VoucherCart';


// Define your routes
const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {
                path: "/",
                element: <Homepage />,
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
                path: "voucherCart",
                element: <VoucherCart />
            },
            {
                path: "order",
                element: <Order />,
            },
            {
                path: "profile",
                element: <Profile />,
            },
            {
                path: "notification",
                element: <Notification />,
            },
        ],
    },
]);

// Wrap your app with the Router component and PopupProvider
const rootElement = document.getElementById('root');
ReactDOM.createRoot(rootElement).render(
   /* <React.StrictMode>*/
        <RouterProvider router={router} />
    /*</React.StrictMode>*/
);
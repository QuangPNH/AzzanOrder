import React from 'react';
import * as ReactDOM from "react-dom/client";
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import "./index.css";
import App from "./App";
import MenuPage from "./components/Menu";
// 2. Define your routes
const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
    },
    {
        path: "menu",
        element: <MenuPage />,
    }
]);

// 3. Wrap your app with the Router component
const rootElement = document.getElementById('root')
ReactDOM.createRoot(rootElement).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
)
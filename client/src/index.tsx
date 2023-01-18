import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Provider } from "react-redux";
import EventsComponent from "./components/EventsComponent";
import EventComponent from "./components/EventComponent";
import ErrorPage from "./error-page";
import UpComingEvents from "./components/UpcomingEvents";
import CreateEvent from "./components/CreateEvent";
import Login from "./components/Login";
import Register from "./components/Register";
import { store } from "./store/configureStore";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <UpComingEvents />,
      },
      {
        path: "events",
        element: <EventsComponent />,
      },
      {
        path: "/event/:id",
        element: <EventComponent />,
      },
      {
        path: "/events/create",
        element: <CreateEvent />,
      },
      {
        path: "/login",
        element: <Login />,
      },
      {
        path: "/register",
        element: <Register />,
      },
    ],
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);

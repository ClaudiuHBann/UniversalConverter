import React from "react";
import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { MantineProvider } from "@mantine/core";
import { Notifications } from "@mantine/notifications";
import "@mantine/notifications/styles.css";
import "@mantine/core/styles.css";
import Root from "./routes/root/Root.tsx";
import Redirect from "./routes/redirect/Redirect.tsx";
import UCContextProvider from "./components/UCContextProvider.tsx";

if (import.meta.hot) {
  import.meta.hot.on("vite:beforeUpdate", () => console.clear());
}

const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <UCContextProvider>
        <Root />
      </UCContextProvider>
    ),
  },
  {
    path: "/LinkZip/",
    element: <Redirect />,
  },
]);

const clientQuery = new QueryClient({
  defaultOptions: {
    queries: {
      throwOnError: true,
    },
    mutations: {
      throwOnError: true,
      onError(error, variables, context) {
        console.error(error, variables, context);
      },
    },
  },
});

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <MantineProvider defaultColorScheme="dark">
      <QueryClientProvider client={clientQuery}>
        <RouterProvider router={router} />

        <Notifications />
        <ReactQueryDevtools />
      </QueryClientProvider>
    </MantineProvider>
  </React.StrictMode>
);

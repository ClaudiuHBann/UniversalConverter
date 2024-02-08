/*
  Mantine v7.5: https://mantine.dev/
  TanStack Query v3: https://tanstack.com/query/v3/docs/framework/react/quick-start
  React v18.2: https://react.dev/reference/react
  Tabler Icons: https://tabler-icons-react.vercel.app/
*/

import "@mantine/core/styles.css";
import { MantineProvider } from "@mantine/core";
import Main from "./components/Main";

function App() {
  return (
    <MantineProvider defaultColorScheme="dark">
      <Main />
    </MantineProvider>
  );
}

export default App;

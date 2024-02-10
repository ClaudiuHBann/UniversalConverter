import { Stack, Text } from "@mantine/core";

import Input from "./io/Input";
import Output from "./io/Output";
import Actions from "./Actions";
import AppShellEx from "./app-shell/AppShellEx";

function App() {
  return (
    <AppShellEx>
      <Stack>
        <Text>Choose a category...</Text>

        <Input />
        <Actions />
        <Output />
      </Stack>
    </AppShellEx>
  );
}

export default App;

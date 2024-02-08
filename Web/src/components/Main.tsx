import { Stack, Text } from "@mantine/core";

import Input from "./io/Input";
import Output from "./io/Output";
import Actions from "./Actions";
import NavBar from "./NavBar/NavBar";

function App() {
  return (
    <NavBar>
      <Stack>
        <Text>Choose a category...</Text>

        <Input />
        <Actions />
        <Output />
      </Stack>
    </NavBar>
  );
}

export default App;

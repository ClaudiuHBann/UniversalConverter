import { Stack, Text } from "@mantine/core";
import { useUCContext } from "../../../contexts/UCContext";

function Aside() {
  const context = useUCContext();

  return (
    <Stack>
      {context?.GetLogs().map((log) => {
        return <Text>{log}</Text>;
      })}
    </Stack>
  );
}

export default Aside;

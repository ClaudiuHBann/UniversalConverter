import { Center, Notification, rem, ScrollArea, Stack, Text } from "@mantine/core";
import { IconX } from "@tabler/icons-react";
import { useUCContext } from "../../../contexts/UCContext";

function Aside() {
  const context = useUCContext();

  return (
    <ScrollArea m={10} offsetScrollbars="y">
      <Center mb={10}>
        <Text size={rem(18)}>Logs</Text>
      </Center>

      <Stack align="stretch" justify="flex-start" gap="xs">
        {context!.GetLogs().map((log, index) => {
          return (
            <Notification
              key={index}
              withCloseButton={false}
              color="red"
              radius="md"
              icon={<IconX />}
            >
              {log}
            </Notification>
          );
        })}
      </Stack>
    </ScrollArea>
  );
}

export default Aside;

import { Stack, ScrollArea, Notification } from "@mantine/core";
import { useUCContext } from "../../../contexts/UCContext";
import { IconX } from "@tabler/icons-react";

function Aside() {
  const context = useUCContext();

  return (
    <ScrollArea m={10} offsetScrollbars>
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

import { Stack, Notification } from "@mantine/core";
import { useUCContext } from "../../../contexts/UCContext";
import { IconX } from "@tabler/icons-react";

function Aside() {
  const context = useUCContext();

  return (
    <Stack align="stretch" justify="flex-start" gap="xs" m={10}>
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
  );
}

export default Aside;

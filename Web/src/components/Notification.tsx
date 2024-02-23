import { IconX } from "@tabler/icons-react";
import { notifications } from "@mantine/notifications";

export function NotificationEx(message: string, title?: string) {
  notifications.show({
    autoClose: 5000,
    icon: <IconX />,

    title: title,
    message: message,

    color: "red",
    radius: "md",
  });
}

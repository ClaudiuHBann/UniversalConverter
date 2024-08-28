import { IconX } from "@tabler/icons-react";
import { notifications } from "@mantine/notifications";
import { UCContext } from "../contexts/UCContext";

export function NotificationEx(
  context: UCContext,
  message: string,
  title?: string
) {
  context.AddLog(message);
  if (context.AreLogsVisible()) {
    return;
  }

  Notification(message, title);
}

export function Notification(message: string, title?: string) {
  notifications.show({
    autoClose: 5000,
    icon: <IconX />,

    title: title,
    message: message,

    color: "red",
    radius: "md",
  });
}

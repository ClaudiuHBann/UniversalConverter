import { rem } from "@mantine/core";
import { IconCheck, IconClearAll, IconCopy } from "@tabler/icons-react";

export function FindIconClear(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconClearAll style={{ width: rem(69) }} />;
  }
}

export function FindTooltipClear(state: boolean) {
  return state ? "Cleared" : "Clear";
}

export function FindIconCopy(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconCopy style={{ width: rem(69) }} />;
  }
}

export function FindTooltipCopy(state: boolean) {
  return state ? "Copied" : "Copy";
}

export function HandleButtonIconCopy(value: string) {
  if (value.length === 0) {
    return;
  }

  navigator.clipboard.writeText(value);
}

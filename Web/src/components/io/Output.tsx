import { Textarea, ActionIcon, rem } from "@mantine/core";
import { IconCopy, IconCheck } from "@tabler/icons-react";
import StateButton from "../StateButton";

function GetButtonIconCopy(copied: boolean) {
  if (copied) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconCopy style={{ width: rem(69) }} />;
  }
}

function Output() {
  return (
    <div style={{ position: "relative" }}>
      <Textarea
        variant="filled"
        resize="vertical"
        readOnly
        size="md"
        radius="md"
        label="Output"
        description="Your Converted Values Here"
        placeholder="Read"
      />

      <StateButton>
        {({ state, transition }) => (
          <ActionIcon
            style={{
              color: state ? "teal" : "gray",
              position: "absolute",
              top: 10,
              right: 10,
            }}
            variant="subtle"
            onClick={transition}
          >
            {GetButtonIconCopy(state)}
          </ActionIcon>
        )}
      </StateButton>
    </div>
  );
}

export default Output;

import { ActionIcon, Textarea, rem } from "@mantine/core";
import { IconCheck, IconClearAll } from "@tabler/icons-react";
import StateButton from "../StateButton";

function GetButtonIconClear(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconClearAll style={{ width: rem(69) }} />;
  }
}

function Input() {
  return (
    <div style={{ position: "relative" }}>
      <Textarea
        variant="filled"
        size="md"
        radius="md"
        label="Input"
        resize="vertical"
        description="Insert Your Values Below"
        placeholder="Write your values here..."
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
            {GetButtonIconClear(state)}
          </ActionIcon>
        )}
      </StateButton>
    </div>
  );
}

export default Input;

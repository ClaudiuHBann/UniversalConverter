import { Textarea, rem } from "@mantine/core";
import { IconCheck, IconClearAll } from "@tabler/icons-react";
import ActionIconEx from "../ActionIconEx";
import { useState } from "react";

function GetButtonIconClear(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconClearAll style={{ width: rem(69) }} />;
  }
}

function Input() {
  const [inputValue, setInputValue] = useState("");

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
        value={inputValue}
        onChange={(event) => setInputValue(event.currentTarget.value)}
      />

      <ActionIconEx
        onClick={() => setInputValue("")}
        findIcon={GetButtonIconClear}
      />
    </div>
  );
}

export default Input;

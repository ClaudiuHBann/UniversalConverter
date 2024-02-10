import { Textarea, rem } from "@mantine/core";
import { IconCopy, IconCheck } from "@tabler/icons-react";
import ActionIconEx from "../ActionIconEx";
import { useState } from "react";

function GetButtonIconCopy(copied: boolean) {
  if (copied) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconCopy style={{ width: rem(69) }} />;
  }
}

function HandleButtonIconCopy(value: string) {
  if (value.length === 0) {
    return;
  }

  navigator.clipboard.writeText(value);
}

function Output() {
  const [outputValue, setOutputValue] = useState("");

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
        value={outputValue}
        onChange={(event) => setOutputValue(event.currentTarget.value)}
      />

      <ActionIconEx
        onClick={() => {
          HandleButtonIconCopy(outputValue);
        }}
        findIcon={GetButtonIconCopy}
      />
    </div>
  );
}

export default Output;

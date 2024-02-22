import { Textarea, rem } from "@mantine/core";
import { IconCheck, IconClearAll } from "@tabler/icons-react";
import ActionIconEx from "../extensions/ActionIconEx";
import { useUCContext } from "../../contexts/UCContext";

function FindIconClear(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconClearAll style={{ width: rem(69) }} />;
  }
}

function FindTooltipClear(state: boolean) {
  return state ? "Cleared" : "Clear";
}

function Input() {
  const context = useUCContext();
  const [inputValue, setInputValue] = context.GetInput();

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
        findIcon={FindIconClear}
        findTooltip={FindTooltipClear}
        style={{
          position: "absolute",
          top: 10,
          right: 10,
        }}
      />
    </div>
  );
}

export default Input;

import { Textarea, rem } from "@mantine/core";
import { IconCopy, IconCheck } from "@tabler/icons-react";
import ActionIconEx from "../../../components/ActionIconEx";
import { useUCContext } from "../../../contexts/UCContext";
import classes from "./IO.module.css";

function FindIconCopy(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconCopy style={{ width: rem(69) }} />;
  }
}

function FindTooltipCopy(state: boolean) {
  return state ? "Copied" : "Copy";
}

function HandleButtonIconCopy(value: string) {
  if (value.length === 0) {
    return;
  }

  navigator.clipboard.writeText(value);
}

function Output() {
  const context = useUCContext();
  const [outputValue, setOutputValue] = context
    ? context.GetOutput()
    : ["", () => {}];

  return (
    <div style={{ position: "relative" }}>
      <Textarea
        variant="filled"
        resize="vertical"
        classNames={{ input: classes.input }}
        readOnly
        size="md"
        radius="md"
        label="Output"
        description="Output From Values Above"
        placeholder="Example..."
        value={outputValue}
        onChange={(event) => setOutputValue(event.currentTarget.value)}
      />

      <ActionIconEx
        onClick={() => {
          HandleButtonIconCopy(outputValue);
        }}
        findIcon={FindIconCopy}
        findTooltip={FindTooltipCopy}
        style={{
          position: "absolute",
          top: 10,
          right: 10,
        }}
      />
    </div>
  );
}

export default Output;

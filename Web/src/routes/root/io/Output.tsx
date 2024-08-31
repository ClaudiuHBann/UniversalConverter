import { Textarea } from "@mantine/core";
import ActionIconEx from "../../../components/ActionIconEx";
import { useUCContext } from "../../../contexts/UCContext";
import classes from "./IO.module.css";
import {
  FindIconCopy,
  FindTooltipCopy,
  HandleButtonIconCopy,
  FindIconClear,
  FindTooltipClear,
} from "./IOShortcutMenu";
import { URLSearchParamsEx } from "../../../utilities/URLSearchParamsEx";
import { useEffect } from "react";

function Output() {
  const context = useUCContext();
  const [outputValue, setOutputValue] = context!.GetOutput();

  const searchParamsEx = new URLSearchParamsEx(context);
  const category = searchParamsEx.GetCategory();

  useEffect(() => {
    setOutputValue(context!.GetFromToDefaultValue(category, false) || "");
  }, [context, category]);

  return (
    <div style={{ position: "relative" }}>
      <Textarea
        variant="filled"
        resize="vertical"
        classNames={{ input: classes.io }}
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
        onClick={() => setOutputValue("")}
        findIcon={FindIconClear}
        findTooltip={FindTooltipClear}
        style={{
          position: "absolute",
          top: 10,
          right: 10,
        }}
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
          right: 45,
        }}
      />
    </div>
  );
}

export default Output;

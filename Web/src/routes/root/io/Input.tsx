import { Textarea } from "@mantine/core";
import ActionIconEx from "../../../components/ActionIconEx";
import { useUCContext } from "../../../contexts/UCContext";
import classes from "./IO.module.css";
import {
  FindIconClear,
  FindTooltipClear,
  FindIconCopy,
  FindTooltipCopy,
  HandleButtonIconCopy,
} from "./IOShortcutMenu";
import { URLSearchParamsEx } from "../../../utilities/URLSearchParamsEx";
import { useEffect } from "react";

function Input() {
  const context = useUCContext();
  const [inputValue, setInputValue] = context!.GetInput();

  const searchParamsEx = new URLSearchParamsEx(context);
  const category = searchParamsEx.GetCategory();

  useEffect(() => {
    setInputValue(context!.GetFromToDefaultValue(category, true) || "");
  }, [context, category]);

  return (
    <div style={{ position: "relative" }}>
      <Textarea
        variant="filled"
        classNames={{ input: classes.io }}
        size="md"
        radius="md"
        label="Input"
        resize="vertical"
        description="Input Your Values Below"
        placeholder="Example..."
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

      <ActionIconEx
        onClick={() => {
          HandleButtonIconCopy(inputValue);
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

export default Input;

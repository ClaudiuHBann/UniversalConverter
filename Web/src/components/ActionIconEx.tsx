import { ActionIcon, MantineStyleProp, Tooltip } from "@mantine/core";
import { useState } from "react";

export interface ActionIconExProps {
  onClick: () => void;
  findIcon: (state: boolean) => React.ReactNode;
  findTooltip?: (state: boolean) => string;
  style?: MantineStyleProp;
  delay?: number;
}

function ActionIconEx({
  onClick,
  findIcon,
  findTooltip,
  style,
  delay = 1000,
}: ActionIconExProps) {
  const [state, setState] = useState(false);

  const transition = () => {
    if (state) {
      return;
    }

    setState(true);

    setTimeout(() => {
      setState(false);
    }, delay);
  };

  // TODO: "perfect forward" the props
  const createActionIcon = () => (
    <ActionIcon
      style={[{ color: state ? "teal" : "gray" }, style]}
      disabled={state}
      variant="subtle"
      onClick={() => {
        onClick();
        transition();
      }}
    >
      {findIcon(state)}
    </ActionIcon>
  );

  const createActionIconWithTooltip = () => {
    if (findTooltip) {
      return (
        <Tooltip label={findTooltip(state)} color="gray">
          {createActionIcon()}
        </Tooltip>
      );
    } else {
      return createActionIcon();
    }
  };

  return createActionIconWithTooltip();
}

export default ActionIconEx;

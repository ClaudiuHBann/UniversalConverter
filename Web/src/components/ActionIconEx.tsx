import { ActionIcon } from "@mantine/core";
import { useState } from "react";

export interface ActionIconExProps {
  onClick: () => void;
  findIcon: (state: boolean) => React.ReactNode;
  delay?: number;
}

function ActionIconEx({ onClick, findIcon, delay = 1000 }: ActionIconExProps) {
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
  return (
    <ActionIcon
      style={{
        color: state ? "teal" : "gray",
        position: "absolute",
        top: 10,
        right: 10,
      }}
      variant="subtle"
      onClick={() => {
        onClick();
        transition();
      }}
    >
      {findIcon(state)}
    </ActionIcon>
  );
}

export default ActionIconEx;

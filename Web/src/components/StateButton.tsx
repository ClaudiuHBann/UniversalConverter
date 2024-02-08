import { useState } from "react";

export interface StateButtonProps {
  children: (payload: {
    state: boolean;
    transition: () => void;
  }) => React.ReactNode;
  delay?: number;
}

function StateButton({ children, delay = 1000 }: StateButtonProps) {
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

  return <>{children({ state, transition })}</>;
}

export default StateButton;

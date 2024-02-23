import { useEffect, useState } from "react";
import { useFromToAll } from "../hooks/Queries";
import { UCContext, ucContext } from "./UCContext";
import { useDisclosure } from "@mantine/hooks";
import { Box, LoadingOverlay } from "@mantine/core";

function UCContextProvider({ children }: { children: React.ReactNode }) {
  const [visible, toggle] = useDisclosure(true);

  const queryFromToAll = useFromToAll();

  const [categoryToFromTo, setCategoryToFromTo] = useState(
    new Map<string, string[]>()
  );
  const input = useState("");
  const output = useState("");

  const context = new UCContext(categoryToFromTo, input, output);

  useEffect(() => {
    if (!queryFromToAll.data) {
      return;
    }

    setCategoryToFromTo(queryFromToAll.data.fromToAll);
    toggle.close();
  }, [queryFromToAll.data, input, output]);

  return (
    <Box pos="relative">
      <LoadingOverlay
        visible={visible}
        overlayProps={{ radius: "sm", blur: 2 }}
      />

      <ucContext.Provider value={context}>{children}</ucContext.Provider>
    </Box>
  );
}

export default UCContextProvider;

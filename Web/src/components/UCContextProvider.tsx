import { useEffect, useState } from "react";
import { useFromToAll } from "../hooks/Queries";
import { UCContext, ucContext } from "../contexts/UCContext";
import { useDisclosure } from "@mantine/hooks";
import { Box, LoadingOverlay } from "@mantine/core";
import { FromToResponse } from "../models/responses/FromToResponse";

function UCContextProvider({ children }: { children: React.ReactNode }) {
  const [categoryToFromTo, setCategoryToFromTo] = useState(
    new Map<string, FromToResponse>()
  );
  const input = useState("");
  const output = useState("");

  const logs = useState<string[]>([]);
  const logsVisible = useState(false);

  const context = new UCContext(
    categoryToFromTo,
    input,
    output,
    logs,
    logsVisible
  );

  const queryFromToAll = useFromToAll(context);

  const [visible, toggle] = useDisclosure(true);

  useEffect(() => {
    if (queryFromToAll.data) {
      setCategoryToFromTo(queryFromToAll.data.fromToAll);
    }

    toggle.close();
  }, [queryFromToAll.data]);

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

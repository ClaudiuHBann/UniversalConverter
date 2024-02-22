import { useState } from "react";
import { useFromToAll } from "../hooks/Queries";
import { UCContext, ucContext } from "./UCContext";

function UCContextProvider({ children }: { children: React.ReactNode }) {
  const queryFromToAll = useFromToAll();
  if (queryFromToAll.isLoading) {
    // TODO: add skeleton
    return <>Loading...</>;
  }

  var categoryToFromTo = queryFromToAll.data?.fromToAll!;

  const input = useState("");
  const output = useState("");

  return (
    <ucContext.Provider value={new UCContext(categoryToFromTo, input, output)}>
      {children}
    </ucContext.Provider>
  );
}

export default UCContextProvider;

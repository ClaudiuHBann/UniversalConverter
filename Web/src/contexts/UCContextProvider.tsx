import { useFromToAll } from "../hooks/Queries";
import { UCContext, ucContext } from "./UCContext";

function UCContextProvider({ children }: { children: React.ReactNode }) {
  const queryFromToAll = useFromToAll();
  if (queryFromToAll.isLoading) {
    return <>Loading...</>;
  }

  var categoryToFromTo = queryFromToAll.data?.fromToAll!;

  return (
    <ucContext.Provider value={new UCContext(categoryToFromTo)}>
      {children}
    </ucContext.Provider>
  );
}

export default UCContextProvider;

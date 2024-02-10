import { createContext } from "react";

export interface UCContext {
  categories: string[];
}

export const UCContext = createContext({
  categories: ["Currency", "Temperature", "Radix"],
});

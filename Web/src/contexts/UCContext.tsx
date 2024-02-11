import { createContext } from "react";
import { ToLowerCaseAndCapitalize } from "../utilities/StringExtensions";

var categoryToFromTo: Map<string, string[]> = new Map();

export interface UCContext {
  fromTo: (category: string) => string[];
  hasFromTo: (category: string, fromTo: string) => boolean;
  categories: () => string[];
  hasCategory: (category: string) => boolean;
}

function GetCategories(): string[] {
  if (categoryToFromTo.size === 0) {
    // TODO: populate with real data
    categoryToFromTo.set("Currency", []);
    categoryToFromTo.set("Temperature", []);
    categoryToFromTo.set("Radix", []);
  }

  return [...categoryToFromTo.keys()];
}

function GetFromTo(category: string) {
  if (!HasCategory(category)) {
    return [];
  }

  category = ToLowerCaseAndCapitalize(category);
  if (categoryToFromTo.get(category)!.length === 0) {
    // TODO: populate with real data
    categoryToFromTo.set(category, ["a", "b", "c"]);
  }

  return categoryToFromTo.get(category)!;
}

function HasCategory(category: string) {
  return GetCategories().includes(ToLowerCaseAndCapitalize(category));
}

function HasFromTo(category: string, fromTo: string) {
  return GetFromTo(category).includes(ToLowerCaseAndCapitalize(fromTo));
}

export const UCContext = createContext({
  fromTo: GetFromTo,
  hasFromTo: HasFromTo,
  categories: GetCategories,
  hasCategory: HasCategory,
});

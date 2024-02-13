import { createContext } from "react";
import { ToLowerCaseAndCapitalize } from "../utilities/StringExtensions";

var categoryToFromTo: Map<string, string[]> = new Map();

export interface UCContext {
  fromTo: (category: string | null) => string[];
  hasFromTo: (category: string | null, fromTo: string | null) => boolean;
  findFromTo: (category: string | null, fromTo: string | null) => string | null;
  categories: () => string[];
  hasCategory: (category: string | null) => boolean;
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

function GetFromTo(category: string | null) {
  if (!category || !HasCategory(category)) {
    return [];
  }

  category = ToLowerCaseAndCapitalize(category);
  if (categoryToFromTo.get(category)!.length === 0) {
    // TODO: populate with real data
    categoryToFromTo.set(category, ["a", "b", "c"]);
  }

  return categoryToFromTo.get(category)!;
}

function FindFromTo(category: string | null, fromTo: string | null) {
  if (!category || !fromTo) {
    return null;
  }

  const fromTos = GetFromTo(category);
  var index = fromTos.findIndex(
    (item) => fromTo.toLowerCase() === item.toLowerCase()
  );

  return index === -1 ? null : fromTos[index];
}

function HasCategory(category: string | null) {
  if (!category) {
    return false;
  }

  return GetCategories().includes(ToLowerCaseAndCapitalize(category));
}

function HasFromTo(category: string | null, fromTo: string | null) {
  if (!category || !fromTo) {
    return false;
  }

  fromTo = fromTo.toLowerCase();
  return GetFromTo(category).some((value) => value.toLowerCase() === fromTo);
}

export const UCContext = createContext({
  fromTo: GetFromTo,
  hasFromTo: HasFromTo,
  findFromTo: FindFromTo,
  categories: GetCategories,
  hasCategory: HasCategory,
});

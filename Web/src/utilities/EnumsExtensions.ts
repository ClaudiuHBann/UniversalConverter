import { ECategory, ESearchParam } from "./Enums";
import { ToLowerCaseAndCapitalize } from "./StringExtensions";

export function ToSearchParam(param: string): ESearchParam | null {
  param = ToLowerCaseAndCapitalize(param);

  const paramIndex = Object.keys(ESearchParam).indexOf(param);
  return paramIndex === -1 ? null : Object.values(ESearchParam)[paramIndex];
}

export function ToCategory(category: string): string | null {
  category = ToLowerCaseAndCapitalize(category);

  const categoryIndex = Object.keys(ECategory).indexOf(category);
  return categoryIndex === -1 ? null : Object.values(ECategory)[categoryIndex];
}

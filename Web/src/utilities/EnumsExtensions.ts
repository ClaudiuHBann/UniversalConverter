import { Category, SearchParam } from "./Enums";
import { ToLowerCaseAndCapitalize } from "./StringExtensions";

export function ToSearchParam(param: string): SearchParam | null {
  param = ToLowerCaseAndCapitalize(param);

  const paramIndex = Object.keys(SearchParam).indexOf(param);
  return paramIndex === -1 ? null : Object.values(SearchParam)[paramIndex];
}

export function ToCategory(category: string): string | null {
  category = ToLowerCaseAndCapitalize(category);

  const categoryIndex = Object.keys(Category).indexOf(category);
  return categoryIndex === -1 ? null : Object.values(Category)[categoryIndex];
}

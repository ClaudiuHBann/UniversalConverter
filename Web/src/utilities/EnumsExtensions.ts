import { SearchParam } from "./Enums";
import { ToLowerCaseAndCapitalize } from "./StringExtensions";

export function ToSearchParam(param: string): SearchParam | null {
  param = ToLowerCaseAndCapitalize(param);

  const paramIndex = Object.keys(SearchParam).indexOf(param);
  return paramIndex === -1 ? null : Object.values(SearchParam)[paramIndex];
}

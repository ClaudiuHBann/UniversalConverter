import { SearchParam } from "./Enums";
import { ToLowerCaseAndCapitalize } from "./StringExtensions";

export function ToSearchParam(param: string): SearchParam | null {
  param = ToLowerCaseAndCapitalize(param);

  const paramIndex = Object.values(SearchParam).indexOf(param as SearchParam);
  return paramIndex === -1 ? null : (param as SearchParam);
}

import { FindIndex } from "./ArrayExtensions";
import { ECategory, ESearchParam } from "./Enums";

export function ToSearchParam(param: string): ESearchParam | null {
  const paramIndex = FindIndex(Object.keys(ESearchParam), param);
  return paramIndex === -1 ? null : Object.values(ESearchParam)[paramIndex];
}

export function ToCategory(category: string): string | null {
  const categoryIndex = FindIndex(Object.keys(ECategory), category);
  return categoryIndex === -1 ? null : Object.values(ECategory)[categoryIndex];
}

import { createContext, useContext } from "react";
import { ToLowerCaseAndCapitalize } from "../utilities/StringExtensions";

export class UCContext {
  private categoryToFromTo: Map<string, string[]> = new Map();

  public constructor(categoryToFromTo: Map<string, string[]>) {
    this.categoryToFromTo = categoryToFromTo;
  }

  public GetCategories(): string[] {
    return [...this.categoryToFromTo.keys()];
  }

  public GetFromTo(category: string | null) {
    if (!category || !this.HasCategory(category)) {
      return [];
    }

    category = ToLowerCaseAndCapitalize(category);
    return this.categoryToFromTo.get(category)!;
  }

  public FindFromTo(category: string | null, fromTo: string | null) {
    if (!category || !fromTo) {
      return null;
    }

    const fromTos = this.GetFromTo(category);
    var index = fromTos.findIndex(
      (item) => fromTo.toLowerCase() === item.toLowerCase()
    );

    return index === -1 ? null : fromTos[index];
  }

  public HasCategory(category: string | null) {
    if (!category) {
      return false;
    }

    return this.GetCategories().includes(ToLowerCaseAndCapitalize(category));
  }

  public HasFromTo(category: string | null, fromTo: string | null) {
    if (!category || !fromTo) {
      return false;
    }

    fromTo = fromTo.toLowerCase();
    return this.GetFromTo(category).some(
      (value) => value.toLowerCase() === fromTo
    );
  }
}

export const ucContext = createContext(new UCContext(new Map()));

export function useUCContext() {
  return useContext(ucContext);
}

import { createContext, useContext } from "react";
import { Contains, FindItem } from "../utilities/ArrayExtensions";

export class UCContext {
  private categoryToFromTo: Map<string, string[]> = new Map();

  public constructor(categoryToFromTo: Map<string, string[]>) {
    this.categoryToFromTo = categoryToFromTo;
  }

  public GetCategories(): string[] {
    return [...this.categoryToFromTo.keys()];
  }

  public GetFromTo(category: string | null): string[] {
    if (!category || !this.HasCategory(category)) {
      return [];
    }

    category = FindItem(this.GetCategories(), category);
    if (!category) {
      return [];
    }

    return this.categoryToFromTo.get(category) || [];
  }

  public FindFromTo(
    category: string | null,
    fromTo: string | null
  ): string | null {
    if (!category || !fromTo) {
      return null;
    }

    return FindItem(this.GetFromTo(category), fromTo);
  }

  public FindCategory(category: string | null): string | null {
    if (!category) {
      return null;
    }

    return FindItem(this.GetCategories(), category);
  }

  public HasCategory(category: string | null): boolean {
    if (!category) {
      return false;
    }

    return Contains(this.GetCategories(), category);
  }

  public HasFromTo(category: string | null, fromTo: string | null) {
    if (!category || !fromTo) {
      return false;
    }

    return Contains(this.GetFromTo(category), fromTo);
  }
}

export const ucContext = createContext(new UCContext(new Map()));

export function useUCContext() {
  return useContext(ucContext);
}
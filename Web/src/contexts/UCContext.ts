import { Dispatch, SetStateAction, createContext, useContext } from "react";
import { Contains, FindItem } from "../utilities/ArrayExtensions";
import { FromToResponse } from "../models/responses/FromToResponse";

export class UCContext {
  private categoryToFromTo: Map<string, FromToResponse> = new Map();

  private input: [string, Dispatch<SetStateAction<string>>] = ["", () => {}];
  private output: [string, Dispatch<SetStateAction<string>>] = ["", () => {}];

  public constructor(
    categoryToFromTo: Map<string, FromToResponse>,
    input: [string, Dispatch<SetStateAction<string>>],
    output: [string, Dispatch<SetStateAction<string>>]
  ) {
    this.categoryToFromTo = categoryToFromTo;
    this.input = input;
    this.output = output;
  }

  public GetInput() {
    return this.input;
  }

  public GetOutput() {
    return this.output;
  }

  public GetCategories(): string[] {
    return [...this.categoryToFromTo.keys()];
  }

  private GetFromToResponse(category: string | null): FromToResponse | null {
    if (!category || !this.HasCategory(category)) {
      return null;
    }

    category = FindItem(this.GetCategories(), category);
    if (!category) {
      return null;
    }

    return this.categoryToFromTo.get(category)!;
  }

  public GetFromToDefault(
    category: string | null,
    fromTo: boolean
  ): string | null {
    const fromToResponse = this.GetFromToResponse(category);
    if (!fromToResponse) {
      return null;
    }

    return fromTo ? fromToResponse.defaultFrom : fromToResponse.defaultTo;
  }

  public GetFromTo(category: string | null): string[] {
    return this.GetFromToResponse(category)?.fromTo || [];
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

export const ucContext = createContext<UCContext | null>(null);

export function useUCContext() {
  return useContext(ucContext);
}

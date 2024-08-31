import { Dispatch, SetStateAction, createContext, useContext } from "react";
import { Contains, FindItem } from "../utilities/ArrayExtensions";
import { FromToResponse } from "../models/responses/FromToResponse";
import { NotificationEx } from "../components/Notification";

export class UCContext {
  private categoryToFromTo: Map<string, FromToResponse> = new Map();

  private input: [string, Dispatch<SetStateAction<string>>] = ["", () => {}];
  private output: [string, Dispatch<SetStateAction<string>>] = ["", () => {}];

  private logs: [string[], Dispatch<SetStateAction<string[]>>] = [[], () => {}];
  private logsVisible: [boolean, Dispatch<SetStateAction<boolean>>] = [
    false,
    () => {},
  ];

  private callbackPromiseCatch: (error: any) => void = () => {};

  public constructor(
    categoryToFromTo: Map<string, FromToResponse>,
    input: [string, Dispatch<SetStateAction<string>>],
    output: [string, Dispatch<SetStateAction<string>>],
    logs: [string[], Dispatch<SetStateAction<string[]>>],
    logsVisible: [boolean, Dispatch<SetStateAction<boolean>>]
  ) {
    this.categoryToFromTo = categoryToFromTo;
    this.input = input;
    this.output = output;
    this.logs = logs;
    this.logsVisible = logsVisible;

    this.callbackPromiseCatch = this.CallbackPromiseCatch.bind(this);
  }

  public GetInput() {
    return this.input;
  }

  public GetOutput() {
    return this.output;
  }

  public GetLogs() {
    const [logs] = this.logs;
    return logs;
  }

  public AreLogsVisible() {
    const [logsVisible] = this.logsVisible;
    return logsVisible;
  }

  public SetLogsVisibility(visibility: boolean) {
    const [, setLogsVisible] = this.logsVisible;
    setLogsVisible(visibility);
  }

  public GetCategories(): string[] {
    return [...this.categoryToFromTo.keys()];
  }

  private CallbackPromiseCatch(error: any) {
    NotificationEx(this, error.message);
    return null;
  }

  public GetCallbackPromiseCatch() {
    return this.callbackPromiseCatch;
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

  public GetFromToDefaultValue(
    category: string | null,
    fromTo: boolean
  ): string | null {
    const fromToResponse = this.GetFromToResponse(category);
    if (!fromToResponse) {
      return null;
    }

    return fromTo
      ? fromToResponse.defaultFromValue
      : fromToResponse.defaultToValue;
  }

  public GetFromTo(category: string | null): string[] {
    return this.GetFromToResponse(category)?.fromTo || [];
  }

  public AddLog(log: string) {
    const [logs, setLogs] = this.logs;
    setLogs(() => [...logs, log]);
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

import { ErrorResponse } from "../responses/ErrorResponse";

export enum EException {
  Unknown = "Unknown",
  FromTo = "FromTo",
  Value = "Value",
  Database = "Database",
}

export class BaseException extends Error {
  public error: ErrorResponse;
  public type: EException = EException.Unknown;

  protected constructor(
    type: EException,
    error: ErrorResponse,
    stack?: string
  ) {
    super(error.message);
    super.name = type;
    super.stack = stack;

    this.type = type;
    this.error = error;
    this.error.typeException = type;
  }
}

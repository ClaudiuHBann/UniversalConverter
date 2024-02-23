import { ToExceptionStr } from "../../utilities/EnumsExtensions";
import { ErrorResponse } from "../responses/ErrorResponse";

export enum EException {
  Unknown,
  FromTo,
  Value,
  Database,
}

export class BaseException extends Error {
  public error: ErrorResponse;
  public type: EException = EException.Unknown;

  protected constructor(type: EException, error: ErrorResponse) {
    super(error.message);
    super.name = ToExceptionStr(type);

    this.type = type;
    this.error = error;
    this.error.typeException = type;
  }
}

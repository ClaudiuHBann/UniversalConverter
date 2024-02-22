import { ErrorResponse } from "../responses/ErrorResponse";
import { BaseException, EException } from "./BaseException";

export class ValueException extends BaseException {
  public constructor(error: ErrorResponse, stack?: string) {
    super(EException.Value, error, stack);
  }
}

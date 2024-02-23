import { ErrorResponse } from "../responses/ErrorResponse";
import { BaseException, EException } from "./BaseException";

export class ValueException extends BaseException {
  public constructor(error: ErrorResponse) {
    super(EException.Value, error);
  }
}

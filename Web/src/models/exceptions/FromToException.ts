import { ErrorResponse } from "../responses/ErrorResponse";
import { BaseException, EException } from "./BaseException";

export class FromToException extends BaseException {
  public constructor(error: ErrorResponse) {
    super(EException.FromTo, error);
  }
}

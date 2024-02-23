import { ErrorResponse } from "../responses/ErrorResponse";
import { BaseException, EException } from "./BaseException";

export class DatabaseException extends BaseException {
  public constructor(error: ErrorResponse) {
    super(EException.Database, error);
  }
}

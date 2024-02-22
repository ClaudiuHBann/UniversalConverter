import { ErrorResponse } from "../responses/ErrorResponse";
import { BaseException, EException } from "./BaseException";

export class DatabaseException extends BaseException {
  public constructor(error: ErrorResponse, stack?: string) {
    super(EException.Database, error, stack);
  }
}

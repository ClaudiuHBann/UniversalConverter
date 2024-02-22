import { EException } from "../models/exceptions/BaseException";
import { DatabaseException } from "../models/exceptions/DatabaseException";
import { FromToException } from "../models/exceptions/FromToException";
import { ValueException } from "../models/exceptions/ValueException";
import { ErrorResponse } from "../models/responses/ErrorResponse";

export function CreateException(type: EException, error: ErrorResponse) {
  switch (type) {
    case EException.Database:
      return new DatabaseException(error);

    case EException.FromTo:
      return new FromToException(error);

    case EException.Value:
      return new ValueException(error);

    default:
      throw new Error(`The EException type '${type}' is not allowed!`);
  }
}

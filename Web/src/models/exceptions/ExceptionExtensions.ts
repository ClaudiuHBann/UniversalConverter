import { EException } from "./BaseException";
import { DatabaseException } from "./DatabaseException";
import { FromToException } from "./FromToException";
import { ValueException } from "./ValueException";
import { ErrorResponse } from "../responses/ErrorResponse";

export function CreateException(type: EException, error: ErrorResponse) {
  switch (type) {
    case EException.Database:
      return new DatabaseException(error);

    case EException.FromTo:
      return new FromToException(error);

    case EException.Value:
      return new ValueException(error);

    default:
      throw new Error(`An exception of type '${type}' has occurred!`);
  }
}

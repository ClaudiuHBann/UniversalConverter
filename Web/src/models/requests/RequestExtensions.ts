import { ERequest } from "./BaseRequest";
import { CommonRequest } from "./CommonRequest";
import { CurrencyRequest } from "./CurrencyRequest";
import { LinkZipRequest } from "./LinkZipRequest";
import { RadixRequest } from "./RadixRequest";
import { TemperatureRequest } from "./TemperatureRequest";
import {
  SplitByAnySpace,
  SplitByAnySpaceAndComma,
} from "../../utilities/ArrayExtensions";
import { ECategory } from "../../utilities/Enums";
import { RankRequest } from "./RankRequest";

export function ToRequest(type: ECategory): ERequest {
  switch (type) {
    case ECategory.Currency:
      return ERequest.Currency;

    case ECategory.LinkZip:
      return ERequest.LinkZip;

    case ECategory.Radix:
      return ERequest.Radix;

    case ECategory.Temperature:
      return ERequest.Temperature;

    default:
      throw new Error(`The ECategory type '${type}' is not allowed!`);
  }
}

export function ParseInput(type: ERequest, input: any) {
  if (typeof input !== "string") {
    throw new Error("Invalid parse input!");
  }

  switch (type) {
    case ERequest.Currency:
      return SplitByAnySpaceAndComma(input).map(Number);

    case ERequest.LinkZip:
      return SplitByAnySpace(input);

    case ERequest.Radix:
      return SplitByAnySpaceAndComma(input);

    case ERequest.Temperature:
      return SplitByAnySpaceAndComma(input).map(Number);

    default:
      throw new Error(`The ERequest type '${type}' is not allowed!`);
  }
}

export function CreateRequest(
  type: ERequest,
  from?: string,
  to?: string,
  data?: any
) {
  if (type === ERequest.Common && (!from || !to || !data)) {
    throw new Error("The from && to && data are required!");
  }

  var request;
  switch (type) {
    case ERequest.Common:
      request = new CommonRequest();
      break;

    case ERequest.Rank:
      request = new RankRequest();
      break;

    case ERequest.Currency:
      request = new CurrencyRequest(from!, to!);
      break;

    case ERequest.LinkZip:
      request = new LinkZipRequest(from!, to!);
      break;

    case ERequest.Radix:
      request = new RadixRequest(from!, to!);
      break;

    case ERequest.Temperature:
      request = new TemperatureRequest(from!, to!);
      break;

    default:
      throw new Error(`The ERequest type '${type}' is not allowed!`);
  }

  request.Initialize(data);
  return request;
}

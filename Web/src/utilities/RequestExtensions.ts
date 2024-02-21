import { ERequest } from "../models/requests/BaseRequest";
import { CommonRequest } from "../models/requests/CommonRequest";
import { CurrencyRequest } from "../models/requests/CurrencyRequest";
import { LinkZipRequest } from "../models/requests/LinkZipRequest";
import { RadixRequest } from "../models/requests/RadixRequest";
import { TemperatureRequest } from "../models/requests/TemperatureRequest";

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

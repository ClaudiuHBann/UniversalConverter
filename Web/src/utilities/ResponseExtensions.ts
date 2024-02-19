import { ErrorResponse } from "../models/responses/ErrorResponse";
import { EResponse, BaseResponse } from "../models/responses/BaseResponse";
import { CommonResponse } from "../models/responses/CommonResponse";
import { CurrencyResponse } from "../models/responses/CurrencyResponse";
import { FromToResponse } from "../models/responses/FromToResponse";
import { LinkZipResponse } from "../models/responses/LinkZipResponse";
import { RadixResponse } from "../models/responses/RadixResponse";
import { TemperatureResponse } from "../models/responses/TemperatureResponse";

export function CreateResponse(type: EResponse, data: any) {
  var response: BaseResponse;
  switch (type) {
    case EResponse.Common:
      response = new CommonResponse();
      break;

    case EResponse.Currency:
      response = new CurrencyResponse();
      break;

    case EResponse.Error:
      response = new ErrorResponse();
      break;

    case EResponse.FromTo:
      response = new FromToResponse();
      break;

    case EResponse.LinkZip:
      response = new LinkZipResponse();
      break;

    case EResponse.Radix:
      response = new RadixResponse();
      break;

    case EResponse.Temperature:
      response = new TemperatureResponse();
      break;

    default:
      throw new Error(`The EResponse type '${type}' is not allowed!`);
  }

  response.Initialize(data);
  return response;
}

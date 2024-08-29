import { ErrorResponse } from "./ErrorResponse";
import { BaseResponse, EResponse } from "./BaseResponse";
import { CommonResponse } from "./CommonResponse";
import { CurrencyResponse } from "./CurrencyResponse";
import { FromToResponse } from "./FromToResponse";
import { LinkZipResponse } from "./LinkZipResponse";
import { RadixResponse } from "./RadixResponse";
import { TemperatureResponse } from "./TemperatureResponse";
import { RankResponse } from "./RankResponse";
import { ToStringEResponse } from "../../utilities/EnumsExtensions";

export function ToOutput(response: BaseResponse) {
  switch (response.type) {
    case EResponse.Currency:
      return (response as CurrencyResponse).money.join("\n");

    case EResponse.LinkZip:
      return (response as LinkZipResponse).urls.join("\n");

    case EResponse.Radix:
      return (response as RadixResponse).numbers.join("\n");

    case EResponse.Temperature:
      return (response as TemperatureResponse).temperatures.join("\n");

    default:
      throw new Error(
        `Method 'ToOutput' doesn't allow the type '${ToStringEResponse(
          response.type
        )}' !`
      );
  }
}

export function CreateResponse(type: EResponse, data: any) {
  let response: BaseResponse;
  switch (type) {
    case EResponse.Common:
      response = new CommonResponse();
      break;

    case EResponse.Rank:
      response = new RankResponse();
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
      throw new Error(
        `Method 'CreateResponse' doesn't allow the type '${ToStringEResponse(
          type
        )}' !`
      );
  }

  response.Initialize(data);
  return response;
}

import axios, { AxiosResponse } from "axios";
import { BaseRequest } from "../../models/requests/BaseRequest";
import { BaseResponse } from "../../models/responses/BaseResponse";
import { ErrorResponse } from "../../models/responses/ErrorResponse";
import { CreateResponse } from "../../utilities/ResponseExtensions";

enum EDBAction {
  FromTo,
  Convert,
}

export enum EHTTPRequest {
  Post,
  Get,
}

export class BaseUCService<
  TRequest extends BaseRequest,
  TResponse extends BaseResponse
> {
  readonly urlBase: string = "https://localhost:7212/";

  protected GetControllerName() {
    throw new Error("Not implemented");
  }

  public async FromTo() {
    return await this.Request(EHTTPRequest.Get, EDBAction.FromTo.toString());
  }

  public async Convert(request: TRequest) {
    return await this.Request(
      EHTTPRequest.Post,
      EDBAction.Convert.toString(),
      request
    );
  }

  protected async Request(
    requestHTTP: EHTTPRequest,
    action: string,
    value?: TRequest
  ) {
    var uri = `${this.urlBase}${this.GetControllerName()}/${action}`;

    var promise;
    switch (requestHTTP) {
      case EHTTPRequest.Get:
        promise = axios.get<TResponse>(uri);
        break;
      case EHTTPRequest.Post:
        promise = axios.post<TResponse>(uri, value);
        break;

      default:
        throw new Error(
          `The EHTTPRequest type '${requestHTTP}' is not allowed!`
        );
    }

    // TODO: use a config
    promise.catch((error) => console.error(error));

    return this.ProcessResponse(await promise);
  }

  private async ProcessResponse(result: AxiosResponse<TResponse, any>) {
    var response = CreateResponse(result.data.type, result.data);
    if (result.status === 200) {
      return response as TResponse;
    } else {
      // TODO: create the specific exception
      throw new Error((response as ErrorResponse).message);
    }
  }
}

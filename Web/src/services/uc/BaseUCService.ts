import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { BaseRequest } from "../../models/requests/BaseRequest";
import { BaseResponse } from "../../models/responses/BaseResponse";
import { ErrorResponse } from "../../models/responses/ErrorResponse";
import { CreateResponse } from "../../models/responses/ResponseExtensions";
import { CreateException } from "../../models/exceptions/ExceptionExtensions";

enum EDBAction {
  FromTo = "FromTo",
  Convert = "Convert",
}

export enum EHTTPRequest {
  Post,
  Get,
}

export class BaseUCService<
  TRequest extends BaseRequest,
  TResponse extends BaseResponse
> {
  readonly urlBase: string = "";
  readonly axiosConfig = {
    validateStatus: () => true,
  };

  protected constructor() {
    if (import.meta.env.DEV) {
      this.urlBase = "http://localhost:32406/";
    } else {
      this.urlBase = "http://162.55.32.18:32406/";
    }
  }

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
    const uri = `${this.urlBase}${this.GetControllerName()}/${action}`;

    let promise;
    switch (requestHTTP) {
      case EHTTPRequest.Get:
        promise = axios.get<TResponse>(uri, this.axiosConfig);
        break;

      case EHTTPRequest.Post:
        promise = axios.post<TResponse>(uri, value, this.axiosConfig);
        break;

      default:
        throw new Error(
          `The EHTTPRequest type '${requestHTTP}' is not allowed!`
        );
    }

    return this.ProcessResponse(await promise);
  }

  private async ProcessResponse(
    result: AxiosResponse<TResponse, AxiosRequestConfig<any>>
  ) {
    const response = CreateResponse(result.data.type, result.data);
    if (result.status === 200) {
      return response as TResponse;
    } else {
      const error = response as ErrorResponse;
      throw CreateException(error.typeException, error);
    }
  }
}

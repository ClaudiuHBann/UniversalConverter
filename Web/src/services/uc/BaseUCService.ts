import axios, { AxiosResponse } from "axios";

enum EDBAction {
  FromTo,
  Convert,
}

export enum EHTTPRequest {
  Post,
  Get,
}

export class BaseUCService<
  Request extends BaseRequest,
  Response extends BaseResponse
> {
  readonly urlBase: string = "https://localhost:7212/";

  protected GetControllerName() {
    throw new Error("Not implemented");
  }

  public async FromTo() {
    return await this.Request(EHTTPRequest.Get, EDBAction.FromTo.toString());
  }

  public async Convert(request: Request) {
    return await this.Request(
      EHTTPRequest.Post,
      EDBAction.Convert.toString(),
      request
    );
  }

  protected async Request(
    requestHTTP: EHTTPRequest,
    action: string,
    value?: Request
  ): Promise<Response> {
    var uri = `${this.urlBase}${this.GetControllerName()}/${action}`;

    var promise: Promise<AxiosResponse<any, any>>;
    switch (requestHTTP) {
      case EHTTPRequest.Get:
        promise = axios.get<Response>(uri);
        break;
      case EHTTPRequest.Post:
        promise = axios.post<Response>(uri, value);
        break;

      default:
        throw new Error(
          `The EHTTPRequest type '${requestHTTP}' is not allowed!`
        );
    }

    promise.catch((error) => console.error(error));
    return (await promise).data;
  }
}

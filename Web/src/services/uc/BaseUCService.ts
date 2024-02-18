import { Axios, AxiosResponse } from "axios";

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

  axios: Axios;

  constructor(axios: Axios) {
    this.axios = axios;
  }

  GetControllerName() {
    throw new Error("Not implemented");
  }

  FromTo() {
    return this.Request(EHTTPRequest.Get, EDBAction.FromTo.toString());
  }

  Convert(request: Request) {
    return this.Request(
      EHTTPRequest.Post,
      EDBAction.Convert.toString(),
      request
    );
  }

  Request(requestHTTP: EHTTPRequest, action: string, value: any = null) {
    var uri = `${this.urlBase}${this.GetControllerName()}/${action}`;

    var promise: Promise<AxiosResponse<any, any>>;
    switch (requestHTTP) {
      case EHTTPRequest.Get:
        promise = this.axios.get(uri);
        break;
      case EHTTPRequest.Post:
        promise = this.axios.post(uri, value);
        break;

      default:
        throw new Error(
          `The EHTTPRequest type '${requestHTTP}' is not allowed!`
        );
    }

    promise.catch((error) => console.error(error));
    return promise;
  }
}

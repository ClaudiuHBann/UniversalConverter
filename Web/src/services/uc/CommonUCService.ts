import { Axios } from "axios";
import { BaseUCService, EHTTPRequest } from "./BaseUCService";

export class CommonUCService extends BaseUCService<
  CommonRequest,
  CommonResponse
> {
  GetControllerName() {
    return "Common";
  }

  FromToAll() {
    return super.Request(EHTTPRequest.Get, "FromToAll");
  }

  constructor(axios: Axios) {
    super(axios);
  }
}

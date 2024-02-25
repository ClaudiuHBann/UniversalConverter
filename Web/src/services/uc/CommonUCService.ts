import { CommonRequest } from "../../models/requests/CommonRequest";
import { CommonResponse } from "../../models/responses/CommonResponse";
import { BaseUCService, EHTTPRequest } from "./BaseUCService";

export class CommonUCService extends BaseUCService<
  CommonRequest,
  CommonResponse
> {
  public constructor() {
    super();
  }

  protected override GetControllerName() {
    return "Common";
  }

  public async FromToAll() {
    return await super.Request(EHTTPRequest.Get, "FromToAll");
  }
}

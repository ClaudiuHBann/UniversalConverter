import { BaseUCService, EHTTPRequest } from "./BaseUCService";

export class CommonUCService extends BaseUCService<
  CommonRequest,
  CommonResponse
> {
  protected override GetControllerName() {
    return "Common";
  }

  public async FromToAll() {
    return await super.Request(EHTTPRequest.Get, "FromToAll");
  }
}

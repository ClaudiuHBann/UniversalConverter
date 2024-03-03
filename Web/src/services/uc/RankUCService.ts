import { RankRequest } from "../../models/requests/RankRequest";
import { RankResponse } from "../../models/responses/RankResponse";
import { BaseUCService, EHTTPRequest } from "./BaseUCService";

export class RankUCService extends BaseUCService<RankRequest, RankResponse> {
  public constructor() {
    super();
  }

  protected override GetControllerName() {
    return "Rank";
  }

  public async Converters(request: RankRequest) {
    return await super.Request(EHTTPRequest.Post, "Converters", request);
  }
}

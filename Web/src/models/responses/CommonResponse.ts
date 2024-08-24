import { BaseResponse, EResponse } from "./BaseResponse";
import { FromToResponse } from "./FromToResponse";

export class CommonResponse extends BaseResponse {
  public fromToAll: Map<string, FromToResponse> = new Map<
    string,
    FromToResponse
  >();

  public constructor(fromToAll?: Map<string, FromToResponse>) {
    super(EResponse.Common);

    if (fromToAll) {
      this.fromToAll = fromToAll;
    }
  }

  public override Initialize(data: any) {
    this.fromToAll = new Map(Object.entries(data.fromToAll));
  }
}

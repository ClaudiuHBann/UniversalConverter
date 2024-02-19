import { BaseResponse, EResponse } from "./BaseResponse";

export class CommonResponse extends BaseResponse {
  public fromToAll: Map<string, string[]> = new Map<string, string[]>();

  public constructor(fromToAll?: Map<string, string[]>) {
    super(EResponse.Common);

    if (fromToAll) {
      this.fromToAll = fromToAll;
    }
  }

  public override Initialize(data: any) {
    this.fromToAll = new Map(Object.entries(data.fromToAll!));
  }
}

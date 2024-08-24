import { BaseResponse, EResponse } from "./BaseResponse";

export class RankResponse extends BaseResponse {
  public converters: string[] = [];

  public constructor(converters?: string[]) {
    super(EResponse.Rank);

    if (converters) {
      this.converters = converters;
    }
  }

  public override Initialize(data: any) {
    this.converters = Array.from(data.converters);
  }
}

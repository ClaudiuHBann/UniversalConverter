import { BaseResponse, EResponse } from "./BaseResponse";

export class FromToResponse extends BaseResponse {
  public fromTo: string[] = [];
  public defaultFrom: string = "";
  public defaultTo: string = "";

  public constructor(fromTo?: string[]) {
    super(EResponse.FromTo);

    if (fromTo) {
      this.fromTo = fromTo;
    }
  }

  public override Initialize(data: any) {
    this.fromTo = Array.from(data.fromTo);
    this.defaultFrom = data.defaultFrom;
    this.defaultTo = data.defaultTo;
  }
}

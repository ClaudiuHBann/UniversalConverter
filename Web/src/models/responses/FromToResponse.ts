import { BaseResponse, EResponse } from "./BaseResponse";

export class FromToResponse extends BaseResponse {
  public fromTo: string[] = [];

  public defaultFrom: string = "";
  public defaultFromValue: string = "";

  public defaultTo: string = "";
  public defaultToValue: string = "";

  public constructor(fromTo?: string[]) {
    super(EResponse.FromTo);

    if (fromTo) {
      this.fromTo = fromTo;
    }
  }

  public override Initialize(data: any) {
    this.fromTo = Array.from(data.fromTo);

    this.defaultFrom = data.defaultFrom;
    this.defaultFromValue = data.defaultFromValue;

    this.defaultTo = data.defaultTo;
    this.defaultToValue = data.defaultToValue;
  }
}

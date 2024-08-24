import { BaseRequest } from "./BaseRequest";

export class RankRequest extends BaseRequest {
  public converters: number = 3;

  public constructor(converters?: number) {
    super();

    if (converters) {
      this.converters = converters;
    }
  }

  public override Initialize(data: number) {
    if (!data) {
      throw new Error("The converters are null!");
    }

    this.converters = data;
  }
}

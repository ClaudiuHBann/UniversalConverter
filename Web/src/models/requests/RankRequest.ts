import { BaseRequest } from "./BaseRequest";

export class RankRequest extends BaseRequest {
  public converters: number = 3;

  public constructor(converters?: number) {
    super();

    if (converters) {
      this.converters = converters;
    }
  }

  public override Initialize(data?: any) {
    if (!data) {
      throw new Error("The converters are null!");
    }

    if (typeof data !== typeof this.converters) {
      throw new Error(`The data is not of type ${typeof this.converters}!`);
    }

    this.converters = data;
  }
}

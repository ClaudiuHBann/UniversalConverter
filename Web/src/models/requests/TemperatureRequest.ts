import { BaseRequest } from "./BaseRequest";

export class TemperatureRequest extends BaseRequest {
  public temperatures: number[] = [];

  public constructor(from: string, to: string, temperatures?: number[]) {
    super(from, to);

    if (temperatures) {
      this.temperatures = temperatures;
    }
  }

  public override Initialize(data?: any) {
    if (!data) {
      throw new Error("The temperatures are null!");
    }

    if (typeof data !== typeof this.temperatures) {
      throw new Error(`The data is not of type ${typeof this.temperatures}!`);
    }

    this.temperatures = data;
  }
}

import { BaseRequest } from "./BaseRequest";

export class TemperatureRequest extends BaseRequest {
  public temperatures: number[] = [];

  public constructor(from: string, to: string, temperatures?: number[]) {
    super(from, to);

    if (temperatures) {
      this.temperatures = temperatures;
    }
  }

  public override Initialize(data: number[]) {
    if (!data) {
      throw new Error("The temperatures are null!");
    }

    this.temperatures = data;
  }
}

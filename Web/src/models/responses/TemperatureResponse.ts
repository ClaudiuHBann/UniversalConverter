import { BaseResponse, EResponse } from "./BaseResponse";

export class TemperatureResponse extends BaseResponse {
  public temperatures: number[] = [];

  public constructor(temperatures?: number[]) {
    super(EResponse.Temperature);

    if (temperatures) {
      this.temperatures = temperatures;
    }
  }

  public override Initialize(data: any) {
    this.temperatures = Array.from(data.temperatures);
  }
}

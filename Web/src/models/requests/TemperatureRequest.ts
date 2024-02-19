import { BaseRequest } from "./BaseRequest";

export class TemperatureRequest extends BaseRequest {
  public temperatures: number[] = [];

  public constructor(from: string, to: string, temperatures: number[]) {
    super(from, to);
    this.temperatures = temperatures;
  }
}

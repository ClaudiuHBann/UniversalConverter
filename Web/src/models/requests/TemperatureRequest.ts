class TemperatureRequest extends BaseRequest {
  temperatures: number[] = [];

  constructor(from: string, to: string, temperatures: number[]) {
    super(from, to);
    this.temperatures = temperatures;
  }
}

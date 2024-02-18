class TemperatureResponse extends BaseResponse {
  temperatures: number[] = [];

  constructor(temperatures: number[]) {
    super();
    this.temperatures = temperatures;
  }
}

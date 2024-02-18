class RadixResponse extends BaseResponse {
  numbers: string[] = [];

  constructor(numbers: string[]) {
    super();
    this.numbers = numbers;
  }
}

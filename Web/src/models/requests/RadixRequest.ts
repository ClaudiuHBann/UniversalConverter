class RadixRequest extends BaseRequest {
  numbers: string[] = [];

  constructor(from: string, to: string, numbers: string[]) {
    super(from, to);
    this.numbers = numbers;
  }
}

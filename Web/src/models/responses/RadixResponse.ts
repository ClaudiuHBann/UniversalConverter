import { BaseResponse, EResponse } from "./BaseResponse";

export class RadixResponse extends BaseResponse {
  public numbers: string[] = [];

  public constructor(numbers?: string[]) {
    super(EResponse.Radix);

    if (numbers) {
      this.numbers = numbers;
    }
  }

  public override Initialize(data: any) {
    this.numbers = Array.from(data.numbers);
  }
}

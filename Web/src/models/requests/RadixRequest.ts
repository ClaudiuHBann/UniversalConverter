import { BaseRequest } from "./BaseRequest";

export class RadixRequest extends BaseRequest {
  public numbers: string[] = [];

  public constructor(from: string, to: string, numbers?: string[]) {
    super(from, to);

    if (numbers) {
      this.numbers = numbers;
    }
  }

  public override Initialize(data: string[]) {
    if (!data) {
      throw new Error("The numbers are null!");
    }

    this.numbers = data;
  }
}

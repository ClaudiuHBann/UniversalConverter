import { BaseRequest } from "./BaseRequest";

export class RadixRequest extends BaseRequest {
  public numbers: string[] = [];

  public constructor(from: string, to: string, numbers?: string[]) {
    super(from, to);

    if (numbers) {
      this.numbers = numbers;
    }
  }

  public override Initialize(data?: any) {
    if (!data) {
      throw new Error("The numbers are null!");
    }

    if (typeof data !== typeof this.numbers) {
      throw new Error(`The data is not of type ${typeof this.numbers}!`);
    }

    this.numbers = data;
  }
}

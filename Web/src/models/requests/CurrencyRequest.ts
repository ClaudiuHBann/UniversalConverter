import { BaseRequest } from "./BaseRequest";

export class CurrencyRequest extends BaseRequest {
  public money: number[] = [];

  public constructor(from: string, to: string, money?: number[]) {
    super(from, to);

    if (money) {
      this.money = money;
    }
  }

  public override Initialize(data?: any) {
    if (!data) {
      throw new Error("The money are null!");
    }

    if (typeof data !== typeof this.money) {
      throw new Error(`The data is not of type ${typeof this.money}!`);
    }

    this.money = data;
  }
}

import { BaseRequest } from "./BaseRequest";

export class CurrencyRequest extends BaseRequest {
  public money: number[] = [];

  public constructor(from: string, to: string, money?: number[]) {
    super(from, to);

    if (money) {
      this.money = money;
    }
  }

  public override Initialize(data: number[]) {
    if (!data) {
      throw new Error("The money are null!");
    }

    this.money = data;
  }
}

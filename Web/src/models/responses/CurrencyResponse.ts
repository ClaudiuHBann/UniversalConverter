import { BaseResponse, EResponse } from "./BaseResponse";

export class CurrencyResponse extends BaseResponse {
  public money: number[] = [];

  public constructor(money?: number[]) {
    super(EResponse.Currency);

    if (money) {
      this.money = money;
    }
  }

  public override Initialize(data: any) {
    this.money = Array.from(data.money);
  }
}

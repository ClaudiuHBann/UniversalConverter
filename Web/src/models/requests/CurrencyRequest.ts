import { BaseRequest } from "./BaseRequest";

export class CurrencyRequest extends BaseRequest {
  public money: number[] = [];

  public constructor(from: string, to: string, money: number[]) {
    super(from, to);
    this.money = money;
  }
}

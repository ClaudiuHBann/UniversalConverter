class CurrencyRequest extends BaseRequest {
  money: number[] = [];

  constructor(from: string, to: string, money: number[]) {
    super(from, to);
    this.money = money;
  }
}

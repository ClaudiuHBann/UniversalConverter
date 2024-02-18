class CurrencyResponse extends BaseResponse {
  money: number[] = [];

  constructor(money: number[]) {
    super();
    this.money = money;
  }
}

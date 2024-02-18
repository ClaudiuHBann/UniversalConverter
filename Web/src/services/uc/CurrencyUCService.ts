import { BaseUCService } from "./BaseUCService";

export class CurrencyUCService extends BaseUCService<
  CurrencyRequest,
  CurrencyResponse
> {
  protected override GetControllerName() {
    return "Currency";
  }
}

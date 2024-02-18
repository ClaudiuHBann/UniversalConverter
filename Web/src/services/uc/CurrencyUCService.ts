import { Axios } from "axios";
import { BaseUCService } from "./BaseUCService";

export class CurrencyUCService extends BaseUCService<
  CurrencyRequest,
  CurrencyResponse
> {
  GetControllerName() {
    return "Currency";
  }

  constructor(axios: Axios) {
    super(axios);
  }
}

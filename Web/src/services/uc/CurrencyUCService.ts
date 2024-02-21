﻿import { CurrencyRequest } from "../../models/requests/CurrencyRequest";
import { CurrencyResponse } from "../../models/responses/CurrencyResponse";
import { BaseUCService } from "./BaseUCService";

export class CurrencyUCService extends BaseUCService<
  CurrencyRequest,
  CurrencyResponse
> {
  protected override GetControllerName() {
    return "Currency";
  }
}
